using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sistema.Classes.Util;
using Sistema.Classes.Acesso;
using FastReport;

namespace Sistema.Janelas.Acesso
{
    public partial class frmAcesso : DevExpress.XtraEditors.XtraForm
    {
        public frmAcesso()
        {
            InitializeComponent();
        }

        private void frmAcesso_Load(object sender, EventArgs e)
        {
            carregaGridUsuario();
            carregaAccessibleName();
        }

        private void frmAcesso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (ActiveControl.Parent.Name.Equals("txtcdUsuario"))
                {
                    txtcdUsuario.Text = Valida.consultaF9(1, "", "cd_usuario");
                    txtcdUsuario_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdUsuarioPermissao"))
                {
                    txtcdUsuarioPermissao.Text = Valida.consultaF9(1, "", "cd_usuario");
                    txtcdUsuarioPermissao_Validated(sender, e);
                    btnConsultaUsuarioPermissao_Click(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdUsuarioMenuArvore"))
                {
                    txtcdUsuarioMenuArvore.Text = Valida.consultaF9(1, "", "cd_usuario");
                    txtcdUsuarioMenuArvore_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdModuloMenuArvore"))
                {
                    txtcdModuloMenuArvore.Text = Valida.consultaF9(2, "", "cd_modulo");
                    txtcdModuloMenuArvore_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdProgramaMenuArvore"))
                {
                    txtcdProgramaMenuArvore.Text = Valida.consultaF9(4, String.Format(" where cd_modulo = '{0}'", txtcdModuloMenuArvore.Text), "cd_programa");
                    txtcdProgramaMenuArvore_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdUsuarioOrigem"))
                {
                    txtcdUsuarioOrigem.Text = Valida.consultaF9(1, "", "cd_usuario");
                    txtcdUsuarioOrigem_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdUsuarioDestino"))
                {
                    txtcdUsuarioDestino.Text = Valida.consultaF9(1, "", "cd_usuario");
                    txtcdUsuarioDestino_Validated(sender, e);
                }
                #region 'Relatório de Acessos'
                if (ActiveControl.Parent.Name.Equals("txtcdUsuarioRelatorio"))
                {
                    txtcdUsuarioRelatorio.Text = Valida.consultaF9(1, "", "cd_usuario");
                    txtcdUsuarioRelatorio_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdModuloRelatorio"))
                {
                    txtcdModuloRelatorio.Text = Valida.consultaF9(2, "", "cd_modulo");
                    txtcdModuloRelatorio_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdProgramaRelatorio"))
                {
                    txtcdProgramaRelatorio.Text = Valida.consultaF9(4, String.Format(" where cd_modulo = '{0}'", txtcdModuloRelatorio.Text), "cd_programa");
                    txtcdProgramaRelatorio_Validated(sender, e);
                }
                #endregion
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        public void carregaAccessibleName()
        {
            txtcdUsuario.AccessibleName = "o código do usuário";
            txtdsUsuario.AccessibleName = "o nome do usuário";
            txtdsSenha.AccessibleName = "a senha do usuário";
            txtcdUsuarioPermissao.AccessibleName = "a permissão do usuário";
            txtcdUsuarioMenuArvore.AccessibleName = "menu desejado";
            txtcdModuloMenuArvore.AccessibleName = "o módulo";
            txtcdProgramaMenuArvore.AccessibleName = "o programa";
            txtcdUsuarioOrigem.AccessibleName = "o usuário origem";
            txtcdUsuarioDestino.AccessibleName = "o usuário destino";
        }

        public void carregaGridUsuario()
        {
            Utilidades.getGrid(GetDataGrid.getDadosUsuario(), gcGridCadUsuario, "dos Usuários");
        }

        public void carregaGridMenuArvore(String cdUsuario)
        {
            Utilidades.getGrid(GetDataGrid.getDadosMenuArvore(cdUsuario), gcGridMenuArvore, "dos Usuários");
        }

        private void btnConsultaUsuarioPermissao_Click(object sender, EventArgs e)
        {
            if (!txtcdUsuarioPermissao.Text.Equals(String.Empty))
            {
                caregaNo(txtcdUsuarioPermissao.Text);
            }
            else
            {
                txtcdUsuarioPermissao.Text = Valida.consultaF9(1, "", "cd_usuario");
                txtcdUsuarioPermissao.Focus();
                txtcdUsuarioPermissao_Validated(sender, e);
            }            
        }

        public void caregaNo(String cdUsuario)
        {
            try
            {
                arvacesso.Nodes.Clear();
                arvprogramas.Nodes.Clear();
                TreeNode modulo = null;

                List<List<Object>> mds = Conexao.getInstance().toList("select m.cd_modulo, m.ds_modulo from modulo as m");
                if (mds != null)
                {
                    foreach (List<Object> md in mds)
                    {
                        modulo = arvprogramas.Nodes.Add(Convert.ToString(md.ElementAt(0)), Convert.ToString(md.ElementAt(1)), 0, 0);

                        String vsql =
                            "select m.cd_modulo, m.ds_modulo, p.cd_programa, (m.cd_modulo || ''|| p.cd_programa || ' - '|| p.ds_programa)  " +
                            "  from modulo as m " +
                            " inner join programa as p on (p.cd_modulo = m.cd_modulo) " +
                            " where m.cd_modulo = '" + md.ElementAt(0) + "'  " +
                            "   and (m.cd_modulo, p.cd_programa) not in (select a.cd_modulo, a.cd_programa " +
                            "                                              from acessoprograma as a " +
                            "                                             where a.cd_usuario = '" + cdUsuario + "' " +
                            "                                               and a.cd_modulo = m.cd_modulo " +
                            "                                               and a.cd_programa = p.cd_programa ) " +
                            " order by m.cd_modulo, cd_programa ";
                        List<List<Object>> programas = Conexao.getInstance().toList(vsql);
                        if (programas != null)
                        {
                            foreach (List<Object> prg in programas)
                            {
                                modulo.Nodes.Add(String.Format("{0}#{1}", Convert.ToString(prg.ElementAt(0)), Convert.ToString(prg.ElementAt(2))), Convert.ToString(prg.ElementAt(3)), 1, 1);
                            }
                        }
                    }
                }

                TreeNode moduloAcess = null;
                List<List<Object>> modsacesso = Conexao.getInstance().toList(
                    "select m.cd_modulo, m.ds_modulo " +
                    "  from modulo as m " +
                    " inner join acessoprograma as a on (a.cd_modulo = m.cd_modulo) " +
                    " where a.cd_usuario = '" + cdUsuario + "' " +
                    " group by m.cd_modulo, m.ds_modulo ");
                if (modsacesso != null)
                {
                    foreach (List<Object> mdacs in modsacesso)
                    {
                        moduloAcess = arvacesso.Nodes.Add(Convert.ToString(mdacs.ElementAt(0)), Convert.ToString(mdacs.ElementAt(1)), 0, 0);

                        String vsql =
                            "select m.cd_modulo, m.ds_modulo, p.cd_programa, (m.cd_modulo || ''|| p.cd_programa || ' - '|| p.ds_programa)  " +
                            "  from modulo as m " +
                            " inner join programa as p on (p.cd_modulo = m.cd_modulo) " +
                            " inner join acessoprograma as a on (a.cd_modulo = m.cd_modulo and a.cd_programa = p.cd_programa) " +
                            " where m.cd_modulo = '" + Convert.ToString(mdacs.ElementAt(0)) + "'  " +
                            "   and a.cd_usuario = '" + cdUsuario + "' " +
                            " group by m.cd_modulo, m.ds_modulo, p.cd_programa, p.ds_programa " +
                            " order by m.cd_modulo, cd_programa ";

                        List<List<Object>> prgacesso = Conexao.getInstance().toList(vsql);
                        if (prgacesso != null)
                        {
                            foreach (List<Object> prg in prgacesso)
                            {
                                moduloAcess.Nodes.Add(String.Format("{0}#{1}", Convert.ToString(prg.ElementAt(0)), Convert.ToString(prg.ElementAt(2))), Convert.ToString(prg.ElementAt(3)), 1, 1);
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao carregar lista \n" + erro.Message);
            }
        }

        public void trocaNo(TreeView origem, TreeView destino)
        {
            // se for programa, pega o nó e adiciona
            // se for pasta, adiciona todos os programas da pasta
            TreeNode no = origem.SelectedNode;
            if (no == null)
                return;
            if (no.Name.Contains("#"))
            {
                TreeNode pai = no.Parent;

                int idxpai = destino.Nodes.IndexOfKey(pai.Name);
                if (idxpai >= 0)
                {
                    origem.Nodes.Remove(no);
                    destino.Nodes[idxpai].Nodes.Add(no);
                }
                else
                {
                    TreeNode paux = (TreeNode)pai.Clone();
                    paux.Nodes.Clear();
                    origem.Nodes.Remove(no);
                    paux.Nodes.Add(no);
                    destino.Nodes.Add(paux);
                }
            }
            else
            {
                origem.Nodes.Remove(no);
                int idx1 = destino.Nodes.IndexOfKey(no.Name);
                if (idx1 >= 0)
                {
                    foreach (TreeNode tr in no.Nodes)
                    {
                        destino.Nodes[idx1].Nodes.Add(tr);
                    }
                }
                else
                {
                    destino.Nodes.Add(no);
                }
            }
        }

        private void trocaTodos(TreeView origem, TreeView destino)
        {
            while (true)
            {
                if (origem.Nodes.Count <= 0)
                    break;
                origem.SelectedNode = origem.Nodes[0].FirstNode;
                if (origem.SelectedNode == null)
                {
                    origem.Nodes[0].Remove();
                    continue;
                }
                trocaNo(origem, destino);
            }
        }

        private void txtcdUsuario_Validated(object sender, EventArgs e)
        {
            if (!txtcdUsuario.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select cd_usuario, nm_usuario, sn_usuario, ds_email, ds_senha, ds_smtp, " +
                                            "  nr_porta, st_ativo, st_ssl " +
                                            "  from public.usuario where cd_usuario = '{0}';", txtcdUsuario.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtcdUsuario.Text = Convert.ToString(result[0]);
                    txtdsUsuario.Text = Convert.ToString(result[1]);
                    txtdsSenha.Text = Convert.ToString(result[2]);
                    txtdsEmail.Text = Convert.ToString(result[3]);
                    txtdsSenhaEmail.Text = Convert.ToString(result[4]);
                    txtdsSMTP.Text = Convert.ToString(result[5]);
                    txtnrPortaEmail.Text = Convert.ToString(result[6]);
                    chkAtivo.Checked = Convert.ToBoolean(result[7]);
                    chkSSL.Checked = Convert.ToBoolean(result[8]);
                }
            }
        }


        private void btnGravarUsuario_Click(object sender, EventArgs e)
        {
            Boolean prossegue = Valida.verificaObrigatorios(new Object[] { txtcdUsuario, txtdsUsuario, txtdsSenha });
            if (prossegue == true)
            {
                cadUsuario usuario = new cadUsuario();
                usuario.cd_usuario = txtcdUsuario.Text;
                usuario.nm_usuario = txtdsUsuario.Text;
                usuario.sn_usuario = txtdsSenha.Text;
                usuario.ds_email = txtdsEmail.Text;
                usuario.ds_senha = txtdsSenhaEmail.Text;
                usuario.ds_smtp = txtdsSMTP.Text;
                usuario.nr_porta = Convert.ToInt16(txtnrPortaEmail.Text);
                usuario.st_ativo = chkAtivo.Checked;
                usuario.st_ssl = chkSSL.Checked;

                try
                {
                    String vret = "";
                    vret = cadUsuarioDAO.persist(usuario);
                    limpaCamposUsuario();
                    carregaGridUsuario();
                }
                catch (Exception erro)
                {
                    Alert.erro(String.Format("Erro ao Gravar Registro! \n {0}", erro.Message));
                }
            }
        }

        public void limpaCamposUsuario()
        {
            txtcdUsuario.Text = "";
            txtdsUsuario.Text = "";
            txtdsSenha.Text = "";
            txtdsEmail.Text = "";
            txtdsSenhaEmail.Text = "";
            txtdsSMTP.Text = "";
            txtnrPortaEmail.Text = "587";
            chkSSL.Checked = true;
            chkAtivo.Checked = true;
            txtcdUsuario.Focus();
        }

        public void editaUsuario(object sender, EventArgs e)
        {
            try
            {
                txtcdUsuario.Text = Convert.ToString(gvGridCadUsuario.GetRowCellValue(gvGridCadUsuario.FocusedRowHandle, "cd_usuario"));
                txtcdUsuario_Validated(sender, e);
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao editar Usuário. \n" + erro.Message);
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editaUsuario(sender, e);
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvGridCadUsuario.IsFocusedView == true)
            {
                String cd_usuario = Convert.ToString(gvGridCadUsuario.GetRowCellValue(gvGridCadUsuario.FocusedRowHandle, "cd_usuario"));
                String nm_usuario = Convert.ToString(gvGridCadUsuario.GetRowCellValue(gvGridCadUsuario.FocusedRowHandle, "nm_usuario"));
                if (Alert.pergunta(String.Format(" Usuário {0} - {1}", cd_usuario, nm_usuario)))
                {
                    Boolean sucesso = Utilidades.remove("Usuário",
                        String.Format("select 1 from usuarioprogmenu where cd_usuario = '{0}'", cd_usuario),
                        String.Format(" delete from usuario where cd_usuario = '{0}'", cd_usuario));
                    if (sucesso)
                    {
                        carregaGridUsuario();
                        limpaCamposUsuario();
                    }
                }
            }
        }

        private void txtcdUsuarioPermissao_Validated(object sender, EventArgs e)
        {
            if (!txtcdUsuarioPermissao.Text.Equals(String.Empty))
            {
                if (cadUsuario.retornaDescricaoUsuario(txtcdUsuarioPermissao.Text) != null)
                {
                    txtdsUsuarioPermissao.Text = cadUsuario.retornaDescricaoUsuario(txtcdUsuarioPermissao.Text);
                    btnConsultaUsuarioPermissao_Click(sender, e);
                }
                else
                {
                    txtcdUsuarioPermissao.Text = "";
                    txtdsUsuarioPermissao.Text = "";
                    txtcdUsuarioPermissao.Focus();
                }
            }
        }

        private void txtcdUsuarioMenuArvore_Validated(object sender, EventArgs e)
        {
            if (!txtcdUsuarioMenuArvore.Text.Equals(String.Empty))
            {
                if (cadUsuario.retornaDescricaoUsuario(txtcdUsuarioMenuArvore.Text) != null)
                {
                    txtdsUsuarioMenuArvore.Text = cadUsuario.retornaDescricaoUsuario(txtcdUsuarioMenuArvore.Text);
                    carregaGridMenuArvore(txtcdUsuarioMenuArvore.Text);
                }
                else
                {
                    txtcdUsuarioMenuArvore.Text = "";
                    txtdsUsuarioMenuArvore.Text = "";
                    txtcdUsuarioMenuArvore.Focus();
                }
            }
        }

        private void txtcdModuloMenuArvore_Validated(object sender, EventArgs e)
        {
            if (!txtcdModuloMenuArvore.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_modulo from public.modulo where cd_modulo = '{0}';", txtcdModuloMenuArvore.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsModuloMenuArvore.Text = Convert.ToString(result[0]);
                }
                else
                {
                    txtcdModuloMenuArvore.Text = "";
                    txtdsModuloMenuArvore.Text = "";
                    txtcdModuloMenuArvore.Focus();
                }
            }
        }

        private void txtcdProgramaMenuArvore_Validated(object sender, EventArgs e)
        {
            if (!txtcdProgramaMenuArvore.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_programa from public.programa where cd_programa = '{0}';", txtcdProgramaMenuArvore.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsProgramaMenuArvore.Text = Convert.ToString(result[0]);
                }
                else
                {
                    txtcdProgramaMenuArvore.Text = "";
                    txtdsProgramaMenuArvore.Text = "";
                    txtcdProgramaMenuArvore.Focus();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            trocaNo(arvprogramas, arvacesso);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            trocaNo(arvacesso, arvprogramas);
        }

        private void btnAddTudo_Click(object sender, EventArgs e)
        {
            trocaTodos(arvprogramas, arvacesso);
        }

        private void btnRemoveTudo_Click(object sender, EventArgs e)
        {
            trocaTodos(arvacesso, arvprogramas);
        }

        private void arvprogramas_DoubleClick(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }

        private void arvacesso_DoubleClick(object sender, EventArgs e)
        {
            btnRemove_Click(sender, e);
        }

        private void getProgramaList(List<String> @acessos, TreeNode no)
        {
            foreach (TreeNode tr in no.Nodes)
            {
                acessos.Add(tr.Name);
                getProgramaList(acessos, tr);
            }
        }

        public List<String> buscaAcesso(TreeView arv)
        {
            List<String> programasAcesso = new List<String>();
            for (int i = 0; i < arv.Nodes.Count; i++)
                getProgramaList(programasAcesso, arv.Nodes[i]);
            return programasAcesso;
        }

        private void limpaArvores()
        {
            arvacesso.Nodes.Clear();
            arvprogramas.Nodes.Clear();
        }

        private void bntGravarPermissao_Click(object sender, EventArgs e)
        {
            // laço removendo as permissoes que estão na árvore de programas
            String vsql = "";
            foreach (String st in buscaAcesso(arvprogramas))
            {
                if (Utilidades.consultar(Conexao.getInstance().getConnection(),
                                    " select true " +
                                    "   from acessoprograma " +
                                    "  where cd_modulo   = '" + st.Split('#')[0] + "' " +
                                    "    and cd_programa = " + st.Split('#')[1] +
                                    "    and cd_usuario  = '" + txtcdUsuarioPermissao.Text + "'") != null)
                {
                    vsql += "delete from acessoprograma " +
                            " where cd_modulo   = '" + st.Split('#')[0] + "' " +
                            "   and cd_programa = " + st.Split('#')[1] +
                            "   and cd_usuario  = '" + txtcdUsuarioPermissao.Text + "'";
                }
            }
            // laço concedendo as permissoes que estão na árvore do acessos liberados
            foreach (String st in buscaAcesso(arvacesso))
            {
                if (Utilidades.consultar(Conexao.getInstance().getConnection(),
                    " select true " +
                    "   from acessoprograma " +
                    "  where cd_modulo   = '" + st.Split('#')[0] + "' " +
                    "    and cd_programa = " + st.Split('#')[1] +
                    "    and cd_usuario  = '" + txtcdUsuarioPermissao.Text + "'") == null)
                {
                    vsql += "insert into acessoprograma (cd_modulo, cd_programa, cd_usuario, dt_registro) " +
                        "values('" + st.Split('#')[0] + "'," + st.Split('#')[1] + ", '" + txtcdUsuarioPermissao.Text + "', current_timestamp);";
                }
            }

            if (vsql.Trim().Length > 0)
            {
                try
                {
                    Conexao.getInstance().startTransaction();
                    Utilidades.execSQLWithTransaction(Conexao.getInstance().getConnection(), Conexao.getInstance().getTransaction(), vsql);
                    Conexao.getInstance().commit();
                    limpaArvores();
                    txtcdUsuarioPermissao.Text = "";
                    txtdsUsuarioPermissao.Text = "";
                    txtcdUsuarioPermissao.Focus();                    
                    Alert.informacao("Acessos salvos com sucesso");
                }
                catch (Exception erro)
                {
                    Alert.erro(String.Format("Erro ao atualizar menu do usuário {0}", erro.Message));
                }
            }
            else
            {
                Alert.atencao("Não existem modificações para salvar!");
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (gvGridMenuArvore.IsFocusedView == true)
            {
                String cd_usuarioArvore = Convert.ToString(gvGridMenuArvore.GetRowCellValue(gvGridMenuArvore.FocusedRowHandle, "cd_usuario"));
                String cd_moduloArvore = Convert.ToString(gvGridMenuArvore.GetRowCellValue(gvGridMenuArvore.FocusedRowHandle, "cd_modulo"));
                String cd_programaArvore = Convert.ToString(gvGridMenuArvore.GetRowCellValue(gvGridMenuArvore.FocusedRowHandle, "cd_programa"));
                String ds_programaArvore = Convert.ToString(gvGridMenuArvore.GetRowCellValue(gvGridMenuArvore.FocusedRowHandle, "ds_programa"));
                if (Alert.pergunta(String.Format(" Acesso ao menu Lateral do usuário {0} Módulo {1} - {2}", cd_usuarioArvore, cd_programaArvore, ds_programaArvore)))
                {
                    Boolean sucesso = Utilidades.remove("Acesso Progama Menu Lateral",
                        "",
                        String.Format(" delete from usuarioprogmenu where cd_usuario = '{0}' and cd_modulo = '{1}' and cd_programa = '{2}'", cd_usuarioArvore, cd_moduloArvore, cd_programaArvore));
                    if (sucesso)
                    {
                        carregaGridMenuArvore(txtcdUsuarioMenuArvore.Text);
                    }
                }
            }
        }

        public void limpaCamposMenuArvore()
        {
            txtcdUsuarioMenuArvore.Text = "";
            txtdsUsuarioMenuArvore.Text = "";
            txtcdModuloMenuArvore.Text = "";
            txtdsModuloMenuArvore.Text = "";
            txtcdProgramaMenuArvore.Text = "";
            txtdsProgramaMenuArvore.Text = "";
            txtcdUsuarioMenuArvore.Focus();
        }

        private void btnInserirMenuArvore_Click(object sender, EventArgs e)
        {
            Boolean prossegue = Valida.verificaObrigatorios(new Object[] { txtcdUsuarioMenuArvore, txtcdModuloMenuArvore, txtcdProgramaMenuArvore });
            if (prossegue == true)
            {
                cadModulo cadmodulo = new cadModulo();
                cadmodulo.cd_usuario = txtcdUsuarioMenuArvore.Text;
                cadmodulo.cd_modulo = txtcdModuloMenuArvore.Text;
                cadmodulo.cd_programa = Convert.ToInt32(txtcdProgramaMenuArvore.Text);
                try
                {
                    String vret = "";
                    vret = cadModuloDAO.gravaAcessoRapidoUsuario(cadmodulo);
                    limpaCamposMenuArvore();
                    carregaGridMenuArvore(txtcdUsuarioMenuArvore.Text);
                }
                catch (Exception erro)
                {
                    Alert.erro(String.Format("Erro ao Gravar Registro! \n {0}", erro.Message));
                }
            }
        }

        public void limpaCamposAcessoUsuario()
        {
            txtcdUsuarioOrigem.Text = "" ;
            txtdsUsuarioOrigem.Text = "";
            txtcdUsuarioDestino.Text = "";
            txtdsUsuarioDestino.Text = "";
        }

        private void btnCopiaAcessoUsuario_Click(object sender, EventArgs e)
        {
            Boolean prossegue = Valida.verificaObrigatorios(new Object[] { txtcdUsuarioOrigem, txtcdUsuarioDestino });
            if (prossegue == true)
            {
                String retorno = "";
                String copiaAcessoPrograma = String.Format("select copiacessoprograma('{0}','{1}')", txtcdUsuarioOrigem.Text, txtcdUsuarioDestino.Text);
                String copiausuarioprogmenu = String.Format("select copiausuarioprogmenu('{0}','{1}')", txtcdUsuarioOrigem.Text, txtcdUsuarioDestino.Text);
                Utilidades.arrumaDados(copiaAcessoPrograma);
                retorno = Utilidades.arrumaDados(copiausuarioprogmenu);
                if (retorno.Equals(""))
                {
                    Alert.informacao("Cópia realizada com sucesso.");                    
                }
                else
                {
                    Alert.atencao(retorno);
                }
                limpaCamposAcessoUsuario();
            }
        }

        private void txtcdUsuarioOrigem_Validated(object sender, EventArgs e)
        {
            if (cadUsuario.retornaDescricaoUsuario(txtcdUsuarioOrigem.Text) != null)
            {
                txtdsUsuarioOrigem.Text = cadUsuario.retornaDescricaoUsuario(txtcdUsuarioOrigem.Text);
            }
            else
            {
                txtcdUsuarioOrigem.Text = "";
                txtdsUsuarioOrigem.Text = "";
                txtcdUsuarioOrigem.Focus();
            }
        }

        private void txtcdUsuarioDestino_Validated(object sender, EventArgs e)
        {
            if (cadUsuario.retornaDescricaoUsuario(txtcdUsuarioOrigem.Text) != null)
            {
                txtdsUsuarioDestino.Text = cadUsuario.retornaDescricaoUsuario(txtcdUsuarioDestino.Text);
            }
            else
            {
                txtcdUsuarioDestino.Text = "";
                txtdsUsuarioDestino.Text = "";
                txtcdUsuarioDestino.Focus();
            }
        }

        private void txtcdUsuarioRelatorio_Validated(object sender, EventArgs e)
        {
            if (cadUsuario.retornaDescricaoUsuario(txtcdUsuarioOrigem.Text) != null)
            {
                txtdsUsuarioRelatorio.Text = cadUsuario.retornaDescricaoUsuario(txtcdUsuarioRelatorio.Text);
            }
            else
            {
                txtcdUsuarioRelatorio.Text = "";
                txtdsUsuarioRelatorio.Text = "";
                txtcdUsuarioRelatorio.Focus();
            }
        }

        private void txtcdModuloRelatorio_Validated(object sender, EventArgs e)
        {
            if (!txtcdModuloRelatorio.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_modulo from public.modulo where cd_modulo = '{0}';", txtcdModuloRelatorio.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsModuloRelatorio.Text = Convert.ToString(result[0]);
                }
                else
                {
                    txtcdModuloRelatorio.Text = "";
                    txtdsModuloRelatorio.Text = "";
                }
            }
        }

        private void txtcdProgramaRelatorio_Validated(object sender, EventArgs e)
        {
            if (!txtcdProgramaRelatorio.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_programa from public.programa where cd_programa = '{0}';", txtcdProgramaRelatorio.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsProgramaRelatorio.Text = Convert.ToString(result[0]);
                }
                else
                {
                    txtcdProgramaRelatorio.Text = "";
                    txtdsProgramaRelatorio.Text = "";
                }
            }
        }

        private void btnImprimirRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                String vWhere = "";
                Report report = GetReport.loadReport("RelCadAcessoCliente.frx");
                String vSql = "";
                if (!txtcdUsuarioRelatorio.Text.Equals(String.Empty))
                {
                    vWhere += String.Format(" and acessoprograma.cd_usuario = '{0}' ", txtcdUsuarioRelatorio.Text);
                }
                if (!txtcdModuloRelatorio.Text.Equals(String.Empty))
                {
                    vWhere += String.Format(" and modulo.cd_modulo = '{0}' ", txtcdModuloRelatorio.Text);
                }
                if (!txtcdProgramaRelatorio.Text.Equals(String.Empty))
                {
                    vWhere += String.Format(" and programa.cd_programa = '{0}' ", txtcdProgramaRelatorio.Text);
                }
                vSql = "select  " +
                       "	acessoprograma.cd_usuario, usuario.nm_usuario, modulo.cd_modulo, modulo.ds_modulo, " +
                       "	programa.cd_programa, programa.ds_programa" +
                       "  from modulo " +
                       " inner join programa on programa.cd_modulo = modulo.cd_modulo " +
                       " inner join acessoprograma on acessoprograma.cd_modulo = programa.cd_modulo " +
                       " 	                  and acessoprograma.cd_programa = programa.cd_programa " +
                       " inner join usuario on usuario.cd_usuario = acessoprograma.cd_usuario "+                       
                       " where acessoprograma.cd_programa = programa.cd_programa " +
                       vWhere +
                       " order by acessoprograma.cd_usuario, modulo.cd_modulo, programa.cd_programa";

                GetReport.buildReport("tabacesso", vSql, report);
                GetReport.abreVisualizador(report);
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao gerar Relatório " + erro.Message);
            }
        }

        private void txtcdProgramaMenuArvore_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }

        private void btnEnvioTesteEmail_Click(object sender, EventArgs e)
        {
            try
            {                
                if (!txtcdUsuario.Text.Equals(String.Empty))
                {
                    Utilidades.enviaEmail(new String[] {}, txtdsSMTP.Text, Convert.ToInt16(txtnrPortaEmail.Text), chkSSL.Checked,
                        txtdsEmail.Text, txtdsSenhaEmail.Text, txtdsEmail.Text, txtdsEmail.Text,"Teste de confirmação de e-mail", "Teste de confirmação de e-mail");
                }
                else
                {
                    Alert.atencao("É necessário escolher o usuário");
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao realizar o teste de E-mail \n{0}", erro.Message));
            }
        }

        private void gcGridCadUsuario_DoubleClick(object sender, EventArgs e)
        {
            editaUsuario(sender, e);
        }

    }
}