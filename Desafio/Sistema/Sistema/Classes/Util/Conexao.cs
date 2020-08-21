using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sistema.Classes.Util
{
    class Conexao
    {
        public static Conexao instance;

        public static Conexao getInstance()
        {
            if (instance == null)
                instance = new Conexao();
            return instance;
        }

        private NpgsqlConnection BancoDados { get; set; }

        private NpgsqlTransaction transacao { get; set; }

        public Conexao()
        {
            Conectar();
        }

        public NpgsqlConnection getConnection()
        {
            return BancoDados;
        }

        public NpgsqlTransaction getTransaction()
        {
            return transacao;
        }

        public void startTransaction()
        {
            if (!isTransactionActive(getTransaction()))
            {
                transacao = getConnection().BeginTransaction();
            }
        }

        public void commit()
        {
            if (getTransaction() != null)
            {
                getTransaction().Commit();
                disposeTransaction();
            }
        }

        public void rollback()
        {
            getTransaction().Rollback();
            disposeTransaction();
        }

        public void disposeTransaction()
        {
            transacao.Dispose();
            transacao = null;
        }

        public void Conectar()
        {
            try
            {
                if (BancoDados == null)
                {
                    BancoDados = new NpgsqlConnection();
                }

                if ((StaticVariables.dbName != null) && (BancoDados.State == ConnectionState.Closed) || (BancoDados.State == ConnectionState.Broken))
                {
                    BancoDados.ConnectionString =
                        String.Format("Server= {0};Port={1};User Id={2};Password={3};Database={4};CommandTimeout=1800000;Pooling=false;Timeout=1024;",
                        StaticVariables.ServerName, StaticVariables.nrPorta, StaticVariables.nmUser, StaticVariables.snUser, StaticVariables.dbName);

                    if (!StaticVariables.RemoteServerName.Equals(""))
                    {
                        try
                        {
                            BancoDados.Open();
                        }
                        catch (Exception erro)
                        {
                            Alert.erro("Conexao: " + erro.Message);
                            Application.Exit();
                        }
                    }
                    else
                    {
                        try
                        {
                            BancoDados.Open();
                        }
                        catch (Exception erro)
                        {
                            Alert.erro("Erro ao Conectar ao Banco de dados: " + erro.Message);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao conectar ao banco de dados: " + erro.Message);
                Application.Exit();
            }
        }

        internal static bool isTransactionActive(NpgsqlTransaction transacao)
        {
            if (transacao != null)
                if (transacao.Connection != null)
                    return true;
            return false;
        }

        public List<List<Object>> toList(String isql)
        {
            if (!isTransactionActive(getTransaction()))
                startTransaction();
            List<List<Object>> retorno = null;
            try
            {
                NpgsqlDataReader ireader = new NpgsqlCommand(isql, Conexao.getInstance().getConnection(), getTransaction()).ExecuteReader();

                if (ireader.HasRows)
                {
                    retorno = new List<List<object>>();
                    while (ireader.Read())
                    {
                        List<Object> colunas = new List<object>();
                        for (int i = 0; i < ireader.FieldCount; i++)
                            colunas.Add(!ireader.IsDBNull(i) ? ireader.GetValue(i) : null);
                        retorno.Add(colunas);
                    }
                }
                ireader.Close();
                ireader.Dispose();
            }
            catch (Exception erro)
            {
                string msg = erro.Message;
            }
            return retorno;
        }
        /** retorna um vetor onde cada elemento é uma coluna da SQL
        *  portanto, a SQL deve retornar apenas uma linha */
        public static List<Object[]> Listar(String sql)
        {
            List<Object[]> ObjLista = new List<object[]>();
            try
            {
                Object[] retorno = null;
                List<List<Object>> dados = Conexao.getInstance().toList(sql);
                if (dados != null)
                {
                    foreach (List<Object> linha in dados)
                    {
                        retorno = new Object[linha.Count];
                        for (int i = 0; i < linha.Count; i++)
                        {
                            retorno[i] = linha.ElementAt(i) != null ? linha.ElementAt(i) : null;
                        }
                        ObjLista.Add(retorno);
                    }
                }
            }
            catch { }
            return ObjLista;
        }
        public List<List<Object>> toList(String isql, List<ParametroPGSQL> parametros)
        {
            List<List<Object>> retorno = null;
            try
            {
                NpgsqlCommand comando = getConnection().CreateCommand();
                comando.CommandText = isql;
                foreach (ParametroPGSQL p in parametros)
                {
                    comando.Parameters.Add(new NpgsqlParameter(p.nome, p.valor));
                }

                NpgsqlDataReader ireader = comando.ExecuteReader();
                if (ireader.HasRows)
                {
                    retorno = new List<List<object>>();
                    while (ireader.Read())
                    {
                        List<Object> colunas = new List<object>();
                        for (int i = 0; i < ireader.FieldCount; i++)
                            colunas.Add(!ireader.IsDBNull(i) ? ireader.GetValue(i) : null);
                        retorno.Add(colunas);
                    }
                }
                ireader.Close();
                ireader.Dispose();
            }
            catch (Exception erro)
            {
                Alert.erro("(Erro-Listar) :" + erro.Message);
            }
            return retorno;
        }

        /** retorna um vetor onde cada elemento é uma coluna da SQL
         *  portanto, a SQL deve retornar apenas uma linha */
        public static Object[] consultar(NpgsqlConnection conn, String sql)
        {
            Object[] retorno = null;
            try
            {
                NpgsqlDataReader comm = new NpgsqlCommand(sql, conn).ExecuteReader();
                while (comm.Read())
                {
                    retorno = new Object[comm.FieldCount];
                    for (int i = 0; i < comm.FieldCount; i++)
                    {
                        retorno[i] = !comm.IsDBNull(i) ? comm.GetValue(i) : null;
                    }
                }
                comm.Close();
                comm.Dispose();
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("(Erro-Consultar) :(sql = {0}){1}", sql, erro.Message));
            }
            return retorno;
        }

        public static Object[] consultar(String sql)
        {
            return consultar(Conexao.getInstance().getConnection(), sql);
        }

        public string gravar(String isql, List<ParametroPGSQL> iparametros)
        {
            string retorno = "";
            try
            {
                NpgsqlCommand comando = new NpgsqlCommand(isql, getConnection(), getTransaction());
                comando.Parameters.Clear();
                if (iparametros != null)
                {
                    if (iparametros.Count > 0)
                    {
                        int i = 0;
                        foreach (ParametroPGSQL px in iparametros)
                        {
                            comando.Parameters.Add(new NpgsqlParameter(px.nome, px.tipo));
                            if (px.valor == null)
                            {
                                comando.Parameters[i].Value = DBNull.Value;
                            }
                            else
                            {
                                if (px.valor.Equals(String.Empty))
                                {
                                    comando.Parameters[i].Value = DBNull.Value;
                                }
                                else
                                {
                                    comando.Parameters[i].Value = px.valor;
                                }
                            }
                            i++;
                        }
                    }
                }
                comando.Prepare();
                comando.ExecuteNonQuery();
                comando.Dispose();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return retorno;
        }

        public static Int32 proximoItem(NpgsqlConnection ibase, String icampo, String itabela, String iwhere = "")
        {
            Int32 retorno = 1;

            NpgsqlDataReader comm = new NpgsqlCommand(String.Format(" select coalesce(max({0}),0) + 1 from {1} {2}", icampo, itabela, iwhere), ibase).ExecuteReader();
            while (comm.Read())
            {
                retorno = comm.GetInt32(0);
            }
            comm.Close();
            return retorno;
        }

        public List<List<Object>> toList(NpgsqlConnection ibase, String isql)
        {
            NpgsqlDataReader ireader = new NpgsqlCommand(isql, ibase).ExecuteReader();
            List<List<Object>> retorno = null;
            if (ireader.HasRows)
            {
                retorno = new List<List<object>>();
                while (ireader.Read())
                {
                    List<Object> colunas = new List<object>();
                    for (int i = 0; i < ireader.FieldCount; i++)
                        colunas.Add(!ireader.IsDBNull(i) ? ireader.GetValue(i) : null);
                    retorno.Add(colunas);
                }
            }
            ireader.Close();
            ireader.Dispose();
            return retorno;
        }
    }

    public class ParametroPGSQL
    {
        public string nome { get; set; }
        public Object valor { get; set; }
        public DbType tipo { get; set; }
        public ParametroPGSQL() { }
        public ParametroPGSQL(string inome, Object ivalor)
        {
            nome = inome;
            valor = ivalor;
        }
        public ParametroPGSQL(string inome, Object ivalor, DbType itipo)
        {
            nome = inome;
            valor = ivalor;
            tipo = itipo;
        }
    }
}
