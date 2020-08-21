using Sistema.Classes.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Sistema.Classes.Acesso
{
    class cadModulo
    {
        public String cd_usuario { get; set; }

        public String cd_modulo { get; set; }

        public String ds_modulo { get; set; }

        public Int32 cd_programa { get; set; }

        public String ds_programa { get; set; }

        public Int32 cd_submenu { get; set; }

        public String ds_submenu { get; set; }

    }
    
    class cadModuloDAO
    {
        public static String retornaDescricaoModulo(String cdModulo)
        {
            String vSql = String.Format("select ds_modulo from public.modulo where cd_modulo = '{0}';", cdModulo);
            String retorno = null;
            Object[] result = Utilidades.consultar(vSql);
            if (result != null)
            {
                retorno = Convert.ToString(result[0]);
            }
            return retorno;
        }

        public static bool existeModulo(String cdModulo)
        {
            if (!cdModulo.Equals(""))
            {
                Object[] result = Utilidades.consultar(String.Format("select 1 from modulo where cd_modulo = '{0}';", cdModulo));
                if (result != null)
                {
                    return result.Length > 0;
                }
            }
            return false;
        }

        public static string gravaModulo(cadModulo cadmodulo)
        {
            String vret = "", vsql = "";
            try
            {
                vsql = " insert into public.modulo(cd_modulo, ds_modulo, dt_registro)" +
                       "    values (@cdmodulo, @dsmodulo, current_timestamp);";

                if (cadModuloDAO.existeModulo(cadmodulo.cd_modulo))
                {
                    vsql = "update modulo set ds_modulo = @dsmodulo where cd_modulo = @cdmodulo;";
                }

                Conexao.getInstance().startTransaction();
                List<ParametroPGSQL> plist = new List<ParametroPGSQL>();
                plist.Add(new ParametroPGSQL("cdmodulo", cadmodulo.cd_modulo, DbType.String));
                plist.Add(new ParametroPGSQL("dsmodulo", cadmodulo.ds_modulo, DbType.String));                

                vret = Conexao.getInstance().gravar(vsql, plist);
                if (vret.Equals(string.Empty))
                {
                    Conexao.getInstance().commit();
                    Alert.informacao("Registro Salvo com Sucesso!");
                }
                else
                {
                    Conexao.getInstance().rollback();
                    Alert.erro("Erro ao Gravar Registro! \n" + vret);
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao inserir o registro {0}", erro.Message));
            }
            return vret;
        }

        public static string gravaAcessoRapidoUsuario(cadModulo cadmodulo)
        {
            String vret = "";
            try
            {
                String vsql = " insert into usuarioprogmenu(cd_usuario, cd_modulo, cd_programa, dt_registro) " +
                              " values (@cdusuario, @cdmodulo, @cdprograma, current_timestamp);";
                
                Conexao.getInstance().startTransaction();
                List<ParametroPGSQL> plist = new List<ParametroPGSQL>();
                plist.Add(new ParametroPGSQL("cdusuario", cadmodulo.cd_usuario, DbType.String));
                plist.Add(new ParametroPGSQL("cdmodulo", cadmodulo.cd_modulo, DbType.String));
                plist.Add(new ParametroPGSQL("cdprograma", cadmodulo.cd_programa, DbType.Int32));

                vret = Conexao.getInstance().gravar(vsql, plist);
                if (vret.Equals(string.Empty))
                {
                    Conexao.getInstance().commit();
                    Alert.informacao("Registro Salvo com Sucesso!");
                }
                else
                {
                    Conexao.getInstance().rollback();
                    Alert.erro("Erro ao Gravar Registro! \n" + vret);
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao inserir o registro {0}", erro.Message));
            }
            return vret;
        }
    }

    class cadSubMenuDAO
    {
        public static bool existeSubMenu(String cdModulo, int cdSubMenu)
        {
            if (!cdSubMenu.Equals(""))
            {
                Object[] result = Utilidades.consultar(String.Format("select 1 from submenu where cd_modulo = '{0}' and cd_submenu = '{1}';", cdModulo, cdSubMenu));
                if (result != null)
                {
                    return result.Length > 0;
                }
            }
            return false;
        }

        public static string gravaSubMenu(cadModulo cadmodulo)
        {
            String vret = "", vsql = "";
            try
            {
                vsql = " insert into public.submenu(cd_modulo, cd_submenu, ds_submenu, dt_registro)" +
                       "    values (@cdmodulo, @cdsubmenu, @dssubmenu, current_timestamp);";

                if (cadSubMenuDAO.existeSubMenu(cadmodulo.cd_modulo, cadmodulo.cd_submenu))
                {
                    vsql = "update public.submenu set ds_submenu=@dssubmenu where cd_modulo=@cdmodulo and cd_submenu=@cdsubmenu";
                }

                Conexao.getInstance().startTransaction();
                List<ParametroPGSQL> plist = new List<ParametroPGSQL>();
                plist.Add(new ParametroPGSQL("cdmodulo", cadmodulo.cd_modulo, DbType.String));
                plist.Add(new ParametroPGSQL("cdsubmenu", cadmodulo.cd_submenu, DbType.Int32));
                plist.Add(new ParametroPGSQL("dssubmenu", cadmodulo.ds_submenu, DbType.String));

                vret = Conexao.getInstance().gravar(vsql, plist);
                if (vret.Equals(string.Empty))
                {
                    Conexao.getInstance().commit();
                    Alert.informacao("Registro Salvo com Sucesso!");
                }
                else
                {
                    Conexao.getInstance().rollback();
                    Alert.erro("Erro ao Gravar Registro! \n" + vret);
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao inserir o registro {0}", erro.Message));
            }
            return vret;
        }
    }

    class cadProgramaDAO
    {
        public static String retornaDescricaoPrograma(String cdPrograma)
        {
            String vSql = String.Format("select ds_programa from public.programa where cd_programa = '{0}';", cdPrograma);
            String retorno = null;
            Object[] result = Utilidades.consultar(vSql);
            if (result != null)
            {
                retorno = Convert.ToString(result[0]);
            }
            return retorno;
        }        

        public static bool existePrograma(String cdModulo, int cdSubMenu)
        {
            if (!cdSubMenu.Equals(""))
            {
                Object[] result = Utilidades.consultar(String.Format("select 1 from programa where cd_modulo = '{0}' and cd_programa = '{1}';", cdModulo, cdSubMenu));
                if (result != null)
                {
                    return result.Length > 0;
                }
            }
            return false;
        }

        public static string gravaPrograma(cadModulo cadmodulo)
        {
            String vret = "", vsql = "";
            try
            {
                vsql = "insert into public.programa(cd_modulo, cd_programa, cd_submenu, ds_programa, dt_registro) " +
                       "    values (@cdmodulo, @cdprograma, @cdsubmenu, @dsprograma, current_timestamp);";

                if (cadProgramaDAO.existePrograma(cadmodulo.cd_modulo, cadmodulo.cd_programa))
                {
                    vsql = "update public.programa " +
                           " set cd_submenu = @cdsubmenu, ds_programa = @dsprograma " +
                           " where cd_modulo = @cdmodulo and cd_programa = @cdprograma ";
                }

                Conexao.getInstance().startTransaction();
                List<ParametroPGSQL> plist = new List<ParametroPGSQL>();
                plist.Add(new ParametroPGSQL("cdmodulo", cadmodulo.cd_modulo, DbType.String));
                plist.Add(new ParametroPGSQL("cdprograma", cadmodulo.cd_programa, DbType.Int32));
                plist.Add(new ParametroPGSQL("cdsubmenu", cadmodulo.cd_submenu, DbType.Int32));
                plist.Add(new ParametroPGSQL("dsprograma", cadmodulo.ds_programa, DbType.String));
                vret = Conexao.getInstance().gravar(vsql, plist);
                if (vret.Equals(string.Empty))
                {
                    Conexao.getInstance().commit();
                    Alert.informacao("Registro Salvo com Sucesso!");
                }
                else
                {
                    Conexao.getInstance().rollback();
                    Alert.erro("Erro ao Gravar Registro! \n" + vret);
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao inserir o registro {0}", erro.Message));
            }
            return vret;
        }
    }
}
