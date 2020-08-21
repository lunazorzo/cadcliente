using Sistema.Classes.Acesso;
using Sistema.Classes.Cadastros;
using Sistema.Classes.Ferramentas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sistema.Classes.Util
{
    class GetDataGrid
    {
        public static List<cadListaValores> getDadosListaValores()
        {
            List<cadListaValores> vret = new List<cadListaValores>();
            const String vsql = "select nr_sequencial, ds_titulo, nr_altura, nr_largura from listavalores order by nr_sequencial";
            List<List<Object>> result = Conexao.getInstance().toList(vsql);
            if (result != null)
            {
                foreach (List<Object> dado in result)
                {
                    cadListaValores cmf = new cadListaValores();
                    cmf.nr_sequencial = Convert.ToInt32(dado[0]);
                    cmf.ds_titulo = Convert.ToString(dado[1]);
                    cmf.vl_altura = Convert.ToInt32(dado[2]);
                    cmf.vl_largura = Convert.ToInt32(dado[3]);
                    vret.Add(cmf);
                }
            }
            return vret;
        }

        public static List<cadListaValores> getDadosListaValoresColunas(Int32 nrSequencial)
        {
            List<cadListaValores> vret = new List<cadListaValores>();
            String vsql = "select " +
                    "	nr_sequencial, nr_coluna, nr_posicao, nr_larguracampo, ds_alinhamentocampo, ds_titulocoluna, nm_campoinstrsql " +
                    "from listavalorescolunas " +
                    "where nr_sequencial = " + Convert.ToInt32(nrSequencial);
            List<List<Object>> result = Conexao.getInstance().toList(vsql);
            if (result != null)
            {
                foreach (List<Object> dado in result)
                {
                    cadListaValores cmf = new cadListaValores();
                    cmf.nr_sequencial = Convert.ToInt32(dado[0]);
                    cmf.nr_coluna = Convert.ToInt32(dado[1]);
                    cmf.nr_posicaocoluna = Convert.ToInt32(dado[2]);
                    cmf.nr_larguracampocoluna = Convert.ToInt32(dado[3]);
                    cmf.ds_alinhamentocoluna = Convert.ToString(dado[4]);
                    cmf.ds_titulocoluna = Convert.ToString(dado[5]);
                    cmf.nm_camposqlcoluna = Convert.ToString(dado[6]);
                    vret.Add(cmf);
                }
            }
            return vret;
        }
        
        public static List<cadUsuario> getDadosUsuario()
        {
            List<cadUsuario> vret = new List<cadUsuario>();
            const String vsql = " select cd_usuario, nm_usuario from usuario";
            List<List<Object>> result = Conexao.getInstance().toList(vsql);
            if (result != null)
            {
                foreach (List<Object> dado in result)
                {
                    cadUsuario cmf = new cadUsuario();
                    cmf.cd_usuario = Convert.ToString(dado[0]);
                    cmf.nm_usuario = Convert.ToString(dado[1]);
                    vret.Add(cmf);
                }
            }
            return vret;
        }

        public static List<cadModulo> getDadosMenuArvore(String cdUsuario)
        {            
            List<cadModulo> vret = new List<cadModulo>();
            String vsql = String.Format("select "+
                                        "	usuarioprogmenu.cd_usuario, modulo.cd_modulo, " + 
                                        "	programa.cd_programa, programa.ds_programa "+
                                        "  from modulo "+
                                        " inner join programa on programa.cd_modulo = modulo.cd_modulo "+
                                        " inner join usuarioprogmenu on usuarioprogmenu.cd_modulo = modulo.cd_modulo " +
                                        "	                  and usuarioprogmenu.cd_programa = programa.cd_programa " +
                                        " where usuarioprogmenu.cd_usuario = '{0}' order by modulo.cd_modulo, programa.cd_programa", cdUsuario);
            List<List<Object>> result = Conexao.getInstance().toList(vsql);
            if (result != null)
            {
                foreach (List<Object> dado in result)
                {
                    cadModulo cmf = new cadModulo();
                    cmf.cd_usuario = Convert.ToString(dado[0]);
                    cmf.cd_modulo = Convert.ToString(dado[1]);
                    cmf.cd_programa = Convert.ToInt32(dado[2]);
                    cmf.ds_programa = Convert.ToString(dado[3]);
                    vret.Add(cmf);
                }
            }
            return vret;
        }

        public static List<cadModulo> getDadosModulo()
        {            
            List<cadModulo> vret = new List<cadModulo>();
            String vsql = "select cd_modulo, ds_modulo from modulo order by cd_modulo";
            List<List<Object>> result = Conexao.getInstance().toList(vsql);
            if (result != null)
            {
                foreach (List<Object> dado in result)
                {
                    cadModulo cmf = new cadModulo();                    
                    cmf.cd_modulo = Convert.ToString(dado[0]);
                    cmf.ds_modulo = Convert.ToString(dado[1]);
                    vret.Add(cmf);
                }
            }
            return vret;
        }

        public static List<cadModulo> getDadosSubMenu()
        {
            List<cadModulo> vret = new List<cadModulo>();
            String vsql = "select cd_modulo, cd_submenu, ds_submenu from submenu order by cd_modulo";
            List<List<Object>> result = Conexao.getInstance().toList(vsql);
            if (result != null)
            {
                foreach (List<Object> dado in result)
                {
                    cadModulo cmf = new cadModulo();
                    cmf.cd_modulo = Convert.ToString(dado[0]);
                    cmf.cd_submenu = Convert.ToInt32(dado[1]);
                    cmf.ds_submenu = Convert.ToString(dado[2]);
                    vret.Add(cmf);
                }
            }
            return vret;
        }

        public static List<cadModulo> getDadosPrograma()
        {
            List<cadModulo> vret = new List<cadModulo>();
            String vsql = "select cd_modulo, cd_programa, ds_programa, cd_submenu from programa order by cd_modulo, cd_programa";
            List<List<Object>> result = Conexao.getInstance().toList(vsql);
            if (result != null)
            {
                foreach (List<Object> dado in result)
                {
                    cadModulo cmf = new cadModulo();
                    cmf.cd_modulo = Convert.ToString(dado[0]);
                    cmf.cd_programa = Convert.ToInt32(dado[1]);
                    cmf.ds_programa = Convert.ToString(dado[2]);
                    cmf.cd_submenu = Convert.ToInt32(dado[3]);
                    vret.Add(cmf);
                }
            }
            return vret;
        }

        public static List<cadCliente> getDadosCliente()
        {
            List<cadCliente> vret = new List<cadCliente>();
            String vsql = "select cd_cliente, ds_nome from cliente order by cd_cliente";
            List<List<Object>> result = Conexao.getInstance().toList(vsql);
            if (result != null)
            {
                foreach (List<Object> dado in result)
                {
                    cadCliente cmf = new cadCliente();
                    cmf.cd_cliente = Convert.ToInt32(dado[0]);
                    cmf.ds_nome = Convert.ToString(dado[1]);
                    vret.Add(cmf);
                }
            }
            return vret;
        }

        public static List<cadClienteContato> getDadosClienteContato(String cdCliente)
        {
            List<cadClienteContato> vret = new List<cadClienteContato>();
            String vsql = String.Format("select ds_contato, ds_email, cd_cliente, cd_contato from clientecontato where cd_cliente = {0} order by ds_contato", Convert.ToInt32(cdCliente));
            List<List<Object>> result = Conexao.getInstance().toList(vsql);
            if (result != null)
            {
                foreach (List<Object> dado in result)
                {
                    cadClienteContato cmf = new cadClienteContato();
                    cmf.ds_contato = Convert.ToString(dado[0]);
                    cmf.ds_email = Convert.ToString(dado[1]);
                    cmf.cd_cliente = Convert.ToInt32(dado[2]);
                    cmf.cd_contato = Convert.ToInt32(dado[3]);
                    vret.Add(cmf);
                }
            }
            return vret;
        }
    }
}

