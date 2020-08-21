using Sistema.Classes.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Sistema.Classes.Ferramentas
{
    class cadListaValores
    {
        //Tab Lista Valores 
        public Int32 nr_sequencial { get; set; }
        public String ds_titulo { get; set; }
        public Int32 vl_altura { get; set; }
        public Int32 vl_largura { get; set; }
        public String ds_sql { get; set; }

        //Tab Lista Valores Colunas

        public Int32 nr_coluna { get; set; }
        public Int32 nr_posicaocoluna { get; set; }
        public String ds_titulocoluna { get; set; }
        public String nm_camposqlcoluna { get; set; }
        public Int32 nr_larguracampocoluna { get; set; }
        public String ds_alinhamentocoluna { get; set; }
    }

    class cadListaValoresDAO
    {
        public static bool existeLista(int nrSequencial)
        {
            if (nrSequencial > 0)
            {
                String vsql = String.Format("select 1 from listavalores where nr_sequencial = {0};", nrSequencial);
                Object[] result = Utilidades.consultar(vsql);
                if (result != null)
                {
                    return result.Length > 0;
                }
            }
            return false;
        }

        public static string persistLista(cadListaValores listaValores)
        {
            String vret = "";
            String vsql = " insert into listavalores(nr_sequencial, ds_titulo, nr_altura, nr_largura, ds_instrucaosql, dt_registro)" +
                          " values (@nrsequencial, @dstitulo, @nraltura, @nrlargura, @dsinstrucaosql, current_timestamp);";
            try
            {
                if (cadListaValoresDAO.existeLista(listaValores.nr_sequencial))
                {
                    vsql = "update listavalores set ds_titulo=@dstitulo, nr_altura = @nraltura, nr_largura = @nrlargura, ds_instrucaosql = @dsinstrucaosql where nr_sequencial = @nrsequencial;";
                }
                Conexao.getInstance().startTransaction();
                List<ParametroPGSQL> plist = new List<ParametroPGSQL>();
                plist.Add(new ParametroPGSQL("@nrsequencial", listaValores.nr_sequencial, DbType.Int32));
                plist.Add(new ParametroPGSQL("@dstitulo", listaValores.ds_titulo, DbType.String));
                plist.Add(new ParametroPGSQL("@nraltura", listaValores.vl_altura, DbType.Int32));
                plist.Add(new ParametroPGSQL("@nrlargura", listaValores.vl_largura, DbType.Int32));
                plist.Add(new ParametroPGSQL("@dsinstrucaosql", listaValores.ds_sql, DbType.String));

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
                Alert.erro("Erro ao inserir os dados do Banco!" + erro.Message);
            }
            return vret;
        }

        public static bool existeListaColunas(int nrSequencial, int nrColuna, int nrPosicao)
        {
            if (nrSequencial > 0)
            {
                String vsql = String.Format("select 1 from listavalorescolunas where nr_sequencial = {0} and nr_coluna = {1} and nr_posicao = {2};", nrSequencial, nrColuna, nrPosicao);
                Object[] result = Utilidades.consultar(vsql);
                if (result != null)
                {
                    return result.Length > 0;
                }
            }
            return false;
        }

        public static string persistListaColunas(cadListaValores listaValores)
        {
            String vret = "";
            String vsql = " insert into listavalorescolunas(nr_sequencial, nr_coluna, nr_posicao, ds_titulocoluna, nm_campoinstrsql, nr_larguracampo, ds_alinhamentocampo, dt_registro)" +
                          " values (@nrsequencial, @nrcoluna, @nrposicao, @dstitulocoluna, @nmcampoinstrsql, @nrlarguracampo, @dsalinhamentocampo, current_timestamp);";
            try
            {
                if (cadListaValoresDAO.existeListaColunas(listaValores.nr_sequencial, listaValores.nr_coluna, listaValores.nr_posicaocoluna))
                {
                    vsql = "update listavalorescolunas set ds_titulocoluna = @dstitulocoluna, nm_campoinstrsql = @nmcampoinstrsql, nr_larguracampo = @nrlarguracampo, ds_alinhamentocampo = @dsalinhamentocampo " +
                        " where nr_sequencial = @nrsequencial and nr_coluna = @nrcoluna and nr_posicao = @nrposicao;";
                }
                Conexao.getInstance().startTransaction();
                List<ParametroPGSQL> plist = new List<ParametroPGSQL>();
                plist.Add(new ParametroPGSQL("@nrsequencial", listaValores.nr_sequencial, DbType.Int32));
                plist.Add(new ParametroPGSQL("@nrcoluna", listaValores.nr_coluna, DbType.Int32));
                plist.Add(new ParametroPGSQL("@nrposicao", listaValores.nr_posicaocoluna, DbType.Int32));
                plist.Add(new ParametroPGSQL("@dstitulocoluna", listaValores.ds_titulocoluna, DbType.String));
                plist.Add(new ParametroPGSQL("@nmcampoinstrsql", listaValores.nm_camposqlcoluna, DbType.String));
                plist.Add(new ParametroPGSQL("@nrlarguracampo", listaValores.nr_larguracampocoluna, DbType.Int32));
                plist.Add(new ParametroPGSQL("@dsalinhamentocampo", listaValores.ds_alinhamentocoluna, DbType.String));

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
                Alert.erro("Erro ao inserir os dados do Banco!" + erro.Message);
            }
            return vret;
        }
    }
}

