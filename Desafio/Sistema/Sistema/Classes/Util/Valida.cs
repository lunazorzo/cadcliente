using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sistema.Classes.Util
{
    class Valida
    {
        private static LstValores Lista = new LstValores();

        /* Validação Inscrição  Estadual
         * A validação utiliza a DLL disponibilizad pelo Sintegra.
         * Realizar o download da dll pelo link http://www.sintegra.gov.br/DLL3.zip e insirir a dll DllInscE32.dll na pasta bin junto com o .exe         
         * Exemplo: 
         *      Int64 ie = Valida.ConsisteInscricaoEstadual(123456789, "PR");
                if (ie == 1)
                {
                    Alert.atencao("Inscrição Estadual é Inválida!");
                }
         * 
         */
        [DllImport("DllInscE32.dll")]
        public static extern int ConsisteInscricaoEstadual(string cInsc, string cUF);

        public static String formataDecimal(Decimal vlDecimal)
        {            
            String retorno = String.Format("{0:N}", vlDecimal);
            return retorno;
        }

        public static void keypressCNPJCFP(KeyPressEventArgs e)
        {
            const string caracteresPermitidos = "0123456789./-";
            if (!(caracteresPermitidos.Contains(e.KeyChar.ToString().ToUpper())) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        public static void keypressCEP(KeyPressEventArgs e)
        {
            const string caracteresPermitidos = "0123456789.-";
            if (!(caracteresPermitidos.Contains(e.KeyChar.ToString().ToUpper())) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        public static void keypressSomenteNumero(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)13)
            {
                e.Handled = true;
            }
        }

        public static void keypressDecimal(object sender, KeyPressEventArgs e)
        {
            //https://www.blogson.com.br/como-formatar-moeda-ou-casas-decimais-no-c-sharp-c/
            const string caracteresPermitidos = "0123456789,-";
            if (!(caracteresPermitidos.Contains(e.KeyChar.ToString().ToUpper())) || !(e.KeyChar == (char)Keys.Back) && (e.KeyChar == ',') && ((sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        public static void keypressTelefone(KeyPressEventArgs e)
        {
            const string caracteresPermitidos = "0123456789()-";
            if (!(caracteresPermitidos.Contains(e.KeyChar.ToString().ToUpper())) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        public static void validaEmail(Control field)
        {
            if (field.Text.Equals(string.Empty))
            {
                String email = field.Text;
                if (Valida.validaEmail(email))
                {
                    field.Text = email;
                }
                else
                {
                    Alert.atencao("E-mail Inválido");
                    field.Focus();
                }
            }
        }

        


        public static void clear(object[] campos)
        {
            for (int i = 0; i < campos.Length; i++)
            {
                if (campos[i] is DevExpress.XtraEditors.TextEdit)
                {
                    ((DevExpress.XtraEditors.TextEdit)campos[i]).Text = "";
                }
                else if (campos[i] is DevExpress.XtraEditors.ComboBoxEdit)
                {
                    ((DevExpress.XtraEditors.ComboBoxEdit)campos[i]).SelectedIndex = -1;
                }
                else if (campos[i] is DevExpress.XtraEditors.CalcEdit)
                {
                    ((DevExpress.XtraEditors.CalcEdit)campos[i]).Text = "0.00";
                }
                else if (campos[i] is DevExpress.XtraEditors.DateEdit)
                {
                    ((DevExpress.XtraEditors.DateEdit)campos[i]).EditValue = "";
                }
                else if (campos[i] is DevExpress.XtraRichEdit.RichEditControl)
                {
                    ((DevExpress.XtraRichEdit.RichEditControl)campos[i]).Text = "";
                }
                else if (campos[i] is TextBox)
                {
                    ((TextBox)campos[i]).Text = "";
                }
                else if (campos[i] is ComboBox)
                {
                    ((ComboBox)campos[i]).SelectedIndex = -1;
                }
                else if (campos[i] is ListView)
                {
                    ((ListView)campos[i]).Items.Clear();
                }
                else if (campos[i] is RichTextBox)
                {
                    ((RichTextBox)campos[i]).Clear();
                }
            }
        }

        internal static bool validaEmail(string iemail)
        {
            //https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/EmailAddressAttribute.cs,54
            const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
            Regex rg = new Regex(pattern);
            return rg.IsMatch(iemail.ToLower());
        }

        public static bool validaCPFCNPJ(string cpfcnpj, bool vazio)
        {
            //https://ivanmeirelles.wordpress.com/2012/10/26/validar-cpf-eou-cnpj-em-c/
            if (string.IsNullOrEmpty(cpfcnpj))
                return vazio;
            else
            {
                int[] d = new int[14];
                int[] v = new int[2];
                int j, i, soma;
                string Sequencia, SoNumero;

                SoNumero = Regex.Replace(cpfcnpj, "[^0-9]", string.Empty);

                //verificando se todos os numeros são iguais
                if (new string(SoNumero[0], SoNumero.Length) == SoNumero) return false;

                // se a quantidade de dígitos numérios for igual a 11
                // iremos verificar como CPF
                if (SoNumero.Length == 11)
                {
                    for (i = 0; i <= 10; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 8 + i; j++) soma += d[j] * (10 + i - j);

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[9] & v[1] == d[10]);
                }
                // se a quantidade de dígitos numérios for igual a 14
                // iremos verificar como CNPJ
                else if (SoNumero.Length == 14)
                {
                    Sequencia = "6543298765432";
                    for (i = 0; i <= 13; i++) d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 11 + i; j++)
                            soma += d[j] * Convert.ToInt32(Sequencia.Substring(j + 1 - i, 1));

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[12] & v[1] == d[13]);
                }
                // CPF ou CNPJ inválido se
                // a quantidade de dígitos numérios for diferente de 11 e 14
                else return false;
            }
        }

        public static Boolean validaIE(String dsEstado, String dsIe)
        {
            /*Como chamar o método             
             * if (Valida.validaIE(txtdsIE.Text, "GO") == false)
                {
                    Alert.erro("IE Errada");                
                }
             */
            bool retorno = false;
            String ie = Valida.removeCaracteres(dsIe).Replace(" ", "");
            #region 'Inicia Switch
            switch (dsEstado.ToUpper())
            {
                #region 'AC - Acre'
                case "AC":
                    if (ie.Length == 13)
                    {
                        /* Exemplos de IE: 
                         *  01.151.679/216-00, 01.771.817/990-51, 01.109.772/392-02, 01.020.428/797-63, 01.395.568/348-54, 
                         *  01.130.207/185-25, 01.813.732/675-36, 01.501.835/815-07, 01.428.856/640-88, 01.393.708/432-09*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 2);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                        Int32 D10 = Convert.ToInt32(ie.Substring(9, 1));
                        Int32 D11 = Convert.ToInt32(ie.Substring(10, 1));
                        Int32 somaDig1 = (D1 * 4) + (D2 * 3) + (D3 * 2) + (D4 * 9) + (D5 * 8) + (D6 * 7) + (D7 * 6) + (D8 * 5) + (D9 * 4) + (D10 * 3) + (D11 * 2);
                        Int32 valorDig1 = 11 - (somaDig1 % 11);
                        if (valorDig1 == 10 || valorDig1 == 11)
                        {
                            valorDig1 = 0;
                        }
                        Int32 somaDig2 = (D1 * 5) + (D2 * 4) + (D3 * 3) + (D4 * 2) + (D5 * 9) + (D6 * 8) + (D7 * 7) + (D8 * 6) + (D9 * 5) + (D10 * 4) + (D11 * 3) + (valorDig1 * 2);
                        Int32 valorDig2 = 11 - (somaDig2 % 11);
                        if (valorDig2 == 10 || valorDig2 == 11)
                        {
                            valorDig2 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(valorDig1) + Convert.ToString(valorDig2);
                    }
                    break;
                #endregion
                #region 'AL - Alagoas'
                case "AL":
                    if (ie.Length == 9)
                    {
                        if (Convert.ToInt32(ie.Substring(0, 2)) == 24)
                        {
                            /* Exemplo de IE: 248328670, 248456121, 248913522, 248013963, 248245244, 248679015, 248217046, 248255177, 248691708, 248311999*/
                            String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                            Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                            Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                            Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                            Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                            Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                            Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                            Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                            Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                            Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                            Int32 soma = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                            Int32 produto = soma * 10;
                            Int32 resto = produto - (produto / 11 * 11);
                            if (resto == 10)
                            {
                                resto = 0;
                            }
                            retorno = digitoVerificarOriginal == Convert.ToString(resto);
                        }
                    }
                    break;
                #endregion
                #region 'AP - Amapá'
                case "AP":
                    if (ie.Length == 9)
                    {
                        /*Exemplos de IE: 036171930, 032327951, 039237532, 031480543, 033560854, 031253105, 039416356, 036687847, 032559518, 039424669*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 P = 0, D = 0;
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                        Int32 Veri = Convert.ToInt32(ie.Substring(0, 8));
                        if (Veri >= 3000001 && Veri <= 3017000)
                        {
                            P = 5;
                            D = 0;
                        }
                        if (Veri >= 3017001 && Veri <= 3019022)
                        {
                            P = 9;
                            D = 1;
                        }
                        Int32 X1 = P + (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                        Int32 X2 = X1 - (X1 / 11 * 11);
                        Int32 X3 = 11 - X2;
                        Int32 digitoVerificadorCalculado;
                        if (X3 == 10)
                        {
                            digitoVerificadorCalculado = 0;
                        }
                        else if (X3 == 11)
                        {
                            digitoVerificadorCalculado = D;
                        }
                        else
                        {
                            digitoVerificadorCalculado = X3;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(digitoVerificadorCalculado);
                    }
                    break;
                #endregion
                #region 'AM - Amazonas'
                case "AM":
                    if (ie.Length == 9)
                    {
                        /*Exemplos de IE: 54.456.300-0, 30.796.226-1, 26.202.480-2, 29.776.762-3, 41.539.326-4, 22.257.427-5, 07.428.913-6, 84.150.910-7, 44.204.319-8, 49.250.551-9*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                        Int32 digitoVerificadorCalculado;
                        Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                        if (X1 < 11)
                        {
                            digitoVerificadorCalculado = 11 - X1;
                        }
                        else
                        {
                            Int32 X2 = X1 - (X1 / 11 * 11);
                            if (X2 <= 1)
                            {
                                digitoVerificadorCalculado = 0;
                            }
                            else
                            {
                                digitoVerificadorCalculado = 11 - X2;
                            }
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(digitoVerificadorCalculado);
                    }
                    break;
                #endregion
                #region 'BA - Bahia'
                case "BA":
                    if (ie.Length == 8 || ie.Length == 9)
                    {
                        /*Exemplos de IE:
                        029952-58, 042239-91, 123456-63, 181880-04, 270971-95, 359181-94, 394468-15, 443904-79, 526493-74, 697982-03, 615136-01, 639959-12, 
                        716288-00, 740437-09, 763232-00, 746744-93, 788941-70, 809569-03, 815747-99, 913638-20, 934762-03, 1000003-06 */
                        Int32 modulo = 10;
                        Int32 D9 = 0;

                        String digitoVerificarOriginal = ie.Substring(ie.Length - 2);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));

                        if (ie.Length == 8)
                        {
                            if (D1 >= 6 && D1 <= 7 || D1 == 9)
                            {
                                modulo = 11;
                            }
                            Int32 somaDig2 = (D1 * 7) + (D2 * 6) + (D3 * 5) + (D4 * 4) + (D5 * 3) + (D6 * 2);
                            Int32 valorDig2 = modulo - (somaDig2 % modulo);//Retorna o resto da divisão
                            if (valorDig2 == 10 || valorDig2 == 11)
                            {
                                valorDig2 = 0;
                            }

                            Int32 somaDig1 = (D1 * 8) + (D2 * 7) + (D3 * 6) + (D4 * 5) + (D5 * 4) + (D6 * 3) + (valorDig2 * 2);
                            Int32 valorDig1 = modulo - (somaDig1 % modulo);//Retorna o resto da divisão
                            if (valorDig1 == 10 || valorDig1 == 11)
                            {
                                valorDig1 = 0;
                            }
                            retorno = digitoVerificarOriginal == Convert.ToString(valorDig1) + Convert.ToString(valorDig2);
                        }
                        if (ie.Length == 9)
                        {
                            D9 = Convert.ToInt32(ie.Substring(8, 1));
                            if (D2 >= 6 && D2 <= 7 || D2 == 9)//Não foi possível testar, pois não foi encontrado nenhuma IE com essas características
                            {
                                modulo = 11;
                            }

                            Int32 somaDig2 = (D1 * 8) + (D2 * 7) + (D3 * 6) + (D4 * 5) + (D5 * 4) + (D6 * 3) + (D7 * 2);
                            Int32 valorDig2 = modulo - (somaDig2 % modulo);//Retorna o resto da divisão
                            if (valorDig2 == 10 || valorDig2 == 11)
                            {
                                valorDig2 = 0;
                            }

                            Int32 somaDig1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (valorDig2 * 2);
                            Int32 valorDig1 = modulo - (somaDig1 % modulo);//Retorna o resto da divisão
                            if (valorDig1 == 10 || valorDig1 == 11)
                            {
                                valorDig1 = 0;
                            }
                            retorno = digitoVerificarOriginal == Convert.ToString(valorDig1) + Convert.ToString(valorDig2);
                        }
                    }
                    break;
                #endregion
                #region 'CE - Ceará'
                case "CE":
                    if (ie.Length == 9)
                    {
                        /*Exemplos de IE: 67548532-0, 39110472-1, 78338720-2, 69525097-3, 69449761-4, 92630475-5, 71009344-6, 99413241-7, 44820599-8, 45518893-9*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                        Int32 X2 = 11 - (X1 % 11);
                        if (X2 == 10 || X2 == 11)
                        {
                            X2 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X2);
                    }
                    break;
                #endregion
                #region 'DF - Distrito Federal'
                case "DF":
                    if (ie.Length == 13)
                    {
                        /*Exemplos de IE: 07568131001-60, 07324434001-91, 07633741001-12, 07667064001-83, 07442198001-34, 07325338001-05, 07714974001-66, 07244378001-07, 07045539001-18, 07255468001-49*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 2);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                        Int32 D10 = Convert.ToInt32(ie.Substring(9, 1));
                        Int32 D11 = Convert.ToInt32(ie.Substring(10, 1));
                        Int32 X1 = (D1 * 4) + (D2 * 3) + (D3 * 2) + (D4 * 9) + (D5 * 8) + (D6 * 7) + (D7 * 6) + (D8 * 5) + (D9 * 4) + (D10 * 3) + (D11 * 2);
                        Int32 X2 = 11 - (X1 % 11);
                        if (X2 == 10 || X2 == 11)
                        {
                            X2 = 0;
                        }
                        Int32 X3 = (D1 * 5) + (D2 * 4) + (D3 * 3) + (D4 * 2) + (D5 * 9) + (D6 * 8) + (D7 * 7) + (D8 * 6) + (D9 * 5) + (D10 * 4) + (D11 * 3) + (X2 * 2);
                        Int32 X4 = 11 - (X3 % 11);
                        if (X4 == 10 || X4 == 11)
                        {
                            X4 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X2) + Convert.ToString(X4);
                    }
                    break;
                #endregion
                #region 'ES - Espírito Santo'
                case "ES":
                    if (ie.Length == 9)
                    {
                        /*Exemplos de IE: 36128716-0, 78244278-1, 47955004-2, 17328595-3, 22444546-4, 30647649-5, 23739879-6, 11334090-7, 54412265-8, 55394870-9*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                        Int32 X2 = 11 - (X1 % 11);
                        if (X2 == 10 || X2 == 11)
                        {
                            X2 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X2);
                    }
                    break;
                #endregion
                #region 'GO - Goías'
                case "GO":
                    if (ie.Length == 9)
                    {
                        /* Exemplos de IE
                        10.865.213-0, 10.313.072-1, 10.478.434-2, 10.059.619-3, 10.071.499-4, 10.158.486-5, 10.945.977-6, 10.990.456-7, 10.846.574-8, 10.415.742-9 
                        11.310.546-0, 11.257.905-1, 11.538.206-2, 11.152.862-3, 11.680.859-4, 11.325.679-5, 11.451.340-6, 11.924.290-7, 11.636.891-8, 11.434.947-9 
                        15.210.576-0, 15.541.866-1, 15.046.784-2, 15.591.861-3, 15.569.303-4, 15.562.326-5, 15.246.304-6, 15.620.613-7, 15.406.194-8, 15.468.847-9 
                        */

                        if (Convert.ToInt32(ie.Substring(0, 2)) == 10 || Convert.ToInt32(ie.Substring(0, 2)) == 11 || Convert.ToInt32(ie.Substring(0, 2)) == 15)
                        {
                            String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                            Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                            Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                            Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                            Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                            Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                            Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                            Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                            Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                            Int32 ieCompleta = Convert.ToInt32(ie.Substring(0, 8));
                            Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                            Int32 X2 = X1 - (X1 / 11) * 11;//11 - (X1 % 11);
                            if (X2 >= 2)
                            {
                                X2 = 11 - X2;
                            }
                            else
                            {
                                if (X2 == 1 && ieCompleta >= 10103105 && ieCompleta <= 10103105)
                                {
                                    X2 = 1;
                                }
                                else
                                {
                                    X2 = 0;
                                }
                            }
                            if (ieCompleta == 11094402)
                            {
                                X2 = 0;
                            }
                            retorno = digitoVerificarOriginal == Convert.ToString(X2);
                        }
                    }
                    break;
                #endregion
                #region 'MA - Maranhão'
                case "MA":
                    if (ie.Length == 9)
                    {
                        /* Exemplos de IE: 12115355-0, 12790055-1, 12085845-2, 12020274-3, 12340749-4, 12928402-5, 12634053-6, 12603599-7, 12229941-8, 12802781-9*/
                        if (Convert.ToInt32(ie.Substring(0, 2)) == 12)
                        {
                            String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                            Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                            Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                            Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                            Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                            Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                            Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                            Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                            Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                            Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                            Int32 X2 = X1 % 11;
                            Int32 X3 = 11 - X2;
                            if (X2 >= 0 && X2 <= 1)
                            {
                                X3 = 0;
                            }
                            retorno = digitoVerificarOriginal == Convert.ToString(X3);
                        }
                    }
                    break;
                #endregion
                #region 'MT - Mato Grosso'
                case "MT":
                    if (ie.Length == 11)
                    {
                        /* Exemplos de IE: 1444653828-0, 5640992947-1, 1884666741-2, 1489973603-3, 3881406738-4, 7659366787-5, 9303129820-6, 3800913008-7, 2180361047-8, 2883509256-9 */
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                        Int32 D10 = Convert.ToInt32(ie.Substring(9, 1));
                        Int32 X1 = (D1 * 3) + (D2 * 2) + (D3 * 9) + (D4 * 8) + (D5 * 7) + (D6 * 6) + (D7 * 5) + (D8 * 4) + (D9 * 3) + (D10 * 2);
                        Int32 X2 = X1 % 11;
                        Int32 X3 = 11 - X2;
                        if (X2 >= 0 && X2 <= 1)
                        {
                            X3 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X3);
                    }
                    break;
                #endregion
                #region 'MS - Mato Grosso do Sul'
                case "MS":
                    if (ie.Length == 9)
                    {
                        if (Convert.ToInt32(ie.Substring(0, 2)) == 28)
                        {
                            /* Exemplos de IE: 28911067-0, 28954385-1, 28250663-2, 28029944-3, 28650186-4, 28491404-5, 28330235-6, 28135212-7, 28621901-8, 28051129-9 */
                            String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                            Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                            Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                            Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                            Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                            Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                            Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                            Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                            Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                            Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                            Int32 X2 = X1 % 11;
                            Int32 X3 = 11 - X2;
                            if (X2 == 0)
                            {
                                X3 = 0;
                            }
                            retorno = digitoVerificarOriginal == Convert.ToString(X3);
                        }
                    }
                    break;
                #endregion
                #region 'MG - Minas Gerais REVER'
                case "MG":
                    if (ie.Length == 16)
                    {
                        /* Exmplos de IE: 804.932.758/3840, 225.556.271/7230, 062.307.904/0081, 536.989.154/0112, 948.300.253/4453, 229.999.059/7724, 
                                          976.313.896/0785, 935.311.530/6466, 859.816.222/4857, 013.981.502/8888, 631.572.129/8799 */
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 4);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(9, 1));
                        Int32 D10 = Convert.ToInt32(ie.Substring(9, 1));
                        Int32 D11 = Convert.ToInt32(ie.Substring(10, 1));
                        Int32 D12 = Convert.ToInt32(ie.Substring(11, 1));
                        Int32 D13 = Convert.ToInt32(ie.Substring(12, 1));
                        Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                        Int32 X2 = X1 % 11;
                        Int32 X3 = 11 - X2;
                        if (X2 == 0)
                        {
                            X3 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X3);
                    }
                    break;
                #endregion
                #region 'PA - Pará'
                case "PA":
                    if (ie.Length == 9)
                    {
                        /* Exmplos de IE: 15-645677-0, 15-706055-1, 15-139952-2, 15-648530-3, 15-212462-4, 15-778080-5, 15-755373-6, 15-814848-7, 15-204732-8, 15-835598-9 */
                        if (Convert.ToInt32(ie.Substring(0, 2)) == 15)
                        {
                            String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                            Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                            Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                            Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                            Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                            Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                            Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                            Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                            Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));

                            Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                            Int32 X2 = X1 % 11;
                            Int32 X3 = 11 - X2;
                            if (X2 >= 0 && X2 <= 1)
                            {
                                X3 = 0;
                            }
                            retorno = digitoVerificarOriginal == Convert.ToString(X3);
                        }
                    }
                    break;
                #region 'PB - Paraíba'
                case "PB":
                    if (ie.Length == 9)
                    {
                        /*Exemplos de IE: 31747787-0, 90452081-1, 95232434-2, 56834682-3, 03789745-4, 88190253-5, 93644331-6, 58203691-7, 77912220-8, 54102060-9*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));

                        Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                        Int32 X2 = X1 % 11;
                        Int32 X3 = 11 - X2;
                        if (X3 == 10 || X3 == 11)
                        {
                            X3 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X3);
                    }
                    break;
                #endregion
                #endregion
                #region 'PR - Paraná'
                case "PR":
                    if (ie.Length == 10)
                    {
                        /* Exmplos de IE: 197.28498-60, 279.84372-00, 681.54064-41, 618.75372-72, 473.34556-03, 891.35116-04, 658.27008-75, 396.58498-06, 925.25600-57, 810.88627-78, 858.38077-49 */
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 2);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                        Int32 D10 = Convert.ToInt32(ie.Substring(9, 1));
                        Int32 X1 = (D1 * 3) + (D2 * 2) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                        Int32 X2 = 11 - (X1 % 11);
                        if (X2 == 10 || X2 == 11)
                        {
                            X2 = 0;
                        }
                        Int32 X3 = (D1 * 4) + (D2 * 3) + (D3 * 2) + (D4 * 7) + (D5 * 6) + (D6 * 5) + (D7 * 4) + (D8 * 3) + (X2 * 2);
                        Int32 X4 = 11 - (X3 % 11);
                        if (X4 == 10 || X4 == 11)
                        {
                            X4 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X2) + Convert.ToString(X4);
                    }
                    break;
                #endregion
                #region 'PE - Pernambuco'
                case "PE":
                    if (ie.Length == 9)
                    {
                        /*Exemplos de IE: 3256587-90, 8287016-01, 5420557-32, 2047637-03, 3796810-64, 8745859-45, 5520962-96, 0231892-07, 5302967-48, 7033069-79*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 2);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 X1 = (D1 * 8) + (D2 * 7) + (D3 * 6) + (D4 * 5) + (D5 * 4) + (D6 * 3) + (D7 * 2);
                        Int32 X2 = X1 % 11;
                        Int32 X3 = 11 - X2;
                        if (X2 == 0 || X2 == 1)
                        {
                            X3 = 0;
                        }
                        Int32 X4 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (X3 * 2);
                        Int32 X5 = X4 % 11;
                        Int32 X6 = 11 - X5;
                        if (X5 == 0 || X5 == 1)
                        {
                            X6 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X3) + Convert.ToString(X6);
                    }
                    else if (ie.Length == 14)
                    {
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(9, 1));
                        Int32 D10 = Convert.ToInt32(ie.Substring(9, 1));
                        Int32 D11 = Convert.ToInt32(ie.Substring(10, 1));
                        Int32 D12 = Convert.ToInt32(ie.Substring(11, 1));
                        Int32 D13 = Convert.ToInt32(ie.Substring(12, 1));
                        Int32 X1 = (D1 * 5) + (D2 * 4) + (D3 * 3) + (D4 * 2) + (D5 * 1) + (D6 * 9) + (D7 * 8) + (D8 * 7) + (D9 * 6) + (D10 * 5) + (D11 * 4) + (D12 * 3) + (D13 * 2);
                        Int32 X2 = X1 % 11;
                        Int32 X3 = 11 - X2;
                        if (X2 > 9)
                        {
                            X3 = X3 - 10;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X3);
                    }
                    break;
                #endregion
                #region 'PI - Piauí'
                case "PI":
                    if (ie.Length == 9)
                    {
                        /* Exmplos de IE: 00708537-0, 54990038-1, 23755616-2, 64329223-3, 75777125-4, 01041760-5, 52236301-6, 61551114-7, 25569686-8, 84046755-9 */
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                        Int32 X2 = X1 % 11;
                        Int32 X3 = 11 - X2;
                        if (X3 == 10 || X3 == 11)
                        {
                            X3 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X3);
                    }
                    break;
                #endregion
                #region 'RJ - Rio de Janeiro'
                case "RJ":
                    if (ie.Length == 8)
                    {
                        /* Exemplos de IE: 30.785.80-0, 27.757.90-1, 82.582.59-2, 96.052.86-3, 96.131.84-4, 01.158.46-5, 98.056.69-6, 99.873.95-7, 70.443.53-8, 29.948.25-9*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 X1 = (D1 * 2) + (D2 * 7) + (D3 * 6) + (D4 * 5) + (D5 * 4) + (D6 * 3) + (D7 * 2);
                        Int32 X2 = X1 % 11;
                        Int32 X3 = 11 - X2;
                        if (X2 <= 1)
                        {
                            X3 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X3);
                    }
                    break;
                #endregion
                #region 'RN - Rio Grande do Norte'
                case "RN":
                    if (ie.Length == 9 || ie.Length == 10)
                    {
                        if (Convert.ToInt32(ie.Substring(0, 2)) == 20)
                        {
                            /* Exemplo de IE: 20.757.524-0, 20.091.649-1, 20.672.740-2, 20.187.389-3, 20.750.187-4, 20.465.318-5, 20.818.390-6, 20.412.343-7, 20.118.903-8, 20.360.582-9*/
                            String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                            Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                            Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                            Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                            Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                            Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                            Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                            Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                            Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                            Int32 X1, X2, X3;
                            if (ie.Length == 9)
                            {
                                X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                            }
                            else
                            {
                                Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                                X1 = (D1 * 10) + (D2 * 9) + (D3 * 8) + (D4 * 7) + (D5 * 6) + (D6 * 5) + (D7 * 4) + (D8 * 3) + (D9 * 2);
                            }
                            X2 = X1 * 10;
                            X3 = X2 % 11;
                            if (X3 == 10)
                            {
                                X3 = 0;
                            }
                            retorno = digitoVerificarOriginal == Convert.ToString(X3);
                        }
                    }
                    break;
                #endregion
                #region 'RS - Rio Grande do Sul'
                case "RS":
                    if (ie.Length == 10)
                    {
                        /* Exemplos de IE: 878/7529880, 552/1257221, 028/1434042, 644/8823593, 528/2476904, 572/4178415, 334/3032486, 251/4687947, 462/4654268, 701/6202829 */
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                        Int32 X1 = (D1 * 2) + (D2 * 9) + (D3 * 8) + (D4 * 7) + (D5 * 6) + (D6 * 5) + (D7 * 4) + (D8 * 3) + (D9 * 2);
                        Int32 X2 = 11 - (X1 % 11);
                        if (X2 == 10 || X2 == 11)
                        {
                            X2 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X2);
                    }
                    break;
                #endregion
                #region 'RO - Rondônia'
                case "RO":
                    if (ie.Length == 14)
                    {
                        /* Exemplos de IE: 6711830082013-0, 9199996661319-1, 7530272101417-2, 7348802312107-3, 3009320834075-4, 3583067810153-5, 3829996688626-6, 4228840525927-7, 2418290337453-8, 8949790412953-9*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                        Int32 D10 = Convert.ToInt32(ie.Substring(9, 1));
                        Int32 D11 = Convert.ToInt32(ie.Substring(10, 1));
                        Int32 D12 = Convert.ToInt32(ie.Substring(11, 1));
                        Int32 D13 = Convert.ToInt32(ie.Substring(12, 1));
                        Int32 X1 = (D1 * 6) + (D2 * 5) + (D3 * 4) + (D4 * 3) + (D5 * 2) + (D6 * 9) + (D7 * 8) + (D8 * 7) + (D9 * 6) + (D10 * 5) + (D11 * 4) + (D12 * 3) + (D13 * 2);
                        Int32 X2 = 11 - (X1 % 11);
                        if (X2 == 10 || X2 == 11)
                        {
                            X2 = X2 - 10;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X2);
                    }
                    break;
                #endregion
                #region 'RR - Roraima'
                case "RR":
                    if (ie.Length == 9)
                    {
                        if (Convert.ToInt32(ie.Substring(0, 2)) == 24)
                        {
                            /*Exemplos de IE: 24268187-0, 24018843-1, 24428743-2, 24952934-3, 24019091-4, 24091001-5, 24777948-6, 24150081-7, 24899073-8*/
                            String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                            Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                            Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                            Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                            Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                            Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                            Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                            Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                            Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                            Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                            Int32 X1 = (D1 * 1) + (D2 * 2) + (D3 * 3) + (D4 * 4) + (D5 * 5) + (D6 * 6) + (D7 * 7) + (D8 * 8) + (D9 * 9);
                            Int32 X2 = X1 % 9;
                            retorno = digitoVerificarOriginal == Convert.ToString(X2);
                        }
                    }
                    break;
                #endregion
                #region 'SC - Santa Catarina'
                case "SC":
                    if (ie.Length == 9)
                    {
                        /* Exemplo de IE: 864.143.850, 514.231.971, 970.183.992, 874.285.763, 368.085.414, 901.005.835, 686.219.716, 844.902.837, 163.270.538, 402.160.819*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));

                        Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                        Int32 X2 = X1 % 11;
                        Int32 X3 = 11 - X2;
                        if (X2 >= 0 && X2 <= 1)
                        {
                            X3 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X3);
                    }
                    break;
                #endregion
                #region 'SP - São Paulo'
                case "SP":
                    if (ie.Length == 12 || ie.Length == 13)
                    {
                        String vPosicao = ie.Substring(0, 1);
                        if (!vPosicao.Equals("P"))
                        {
                            /* Exemplos de IE: 633.744.459.770, 049.821.940.751, 744.314.850.652, 452.409.190.431, 959.358.034.754, 762.439.378.835, 655.352.359.986, 109.572.889.567, 299.456.643.568, 924.379.461.899 */
                            Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                            Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                            Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                            Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                            Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                            Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                            Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                            Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                            Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                            Int32 D10 = Convert.ToInt32(ie.Substring(9, 1));
                            Int32 D11 = Convert.ToInt32(ie.Substring(10, 1));
                            Int32 D12 = Convert.ToInt32(ie.Substring(11, 1));
                            Int32 X1 = (D1 * 1) + (D2 * 3) + (D3 * 4) + (D4 * 5) + (D5 * 6) + (D6 * 7) + (D7 * 8) + (D8 * 10);
                            Int32 X2 = X1 % 11;
                            if (X2 == 10)
                            {
                                X2 = 0;
                            }
                            Int32 X3 = (D1 * 3) + (D2 * 2) + (D3 * 10) + (D4 * 9) + (D5 * 8) + (D6 * 7) + (D7 * 6) + (D8 * 5) + (X2 * 4) + (D10 * 3) + (D11 * 2);
                            Int32 X4 = X3 % 11;
                            if (X4 == 10)
                            {
                                X4 = 0;
                            }
                            retorno = Convert.ToString(D9) + Convert.ToString(D12) == Convert.ToString(X2) + Convert.ToString(X4);
                        }
                        else //Produtor Rural
                        {
                            /*Exemplo de IE:  P-01100424.3/002 é a que está disponível no site do sintegra*/
                            String D1 = ie.Substring(0, 1);
                            Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                            Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                            Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                            Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                            Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                            Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                            Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                            Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                            Int32 D10 = Convert.ToInt32(ie.Substring(9, 1));
                            Int32 D11 = Convert.ToInt32(ie.Substring(10, 1));
                            Int32 D12 = Convert.ToInt32(ie.Substring(11, 1));
                            Int32 D13 = Convert.ToInt32(ie.Substring(12, 1));
                            Int32 X1 = (D2 * 1) + (D3 * 3) + (D4 * 4) + (D5 * 5) + (D6 * 6) + (D7 * 7) + (D8 * 8) + (D9 * 10);
                            Int32 X2 = X1 % 11;
                            retorno = Convert.ToString(D10) == Convert.ToString(X2);
                        }
                    }
                    break;
                #endregion
                #region 'SE - Sergipe'
                case "SE":
                    if (ie.Length == 9)
                    {
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 X1 = (D1 * 9) + (D2 * 8) + (D3 * 7) + (D4 * 6) + (D5 * 5) + (D6 * 4) + (D7 * 3) + (D8 * 2);
                        Int32 X2 = 11 - (X1 % 11);
                        if (X2 == 10 || X2 == 11)
                        {
                            X2 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X2);
                    }
                    break;
                #endregion
                #region 'TO - Tocantins'
                case "TO":
                    if (ie.Length == 11)
                    {
                        /* Exemplos de IE: 2703425560-0, 6403046824-1, 6303310587-2, 3303585637-3, 9803190814-4, 4903604541-5, 6503031187-6, 8103969949-7, 3203146786-8, 3903705364-9*/
                        String digitoVerificarOriginal = ie.Substring(ie.Length - 1);
                        Int32 D1 = Convert.ToInt32(ie.Substring(0, 1));
                        Int32 D2 = Convert.ToInt32(ie.Substring(1, 1));
                        Int32 D3 = Convert.ToInt32(ie.Substring(2, 1));
                        Int32 D4 = Convert.ToInt32(ie.Substring(3, 1));
                        Int32 D5 = Convert.ToInt32(ie.Substring(4, 1));
                        Int32 D6 = Convert.ToInt32(ie.Substring(5, 1));
                        Int32 D7 = Convert.ToInt32(ie.Substring(6, 1));
                        Int32 D8 = Convert.ToInt32(ie.Substring(7, 1));
                        Int32 D9 = Convert.ToInt32(ie.Substring(8, 1));
                        Int32 D10 = Convert.ToInt32(ie.Substring(9, 1));
                        Int32 X1 = (D1 * 9) + (D2 * 8) + (D5 * 7) + (D6 * 6) + (D7 * 5) + (D8 * 4) + (D9 * 3) + (D10 * 2);
                        Int32 X2 = X1 % 11;
                        Int32 X3 = 11 - X2;
                        if (X2 <= 2)
                        {
                            X3 = 0;
                        }
                        retorno = digitoVerificarOriginal == Convert.ToString(X3);
                    }
                    break;
                #endregion
            }
            #endregion           
            return retorno;
        }

        public static String formataIE(String dsEstado, String vlIE)
        {
            vlIE = Valida.removeCaracteres(vlIE).Replace(" ", "");
            if (dsEstado.Equals("AC"))
            {
                vlIE = Valida.formatarString("##.###.###/###-##", vlIE);
            }
            if (dsEstado.Equals("AL") || dsEstado.Equals("AP") || dsEstado.Equals("CE") ||
                dsEstado.Equals("ES") || dsEstado.Equals("MA") || dsEstado.Equals("MS") ||
                dsEstado.Equals("PB") || dsEstado.Equals("PE") || dsEstado.Equals("PI") ||
                dsEstado.Equals("RR") || dsEstado.Equals("SE"))
            {
                vlIE = Valida.formatarString("########-#", vlIE);
            }
            if (dsEstado.Equals("AM"))
            {
                vlIE = Valida.formatarString("##.###.###-#", vlIE);
            }
            if (dsEstado.Equals("BA"))
            {
                if (vlIE.Length == 8)
                {
                    vlIE = Valida.formatarString("######-##", vlIE);
                }
                else
                {
                    vlIE = Valida.formatarString("#######-##", vlIE);
                }
            }
            if (dsEstado.Equals("DF"))
            {
                vlIE = Valida.formatarString("###########-##", vlIE);
            }
            if (dsEstado.Equals("GO"))
            {
                vlIE = Valida.formatarString("##.###.###-#", vlIE);
            }
            if (dsEstado.Equals("MT"))
            {
                vlIE = Valida.formatarString("##########-#", vlIE);
            }
            if (dsEstado.Equals("MG"))
            {
                vlIE = Valida.formatarString("###.###.###/####", vlIE);
            }
            if (dsEstado.Equals("PA"))
            {
                vlIE = Valida.formatarString("##-######-#", vlIE);
            }
            if (dsEstado.Equals("PR"))
            {
                vlIE = Valida.formatarString("########-##", vlIE);
            }
            if (dsEstado.Equals("RJ"))
            {
                vlIE = Valida.formatarString("##.###.##-#", vlIE);
            }
            if (dsEstado.Equals("RN"))
            {
                if (vlIE.Length == 9)
                {
                    vlIE = Valida.formatarString("##.###.###-#", vlIE);
                }
                else
                {
                    vlIE = Valida.formatarString("##.#.###.###-#", vlIE);
                }
            }
            if (dsEstado.Equals("RS"))
            {
                vlIE = Valida.formatarString("###/#######", vlIE);
            }
            if (dsEstado.Equals("RO"))
            {
                vlIE = Valida.formatarString("#############-#", vlIE);
            }
            if (dsEstado.Equals("SC"))
            {
                vlIE = Valida.formatarString("###.###.###", vlIE);
            }
            if (dsEstado.Equals("SP"))
            {
                String vPosicao = vlIE.Substring(0, 1);
                if (!vPosicao.Equals("P"))
                {
                    vlIE = Valida.formatarString("###.###.###.###", vlIE);
                }
                else
                {
                    vlIE = Valida.formatarString("#-########.#/###", vlIE);
                }
            }
            if (dsEstado.Equals("TO"))
            {
                vlIE = Valida.formatarString("##########-#", vlIE);
            }
            return vlIE.ToUpper();
        }

        public static Boolean verificaObrigatorios(object[] camposObrigatorios)
        {
            Boolean prossegue = true;
            for (int i = 0; i < camposObrigatorios.Length; i++)
            {
                if (camposObrigatorios[i] is DevExpress.XtraEditors.TextEdit)
                {
                    if (((DevExpress.XtraEditors.TextEdit)camposObrigatorios[i]).Text.Trim().Equals(string.Empty))
                    {
                        prossegue = false;
                    }
                }
                if (camposObrigatorios[i] is DevExpress.XtraEditors.MemoEdit)
                {
                    if (((DevExpress.XtraEditors.MemoEdit)camposObrigatorios[i]).Text.Trim().Equals(string.Empty))
                    {
                        prossegue = false;
                    }
                }
                else if (camposObrigatorios[i] is DevExpress.XtraEditors.ComboBoxEdit)
                {
                    if (((DevExpress.XtraEditors.ComboBoxEdit)camposObrigatorios[i]).SelectedIndex == -1)
                    {
                        prossegue = false;
                    }
                }
                else if (camposObrigatorios[i] is DevExpress.XtraEditors.DateEdit)
                {
                    if (((DevExpress.XtraEditors.DateEdit)camposObrigatorios[i]).DateTime == null)
                    {
                        prossegue = false;
                    }
                }
                else if (camposObrigatorios[i] is DevExpress.XtraEditors.CalcEdit)
                {
                    if (((DevExpress.XtraEditors.CalcEdit)camposObrigatorios[i]).Text.Trim().Equals(string.Empty) || ((DevExpress.XtraEditors.CalcEdit)camposObrigatorios[i]).Value == 0)
                    {
                        prossegue = false;
                    }
                }
                else if (camposObrigatorios[i] is TextBox)
                {
                    if (((TextBox)camposObrigatorios[i]).Text.Trim().Equals(string.Empty))
                    {
                        prossegue = false;
                    }
                }
                else if (camposObrigatorios[i] is RichTextBox)
                {
                    if (((RichTextBox)camposObrigatorios[i]).Text.Trim().Equals(string.Empty))
                    {
                        prossegue = false;
                    }
                }
                else if (camposObrigatorios[i] is ComboBox)
                {
                    if (((ComboBox)camposObrigatorios[i]).SelectedIndex == -1)
                    {
                        prossegue = false;
                    }
                }
                else if (camposObrigatorios[i] is PictureBox)
                {
                    if (((PictureBox)camposObrigatorios[i]) == null)
                    {
                        prossegue = false;
                    }
                }
                else if (camposObrigatorios[i] is DevExpress.XtraEditors.PictureEdit)
                {
                    if (((DevExpress.XtraEditors.PictureEdit)camposObrigatorios[i]).Image == null)
                    {
                        prossegue = false;
                    }
                }
                else if (camposObrigatorios[i] is DateTimePicker)
                {
                    if (((DateTimePicker)camposObrigatorios[i]) == null)
                    {
                        prossegue = false;
                    }
                }
                else if (camposObrigatorios[i] is DevExpress.XtraRichEdit.RichEditControl)
                {
                    if (((DevExpress.XtraRichEdit.RichEditControl)camposObrigatorios[i]).Text == "")
                    {
                        prossegue = false;
                    }
                }
                if (!prossegue)
                {
                    Alert.atencao("É necessário informar " + ((Control)camposObrigatorios[i]).AccessibleName);
                    ((Control)camposObrigatorios[i]).Focus();
                    break;
                }
            }
            return prossegue;
        }

        public static String consultaF9(Int32 cdConsulta, String vWhere, String getTable)
        {
            /* 1 = Valida.consultaF9(2, 3, 4);
             * 1 - Text Box que irá receber o retorno da consulta
             * 2 - Número do lista desejada
             * 3 - Opção de where. Só irá funcionar se existir a opção :pDsWhere na consulta sql na tabela listavalores
             * 4 - Coluna da tabela que será retornado a informação desejada             
             */
            String result = "";
            ArrayList listaRetorno = Lista.ListaValores(cdConsulta, vWhere);
            for (int i = 0; i < listaRetorno.Count; i++)
            {
                DataRow row = listaRetorno[i] as DataRow;
                if (listaRetorno.Count > 0)
                {
                    result = Convert.ToString(row[getTable]);
                }
            }
            return result;
        }

        public static String getValidatedOLD(String sql, String text, String msg)
        {
            String nome = null;
            if (!text.Equals(string.Empty))
            {
                String vSQL = String.Format(sql, text);
                Object[] result = Utilidades.consultar(vSQL);
                if (result != null)
                {
                    nome = Convert.ToString(result[0]);
                }
                else
                {
                    Alert.atencao(msg);
                }
            }
            return nome;
        }

        public static String getValidated(String sql, String msg, Boolean cadastro)
        {
            String nome = null;
            String vSQL = sql;
            Object[] result = Utilidades.consultar(vSQL);
            if (result != null)
            {
                nome = Convert.ToString(result[0]);
            }
            else
            {
                if (cadastro != true)
                {
                    Alert.atencao(msg);
                }
            }
            return nome;
        }

        public static String ZerosEsquerda(string strString, int intTamanho)
        {
            string strResult = "";
            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {
                strResult += "0";
            }
            return strResult + strString;
        }

        public static String somenteNumeros(String itexto)
        {
            if (!itexto.Equals(string.Empty))
            {
                var normalizedString = itexto.Normalize(NormalizationForm.FormD);
                var stringBuilder = new StringBuilder();
                foreach (var c in normalizedString)
                {
                    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    {
                        stringBuilder.Append(c);
                    }
                }
                itexto = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
                return Regex.Replace(itexto, @"[^\d]", string.Empty);
            }
            else
            {
                return ".";
            }
        }

        public static String formatarCpfCnpj(string strCpfCnpj)
        {
            strCpfCnpj = strCpfCnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (strCpfCnpj.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(ZerosEsquerda(strCpfCnpj, 11));
                return mtpCnpj.ToString();
            }
        }

        public static String setCamelCase(string value)
        {
            //https://www.c-sharpcorner.com/blogs/first-letter-in-uppercase-in-c-sharp1
            value = value.ToLower();
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.  
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.  
            // ... Uppercase the lowercase letters following spaces.  
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public static String removeCaracteres(String dsTexto)
        {
            String tiraEspaco = dsTexto
                        .Replace(".", "").Replace("-", "")
                        .Replace(",", "").Replace("*", "")
                        .Replace("+", "").Replace("´", "")
                        .Replace("{", "").Replace("}", "")
                        .Replace(":", "").Replace(";", "")
                        .Replace("(", "").Replace(")", "")
                        .Replace("'", "").Replace("%", "")
                        .Replace("\\", "").Replace("/", "")
                        .Replace("Ä", "A").Replace("ä", "a")
                        .Replace("Å", "A").Replace("å", "a")
                        .Replace("Ë", "E").Replace("ë", "e")
                        .Replace("Ï", "I").Replace("ï", "i")
                        .Replace("Ö", "O").Replace("ö", "o")
                        .Replace("Ü", "U").Replace("ü", "u")
                        .Replace("Ý", "Y").Replace("ý", "y")
                        .Replace("─", "").Replace("━", "")
                        .Replace("˴", "").Replace("\r\n", "")
                        .Replace("\\t", "").Replace("\\n", "").Replace("\"", "")
                        .TrimStart().TrimEnd().ToUpper();
            return tiraEspaco;
        }

        public static String removeAcentos(String text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return removeCaracteres(sbReturn.ToString());
        }

        public static String retornaDataExtenso(String text)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            Int16 dia = Convert.ToInt16(text.Substring(0, 2));
            Int16 mesAtual = Convert.ToInt16(text.Substring(3, 2));
            Int16 ano = Convert.ToInt16(text.Substring(6, 4));
            String mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(mesAtual));
            String diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(Convert.ToDateTime(text).DayOfWeek));
            String data = String.Format("{0}, {1} de {2} de {3}", diasemana, dia, mes, ano);
            return data;
        }

        public static String formatarTelefone(String telefone)
        {
            //http://csharphelper.com/blog/2015/04/remove-non-digits-or-non-letters-from-a-string-in-c/
            telefone = somenteNumero(telefone);
            if (telefone.Length <= 10)
            {
                return Convert.ToUInt64(telefone).ToString(@"(00)0000-0000");
            }
            else
            {
                return Convert.ToUInt64(telefone).ToString(@"(00)00000-0000");
            }
        }

        public static String limpaConteudo(String itexto)
        {
            if (!itexto.Equals(string.Empty))
            {
                var normalizedString = itexto.Normalize(NormalizationForm.FormD);
                var stringBuilder = new StringBuilder();
                foreach (var c in normalizedString)
                {
                    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    {
                        stringBuilder.Append(c);
                    }
                }
                itexto = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
                return removeCaracteres(Regex.Replace(itexto, @"[^\u0000-\u007F]", string.Empty));
            }
            else
            {
                return ".";
            }
        }

        public static String formatarCEP(String cep)
        {
            cep = somenteNumero(cep);
            try
            {
                return Convert.ToUInt64(cep).ToString(@"00\.000\-000");
            }
            catch
            {
                return "";
            }
        }

        public static String formatarString(String mascara, String valor, Char caractere = '#')
        {
            //http://www.linhadecodigo.com.br/artigo/2910/formatando-string-rapidamente.aspx
            string novoValor = string.Empty;
            int posicao = 0;
            for (int i = 0; mascara.Length > i; i++)
            {
                if (mascara[i] == caractere)
                {
                    if (valor.Length > posicao)
                    {
                        novoValor = novoValor + valor[posicao];
                        posicao++;
                    }
                    else
                    {
                        break;
                    }                        
                }
                else
                {
                    if (valor.Length > posicao)
                    {
                        novoValor = novoValor + mascara[i];
                    }                        
                    else
                    {
                        break;
                    }
                }
            }
            return novoValor;
        }

        public static String somenteNumero(String text)
        {
            return Regex.Replace(text, @"[^0-9]", "");
        }

        public static String somenteLetra(String text)
        {
            return Regex.Replace(text, "[^a-zA-Z]", "");
        }

        public static String verificaConexao(String Servidor, String Banco, String Usuario, String Senha, String Porta, String Msg)
        {
            String retorno = "";
            try
            {
                NpgsqlConnection compare = new NpgsqlConnection() { ConnectionString = String.Format("Server= {0};Port={1};User Id={2};Password={3};Database={4};CommandTimeout=1800000;Timeout=1024;", Servidor, Porta, Usuario, Senha, Banco) };
                compare.Open();
                compare.Close();
                retorno = "OK";
            }
            catch (Exception erro)
            {
                retorno = String.Format("Erro ao conectar no banco: {0}\n{1}", Msg, erro.Message);
            }
            return retorno;
        }

        public static String formataValorMonetario(String valor)
        {
            String result = "";
            try
            {
                double media = Convert.ToDouble(valor);
                result = media.ToString("C");
            }
            catch (Exception erro)
            {
                result = "erro ao converter o valor " + erro.Message;
            }
            return result;
        }

        public static Boolean verificaPermissao(String cdModulo, Int32 cdPrograma, String cdUsuario)
        {
            Boolean permiteAcesso = false;
            if (cdModulo != "" && cdUsuario != "")
            {
                String vSql = String.Format("select 1 from acessoprograma where cd_modulo = '{0}' and cd_programa = {1} and cd_usuario = '{2}'", cdModulo, cdPrograma, cdUsuario);
                Object[] permissao = Utilidades.consultar(vSql);
                if (permissao != null)
                {
                    permiteAcesso = true;
                }   
            }
            return permiteAcesso;
        }
        public static string retornaEstado(String cdMunicipio)
        {
            String sgEstado = "";
            String vsql = String.Format("select sg_estado from estado where cd_estado = (select cd_estado from municipio where cd_municipio = {0});", Convert.ToInt32(cdMunicipio));
            Object[] result = Utilidades.consultar(vsql);
            if (result != null)
            {
                sgEstado = Convert.ToString(result[0]);
            }
            return sgEstado;
        }
    }
}
