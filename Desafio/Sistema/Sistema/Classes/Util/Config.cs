using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace Sistema.Classes.Util
{
    public class Config
    {
        public string nmBase { get; set; }
        public bool stDefault { get; set; }
        public string EnderecoDB { get; set; }
        public string dbName { get; set; }
        public string nmUser { get; set; }
        public string snUser { get; set; }
        public string tpUser { get; set; }
        public Int64 nrPorta { get; set; }
        public string localSistema { get; set; }
        public string nmSkin { get; set; }
        public string imagemFundo { get; set; }
        public Config()
        {
            nmBase = "";
            stDefault = false;
            EnderecoDB = "";
            dbName = "";
            nmUser = "";
            snUser = "";
            tpUser = "";
            nrPorta = 0;
            nmSkin = "";
            imagemFundo = "";
        }
        public Config(string inmBase, bool istDefault, string iEnderecoDB, string idbName, string inmUser, string isnUser, string itpUser,
                      Int32 inrPorta, string inmSkin, string ilocalImgFundo)
        {
            stDefault = istDefault;
            nmBase = inmBase;
            EnderecoDB = iEnderecoDB;
            dbName = idbName;
            nmUser = inmUser;
            snUser = isnUser;
            tpUser = itpUser;
            nrPorta = inrPorta;
            nmSkin = inmSkin;
            imagemFundo = ilocalImgFundo;
        }
    }
    public class ArquivoXml
    {
        internal static void loadXml()
        {
            try
            {
                bool terminar = false;
                if (File.Exists("./config.xml"))
                {
                    #region ' Carrega os dados do Arquivo de Configuração '
                    XmlDocument readerFile = new XmlDocument();
                    FileStream fs = new FileStream("./config.xml", FileMode.Open, FileAccess.Read);
                    readerFile.Load(fs);
                    List<Config> Configs = new List<Config>();
                    XmlNodeList conNodes = readerFile.GetElementsByTagName("connections");
                    foreach (XmlNode node in conNodes)
                    {
                        Config configuracao = new Config();
                        for (int i = 0; i < node.ChildNodes.Count; i++)
                        {
                            if (node.ChildNodes[i].Name == "server")
                            {
                                configuracao.EnderecoDB = node.ChildNodes[i].InnerText;
                            }
                            if (node.ChildNodes[i].Name == "dbName")
                            {
                                configuracao.dbName = node.ChildNodes[i].InnerText;
                            }
                            if (node.ChildNodes[i].Name == "nmUsuario")
                            {
                                configuracao.nmUser = Utilidades.Decryption(node.ChildNodes[i].InnerText, StaticVariables.keypass);
                            }
                            if (node.ChildNodes[i].Name == "snUsuario")
                            {
                                configuracao.snUser = Utilidades.Decryption(node.ChildNodes[i].InnerText, StaticVariables.keypass);
                            }
                            if (node.ChildNodes[i].Name == "tpUser")
                            {
                                configuracao.tpUser = node.ChildNodes[i].InnerText;
                            }
                            if (node.ChildNodes[i].Name == "nrPorta")
                            {
                                configuracao.nrPorta = Convert.ToInt32(node.ChildNodes[i].InnerText);
                            }
                            if (node.ChildNodes[i].Name == "nmSkin")
                            {
                                configuracao.nmSkin = node.ChildNodes[i].InnerText;
                            }
                            if (node.ChildNodes[i].Name == "imagemFundo")
                            {
                                configuracao.imagemFundo = node.ChildNodes[i].InnerText;
                            }
                        }
                        Configs.Add(configuracao);
                        configuracao = null;
                    }
                    #endregion
                    fs.Dispose();

                    StaticVariables.Configs = Configs;

                    if (StaticVariables.Configs != null)
                    {
                        if (StaticVariables.Configs.Count > 1)
                        {
                            if (terminar)
                            {
                                Process.GetCurrentProcess().Kill();
                            }
                        }
                        else
                        {
                            if (StaticVariables.Configs.Count != 0)
                            {
                                StaticVariables.nmConexao = StaticVariables.Configs[0].nmBase;
                                StaticVariables.ServerName = StaticVariables.Configs[0].EnderecoDB;
                                StaticVariables.dbName = StaticVariables.Configs[0].dbName;
                                StaticVariables.RemoteServerName = StaticVariables.Configs[0].dbName;
                                StaticVariables.nmUser = StaticVariables.Configs[0].nmUser;
                                StaticVariables.snUser = StaticVariables.Configs[0].snUser;
                                StaticVariables.nrPorta = Convert.ToInt32(StaticVariables.Configs[0].nrPorta);
                                StaticVariables.nmSkin = StaticVariables.Configs[0].nmSkin;
                                StaticVariables.imagemFundo = Convert.ToBoolean(StaticVariables.Configs[0].imagemFundo);
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                Alert.erro("(Erro ao Carregar dados do Arquivo Xml.) :" + erro.Message);
            }
        }
    }
}