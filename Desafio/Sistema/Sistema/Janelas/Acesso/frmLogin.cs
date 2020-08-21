using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sistema.Classes.Util;
using System.Data;

namespace Sistema.Janelas.Acesso
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
            }
            if (e.KeyCode == Keys.F9)
            {
                if (ActiveControl.Parent.Name.Equals("txtcdUsuario"))
                {
                    txtcdUsuario.Text = Valida.consultaF9(7, "", "cd_usuario");
                    txtdsUsuario_Validated(sender, e);
                }
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtcdUsuario.Text.Equals(string.Empty) || !txtdsSenha.Text.Equals(string.Empty))
                {
                    String vSql = String.Format("select sn_usuario from usuario where cd_usuario = '{0}' and sn_usuario = '{1}'", txtcdUsuario.Text, txtdsSenha.Text);
                    Object[] result = Utilidades.consultar(vSql);
                    if (result != null)
                    {
                        StaticVariables.cdUsuario = txtcdUsuario.Text;                        
                        this.Hide();
                        AbreForm.abreFrmPrincipal();
                        this.Close();                        
                    }
                    else
                    {
                        Alert.atencao("Usuário ou Senha inválidos");
                        txtcdUsuario.Focus();
                    }
                }
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao realizar acesso ao sistema. " + erro.Message);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtdsUsuario_Validated(object sender, EventArgs e)
        {
            if (!txtcdUsuario.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select nm_usuario from public.usuario where cd_usuario = '{0}';", txtcdUsuario.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsUsuario.Text = Convert.ToString(result[0]);
                }
                else
                {
                    txtcdUsuario.Text = "";
                    txtdsUsuario.Text = "";
                    Alert.atencao("Usuário não encontrado");
                    txtcdUsuario.Focus();
                }
            }
        }
    }
}