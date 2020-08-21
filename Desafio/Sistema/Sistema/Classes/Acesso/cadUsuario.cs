using Sistema.Classes.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Classes.Acesso
{
    public class cadUsuario
    {
        public String cd_usuario { get; set; }
        public String nm_usuario { get; set; }
        public String sn_usuario { get; set; }
        public String ds_email { get; set; }
        public String ds_senha { get; set; }
        public String ds_smtp { get; set; }
        public Int16 nr_porta { get; set; }
        public Boolean st_ativo { get; set; }
        public Boolean st_ssl { get; set; }
        public DateTime dt_registro { get; set; }

        public static String retornaDescricaoUsuario(String cdUsuario)
        {
            String vSql = String.Format("select nm_usuario from public.usuario where cd_usuario = '{0}';", cdUsuario);
            String retorno = null;
            Object[] result = Utilidades.consultar(vSql);
            if (result != null)
            {
                retorno = Convert.ToString(result[0]);
            }
            return retorno;
        }
    }

    public class cadUsuarioDAO
    {
        public static bool existe(String cdUsuario)
        {
            if (!cdUsuario.Equals(""))
            {
                Object[] result = Utilidades.consultar(String.Format("select 1 from usuario where cd_usuario = '{0}';", cdUsuario));
                if (result != null)
                {
                    return result.Length > 0;
                }
            }
            return false;
        }

        public static string persist(cadUsuario cdUsuario)
        {
            String vret = "";
            String vsql = " insert into public.usuario(" +
                          "        cd_usuario, nm_usuario, sn_usuario, ds_email, ds_senha, ds_smtp, nr_porta, st_ativo, st_ssl, dt_registro)" +
                          " values (@cdusuario, @nmusuario, @snusuario, @dsemail, @dssenha, @dssmtp, @nrporta, @stativo, @stssl, current_timestamp);";

            try
            {
                if (cadUsuarioDAO.existe(cdUsuario.cd_usuario))
                {
                    vsql = "update usuario " +
                           "   set cd_usuario=@cdusuario, nm_usuario=@nmusuario, sn_usuario=@snusuario, ds_email=@dsemail, ds_senha=@dssenha, " +
                           "       ds_smtp=@dssmtp, nr_porta=@nrporta, st_ativo=@stativo, st_ssl=@stssl " +
                           " where cd_usuario = @cdusuario;";
                }

                Conexao.getInstance().startTransaction();
                List<ParametroPGSQL> plist = new List<ParametroPGSQL>();
                plist.Add(new ParametroPGSQL("cdusuario", cdUsuario.cd_usuario, DbType.String));
                plist.Add(new ParametroPGSQL("nmusuario", cdUsuario.nm_usuario, DbType.String));
                plist.Add(new ParametroPGSQL("snusuario", cdUsuario.sn_usuario, DbType.String));
                plist.Add(new ParametroPGSQL("dsemail", cdUsuario.ds_email, DbType.String));
                plist.Add(new ParametroPGSQL("dssenha", cdUsuario.ds_senha, DbType.String));
                plist.Add(new ParametroPGSQL("dssmtp", cdUsuario.ds_smtp, DbType.String));
                plist.Add(new ParametroPGSQL("nrporta", cdUsuario.nr_porta, DbType.Int32));
                plist.Add(new ParametroPGSQL("stativo", cdUsuario.st_ativo, DbType.Boolean));
                plist.Add(new ParametroPGSQL("stssl", cdUsuario.st_ssl, DbType.Boolean));

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
                Alert.erro(String.Format("Erro ao Persistir no banco de dados! \n {0} \n {1}", erro.Message, vret));
            }
            return vret;
        }

        public static cadUsuario getUsuario(String cdUsuario)
        {
            cadUsuario usuario = new cadUsuario();
            
            String vSql = String.Format("select cd_usuario, nm_usuario, sn_usuario, ds_email, ds_senha, ds_smtp, "+
                                        " nr_porta, st_ativo, st_ssl, dt_registro from usuario where cd_usuario = '{0}'", cdUsuario);
            Object[] result = Utilidades.consultar(vSql);
            if (result != null)
            {
                usuario.cd_usuario = Convert.ToString(result[0]);
                usuario.nm_usuario = Convert.ToString(result[1]);
                usuario.sn_usuario = Convert.ToString(result[2]);
                usuario.ds_email = Convert.ToString(result[3]);
                usuario.ds_senha = Convert.ToString(result[4]);
                usuario.ds_smtp = Convert.ToString(result[5]);
                usuario.nr_porta = Convert.ToInt16(result[6]);
                usuario.st_ativo = Convert.ToBoolean(result[7]);
                usuario.st_ssl = Convert.ToBoolean(result[8]);
                usuario.dt_registro = Convert.ToDateTime(result[9]);
            }           
            return usuario;
        }
    }
}
