using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using FastReport;
using FastReport.Export.Pdf;
using MessagingToolkit.QRCode.Codec;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Sistema.Classes.Util
{
    public class Utilidades
    {
        public static string getMD5(string input)
        {
            /*
             * retorno = Utilidades.getMD5(2);
             * 1 - retorno da senha gerada
             * 2 - Texto a ser utilizado para geração da senha em md5
             * Exemplo: 81dc9bdb52d04dc20036dbd8313ed055 = Utilidades.getMD5('1234')             
             */
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sbString = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));
            return sbString.ToString();
        }

        public static TripleDES CreateDES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            des.IV = new byte[des.BlockSize / 8];
            return des;
        }

        public static byte[] Encryption(string PlainText, string key)
        {
            TripleDES des = CreateDES(key);
            ICryptoTransform ct = des.CreateEncryptor();
            byte[] input = Encoding.Unicode.GetBytes(PlainText);
            return ct.TransformFinalBlock(input, 0, input.Length);
        }

        public static string Decryption(string CypherText, string key)
        {
            byte[] output = null;
            try
            {
                byte[] b = Convert.FromBase64String(CypherText);
                TripleDES des = CreateDES(key);
                ICryptoTransform ct = des.CreateDecryptor();
                output = ct.TransformFinalBlock(b, 0, b.Length);
            }
            catch (Exception erro)
            {
                string error = erro.Message;
            }
            return Encoding.Unicode.GetString(output);
        }

        public static void execSQLWithTransaction(NpgsqlConnection db, NpgsqlTransaction transacao, String sql)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(sql.Trim(), db) { Transaction = transacao };
            try
            {
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception erro)
            {
                cmd.Dispose();
                Alert.erro(String.Format("Erro ao executar script: {0}", erro.Message));
            }
        }

        public static Object[] consultar(NpgsqlConnection conn, String sql)
        {
            Object[] retorno = null;
            try
            {
                NpgsqlDataReader comm = new NpgsqlCommand(sql, conn).ExecuteReader();
                while (comm.Read())
                {
                    retorno = new Object[comm.FieldCount];
                    for (int i = 0; i < comm.FieldCount; i++)
                    {
                        retorno[i] = !comm.IsDBNull(i) ? comm.GetValue(i) : null;
                    }
                }
                comm.Close();
                comm.Dispose();
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("(Erro-Consultar) :(sql = {0})\n{1}", sql, erro.Message));
            }
            return retorno;
        }

        public static Object[] consultar(String sql)
        {
            return consultar(Conexao.getInstance().getConnection(), sql);
        }

        public static List<Object[]> Listar(String sql)
        {
            List<Object[]> ObjLista = new List<object[]>();
            try
            {
                Object[] retorno = null;
                List<List<Object>> dados = Conexao.getInstance().toList(sql);
                if (dados != null)
                {
                    foreach (List<Object> linha in dados)
                    {
                        retorno = new Object[linha.Count];
                        for (int i = 0; i < linha.Count; i++)
                        {
                            retorno[i] = linha.ElementAt(i) != null ? linha.ElementAt(i) : null;
                        }
                        ObjLista.Add(retorno);
                    }
                }
            }
            catch { }
            return ObjLista;
        }

        public static String consultaParametro(String inmParametro)
        {
            string valor = "";
            if (!inmParametro.Equals(""))
            {
                Object[] parm = consultar(String.Format(" select vl_parametro from parametro where nm_parametro = '{0}' ", inmParametro));
                if (parm != null && parm[0] != null)
                {
                    valor = parm[0].ToString();
                }
            }
            return valor;
        }

        public static String getLastId(String table, String column, String vWhere = " ")
        {
            String vSql = String.Format("select max({0} +1) from {1} {2}", column, table, vWhere);
            Object[] result = Utilidades.consultar(vSql);
            if (result[0] != null)
            {
                return Convert.ToString(result[0]);
            }
            return "1";
        }

        public static String arrumaDados(String sql)
        {
            //arrumaDados("delete from clifordetalhe where cd_clifor > 1");
            String msg = "";
            try
            {
                Conexao.getInstance().startTransaction();
                String result = Conexao.getInstance().gravar(sql, null);
                if (!result.Equals(""))
                {
                    Conexao.getInstance().rollback();
                    msg = "RoolBack ao executar o sql: " + sql;
                }
                else
                {
                    Conexao.getInstance().commit();
                }
            }
            catch (Exception erro)
            {
                msg = String.Format("Erro ao executar o sql: {0} erro: {1}", sql, erro.Message);
            }
            return msg;
        }

        public static String exportaRelPDF(String localExportacao, String nomeArquivo, Report relatorio)
        {
            /*             
             * Utilidades.exportaRelPDF(1, 2, relatorio); 
             * 1 - Local que será salvo o arquivo gerado em pdf
             * 2 - Nome do arquivo. A descrição irá ficar dessa forma Teste_06112019112927.pdf
             */
            try
            {
                /*Removido, a opção de replace, pois não havia necessidade de usar. - Allan 15-04-2020
                 * nomeArquivo += "_" + DateTime.Now.ToString().Replace(":", "").Replace("/", "").Replace(" ", "");                
                 */
                PDFExport export = new PDFExport();
                relatorio.Export(export, String.Format(@"{0}\{1}.pdf", localExportacao, nomeArquivo));
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao exportar arquivo PDF \n" + erro.Message);
            }
            return String.Format(@"{0}\{1}.pdf", localExportacao, nomeArquivo);
        }

        public static void exportaRelPDF1(String nomeArquivo, String tituloPdf, Report relatorio)
        {
            try
            {
                relatorio.Prepare();
                PDFExport pdfExport = new PDFExport();
                relatorio.PrintSettings.ShowDialog = false;
                pdfExport.ShowProgress = false;
                //                pdfExport.Subject = "NobreSistemas";
                pdfExport.Title = tituloPdf;
                pdfExport.Compressed = true;
                pdfExport.AllowPrint = true;
                pdfExport.EmbeddingFonts = true;
                MemoryStream strm = new MemoryStream();
                relatorio.Export(pdfExport, strm);

                SaveFileDialog salvarPDF = new SaveFileDialog();
                salvarPDF.Filter = "Arquivos PDF|*.pdf";
                salvarPDF.Title = "Salvar Arquivo em PDF";
                salvarPDF.FileName = nomeArquivo;//Irá trazer o nome do arquivo preenchido
                salvarPDF.ShowDialog();

                FileStream arquivoPDF = new FileStream(salvarPDF.FileName, FileMode.Create, FileAccess.Write);
                strm.WriteTo(arquivoPDF);

                Alert.informacao("Arquivo Salvo com Sucesso!");

                arquivoPDF.Close();
                relatorio.Dispose();
                pdfExport.Dispose();
                strm.Position = 0;
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao exportar arquivo PDF \n" + erro.Message);
            }
        }

        public static String getTime(Int16 opcao)
        {
            String dt = "";
            if (opcao == 1)
            {
                dt = Convert.ToString(DateTime.Now);
            }
            else if (opcao == 2)
            {
                dt = DateTime.Now.ToLongDateString();
            }
            else if (opcao == 3)
            {
                dt = DateTime.Now.ToLongTimeString();
            }
            else if (opcao == 4)
            {
                dt = DateTime.Now.ToShortDateString();
            }
            else if (opcao == 5)
            {
                dt = DateTime.Now.ToShortTimeString();
            }
            else if (opcao == 6)
            {
                dt = Convert.ToString(DateTime.Today.DayOfWeek);
            }
            else if (opcao == 7)
            {
                dt = Convert.ToString(DateTime.Today.Day);
            }
            else if (opcao == 8)
            {
                dt = Convert.ToString(DateTime.Today.Month);
            }
            else if (opcao == 9)
            {
                dt = Convert.ToString(DateTime.Today.Year);
            }
            return dt;
        }

        public static String getIdade(DateTime dtNascimento, DateTime dtAtual)
        {
            //txtCNPJ.Text = Utilidades.getIdade(dtnascimento.Value, DateTime.Now);
            String retorno = "";
            double result = 12 * (dtAtual.Year - dtNascimento.Year) + (dtAtual.Month - dtNascimento.Month);
            int qtanos = Convert.ToInt32(Math.Abs(result)) / 12;
            if (qtanos >= 1)
            {
                retorno = String.Format("{0:0}", qtanos) + " Ano(s)";
            }
            int iqtmeses = dtAtual.Month - dtNascimento.Month;
            if (iqtmeses > 0)
            {
                retorno += String.Format(", {0} Mes(es)", string.Format("{0:0}", iqtmeses));
            }
            int iqtdias = dtAtual.Day - dtNascimento.Day;
            if (iqtdias > 0)
            {
                retorno += String.Format(" e {0} Dia(s)", string.Format("{0:0}", iqtdias));
            }
            return retorno;
        }

        public static String loadWord(string irelatorioname)
        {
            String report = "";
            try
            {
                report = String.Format("{0}\\Relatorios\\{1}", Ficheiro.getLocalExecutavel(), irelatorioname);
            }
            catch (Exception ex)
            {
                Alert.erro(String.Format("Erro ao acessar o arquivo do Word {0} \n", irelatorioname, ex.Message));
            }
            return report;
        }

        public static Boolean remove(String ds_fieldName, String vsql_consultar, String vsql_remover)
        {
            Boolean sucesso = false;
            try
            {
                Conexao.getInstance().startTransaction();
                if (vsql_consultar.Equals(String.Empty) || (Utilidades.consultar(vsql_consultar)) == null)
                {
                    String removeRegistro = Conexao.getInstance().gravar(vsql_remover, null);
                    if (!removeRegistro.Equals(""))
                    {
                        Conexao.getInstance().rollback();
                        Alert.erro("Erro ao remover Registro");
                    }
                    else
                    {
                        Conexao.getInstance().commit();
                        Alert.informacao("Registro Excluido!");
                        sucesso = true;
                    }
                }
                else
                {
                    Alert.atencao(String.Format("Existem registros vinculados a esse {0}!", ds_fieldName));
                }
            }
            catch (Exception erro)
            {
                Conexao.getInstance().rollback();
                Alert.erro(String.Format("Erro ao excluir {0}. \n {1}", ds_fieldName, erro.Message));
            }
            return sucesso;
        }

        public static Image geraQrCode(String dsInformacao)
        {
            //pictureBox1.Image = Utilidades.geraQrCode(txtTextoSenhaQRCode.Text);
            String vRetorno = dsInformacao;
            QRCodeEncoder qrCodecEncoder = new QRCodeEncoder();
            qrCodecEncoder.QRCodeBackgroundColor = Color.White;
            qrCodecEncoder.QRCodeForegroundColor = Color.Black;
            qrCodecEncoder.CharacterSet = "UTF-8";
            qrCodecEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodecEncoder.QRCodeScale = 6;
            qrCodecEncoder.QRCodeVersion = 0;
            qrCodecEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            return vRetorno != null && vRetorno.Trim().Length > 0 ? qrCodecEncoder.Encode(vRetorno) : null;
        }

        public static Image gerarQRCode(Int16 vlLargura, Int16 vlAltura, String dsInformacao)
        {
            /*
             * pictureBox1.Image = Utilidades.GerarQRCode(1, 2, 3);
             * 1 - Largura do QRCode
             * 2 - Altura do QRCode
             * 3 - Texto a virar o QRCode
             */
            var bw = new ZXing.BarcodeWriter();
            var encOptions = new ZXing.Common.EncodingOptions() { Width = vlLargura, Height = vlAltura, Margin = 0 };
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            var resultado = new Bitmap(bw.Write(dsInformacao));
            return resultado;
        }

        public static void grid_remove(DevExpress.XtraGrid.Views.Grid.GridView gvGrid)
        {
            try
            {
                int[] aList = gvGrid.GetSelectedRows();
                for (int i = 0; i < aList.Length; i++)
                {
                    gvGrid.DeleteSelectedRows();
                }
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao realizar a remoção \n " + erro.Message);
            }
        }

        public static void startProgressBar(DevExpress.XtraEditors.ProgressBarControl progressBar, int max, Boolean showPercent, Point? position)
        {
            progressBar.Properties.Maximum = 0;
            progressBar.Properties.PercentView = showPercent;
            progressBar.Properties.ShowTitle = showPercent;
            progressBar.Properties.Maximum = max;
            progressBar.Properties.Minimum = 0;
            if (position != null)
            {
                progressBar.Location = (Point)position;
            }
        }

        public static void stepProgressBar(DevExpress.XtraEditors.ProgressBarControl progressBar, int step)
        {
            progressBar.Properties.Step = step;
            progressBar.PerformStep();
            progressBar.Update();
        }

        public static void labelControl(DevExpress.XtraEditors.LabelControl label, String msg)
        {
            label.Text = msg;
            label.Refresh();
        }

        public static void abrePanel(DevExpress.XtraEditors.XtraForm form, Panel painel)
        {
            painel.Visible = true;
            painel.Location = new Point((form.Size.Width / 2) - (painel.Size.Width / 2), (form.Size.Height / 2) - (painel.Size.Height / 2));
        }

        public static void abrePanel(DevExpress.XtraEditors.XtraForm form, DevExpress.XtraEditors.PanelControl painel)
        {
            painel.Visible = true;
            painel.Location = new Point((form.Size.Width / 2) - (painel.Size.Width / 2), (form.Size.Height / 2) - (painel.Size.Height / 2));
        }

        public static void abrePanel(Form form, Panel painel)
        {
            painel.Visible = true;
            painel.Location = new Point((form.Size.Width / 2) - (painel.Size.Width / 2), (form.Size.Height / 2) - (painel.Size.Height / 2));
        }

        public static void getGrid<T>(List<T> dados, GridControl grid, String msg)
        {
            try
            {
                grid.DataSource = null;
                grid.DataSource = dados;
                grid.Refresh();
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao carregar dados {0}. \n Erro: {1} - {2}", msg, erro.Message, erro.Source));
            }
        }

        public static void exportaPreviewGrid(GridControl grid)
        {
            //Modo de chamar = Utilidades.exportaPreviewGrid(gcGrid);
            #region 'Configuração Margem'
            const Int16 esquerda = 30;
            const Int16 direita = 30;
            const Int16 superior = 40;
            const Int16 inferior = 40;
            #endregion

            PrintingSystem PS = new PrintingSystem();
            PrintableComponentLink PCL = new PrintableComponentLink(PS);

            PCL.Component = grid;
            PCL.Landscape = true;
            PCL.PaperKind = System.Drawing.Printing.PaperKind.A4;
            PCL.EnablePageDialog = true;
            PCL.CreateReportHeaderArea += gridPreviewTextoCabecalho;
            PCL.CreateMarginalHeaderArea += gridPreviewPaginacao;
            PCL.Margins = new System.Drawing.Printing.Margins(esquerda, direita, superior, inferior);

            PCL.ShowPreview();
        }

        public static void gridPreviewTextoCabecalho(object sender, CreateAreaEventArgs e)
        {
            //Texto do cabeçalho/rodapé            
            TextBrick brick = e.Graph.DrawString("Relatório", Color.Black, new RectangleF(300, 5, 500, 40), BorderSide.None);
            brick.Font = new Font("Arial", 16);
            brick.StringFormat = new BrickStringFormat(StringAlignment.Center);
        }

        public static void gridPreviewPaginacao(object sender, CreateAreaEventArgs e)
        {
            //https://documentation.devexpress.com/WindowsForms/3244/Controls-and-Libraries/Printing-Exporting/Examples/Using-Printing-Links/How-to-Use-Link-Events-Complete-Sample
            RectangleF r = new RectangleF(0, 0, 0, e.Graph.Font.Height);
            //Paginação
            PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Página {0} de {1}", Color.Black, r, BorderSide.None);
            brick.LineAlignment = BrickAlignment.Far;
            brick.Alignment = BrickAlignment.Far;
            brick.AutoWidth = true;

            //Descrição do dia
            brick = e.Graph.DrawPageInfo(PageInfo.DateTime, "", Color.Black, r, BorderSide.None);
            brick.Alignment = BrickAlignment.Near;
            brick.AutoWidth = true;
        }

        public static void exportaArquivoGrid(GridControl grid, String dsNome, String dsFormato, String dsDiretorio)
        {
            //Utilidades.exportaArquivoGrid(gcGrid, "NomeDoArquivo", "xls", "Relatorios");
            Ficheiro.criaArquivo(dsNome, dsFormato, dsDiretorio);
            string filePath = Ficheiro.getLocalArquivo(dsNome, dsFormato, dsDiretorio);

            if (dsFormato.Equals("csv"))
            {
                grid.ExportToXlsx(filePath);
            }
            else if (dsFormato.Equals("html"))
            {
                grid.ExportToHtml(filePath);
            }
            else if (dsFormato.Equals("mht"))
            {
                grid.ExportToMht(filePath);
            }
            else if (dsFormato.Equals("pdf"))
            {
                grid.ExportToPdf(filePath);
            }
            else if (dsFormato.Equals("rtf"))
            {
                grid.ExportToRtf(filePath);
            }
            else if (dsFormato.Equals("txt"))
            {
                grid.ExportToText(filePath);
            }
            else if (dsFormato.Equals("xlsx"))
            {
                grid.ExportToXlsx(filePath);
            }
            else if (dsFormato.Equals("xls"))
            {
                grid.ExportToXls(filePath);
            }
            System.Diagnostics.Process.Start(@filePath);
        }

        public static void enviaEmail(String[] parametros, String dsHost, Int16 vlPorta, Boolean vlSSL, String dsEmailAutenticadao, String dsPassword, String dsEmailRemetente, String dsEmailDestinatario, String dsEmailAssunto, String dsEmailTexto, Boolean blConfirmacaoLeitura = false)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                /*enviaEmail(new String[] {1}, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
                 * 1 - Anexos. Exemplo "anexo1.zip", "anexo2.zip"
                 * 2 - Servidor do SMPT. Exemplo smtp.gmail.com
                 * 3 - Porta autenticação. Exemplo: 587
                 * 4 - Autenticação segura SSL. Exemplo: true/false
                 * 5 - E-mail usado para a autenticação
                 * 6 - Senha do e-mail usado para a autenticação                 
                 * 7 - E-mail do remetente
                 * 8 - E-mail do destinatário
                 * 9 - Assunto do E-mail
                 * 10 - Texto do E-mail
                 * 11 - Quando estiver como True será solicitado a confirmação de leitura                    
                 */
                smtp.Host = dsHost;
                smtp.Port = vlPorta;
                smtp.EnableSsl = vlSSL;
                smtp.Credentials = new NetworkCredential(dsEmailAutenticadao, dsPassword);

                using (MailMessage mail = new MailMessage())
                {
                    //Prioriedade do e-mail
                    mail.Priority = MailPriority.High;
                    //Confirmação de recebimento
                    if (blConfirmacaoLeitura)
                    {
                        mail.Headers.Add("Disposition-Notification-To", dsEmailRemetente);
                    }
                    //E-mail de Origem
                    mail.From = new MailAddress(dsEmailRemetente);
                    //E-mail destinatário
                    mail.To.Add(new MailAddress(dsEmailDestinatario));
                    //E-mail em cópia
                    //mail.CC.Add(new MailAddress("allanluna@gmail.com"));
                    //E-mail em cópia oculta
                    //mail.Bcc.Add(new MailAddress("allanluna@gmail.com"));

                    //Assunto do E-mail
                    mail.Subject = dsEmailAssunto;
                    //Texto do E-mail
                    mail.Body = dsEmailTexto;

                    foreach (string file in parametros)
                    {
                        mail.Attachments.Add(new Attachment(file));
                    }

                    smtp.Timeout = 100000;
                    try
                    {
                        smtp.Send(mail);
                        mail.Dispose();
                        smtp.Dispose();
                        Alert.informacao("E-mail enviado com Sucesso!");
                    }
                    catch (Exception erro)
                    {
                        mail.Dispose();
                        smtp.Dispose();
                        Alert.erro(String.Format("Erro ao enviar E-mail.\n{0}", erro.Message));
                    }
                }
            }
        }

        internal static void abreForm(DevExpress.XtraEditors.XtraForm form, String msg)
        {
            try
            {
                form.ShowDialog();
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("{0}\n{1}", msg, erro.Message));
            }
        }

        internal static void filtraGridView(DevExpress.XtraGrid.Views.Grid.GridView @gridview, String expressao)
        {
            /*private void txtFiltraGrid_TextChanged(object sender, EventArgs e)
                {
                    Utilidades.filtraGridView(gvGrid, txtFiltraGrid.Text);
                }
             */
            try
            {
                gridview.ActiveFilterEnabled = true;
                String vfiltro = "";
                if (expressao.Trim().Length > 0)
                {
                    String[] vdados = expressao.Split(' ');
                    foreach (DevExpress.XtraGrid.Columns.GridColumn gcc in gridview.Columns)
                    {
                        if (vfiltro.Trim().Length > 0)
                            vfiltro += " OR ";
                        vfiltro += "(";
                        for (int i = 0; i < vdados.Length; i++)
                        {
                            if (i > 0)
                                vfiltro += " AND ";
                            vfiltro += String.Format("[{0}] LIKE '%{1}%'", gcc.FieldName, vdados[i]);
                        }
                        vfiltro += ")";
                    }
                    gridview.ActiveFilterString = vfiltro;
                }
                else
                {
                    gridview.ActiveFilterString = "";
                }
            }
            catch (Exception erro)
            {
                Alert.erro(erro.Message);
            }
        }

        public static Object[] executaDblink(String servidor, String banco, String usuario, String senha, String tabela, String consulta, String where, String parametro, Boolean insert = false, Boolean log = false)
        {
            String vlInsert = "";
            Object[] vlRetorno = null;
            try
            {
                if (insert)
                {
                    vlInsert = String.Format("INSERT INTO {0}({1}) ", tabela, consulta);
                }

                String vsql = vlInsert +
                              " SELECT " + consulta + " FROM " +
                              " dblink('host=" + servidor + " user=" + usuario + "  password=" + senha + "  dbname=" + banco + "'::text," +
                              " 'SELECT " + consulta + " FROM " + tabela + " " + where + "'::text, false)" + tabela + "(" + parametro + ");";
                Object[] result = Utilidades.consultar(vsql);
                if (result != null)
                {
                    vlRetorno = result;
                }
                else if (log)
                {
                    String msg = String.Format("Erro ao executar Select dblink na tabela {0}. Erro: {1}", tabela, result);
                    Ficheiro.criaArquivo(tabela, "txt", "Logs");
                    Ficheiro.escreveArquivo(msg, tabela, "txt", "Logs");
                    Alert.atencao(msg);
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao importar dados da tabela {0} \n{1}", tabela, erro.Message));
            }
            return vlRetorno;
        }
        
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            /*
            String txt = "";
            if (InputBox("Envio de e-mail", "Digite o e-mail para envio:", ref txt) == DialogResult.OK) { }            
            if (!txt.Equals(String.Empty)){ }
             */
            XtraForm form = new XtraForm();
            LabelControl label = new LabelControl();
            TextEdit textBox = new TextEdit();
            SimpleButton buttonCancel = new SimpleButton();
            SimpleButton buttonOk = new SimpleButton();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;
            buttonOk.Image = Properties.Resources.Apply_16x16;

            buttonCancel.Text = "Cancelar";
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Image = Properties.Resources.Cancel_16x16;

            label.SetBounds(7, 10, 372, 20);
            label.Font = new Font(label.Font, FontStyle.Bold);
            label.Font = new Font("Tahoma", 10, FontStyle.Bold);

            textBox.SetBounds(6, 36, 380, 50);
            textBox.Font = new Font("Tahoma", 10);

            buttonOk.SetBounds(220, 65, 75, 23);
            buttonOk.Font = new Font(buttonOk.Font, FontStyle.Bold);
            buttonOk.Font = new Font("Tahoma", 10, FontStyle.Bold);

            buttonCancel.SetBounds(300, 65, 85, 23);
            buttonCancel.Font = new Font(buttonCancel.Font, FontStyle.Bold);
            buttonCancel.Font = new Font("Tahoma", 10, FontStyle.Bold);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 100);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.ShowIcon = false;
            form.ShowInTaskbar = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        internal static void abreTelaRapida(String cdUsuario)
        {
            try
            {
                String vSql = String.Format(" select nm_form from usuario " +
                                         " inner join programa on programa.cd_programa = usuario.cd_programa and " +
                                         " programa.cd_modulo = usuario.cd_modulo " +
                                         " where cd_usuario = '{0}'", cdUsuario);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    ((DevExpress.XtraEditors.XtraForm)Activator.CreateInstance(Type.GetType(Convert.ToString(result[0])))).ShowDialog();
                }
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao abrir tela Rápida \n" +erro.Message);
            }
        }

        public static System.Drawing.Image downloadImageFromUrl(string imageUrl)
        {
            //https://forgetcode.com/csharp/2052-download-images-from-a-url
            System.Drawing.Image image = null;
            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;
                System.Net.WebResponse webResponse = webRequest.GetResponse();
                System.IO.Stream stream = webResponse.GetResponseStream();
                image = System.Drawing.Image.FromStream(stream);
                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            return image;
        }

        public static String salvaImagemLocal(String Url, String diretorioArquivo)
        {
            //Utilidades.salvaImagemLocal(url, "D:\\");
            //https://forgetcode.com/csharp/2052-download-images-from-a-url
            System.Drawing.Image image = Utilidades.downloadImageFromUrl(Url);                        
            String strNome = Path.GetFileName(Url);

            string rootPath = diretorioArquivo;
            string fileName = System.IO.Path.Combine(rootPath, strNome);
            image.Save(fileName);
            return fileName;
        }
    }
}