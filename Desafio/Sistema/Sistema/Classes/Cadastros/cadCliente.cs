using Sistema.Classes.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Classes.Cadastros
{
    class cadCliente
    {
        public Int32 cd_cliente { get; set; }
        public String ds_nome { get; set; }
        public String ds_email { get; set; }
        public String nr_telefone { get; set; }
        public String nr_celular { get; set; }
        public String nr_celular2 { get; set; }        
    }

    class cadClienteDAO
    {
        public static bool existe(int cdClifor)
        {
            if (!cdClifor.Equals(String.Empty))
            {
                Object[] result = Utilidades.consultar(String.Format("select 1 from cliente where cd_cliente = {0}", cdClifor));
                if (result != null)
                {
                    return result.Length > 0;
                }
            }
            return false;
        }

        public static String inserir(cadCliente cliente)
        {
            String vRet = "";
            String vSql = "insert into public.cliente(cd_cliente, ds_nome, dt_registro, nr_telefone, nr_celular, nr_celular2, ds_email)" +
                          "    values (@cd_cliente, @ds_nome, current_timestamp, @nr_telefone, @nr_celular, @nr_celular2, @ds_email);";
            try
            {
                if (cadClienteDAO.existe(cliente.cd_cliente))
                {
                    vSql = " update public.cliente set ds_nome = @ds_nome, nr_telefone = @nr_telefone," +
                           "  nr_celular = @nr_celular, nr_celular2 = @nr_celular2, ds_email = @ds_email where cd_cliente = @cd_cliente;";
                }
                
                List<ParametroPGSQL> plist = new List<ParametroPGSQL>();

                plist.Add(new ParametroPGSQL("cd_cliente", cliente.cd_cliente, DbType.Int32));
                plist.Add(new ParametroPGSQL("ds_nome", cliente.ds_nome, DbType.String));
                plist.Add(new ParametroPGSQL("nr_telefone", cliente.nr_telefone, DbType.String));
                plist.Add(new ParametroPGSQL("nr_celular", cliente.nr_celular, DbType.String));
                plist.Add(new ParametroPGSQL("nr_celular2", cliente.nr_celular2, DbType.String));
                plist.Add(new ParametroPGSQL("ds_email", cliente.ds_email, DbType.String));
                vRet = Conexao.getInstance().gravar(vSql, plist);
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao Persistir no banco de dados! \n {0} \n {1}", erro.Message, vRet));
            }
            return vRet;
        }
    }
}
