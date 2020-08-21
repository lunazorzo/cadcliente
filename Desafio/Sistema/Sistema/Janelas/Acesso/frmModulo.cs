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

namespace Sistema.Janelas.Acesso
{
    public partial class frmModulo : DevExpress.XtraEditors.XtraForm
    {
        public frmModulo()
        {
            InitializeComponent();
        }

        private void frmModulo_Load(object sender, EventArgs e)
        {
            carregaGridModulo();
            carregaGridSubMenu();
            carregaGridPrograma();
            carregaAccessibleName();
        }

        private void frmModulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (ActiveControl.Parent.Name.Equals("txtcdModulo"))
                {
                    txtcdModulo.Text = Valida.consultaF9(2, "", "cd_modulo");
                    txtcdModulo_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdModuloSubMenu"))
                {
                    txtcdModuloSubMenu.Text = Valida.consultaF9(2, "", "cd_modulo");
                    txtcdModuloSubMenu_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdSubMenu"))
                {
                    txtcdSubMenu.Text = Valida.consultaF9(3, "", "cd_submenu");
                    txtcdSubMenu_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdModuloPrograma"))
                {
                    txtcdModuloPrograma.Text = Valida.consultaF9(2, "", "cd_modulo");
                    txtcdModuloPrograma_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdSubMenuPrograma"))
                {
                    txtcdSubMenuPrograma.Text = Valida.consultaF9(3, "", "cd_submenu");
                    txtcdSubMenuPrograma_Validated(sender, e);
                }
                if (ActiveControl.Parent.Name.Equals("txtcdPrograma"))
                {
                    txtcdPrograma.Text = Valida.consultaF9(4, "", "cd_programa");
                    txtcdPrograma_Validated(sender, e);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
        }

        public void carregaAccessibleName()
        {
            txtcdModulo.AccessibleName = "o nódigo do usuário";
            txtdsModulo.AccessibleName = "o nome do usuário";
            txtcdModuloSubMenu.AccessibleName = "a senha do usuário";
            txtcdSubMenu.AccessibleName = "a permissão do usuário";
            txtdsSubMenu.AccessibleName = "a descrição do submenu";
            txtcdModuloSubMenu.AccessibleName = "menu desejado";
            txtcdModuloPrograma.AccessibleName = "o módulo";
            txtcdSubMenuPrograma.AccessibleName = "o programa";
            txtcdPrograma.AccessibleName = "o usuário origem";
            txtdsPrograma.AccessibleName = "a descrição do programa";
        }

        public void limpaCamposModulo()
        {
            txtcdModulo.Text = "";
            txtdsModulo.Text = "";
            txtcdModulo.Focus();
            txtcdModulo.Enabled = true;
        }

        public void carregaGridModulo()
        {
            Utilidades.getGrid(GetDataGrid.getDadosModulo(), gcGridModulos, "dos Módulos");
        }

        public void limpaCamposSubMenu()
        {
            txtcdModuloSubMenu.Text = "";
            txtdsModuloSubMenu.Text = "";
            txtcdSubMenu.Text = "";
            txtdsSubMenu.Text = "";
            txtcdModuloSubMenu.Focus();
            txtcdModuloSubMenu.Enabled = true;
            txtcdSubMenu.Enabled = true;
        }

        public void carregaGridSubMenu()
        {
            Utilidades.getGrid(GetDataGrid.getDadosSubMenu(), gcGridSubMenu, "dos SubMenus");
        }

        public void limpaCamposPrograma()
        {
            txtcdModuloPrograma.Text = "";
            txtdsModuloPrograma.Text = "";
            txtcdSubMenuPrograma.Text = "";
            txtdsSubMenuPrograma.Text = "";
            txtcdPrograma.Text = "";
            txtdsPrograma.Text = "";
            txtcdModuloPrograma.Focus();

            txtcdModuloPrograma.Enabled = true;
            txtcdSubMenuPrograma.Enabled = true;
        }

        public void carregaGridPrograma()
        {
            Utilidades.getGrid(GetDataGrid.getDadosPrograma(), gcGridPrograma, "dos Programas");
        }

        private void txtcdModulo_Validated(object sender, EventArgs e)
        {
            if (!txtcdModulo.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_modulo from public.modulo where cd_modulo = '{0}';", txtcdModulo.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsModulo.Text = Convert.ToString(result[0]);
                }
            }
        }

        private void bntGravarModulo_Click(object sender, EventArgs e)
        {
            Boolean prossegue = Valida.verificaObrigatorios(new Object[] { txtcdModulo, txtdsModulo});
            if (prossegue == true)
            {
                cadModulo cadmodulo = new cadModulo();
                cadmodulo.cd_modulo = txtcdModulo.Text;
                cadmodulo.ds_modulo = txtdsModulo.Text;
                try
                {
                    String vret = "";
                    vret = cadModuloDAO.gravaModulo(cadmodulo);
                    limpaCamposModulo();
                    carregaGridModulo();
                }
                catch (Exception erro)
                {
                    Alert.erro(String.Format("Erro ao Gravar Registro! \n {0}", erro.Message));
                }
            }
        }

        public void editaModulo(object sender, EventArgs e)
        {
            try
            {
                txtcdModulo.Text = Convert.ToString(gvGridModulos.GetRowCellValue(gvGridModulos.FocusedRowHandle, "cd_modulo"));
                txtcdModulo_Validated(sender, e);
                txtcdModulo.Enabled = false;
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao editar Módulo. \n" + erro.Message);
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editaModulo(sender, e);
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvGridModulos.IsFocusedView == true)
            {
                String cd_modulo = Convert.ToString(gvGridModulos.GetRowCellValue(gvGridModulos.FocusedRowHandle, "cd_modulo"));
                String nm_modulo = Convert.ToString(gvGridModulos.GetRowCellValue(gvGridModulos.FocusedRowHandle, "ds_modulo"));
                if (Alert.pergunta(String.Format(" Módulo {0} - {1}", cd_modulo, nm_modulo)))
                {
                    Boolean sucesso = Utilidades.remove("Módulo",
                        String.Format("select 1 from acessoprograma where cd_modulo = '{0}'", cd_modulo),
                        String.Format(" delete from modulo where cd_modulo = '{0}'", cd_modulo));
                    if (sucesso)
                    {
                        limpaCamposModulo();
                        carregaGridModulo();
                    }
                }
            }
        }

        private void txtcdModuloSubMenu_Validated(object sender, EventArgs e)
        {
            if (!txtcdModuloSubMenu.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_modulo from public.modulo where cd_modulo = '{0}';", txtcdModuloSubMenu.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsModuloSubMenu.Text = Convert.ToString(result[0]);
                }
            }
        }

        private void txtcdSubMenu_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }

        private void txtcdSubMenu_Validated(object sender, EventArgs e)
        {
            if (!txtcdSubMenu.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_submenu from public.submenu where cd_submenu = {0} and cd_modulo = '{1}';", txtcdSubMenu.Text, txtcdModuloSubMenu.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsSubMenu.Text = Convert.ToString(result[0]);
                }
            }
        }

        private void txtcdModuloPrograma_Validated(object sender, EventArgs e)
        {
            if (!txtcdModuloPrograma.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_modulo from public.modulo where cd_modulo = '{0}';", txtcdModuloPrograma.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsModuloPrograma.Text = Convert.ToString(result[0]);
                }
            }
        }

        private void txtcdSubMenuPrograma_Validated(object sender, EventArgs e)
        {
            if (!txtcdSubMenuPrograma.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_submenu from public.submenu where cd_submenu = '{0}';", txtcdSubMenuPrograma.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsSubMenuPrograma.Text = Convert.ToString(result[0]);
                }
            }
        }

        private void txtcdPrograma_Validated(object sender, EventArgs e)
        {
            if (!txtcdPrograma.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_programa from public.programa where cd_programa = {0} and cd_modulo = '{1}';", txtcdPrograma.Text, txtcdModuloPrograma.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsPrograma.Text = Convert.ToString(result[0]);
                }
            }
        }

        private void btnGravarSubMenu_Click(object sender, EventArgs e)
        {
            Boolean prossegue = Valida.verificaObrigatorios(new Object[] { txtcdModuloSubMenu, txtcdSubMenu, txtdsSubMenu });
            if (prossegue == true)
            {
                cadModulo cadSubMenu = new cadModulo();
                cadSubMenu.cd_modulo = txtcdModuloSubMenu.Text;
                cadSubMenu.cd_submenu = Convert.ToInt32(txtcdSubMenu.Text);
                cadSubMenu.ds_submenu = txtdsSubMenu.Text;
                try
                {
                    String vret = "";
                    vret = cadSubMenuDAO.gravaSubMenu(cadSubMenu);
                    limpaCamposSubMenu();
                    carregaGridSubMenu();
                }
                catch (Exception erro)
                {
                    Alert.erro(String.Format("Erro ao Gravar Registro! \n {0}", erro.Message));
                }
            }
        }

        public void editaSubMenu(object sender, EventArgs e)
        {
            try
            {
                txtcdModuloSubMenu.Text = Convert.ToString(gvGridSubMenu.GetRowCellValue(gvGridSubMenu.FocusedRowHandle, "cd_modulo"));
                txtcdSubMenu.Text = Convert.ToString(gvGridSubMenu.GetRowCellValue(gvGridSubMenu.FocusedRowHandle, "cd_submenu"));
                txtcdModuloSubMenu_Validated(sender, e);
                txtcdSubMenu_Validated(sender, e);
                txtcdModuloSubMenu.Enabled = false;
                txtcdSubMenu.Enabled = false;
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao editar SubMenu. \n" + erro.Message);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            editaSubMenu(sender, e);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (gvGridSubMenu.IsFocusedView == true)
            {                
                String cdModuloSubMenu = Convert.ToString(gvGridSubMenu.GetRowCellValue(gvGridSubMenu.FocusedRowHandle, "cd_modulo"));
                String cdSubMenu = Convert.ToString(gvGridSubMenu.GetRowCellValue(gvGridSubMenu.FocusedRowHandle, "cd_submenu"));
                String dsSubMenu = Convert.ToString(gvGridSubMenu.GetRowCellValue(gvGridSubMenu.FocusedRowHandle, "ds_submenu"));
                if (Alert.pergunta(String.Format(" SubMenu {0} - {1}", cdSubMenu, dsSubMenu)))
                {
                    Boolean sucesso = Utilidades.remove("SubMenu",
                        String.Format("select 1 from programa where cd_modulo = '{0}' and cd_submenu = {1}", cdModuloSubMenu, cdSubMenu),
                        String.Format(" delete from submenu where cd_modulo = '{0}' and cd_submenu = {1}", cdModuloSubMenu, cdSubMenu));
                    if (sucesso)
                    {
                        limpaCamposSubMenu();
                        carregaGridSubMenu();
                    }
                }
            }
        }

        private void btnGravaPrograma_Click(object sender, EventArgs e)
        {
            //Verificar o processo de edição da grid, pois está carregando dados errados
            Boolean prossegue = Valida.verificaObrigatorios(new Object[] { txtcdModuloPrograma, txtcdPrograma, txtdsPrograma });
            if (prossegue == true)
            {
                cadModulo cadPrograma = new cadModulo();
                if (!txtcdSubMenuPrograma.Text.Equals(String.Empty))
                {
                    cadPrograma.cd_submenu = Convert.ToInt32(txtcdSubMenuPrograma.Text);     
                }
                
                cadPrograma.cd_modulo = txtcdModuloPrograma.Text;                           
                cadPrograma.cd_programa = Convert.ToInt32(txtcdPrograma.Text);
                cadPrograma.ds_programa = txtdsPrograma.Text;
                try
                {
                    String vret = "";
                    vret = cadProgramaDAO.gravaPrograma(cadPrograma);
                    limpaCamposPrograma();
                    carregaGridPrograma();
                }
                catch (Exception erro)
                {
                    Alert.erro(String.Format("Erro ao Gravar Registro! \n {0}", erro.Message));
                }
            }
        }

        public void editaCadPrograma(object sender, EventArgs e)
        {
            try
            {
                String vlSubMenuPrograma = "";
                txtcdModuloPrograma.Text = Convert.ToString(gvGridPrograma.GetRowCellValue(gvGridPrograma.FocusedRowHandle, "cd_modulo"));
                vlSubMenuPrograma = Convert.ToString(gvGridPrograma.GetRowCellValue(gvGridPrograma.FocusedRowHandle, "cd_submenu"));
                txtcdPrograma.Text = Convert.ToString(gvGridPrograma.GetRowCellValue(gvGridPrograma.FocusedRowHandle, "cd_programa"));

                if (!vlSubMenuPrograma.Equals("0"))
                {
                    txtcdSubMenuPrograma.Text = vlSubMenuPrograma;
                    txtcdSubMenuPrograma_Validated(sender, e);
                }

                txtcdModuloPrograma_Validated(sender, e);
                txtcdPrograma_Validated(sender, e);


                txtcdModuloPrograma.Enabled = false;
                txtcdSubMenuPrograma.Enabled = false;
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao editar SubMenu. \n" + erro.Message);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            editaCadPrograma(sender, e);            
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (gvGridPrograma.IsFocusedView == true)
            {
                String cd_modulo = Convert.ToString(gvGridPrograma.GetRowCellValue(gvGridPrograma.FocusedRowHandle, "cd_modulo"));
                String cd_programa = Convert.ToString(gvGridPrograma.GetRowCellValue(gvGridPrograma.FocusedRowHandle, "cd_programa"));
                String ds_programa = Convert.ToString(gvGridPrograma.GetRowCellValue(gvGridPrograma.FocusedRowHandle, "ds_programa"));                

                if (Alert.pergunta(String.Format(" Programa {0} - {1}", cd_programa, ds_programa)))
                {
                    Boolean sucesso = Utilidades.remove("Programa",
                        String.Format("select 1 from usuarioprogmenu where cd_modulo = '{0}' and cd_programa = {1}", cd_modulo, cd_programa),
                        String.Format(" delete from programa where cd_modulo = '{0}' and cd_programa = {1}", cd_modulo, cd_programa));
                    if (sucesso)
                    {                        
                        carregaGridPrograma();
                    }
                }
            }
        }

        private void txtcdPrograma_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }

        private void gcGridModulos_DoubleClick(object sender, EventArgs e)
        {
            editaModulo(sender, e);
        }

        private void gcGridSubMenu_DoubleClick(object sender, EventArgs e)
        {
            editaSubMenu(sender, e);
        }

        private void gcGridPrograma_DoubleClick(object sender, EventArgs e)
        {
            editaCadPrograma(sender, e);
        }
    }
}