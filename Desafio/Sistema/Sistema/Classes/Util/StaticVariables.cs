using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sistema.Classes.Util
{
    class StaticVariables
    {
        public static List<Config> Configs;
        public static NpgsqlConnection dbase;        
        public static String nmConexao;
        public static String ServerName;
        public static String RemoteServerName;
        public static String dbName;
        public static String nmUser;
        public static String snUser;
        public static int nrPorta;
        public static String nmSkin;
        public static String cdUsuario;
        public static Boolean imagemFundo;
        public static string keypass = "texto usado para a criptografia";

        public static void ConexaoDB()
        {
            if (dbase == null)
            {
                dbase = new NpgsqlConnection();
            }
            bool reconecta = false;
            if (dbase.State == ConnectionState.Broken)
            {
                reconecta = true;
            }
            if (dbase.State == ConnectionState.Closed)
            {
                reconecta = true;
            }
            if (reconecta)
            {
                try
                {
                    if (dbase == null)
                    {
                        dbase = new NpgsqlConnection();
                    }

                    if ((dbName != null) && (dbase.State == ConnectionState.Closed))
                    {
                        dbase.ConnectionString =
                            "Server= " + ServerName +
                            ";Port=" + StaticVariables.nrPorta +
                            ";User Id=" + StaticVariables.nmUser +
                            "; Password=" + StaticVariables.snUser +
                            ";Database=" + dbName + ";CommandTimeout=1800000;Timeout=1024;Preload Reader=true;";

                        if (!StaticVariables.RemoteServerName.Equals(""))
                        {
                            try
                            {
                                dbase.Open();
                            }
                            catch
                            {
                                dbase.ConnectionString =
                                    "Server= " + StaticVariables.RemoteServerName +
                                    ";Port=" + StaticVariables.nrPorta +
                                    ";User Id=" + StaticVariables.nmUser +
                                    "; Password=" + StaticVariables.snUser +
                                    "; Database=" + dbName + ";CommandTimeout=1800000;Timeout=1024;Preload Reader=true;";
                                try
                                {
                                    dbase.Open();
                                }
                                catch (Exception erro)
                                {
                                    Alert.erro("Erro ao conectar ao banco de dados: " + erro.Message);
                                    Application.Exit();
                                }
                            }
                        }
                        else
                        {
                            dbase.Open();
                        }
                    }
                }
                catch (Exception erro)
                {
                    Alert.erro("Erro ao conectar ao banco de dados: " + erro.Message);
                    Application.Exit();
                }
            }
        }

        public static String getConnectionString()
        {
            String dsConnectionString = "";
            if (!nmUser.Equals("") && !snUser.Equals("") && !dbName.Equals("") && !ServerName.Equals("") && nrPorta > 0)
            {
                dsConnectionString =
                    "Server= " + ServerName +
                    ";Port=" + nrPorta +
                    ";User Id=" + nmUser +
                    "; Password=" + snUser +
                    ";Database=" + dbName + ";CommandTimeout=1800000;Timeout=1024;";
            }
            return dsConnectionString;
        }

        public static NpgsqlConnection getDBInstance()
        {
            NpgsqlConnection bdados = dbase;
            if (bdados == null)
            {
                ConexaoDB();
                bdados = dbase;
            }
            if (bdados != null)
            {
                if (bdados.State == ConnectionState.Broken || bdados.State == ConnectionState.Closed)
                {
                    ConexaoDB();
                    bdados = dbase;
                }
            }
            return bdados;
        }
    }
}

