using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FastReport;
using Sistema.Classes.Util;
using FastReport.Utils;
using FastReport.Export;

namespace Sistema.Janelas.Ferramentas
{
    public partial class frmVisualizador : DevExpress.XtraEditors.XtraForm
    {
        public Report relatorio { get; set; }

        List<gridEmail> email = null;
        Classes.Acesso.cadUsuario usuario = new Classes.Acesso.cadUsuario();                        
        public frmVisualizador()
        {
            InitializeComponent();
        }

        private void frmVisualizador_Load(object sender, EventArgs e)
        {
            try
            {
                email = new List<gridEmail>();
                pnEmail.Visible = false;
                float zoom = 0.95f;
                preview.Zoom = zoom;
                relatorio.Preview = preview;
                relatorio.Show();
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao abrir o relatório\n" + erro.Message);
            }
        }

        private void frmVisualizador_FormClosed(object sender, FormClosedEventArgs e)
        {
            relatorio.Dispose();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            relatorio.PrintSettings.PageRange = PageRange.All;
            relatorio.PrintSettings.PageNumbers = "";
            relatorio.Preview.Print();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            menuExportacao.Items.Clear();
            List<ObjectInfo> list = new List<ObjectInfo>();
            RegisteredObjects.Objects.EnumItems(list);

            foreach (ObjectInfo info in list)
            {
                if (info.Object != null && info.Object.IsSubclassOf(typeof(ExportBase)))
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(Res.TryGet(info.Text));
                    item.Tag = info;
                    item.Click += new EventHandler(item_Click);
                    if (info.ImageIndex != -1)
                    {
                        item.Image = Res.GetImage(info.ImageIndex);
                    }
                    else
                    {
                        if (info.Object.Namespace.Equals("FastReport.Export.Html"))
                        {
                            item.Image = Properties.Resources.html;
                        }
                        else if (info.Object.Namespace.Equals("FastReport.Export.Mht"))
                        {
                            item.Image = Properties.Resources.mht;
                        }
                        else if (info.Object.Namespace.Equals("FastReport.Export.OoXML"))
                        {
                            item.Image = Properties.Resources.ppt;
                        }
                        else if (info.Object.Namespace.Equals("FastReport.Export.Odf"))
                        {
                            item.Image = Properties.Resources.excel;
                        }
                        else if (info.Object.Namespace.Equals("FastReport.Export.Csv"))
                        {
                            item.Image = Properties.Resources.excel;
                        }
                        else if (info.Object.Namespace.Equals("FastReport.Export.Dbf"))
                        {
                            item.Image = Properties.Resources.database;
                        }
                        else if (info.Object.Namespace.Equals("FastReport.Export.Text"))
                        {
                            item.Image = Properties.Resources.text;
                        }
                        else if (info.Object.Namespace.Equals("FastReport.Export.Image"))
                        {
                            item.Image = Properties.Resources.image;
                        }
                    }
                    menuExportacao.Items.Add(item);
                }
            }
            menuExportacao.Show(btnExportar, new Point(0, btnExportar.Height));
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            usuario = Classes.Acesso.cadUsuarioDAO.getUsuario(StaticVariables.cdUsuario);
            if (!usuario.ds_email.Equals(String.Empty))
            {
                Utilidades.abrePanel(new frmVisualizador(), pnEmail);
                txtEmail.Focus();
                carregaAccessibleName();
            }
            else
            {
                Alert.atencao("Usuário sem parametros de e-mail configurado");
            }
            
        }

        private void btnMenosZoon_Click(object sender, EventArgs e)
        {
            preview.ZoomOut();
        }

        private void btnMaisZoon_Click(object sender, EventArgs e)
        {
            preview.ZoomIn();
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            preview.Prior();
        }

        private void btnAvancar_Click(object sender, EventArgs e)
        {
            preview.Next();
        }

        private void preview_PageChanged(object sender, EventArgs e)
        {
            nrPagina.Text = preview.PageNo.ToString();
        }

        private void nrPagina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (nrPagina.Text != "")
                {
                    preview.PageNo = int.Parse(nrPagina.Text);
                }
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            ObjectInfo info = (sender as ToolStripMenuItem).Tag as ObjectInfo;
            if (info == null)
            {
                preview.Save();
            }
            else
            {
                ExportBase export = Activator.CreateInstance(info.Object) as ExportBase;
                export.CurPage = preview.PageNo;
                export.Export(preview.Report);
            }
        }

        private void btnAddEmail_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean prossegue = true;
                String dsEmail = txtEmail.Text.ToLower();
                prossegue = Valida.verificaObrigatorios(new Object[] { txtEmail });
                if (prossegue)
                {
                    var listaEmail = dsEmail;
                    var emails = listaEmail.Split(';');
                    foreach (var endereco in emails)
                    {
                        for (int i = 0; i < gvGrid.DataRowCount; i++)
                        {
                            if (gvGrid.GetRowCellValue(i, "ds_email").ToString() == endereco)
                            {
                                Alert.atencao("Registro já inserido na grade!");
                                prossegue = false;
                                limpaCampos();
                            }
                        }
                        if (Valida.validaEmail(endereco))
                        {
                            gridEmail grid = new gridEmail();
                            grid.ds_email = endereco;
                            email.Add(grid);
                            limpaCampos();
                            gcGrid.DataSource = null;
                            gcGrid.DataSource = email;
                            gcGrid.Refresh();
                            gvGrid.RefreshData();

                        }
                        else
                        {
                            Alert.atencao(String.Format("E-mail é {0}, é inválido", dsEmail));
                            txtEmail.Focus();
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao adicionar o e-mail: {0}\n{1}", txtEmail.Text, erro.Message));
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnEmail.Visible = false;
            limpaCamposGeral();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean prossegue = true;
                String qtEmail = "";
                relatorio.Prepare();
                prossegue = Valida.verificaObrigatorios(new Object[] { txtTitulo, txtObservacao });
                if (prossegue)
                {
                    for (int i = 0; i < email.Count; i++)
                    {
                        qtEmail += Convert.ToString(email.ElementAt(i).ds_email);
                        if (i != email.Count - 1)
                        {
                            qtEmail += ";";
                        }
                    }
                    if (!qtEmail.Equals(""))
                    {                        
                        usuario = Classes.Acesso.cadUsuarioDAO.getUsuario(StaticVariables.cdUsuario);
                        Utilidades.enviaEmail(new String[] { Utilidades.exportaRelPDF(@"C:\TEMP", Ficheiro.retornaNomeArquivo(relatorio.FileName), relatorio) },
                            usuario.ds_smtp, usuario.nr_porta, usuario.st_ssl, usuario.ds_email, usuario.ds_senha, usuario.ds_email, qtEmail, txtTitulo.Text, txtObservacao.Text);
                        pnEmail.Visible = false;                       
                    }
                    else
                    {
                        Alert.atencao("É necessário informar pelo menos um e-mail.");
                        txtEmail.Focus();
                    }                    
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao enviar o e-mail: {0}\n{1}", txtEmail.Text, erro.Message));
            }
        }

        public void carregaAccessibleName()
        {
            txtEmail.AccessibleName = "o e-mail desejado!";
            txtTitulo.AccessibleName = "a título do e-mail!";
            txtObservacao.AccessibleName = "o texto do corpo do e-mail";
        }

        public void limpaCampos()
        {
            Valida.clear(new object[] { txtEmail });
            txtEmail.Focus();
        }

        public void limpaCamposGeral()
        {
            Valida.clear(new object[] { txtEmail, txtObservacao, txtTitulo });
            txtEmail.Focus();
            //limpa grid
            for (int i = 0; i < gvGrid.RowCount; )
            {
                gvGrid.DeleteRow(i);
            }
        }

        public void editaEmail(object sender, EventArgs e)
        {
            try
            {
                txtEmail.Text = Convert.ToString(gvGrid.GetRowCellValue(gvGrid.FocusedRowHandle, "ds_email"));
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao editar E-mail! \n" + erro.Message);
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editaEmail(sender, e);
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utilidades.grid_remove(gvGrid);
            txtEmail.Focus();
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                txtEmail.Text = Valida.consultaF9(11, "where ds_email is not null and st_ativo = 't'", "ds_email");
            }
        }

        private void gcGrid_DoubleClick(object sender, EventArgs e)
        {
            editaEmail(sender, e);
        }

        private void nrPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }
    }

    class gridEmail
    {
        public String ds_email { get; set; }
    }
}