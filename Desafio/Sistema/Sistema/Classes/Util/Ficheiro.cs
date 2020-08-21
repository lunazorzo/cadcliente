using DevExpress.XtraEditors;
using Ionic.Zip;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sistema.Classes.Util
{
    class Ficheiro
    {
        public static String getLocalExecutavel()
        {
            //Pega o local da pasta que está sendo executado o sistema            
            return Path.GetDirectoryName(Application.ExecutablePath);
        }

        public static String selecionaArquivo(String tpArquivo)
        {
            String caminho = "";
            try
            {
                /*
                 Exemplo de Arquivos "Firebird (.fdb)|*.fdb"
                */
                OpenFileDialog ofBuscaBanco = new OpenFileDialog();
                ofBuscaBanco.Title = "Selecione o Arquivo";
                ofBuscaBanco.Filter = tpArquivo;
                ofBuscaBanco.FilterIndex = 1;
                ofBuscaBanco.RestoreDirectory = true;
                if (ofBuscaBanco.ShowDialog() == DialogResult.OK)
                {
                    caminho = ofBuscaBanco.FileName;
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao abrir o arquivo ({0}).\n{1}", caminho, erro.Message));
            }
            return caminho;
        }

        public static String salvaArquivo(String tpArquivo)
        {
            String caminho = "";
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "Selecione o diretório desejado";
                saveFile.Filter = "File Format|" + Path.GetExtension(tpArquivo);
                saveFile.FilterIndex = 0;
                saveFile.FileName = tpArquivo;
                saveFile.DefaultExt = Path.GetExtension(tpArquivo);
                saveFile.RestoreDirectory = true;
                DialogResult resultado = saveFile.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    caminho = saveFile.FileName;
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao salvar o arquivo localizado no ({0}).\n{1}", caminho, erro.Message));
            }
            return caminho;
        }

        public static String selecionaDiretorio()
        {
            String diretorio = "";
            try
            {
                FolderBrowserDialog dirDialog = new FolderBrowserDialog();
                DialogResult res = dirDialog.ShowDialog();
                if (res == DialogResult.OK)
                {
                    diretorio = dirDialog.SelectedPath;
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao abrir diretorio ({0}).\n{1}", diretorio, erro.Message));
            }
            return diretorio;
        }

        public static String getLocalArquivo(String dsNome, String dsExtensao, String dsDiretorio)
        {
            String path = "";
            try
            {
                if (!dsDiretorio.Equals(""))
                {
                    path = String.Format(getLocalExecutavel() + "\\{5}\\{0}_{1}.{2}.{3}.{4}", dsNome, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, dsExtensao, dsDiretorio);
                }
                else
                {
                    path = String.Format(getLocalExecutavel() + "\\{0}_{1}.{2}.{3}.{4}", dsNome, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, dsExtensao);
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao localizar o arquivo: {0}", dsNome + dsExtensao, erro.Message));
            }
            return path;
        }

        public static String gravaImagem(String sql, String coluna, Image logo)
        {
            //Ficheiro.gravaImagem("update pais set img_logotipo = @imglogotipo where cd_pais = 1058", "imglogotipo", pictureBox1.Image);
            //Ficheiro.gravaImagem("INSERT INTO pais(cd_pais, ds_pais, sg_pais, dt_registro, img_logotipo) VALUES (2, 'teste', 'aa', current_timestamp, @imglogotipo);", "imglogotipo", pictureBox1.Image);
            String vRet = "";
            if (logo != null)
            {
                NpgsqlCommand comando = new NpgsqlCommand(sql, Conexao.getInstance().getConnection()) { Transaction = Conexao.getInstance().getTransaction() };
                comando.Parameters.Clear();
                vRet = commitImagem(comando, logo, coluna);
            }
            return vRet;
        }

        public static String commitImagem(NpgsqlCommand comando, Image logo, String coluna)
        {
            string vRet = "";
            try
            {
                NpgsqlParameter paramImg = new NpgsqlParameter(Valida.removeCaracteres(coluna), NpgsqlDbType.Bytea) { Value = imageToByte(logo) };
                comando.Parameters.Add(paramImg);
                comando.Prepare();
                comando.ExecuteNonQuery();
                Conexao.getInstance().commit();
                vRet = "Logo salvo com sucesso";
            }
            catch (Exception erro)
            {
                vRet = "Erro ao Gravar Logotipo: " + erro.Message;
                Conexao.getInstance().rollback();
            }
            return vRet;
        }

        public static byte[] imageToByte(Image logo)
        {
            MemoryStream stream = new MemoryStream();
            logo.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return stream.ToArray();
        }

        public static Image selecionaImagem()
        {
            //pictureBox1.Image = Ficheiro.selecionaImagem();
            String logo = Ficheiro.selecionaArquivo("Arquivos de Imagem(*.BMP;*.JPG;*.GIF;*.PNG;*.GIF)|*.BMP;*.JPG;*.GIF;*.PNG;*.GIF");
            if (!logo.Equals(String.Empty))
            {
                var resultado = new Bitmap(logo);
                return resultado;
            }
            else
            {
                return null;
            }
        }

        public static Image retornaImagem(String instrucao)
        {
            //pictureBox1.Image = Ficheiro.retornaImagem("select img_logotipo from pais where cd_pais = 1058");
            Bitmap logotipo = null;
            try
            {
                NpgsqlCommand commando = new NpgsqlCommand(String.Format("{0}", instrucao), Conexao.getInstance().getConnection());
                Byte[] result = (Byte[])commando.ExecuteScalar();
                ImageConverter ic = new ImageConverter();
                Image img = (Image)ic.ConvertFrom(result);
                logotipo = new Bitmap(img);
            }
            catch
            {
                logotipo = null;
            }
            return logotipo;
        }

        public static Boolean verificaExistenciaArquivo(String dsDiretorio, String dsExtensao)
        {
            //https://pt.stackoverflow.com/questions/208646/ver-se-existe-um-ficheiro-numa-pasta-c
            //https://pt.stackoverflow.com/questions/296284/buscar-arquivos-pela-extens%C3%A3o
            //if (Ficheiro.verificaArquivo(txtBancoLegado.Text, ".xml"))
            Boolean path;
            Boolean retorno;
            DirectoryInfo directoryInfo = new DirectoryInfo(dsDiretorio);

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (file.Extension.Contains(dsExtensao))
                {
                    retorno = true;
                }
            }
            retorno = directoryInfo.GetFiles().Where(p => p.Extension.Contains(dsExtensao)).ToList().Count > 0;
            path = retorno;
            return path;
        }

        public static Boolean verificaExistenciaDiretorio(String dsDiretorio)
        {
            //if (Ficheiro.verificaExistenciaDiretorio("\\Log\\teste"))            
            Boolean path = true;
            try
            {
                if (!Directory.Exists(dsDiretorio))
                {
                    path = false;
                }
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao verificar a existência diretório \n" + erro.Message);
            }
            return path;
        }

        public static void removeArquivos(String formatoAquivo, String local)
        {
            try
            {
                criaDiretorio(local);
                string[] arquivos = Directory.GetFiles(getLocalExecutavel() + local, formatoAquivo, SearchOption.AllDirectories);
                foreach (var arq in arquivos)
                {
                    File.Delete(arq);
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao realizar a remoção dos arquivos com a extensão: {0}\n{1}", formatoAquivo, erro.Message));
            }
        }

        public static void criaArquivo(String dsNome, String dsExtensao, String dsDiretorio)
        {
            try
            {
                String path = getLocalArquivo(dsNome, dsExtensao, dsDiretorio);
                StreamWriter x = File.CreateText(path);
                x.Close();
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao gerar no arquivo ({0}).\n{1}", dsNome, erro.Message));
            }
        }

        public static void escreveArquivo(String dsTexto, String dsNome, String dsExtensao, String dsDiretorio)
        {
            try
            {
                String path = getLocalArquivo(dsNome, dsExtensao, dsDiretorio);
                StreamWriter sw = File.AppendText(path);
                sw.WriteLine(dsTexto);
                sw.Close();
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao escrever no arquivo de log ({0}).\n{1}", dsNome, erro.Message));
            }
        }

        public static void criaDiretorio(String path)
        {
            //criaDiretorio("\\logs");
            if (!Directory.Exists(getLocalExecutavel() + path))
            {
                Directory.CreateDirectory(getLocalExecutavel() + path);
            }
        }

        public static void abreArquivo(String dsDiretorio)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo() { FileName = dsDiretorio };
                System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro abrir o arquivo localizado {0}!\n {1}", dsDiretorio, erro.Message));
            }
        }

        public static void descompactaArquivo(String diretorioDescompactar, String diretorioSaida, String dsPassword)
        {
            /*
             * descompactaArquivo(1, 2, 3);             
             * 1 - Local do arquivo compactado
             * 2 - Local que será descompactado o arquivo
             * 3 - Senha do arquivo, se não existir passar vazio ""
             */
            try
            {
                using (var zip = new ZipFile(diretorioDescompactar))
                {
                    Directory.CreateDirectory(diretorioSaida);
                    zip.Password = dsPassword;
                    zip.ExtractAll(diretorioSaida, ExtractExistingFileAction.OverwriteSilently);
                    zip.Dispose();
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao Descompactar o diretorio {0} \n", diretorioDescompactar) + erro.Message);
            }
        }

        public static void compactaArquivo(String diretorioCompactar, String diretorioSaida, String dsComentario, String nomeArquivoCompactado, String dsPassword, Boolean criaPasta = true)
        {
            /*             
             * http://grapevine.dyndns-ip.com/download/authentec/download/dotnetzip/examples.htm
             * compactaArquivo(1, 2, 3, 4, 5, 6);
             * 1 - Local do arquivo compactado
             * 2 - Local que será usado para salvar o arquivo compactado
             * 3 - Comentário do arquivo compactado, se não desejar informar passar vazio ""
             * 4 - Nome do arquivo compactado
             * 5 - Senha para a compactação
             * 6 - Se estiver como true ou vazio irá compactar a pasta, se estiver como false irá compactar os arquivos que estão dentro da pasta
             */
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    if (!dsPassword.Equals(""))
                    {
                        zip.Password = dsPassword;
                        zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                    }
                    if (criaPasta)
                    {
                        /*
                         * No arquivo zip, a pasta que irá apacer será do diretório localizado
                         * Exemplo: Users\Allan\Desktop\TransmissorNovo
                         * Se passar algum parametro terá o nome da pasta
                         * Exemplo: zip.AddFile(nomearquivo, "NomeDaPasta");
                         * se deixar zip.AddFile(nomearquivo, ""); não irá pegar o diretório que está sendo compactado
                         */
                        String[] nomeArquivos = Directory.GetFiles(diretorioCompactar);
                        //Irá compactar somente os arquivos do diretório, não levando em consideração as subpastas existentes no diretório
                        foreach (String nomearquivo in nomeArquivos)
                        {
                            zip.AddFile(nomearquivo);
                        }
                    }
                    else
                    {
                        //Compacta os arquivos e as pastas e subpastas existentes no diretório
                        zip.AddDirectory(diretorioCompactar, "");
                    }
                    zip.Comment = String.Format("{0} {1:G}", Valida.removeAcentos(dsComentario), DateTime.Now);
                    zip.Save(String.Format("{0}\\{1}.zip", diretorioSaida, nomeArquivoCompactado));
                    zip.Dispose();
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao compactar o arquivos do diretorio {0} \n", diretorioCompactar) + erro.Message);
            }
        }

        public static void editaTexto(String vlArquivoReplace, String vlRemover, String vlSubstituir)
        {
            try
            {
                //https://social.msdn.microsoft.com/Forums/pt-BR/449d8246-49b5-46e6-9968-f7c92777739f/alterar-arquivo-de-texto-txt-c?forum=vscsharppt
                string conteudoTxt = File.ReadAllText(vlArquivoReplace);
                string novoConteudoTxt = conteudoTxt.Replace(vlRemover, vlSubstituir);
                File.WriteAllText(vlArquivoReplace, novoConteudoTxt);
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao alterar arquivo\n" + erro.Message);
            }
        }

        public static String retornaDiretorio(String diretorio)
        {
            String retorno = "";
            try
            {
                FileInfo TheFile = new FileInfo(diretorio);
                retorno = TheFile.DirectoryName;
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao verificar Diretório " + erro.Message);
            }
            return retorno;
        }

        public static void preencheArquivoWord(String caminhoArquivo, String dsNomeArquivo, String[] dsFindText, String[] dsReplaceWith, Boolean abreVisualizao = false)
        {
            //Somente irá funcionar se o pacote office estiver instalado no computador
            try
            {
                #region 'Inicio'
                object missing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Application oApp = new Microsoft.Office.Interop.Word.Application();
                object template = caminhoArquivo;
                Microsoft.Office.Interop.Word.Document oDoc = oApp.Documents.Add(ref template, ref missing, ref missing, ref missing);

                //Troca o conteúdo de alguns tags
                Microsoft.Office.Interop.Word.Range oRng = oDoc.Range(ref missing, ref missing);
                object MatchWholeWord = true;
                object Forward = false;
                object replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                #endregion

                for (int i = 0; i < dsFindText.Length; i++)
                {
                    object FindText = dsFindText[i];
                    object ReplaceWith = dsReplaceWith[i];

                    oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord,
                        ref missing, ref missing, ref missing, ref Forward,
                        ref missing, ref missing, ref ReplaceWith, ref replace,
                        ref missing, ref missing, ref missing, ref missing);
                }

                #region 'Final'
                oApp.Visible = abreVisualizao;
                oDoc.SaveAs2(String.Format("{0}\\{1}", retornaDiretorio(caminhoArquivo), dsNomeArquivo));
                if (abreVisualizao == false)
                {
                    Alert.informacao("Arquivo Exportado.");
                    oDoc.Close();
                    oApp.Quit();
                }
                #endregion
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao preencher arquivo do Word" + erro.Message);
            }
        }

        public static String getSizeInfo(double size)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int order = 0;
            while (size >= 1024 && ++order < sizes.Length)
            {
                size = size / 1024.0;
            }
            return String.Format("{0:0.##} {1}", size, sizes[order]);
        }

        public static String getIconByFileExtension(string value)
        {
            value = String.Format(" {0} ", value).ToLower();
            if (" .wav .mp3 .ogg .mid .midi .mpg .wmv .mov ".IndexOf(value) != -1)
            {
                return "page_white_sound";
            }
            else if (" .zip .7z .rar .gzip .tar .bz ".IndexOf(value) != -1)
            {
                return "page_white_compressed";
            }
            else if (" .txt .log ".IndexOf(value) != -1)
            {
                return "page_white_text";
            }
            else if (" .bmp .jpg .jpeg .gif .png .ico ".IndexOf(value) != -1)
            {
                return "page_white_picture";
            }
            else if (" .doc .docx .dot .dotx .docm .dotm .rtf ".IndexOf(value) != -1)
            {
                return "page_white_word";
            }
            else if (" .xlsx .xlt .xltx .xlsb .xla .xlam .xlsm .xltm ".IndexOf(value) != -1)
            {
                return "page_white_excel";
            }
            else if (" .bat .conf .exe ".IndexOf(value) != -1)
            {
                return "bat";
            }
            else if (" .pdf ".IndexOf(value) != -1)
            {
                return "page_white_acrobat";
            }
            else if (" .pfx .cer ".IndexOf(value) != -1)
            {
                return "certificates";
            }
            else if (" .html .url .xml .xhtml ".IndexOf(value) != -1)
            {
                return "html";
            }
            else if (" .sql ".IndexOf(value) != -1)
            {
                return "sql";
            }
            else if (" .json ".IndexOf(value) != -1)
            {
                return "json";
            }
            else if (" .backup .bak .fdb .gdb .dat .mdb ".IndexOf(value) != -1)
            {
                return "database";
            }
            else if (" .bmpr ".IndexOf(value) != -1)
            {
                return "mockups";
            }
            else if (" .jar .java ".IndexOf(value) != -1)
            {
                return "file_extension_jar";
            }
            else if (" .ppt .pptx .pps .ppsx .pot .potx .ppa .ppam .pptm .ppsm ".IndexOf(value) != -1)
            {
                return "page_white_powerpoint";
            }
            else if (" .cs ".IndexOf(value) != -1)
            {
                return "page_white_csharp";
            }
            else if (" .apk ".IndexOf(value) != -1)
            {
                return "android";
            }
            return "document_empty";
        }

        public static String retornaNomeArquivo(String dsDiretorio)
        {
            FileInfo fileInfo = new FileInfo(dsDiretorio);
            //Mostra o nome do arquivo
            string fileName = fileInfo.Name;
            //Mostra a extensão do arquivo
            string fileExtension = fileInfo.Extension;
            //Mostra o caminho completo do arquivo junto com o nome
            string fileFullName = fileInfo.FullName;
            return fileName;
        }
    }
}
