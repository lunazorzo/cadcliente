using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.IO;
using Sistema.Classes.Util;

namespace Sistema
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            if (File.Exists("./config.xml"))
            {
                ArquivoXml.loadXml();
                UserLookAndFeel.Default.SetSkinStyle(StaticVariables.nmSkin);
                //Application.Run(new Telas.Cadastros.frmCadCliente());

                using (Janelas.Acesso.frmLogin Login = new Janelas.Acesso.frmLogin() { WindowState = FormWindowState.Normal })
                {
                    Login.ShowDialog();
                }
                //Application.Run(new Telas.Cadastros.frmCadCliente());
            }
            else
            {
                Alert.erro("Não existe o arquivo config.xml na pasta do sistema!");
            }
        }
    }
}
