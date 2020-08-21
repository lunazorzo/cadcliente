using DevExpress.XtraEditors;
using Sistema.Janelas.Acesso;
using Sistema.Janelas.Cadastro;
using Sistema.Janelas.Ferramentas;
using Sistema.Janelas.Relatorios;
using Sistema.Janelas.Relatorios.Cadastros;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sistema.Classes.Util
{
    class AbreForm
    {

        internal static void abreFrmPrincipal()
        {
            Utilidades.abreForm(new frmPrincipal(), "Erro ao abrir a tela Inicial!");
        }

        #region 'Sistema'
        internal static void abreFrmListaValores(String cdUsuario)
        {
            if (Valida.verificaPermissao("SIS", 1, cdUsuario))
            {
                Utilidades.abreForm(new frmListaValores(), "Erro ao abrir a tela Lista de Valores!");
            }
            else
            {
                Alert.atencao("Usuário sem permissão de acesso a tela de Lista de Valores");
            }
        }

        internal static void abreModulo(String cdUsuario)
        {            
            if (Valida.verificaPermissao("SIS", 2, cdUsuario))
            {
                Utilidades.abreForm(new frmModulo(), "Erro ao abrir a tela Cadastro de Módulos!");
            }
            else
            {
                Alert.atencao("Usuário sem permissão de acesso a tela de Cadastro de Módulos!");
            }
        }

        internal static void abreAcesso(String cdUsuario)
        {
            if (Valida.verificaPermissao("SIS", 3, cdUsuario))
            {
                Utilidades.abreForm(new frmAcesso(), "Erro ao abrir a tela Cadastro de Acesso!");
            }
            else
            {
                Alert.atencao("Usuário sem permissão de acesso a tela de Cadastro de Acesso!");
            }            
        }
        #endregion

        #region 'Cadastro'

        internal static void abrefrmCliente(String cdUsuario)
        {
            if (Valida.verificaPermissao("CAD", 1, cdUsuario))
            {
                Utilidades.abreForm(new frmCliente(), "Erro ao abrir a tela de cadastro de Cliente/Fornecedor!");
            }
            else
            {
                Alert.atencao("Usuário sem permissão de acesso a tela de cadastro de Cliente/Fornecedor!");
            }            
        }      
        #endregion

        #region 'Relatórios'
        internal static void abreRelCliente(String cdUsuario)
        {
            Utilidades.abreForm(new frmRelCliente(), "Erro ao abrir a tela Relatórios Países!");
        }
        #endregion
    }
}
