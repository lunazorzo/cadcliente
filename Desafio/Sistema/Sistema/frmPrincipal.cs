using Sistema.Classes.Acesso;
using Sistema.Classes.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sistema
{
    public partial class frmPrincipal : DevExpress.XtraEditors.XtraForm
    {
        private String dsPrograma { get; set; }
        private String nomePrograma { get; set; }

        List<List<Object>> vmenus = null;
        List<ParametroPGSQL> prsql = null;

        public frmPrincipal()
        {
            InitializeComponent();
            LookAndFeel.SkinName = StaticVariables.nmSkin;
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            dsPrograma = "";
            carregaAcesso();
            carregaDadosText();
            imgFundo(StaticVariables.imagemFundo);
        }

        public void imgFundo(Boolean habilita = true)
        {
            if (habilita)
            {
                try
                {
                    String localImagem = String.Format("{0}\\{1}", Ficheiro.getLocalExecutavel(), "logo.png");
                    BackgroundImage = new Bitmap(localImagem);
                    BackgroundImageLayout = ImageLayout.Center;
                }
                catch (Exception erro)
                {
                    Alert.erro("Erro ao carregar a imagem de fundo. " + erro.Message);
                }
            }            
        }

        public void carregaDadosText()
        {
            String nomeUsuario = "";
            Object[] usu = Utilidades.consultar(String.Format(" select nm_usuario from usuario where cd_usuario = '{0}'", StaticVariables.cdUsuario));
            if (usu != null)
            {
                nomeUsuario = usu[0].ToString();
            }
            try
            {
                String usuario = "Usuário: " + StaticVariables.cdUsuario + " - " + nomeUsuario;
                String conexaoBanco = "Banco: " + Conexao.getInstance().getConnection().Host + ":" + Conexao.getInstance().getConnection().Port + "/" + Conexao.getInstance().getConnection().Database;
                this.Text = "Sistema          " + usuario + "          " + conexaoBanco;                   
            }
            catch (Exception erro)
            {                
                Alert.erro(String.Format("Erro ao carregar dados da conexão \n {0}", erro.Message));
            }
        }

        private void menuLogout(object sender, EventArgs e)
        {
            Application.Restart();
        }

        public void carregaAcesso()
        {
            try
            {
                bool stSistema = false;
                bool stCadastro = false;
                bool stConversao = false;
                bool stRelatorios = false;

                String vSqlAcesso = String.Format("select acessoprograma.cd_modulo from acessoprograma where acessoprograma.cd_usuario = '{0}'", StaticVariables.cdUsuario);
                List<List<Object>> consultaAcesso = Conexao.getInstance().toList(vSqlAcesso);
                if (consultaAcesso != null)
                {
                    foreach (List<Object> acesso in consultaAcesso)
                    {
                        if (acesso.ElementAt(0).ToString() == "SIS")
                        {
                            stSistema = true;
                        }
                        if (acesso.ElementAt(0).ToString() == "CAD")
                        {
                            stCadastro = true;
                        }
                        if (acesso.ElementAt(0).ToString() == "REL")
                        {
                            stRelatorios = true;
                        }
                    }
                }
                #region 'Sistema'
                if (stSistema)
                {
                    addMenu("SIS", "Sistema");
                }
                #endregion
                #region 'Cadastro'
                if (stCadastro)
                {
                    addMenu("CAD", "Cadastros");
                }
                #endregion
                #region 'Relatório'
                if (stRelatorios)
                {
                    addMenu("REL", "Relatórios");
                }
                #endregion
                carregaArvoreAcesso();
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao carregar acessos do usuário \n {0}", erro.Message));
            }
        }

        public void addMenu(String dsModulo, String dsDescricao)
        {
            string buscaProgramas = " select acessoprograma.cd_modulo,programa.cd_programa,ds_programa " +
                                    "   from acessoprograma,programa " +
                                    "  where acessoprograma.cd_usuario = '" + StaticVariables.cdUsuario + "' " +                                    
                                    "    and acessoprograma.cd_programa = programa.cd_programa " +
                                    "    and acessoprograma.cd_modulo   = programa.cd_modulo " +
                                    "    and acessoprograma.cd_modulo   = @cdModulo " +
                                    "    and programa.cd_submenu is null " +
                                    "  order by acessoprograma.cd_modulo,acessoprograma.cd_programa ";

            ToolStripMenuItem Pai = new ToolStripMenuItem() { Text = dsDescricao };
            String vsqlSubMenu = String.Format(" select submenu.cd_submenu,submenu.ds_submenu from submenu where submenu.cd_modulo = '{0}' ", dsModulo);
            Object[] result = Utilidades.consultar(vsqlSubMenu);
            if (result != null)
            {
                List<List<Object>> vcads = Conexao.getInstance().toList(vsqlSubMenu);
                foreach (List<Object> vcd in vcads)
                {
                    ToolStripMenuItem SubMenu = new ToolStripMenuItem() { Text = vcd.ElementAt(1).ToString() };
                    String vSqlSubs = " select submenu.cd_submenu,submenu.ds_submenu,acessoprograma.cd_modulo,programa.cd_programa,ds_programa " +
                                               "   from submenu,acessoprograma,programa " +
                                               "  where programa.cd_modulo         = '" + dsModulo + "' " +
                                               "    and programa.cd_modulo         = submenu.cd_modulo " +
                                               "    and programa.cd_submenu        = submenu.cd_submenu " +
                                               "    and acessoprograma.cd_programa = programa.cd_programa " +
                                               "    and acessoprograma.cd_modulo   = programa.cd_modulo " +
                                               "    and programa.cd_submenu        = " + vcd.ElementAt(0) +
                                               "    and acessoprograma.cd_usuario  = '" + StaticVariables.cdUsuario + "' " +
                                               "    and acessoprograma.cd_modulo   = submenu.cd_modulo order by acessoprograma.cd_modulo,acessoprograma.cd_programa ";
                    List<List<Object>> vsubs = Conexao.getInstance().toList(vSqlSubs);
                    if (vsubs != null)
                    {
                        foreach (List<Object> vsub in vsubs)
                        {
                            ToolStripMenuItem Filho = new ToolStripMenuItem() { Text = String.Format("{0}{1}-{2}", vsub.ElementAt(2), String.Format("{0:000}", Convert.ToInt32(vsub.ElementAt(3))), vsub.ElementAt(4)) };
                            Filho.Click += new EventHandler(abreTelas);
                            SubMenu.DropDownItems.Add(Filho);
                        }
                        Pai.DropDownItems.Add(SubMenu);
                    }
                }
            }
            prsql = new List<ParametroPGSQL>();
            prsql.Add(new ParametroPGSQL("cdModulo", dsModulo));
            vmenus = Conexao.getInstance().toList(buscaProgramas, prsql);
            if (vmenus != null)
            {
                foreach (List<Object> vacesso in vmenus)
                {
                    ToolStripMenuItem Filho = new ToolStripMenuItem() { Text = String.Format("{0}{1}-{2}", vacesso.ElementAt(0), String.Format("{0:000}", Convert.ToInt32(vacesso.ElementAt(1))), vacesso.ElementAt(2)) };
                    Filho.Click += new EventHandler(abreTelas);
                    Pai.DropDownItems.Add(Filho);
                }
            }

            //Add Opção de Logout
            if (dsModulo.Equals("SIS"))
            {
                ToolStripMenuItem MenuLogout = new ToolStripMenuItem() { Text = "&Logout - Selecionar Conexão" };
                MenuLogout.Click += new EventHandler(menuLogout);
                Pai.DropDownItems.Add(MenuLogout);
            }
            
            menuPrincipal.Items.Add(Pai);
        }

        private void abreTelas(object sender, EventArgs e)
        {
            try
            {
                nomePrograma = "";
                if (!dsPrograma.Equals(""))
                {
                    nomePrograma = dsPrograma;
                }
                else
                {
                    nomePrograma = sender.ToString();
                }
                dsPrograma = "";

                if (nomePrograma.Length > 6)
                {
                    try
                    {
                        String Modulo = nomePrograma.Substring(0, 3);
                        Int32 Programa = Convert.ToInt32(nomePrograma.Substring(3, 3));

                        #region 'Sistema'
                        if (Modulo == "SIS")
                        {
                            if (Programa == 1)
                            {
                                AbreForm.abreFrmListaValores(StaticVariables.cdUsuario); 
                            }
                            if (Programa == 2)
                            {
                                AbreForm.abreModulo(StaticVariables.cdUsuario);
                            }
                            if (Programa == 3)
                            {
                                AbreForm.abreAcesso(StaticVariables.cdUsuario);
                            }
                        }
                        #endregion
                        #region 'Cadastro'
                        if (Modulo == "CAD")
                        {
                            if (Programa == 1)
                            {
                                AbreForm.abrefrmCliente(StaticVariables.cdUsuario);
                            }
                        }
                        #endregion
                        #region 'Relatório'
                        if (Modulo == "REL")
                        {
                            if (Programa == 1)
                            {
                                AbreForm.abreRelCliente(StaticVariables.cdUsuario);
                            }
                        }
                        #endregion
                    }
                    catch { }
                }
            }
            catch (Exception erro)
            {
                Alert.erro("erro 1" + erro.Message);
            }
        }

        public void carregaArvoreAcesso()
        {
            String noAtual = "";
            int idxNode = 0;
            String vSql = String.Format("select " +
                                        "	usuarioprogmenu.cd_usuario, usuarioprogmenu.cd_modulo, modulo.ds_modulo, " +
                                        "	usuarioprogmenu.cd_programa, programa.ds_programa " +
                                        " from usuarioprogmenu " +
                                        " inner join modulo on modulo.cd_modulo = usuarioprogmenu.cd_modulo " +
                                        " inner join programa on programa.cd_programa = usuarioprogmenu.cd_programa " +
                                        " and programa.cd_modulo = usuarioprogmenu.cd_modulo " +
                                        " inner join acessoprograma on acessoprograma.cd_modulo = usuarioprogmenu.cd_modulo " +
                                        " and acessoprograma.cd_programa = usuarioprogmenu.cd_programa " +
                                        " and acessoprograma.cd_usuario = usuarioprogmenu.cd_usuario " +
                                        " where usuarioprogmenu.cd_usuario = '{0}' order by usuarioprogmenu.cd_modulo,usuarioprogmenu.cd_programa", StaticVariables.cdUsuario);
            List<List<Object>> arvs = Conexao.getInstance().toList(vSql);
            if (arvs != null)
            {
                foreach (List<Object> arv in arvs)
                {
                    if (!ArvoreAcesso.Visible)
                    {
                        ArvoreAcesso.Visible = true;
                        ArvoreAcesso.Nodes.Clear();
                    }
                    try
                    {
                        if (noAtual.Equals(""))
                        {
                            noAtual = arv.ElementAt(1).ToString();

                            ArvoreAcesso.Nodes.Add(new TreeNode(new CultureInfo("pt-BR").TextInfo.ToTitleCase(arv.ElementAt(2).ToString().ToLower())));
                            ArvoreAcesso.Nodes[idxNode].Nodes.Add(String.Format("{0}{1} - {2}", arv.ElementAt(1), String.Format("{0:000}", Convert.ToInt32(arv.ElementAt(3).ToString())), arv.ElementAt(4)));
                            ArvoreAcesso.Nodes[idxNode].Nodes[0].ImageIndex = 1;
                            ArvoreAcesso.Nodes[idxNode].Nodes[0].SelectedImageIndex = 1;
                        }
                        else
                        {
                            if (noAtual != arv.ElementAt(1).ToString())
                            {
                                idxNode++;
                                ArvoreAcesso.Nodes.Add(new TreeNode(new CultureInfo("pt-BR").TextInfo.ToTitleCase(arv.ElementAt(2).ToString().ToLower())));
                                ArvoreAcesso.Nodes[idxNode].Nodes.Add(String.Format("{0}{1} - {2}", arv.ElementAt(1), String.Format("{0:000}", Convert.ToInt32(arv.ElementAt(3).ToString())), arv.ElementAt(4)));
                                int idxChild = ArvoreAcesso.Nodes[idxNode].Nodes.Count - 1;
                                ArvoreAcesso.Nodes[idxNode].Nodes[idxChild].ImageIndex = 1;
                                ArvoreAcesso.Nodes[idxNode].Nodes[idxChild].SelectedImageIndex = 1;
                            }
                            else
                            {
                                ArvoreAcesso.Nodes[idxNode].Nodes.Add(String.Format("{0}{1} - {2}", arv.ElementAt(1), String.Format("{0:000}", Convert.ToInt32(arv.ElementAt(3).ToString())), arv.ElementAt(4)));
                                int idxChild = ArvoreAcesso.Nodes[idxNode].Nodes.Count - 1;
                                ArvoreAcesso.Nodes[idxNode].Nodes[idxChild].ImageIndex = 1;
                                ArvoreAcesso.Nodes[idxNode].Nodes[idxChild].SelectedImageIndex = 1;
                            }
                        }
                        noAtual = arv.ElementAt(1).ToString();
                    }
                    catch (Exception erro)
                    {
                        string dsErro = erro.Message;
                    }
                }
            }
            ArvoreAcesso.CollapseAll();
            ArvoreAcesso.ExpandAll();
            Refresh();
        }

        private void ArvoreAcesso_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                String ModuloPrograma = e.Node.Text;
                dsPrograma = ModuloPrograma;
                abreTelas(sender, e);
            }
            catch { }
        }
    }
}
