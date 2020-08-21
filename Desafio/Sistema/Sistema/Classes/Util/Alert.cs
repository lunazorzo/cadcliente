using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sistema.Classes.Util
{
    class Alert
    {
        public static void informacao(string msg)
        {
            Font defaultFont = AppearanceObject.DefaultFont;
            AppearanceObject.DefaultFont = new Font("Tahoma", 10, FontStyle.Bold);
            XtraMessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AppearanceObject.DefaultFont = defaultFont;
        }

        public static void atencao(string msg)
        {
            Font defaultFont = AppearanceObject.DefaultFont;
            AppearanceObject.DefaultFont = new Font("Tahoma", 10, FontStyle.Bold);
            XtraMessageBox.Show(msg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            AppearanceObject.DefaultFont = defaultFont;
        }

        public static void erro(string msg)
        {
            Font defaultFont = AppearanceObject.DefaultFont;
            AppearanceObject.DefaultFont = new Font("Tahoma", 10, FontStyle.Bold);
            XtraMessageBox.Show(msg, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            AppearanceObject.DefaultFont = defaultFont;
        }

        public static Boolean pergunta(string msg)
        {
            //Exemplo if (Alert.pergunta("Mensagem")){procedimento a ser realizado}
            Font defaultFont = AppearanceObject.DefaultFont;
            AppearanceObject.DefaultFont = new Font("Tahoma", 10, FontStyle.Bold);
            Boolean resposta = XtraMessageBox.Show(String.Format("Você deseja remover o {0}?", msg), "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
            AppearanceObject.DefaultFont = defaultFont;
            return !resposta;
        }
    }
}