using System;
using System.Collections.Generic;
using System.Linq;

namespace Sistema.Classes.Util
{
    class GetData
    {
        public static Object[] getListaValores(Int32 nrSequencial)
        {
            String vsql = String.Format(" select ds_titulo, nr_altura, nr_largura, ds_instrucaosql " +
                                            "   from public.listavalores where nr_sequencial = {0}", nrSequencial);
            Object[] result = Utilidades.consultar(vsql);
            return result;
        }

        public static Object[] getListaValoresColunas(Int32 nrSequencial, Int32 nrColuna, Int32 nrPosicao)
        {
            String vsql = String.Format("select nr_sequencial, nr_coluna, nr_posicao, ds_titulocoluna, nm_campoinstrsql, " +
                    "       nr_larguracampo, ds_alinhamentocampo " +
                    "  from listavalorescolunas where nr_sequencial = {0} and nr_coluna = {1} and nr_posicao = {2} ", nrSequencial, nrColuna, nrPosicao);
            Object[] result = Utilidades.consultar(vsql);
            return result;
        }

        public static String getPais(String pais, Boolean cadastro)
        {
            String msg = "Código do país não encontrado";
            String vSQL = String.Format("select ds_pais from pais where cd_pais = {0};", pais);
            return Valida.getValidated(vSQL, msg, cadastro);
        }

        public static Object[] getPais(String pais)
        {
            String vSQL = String.Format("select ds_paisbr, ds_paising, ds_paisesp, ds_paisfra, sg_paisalfa2, " +
                                        "       sg_paisalfa3, cd_continente, ds_capital, ds_idioma, ds_moeda, " +
                                        "       img_pais " +
                                        "  from pais where cd_pais = {0};", Convert.ToInt32(pais));
            Object[] result = Utilidades.consultar(vSQL);
            return result;
        }

        public static Object[] getContinente(String continente)
        {
            String vSQL = String.Format("select ds_continente from continente where cd_continente = {0};", Convert.ToInt32(continente));
            Object[] result = Utilidades.consultar(vSQL);
            return result;
        }

        public static String getRegiao(String regiao, Boolean cadastro)
        {
            String msg = "Código da região não encontrado";
            String vSQL = String.Format("select ds_regiao from regiao where cd_regiao = {0};", regiao);
            return Valida.getValidated(vSQL, msg, cadastro);
        }

        public static Object[] getRegiao(String regiao)
        {
            String vSQL = String.Format("select ds_regiao, sg_regiao from regiao where cd_regiao = {0};", regiao);
            Object[] result = Utilidades.consultar(vSQL);
            return result;
        }

        public static String getEstado(String estado, Boolean cadastro)
        {
            String msg = "Código do estado não encontrado";
            String vSQL = String.Format("select ds_estado from estado where cd_estado = {0};", estado);
            return Valida.getValidated(vSQL, msg, cadastro);
        }

        public static Object[] getEstado(String estado)
        {
            String vSQL = String.Format("select cd_pais, cd_regiao, sg_estado, ds_estado from estado where cd_estado = {0};", estado);
            Object[] result = Utilidades.consultar(vSQL);
            return result;
        }
    }
}

