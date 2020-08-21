using Sistema.Classes.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Classes.Cadastros
{
    public class cadClienteContato
    {
        public Int32 cd_cliente { get; set; }
        public Int32 cd_contato { get; set; }
        public String ds_contato { get; set; }
        public String ds_email { get; set; }
        public String nr_telefone { get; set; }
        public String nr_celular { get; set; }
        public String nr_celular2 { get; set; }
    }

    public class cadClienteContatoDAO
    {
        public static bool existe(int cd_clifor, Int32 cd_contato)
        {
            if (cd_clifor > 0 && !cd_contato.Equals(""))
            {
                String vSql = String.Format("select 1 from clientecontato where cd_cliente = {0} and cd_contato = {1}", cd_clifor, cd_contato);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    return result.Length > 0;
                }
            }
            return false;
        }

        public static string inserir(cadClienteContato cliforContato)
        {
            String vret = "";
            String vsql = "insert into public.clientecontato(cd_cliente, cd_contato, ds_contato, ds_email, nr_telefone, nr_celular, nr_celular2, dt_registro)" +
                          "    values (@cd_cliente, @cd_contato, @ds_contato, @ds_email, @nr_telefone, @nr_celular, @nr_celular2, current_timestamp);";

            try
            {
                if (cadClienteContatoDAO.existe(cliforContato.cd_cliente, cliforContato.cd_contato))
                {
                    vsql = "update public.clientecontato " +
                           "   set ds_contato = @ds_contato, ds_email = @ds_email, nr_telefone = @nr_telefone, nr_celular = @nr_celular, nr_celular2 = @nr_celular2 " +
                           " where cd_cliente = @cd_cliente and cd_contato = @cd_contato;";
                }
                Conexao.getInstance().startTransaction();
                List<ParametroPGSQL> plist = new List<ParametroPGSQL>();
                plist.Add(new ParametroPGSQL("cd_cliente", cliforContato.cd_cliente, DbType.Int32));
                plist.Add(new ParametroPGSQL("cd_contato", cliforContato.cd_contato, DbType.Int32));
                plist.Add(new ParametroPGSQL("ds_contato", cliforContato.ds_contato, DbType.String));
                plist.Add(new ParametroPGSQL("ds_email", cliforContato.ds_email, DbType.String));
                plist.Add(new ParametroPGSQL("nr_telefone", cliforContato.nr_telefone, DbType.String));
                plist.Add(new ParametroPGSQL("nr_celular", cliforContato.nr_celular, DbType.String));
                plist.Add(new ParametroPGSQL("nr_celular2", cliforContato.nr_celular, DbType.String));
                vret = Conexao.getInstance().gravar(vsql, plist);
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao inserir os dados do Contato!" + erro.Message);
            }
            return vret;
        }
    }
}
