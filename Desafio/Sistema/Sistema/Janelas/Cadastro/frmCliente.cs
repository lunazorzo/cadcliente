using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sistema.Classes.Util;
using Sistema.Classes.Cadastros;
using FastReport;

namespace Sistema.Janelas.Cadastro
{
    public partial class frmCliente : DevExpress.XtraEditors.XtraForm
    {
        public frmCliente()
        {
            InitializeComponent();
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            carregaGridCliente();
            btnBuscar.Focus();

        }

        private void btnGravarCliente_Click(object sender, EventArgs e)
        {
            gravaCliente();
        }

        private void frmCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                limpaCampos();
                iniciaCadastro();               
            }
            if (e.KeyCode == Keys.F9)
            {
                btnBuscar_Click(sender, e);
            }
            if (e.KeyCode == Keys.F12)
            {
                AbreForm.abreRelCliente(StaticVariables.cdUsuario);
            }
        }

        public void iniciaCadastro()
        {
            txtcdCliente.Text = Utilidades.getLastId("cliente", "cd_cliente");
            txtdsNome.Focus();
        }

        public void carregaGridCliente()
        {
            Utilidades.getGrid(GetDataGrid.getDadosCliente(), gcCliente, "dos Clientes");
        }
        public void carregaGridClienteContato(String cdCliente)
        {
            Utilidades.getGrid(GetDataGrid.getDadosClienteContato(cdCliente), gcContato, "dos Contatos");
        }

        public void limpaCampos()
        {
            Valida.clear(new Object[] { txtcdCliente, txtdsNome, txtdsEmail, txtnrTelefone, txtnrCelular, txtnrCelular2, txtcdClienteContato, txtdsClienteContato});
            txtdsNome.Focus();
        }

        public void gravaCliente()
        {
            if (txtcdCliente.Text.Equals(String.Empty))
            {
                iniciaCadastro();
            }

            Boolean prossegue = Valida.verificaObrigatorios(new Object[] { txtcdCliente, txtdsNome });
            if (prossegue)
            {
                try
                {
                    cadCliente cliente = new cadCliente();
                    cliente.cd_cliente = Convert.ToInt32(txtcdCliente.Text);
                    cliente.ds_nome = txtdsNome.Text;
                    cliente.ds_email = txtdsEmail.Text;
                    cliente.nr_telefone = txtnrTelefone.Text;
                    cliente.nr_celular = txtnrCelular.Text;
                    cliente.nr_celular2 = txtnrCelular2.Text;
                    Conexao.getInstance().startTransaction();

                    String vRet = "";
                    vRet = cadClienteDAO.inserir(cliente);

                    if (vRet.Equals(string.Empty))
                    {
                        Conexao.getInstance().commit();
                        Alert.informacao("Registro Salvo com Sucesso!");
                        limpaCampos();
                        carregaGridCliente();
                        //limpa a grid dos contatos
                        gcContato.DataSource = null;                        
                    }
                    else
                    {
                        Conexao.getInstance().rollback();
                        Alert.erro("Erro ao Gravar Registro! \n" + vRet);
                    }

                }
                catch (Exception erro)
                {
                    Alert.erro("Erro ao inserir o cadastro \n " + erro.Message);
                }
            }            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtcdCliente.Text = Valida.consultaF9(5, "", "cd_cliente");
            txtcdCliente_Validated(sender, e);
        }

        private void txtcdCliente_Validated(object sender, EventArgs e)
        {
            if (!txtcdCliente.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_nome, ds_email, nr_telefone, nr_celular, nr_celular2 " +
                                            " from cliente  where cd_cliente = {0}", txtcdCliente.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsNome.Text = Convert.ToString(result[0]);
                    txtdsEmail.Text = Convert.ToString(result[1]);
                    txtnrTelefone.Text = Convert.ToString(result[2]);
                    txtnrCelular.Text = Convert.ToString(result[3]);
                    txtnrCelular2.Text = Convert.ToString(result[4]);
                    txtdsNome.Focus();
                    
                    //AbaContatos
                    carregaDadosContato();                    
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            
            if (!txtcdCliente.Text.Equals(String.Empty))
            {
                Report report = GetReport.loadReport("CadClienteContato.frx");                
                String vSqlCliente = String.Format("select cliente.cd_cliente, cliente.ds_nome, cliente.ds_email, cliente.nr_telefone, cliente.nr_celular, cliente.nr_celular2 " +
                                                " from cliente where cd_cliente = {0}", txtcdCliente.Text);
                String vSqlContato = String.Format("select clientecontato.cd_cliente, clientecontato.cd_contato, clientecontato.ds_contato, " +
                                    " clientecontato.nr_telefone, clientecontato.nr_celular, clientecontato.nr_celular2, clientecontato.ds_email " +
                                    " from clientecontato where cd_cliente = {0}", txtcdCliente.Text);

                GetReport.buildReport("tabcadcliente", vSqlCliente, report);
                GetReport.buildReport("tabcadclientecontato", vSqlContato, report);
                GetReport.abreVisualizador(report);
            }
            else
            {
                Alert.atencao("É necessário buscar o cliente desejado, para realizar a impressão do relatório individual.");
            }
        }

        private void txtdsEmail_Validated(object sender, EventArgs e)
        {
            if (!txtdsEmail.Text.Equals(String.Empty))
            {
                if (!Valida.validaEmail(txtdsEmail.Text))
                {
                    Alert.atencao("E-mail inválido. Verifique o e-mail informado;");
                    txtdsEmail.Focus();
                }
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtcdCliente.Text = Convert.ToString(gvCliente.GetRowCellValue(gvCliente.FocusedRowHandle, "cd_cliente"));
                txtcdCliente_Validated(sender, e);                
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao editar Cliente. \n" + erro.Message);
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvCliente.IsFocusedView == true)
            {
                String cdCliente = Convert.ToString(gvCliente.GetRowCellValue(gvCliente.FocusedRowHandle, "cd_cliente"));
                String dsNome = Convert.ToString(gvCliente.GetRowCellValue(gvCliente.FocusedRowHandle, "ds_nome"));
                if (!cdCliente.Equals(String.Empty))
                {
                    if (txtcdCliente.Text.Equals(cdCliente))
                    {
                        limpaCampos();
                    }
                    if (Alert.pergunta(String.Format("cliente {0}", dsNome)))
                    {
                        Boolean sucesso = Utilidades.remove("Cliente",
                            String.Format("select 1 from clientecontato where cd_cliente = {0};", cdCliente),
                            String.Format(" delete from cliente where cd_cliente =  {0};", cdCliente));
                        if (sucesso)
                        {
                            carregaGridCliente();
                        }
                    }
                }
            }  
        }

        private void gcCliente_DoubleClick(object sender, EventArgs e)
        {
            editarToolStripMenuItem_Click(sender, e);
        }

        public void carregaDadosContato()
        {
            limpaCamposContatos();
            txtcdClienteContato.Text = txtcdCliente.Text;
            txtdsClienteContato.Text = txtdsNome.Text;
            carregaGridClienteContato(txtcdClienteContato.Text);
        }

        public void limpaCamposContatos()
        {
            Valida.clear(new Object[] { txtcdContato, txtdsNomeContato, txtdsEmailContato, txtnrTelefoneContato, txtnrCelularContato, txtnrCelular2Contato });
            txtdsNomeContato.Focus();
        }

        private void btnGravarContato_Click(object sender, EventArgs e)
        {
            Int32 cdContato = 0;
            Boolean prossegue = Valida.verificaObrigatorios(new Object[] { txtcdCliente, txtdsNomeContato });
            if (prossegue)
            {
                try
                {
                    if (txtcdContato.Text.Equals(String.Empty))
                    {
                        cdContato = Convert.ToInt32(Utilidades.getLastId("clientecontato", "cd_contato"));
                    }
                    else
                    {
                        cdContato = Convert.ToInt32(txtcdContato.Text);
                    }

                    cadClienteContato clienteContato = new cadClienteContato();
                    clienteContato.cd_cliente = Convert.ToInt32(txtcdClienteContato.Text);
                    clienteContato.cd_contato = cdContato;
                    clienteContato.ds_contato = txtdsNomeContato.Text;
                    clienteContato.ds_email = txtdsEmailContato.Text;
                    clienteContato.nr_telefone = txtnrTelefoneContato.Text;
                    clienteContato.nr_celular = txtnrCelularContato.Text;
                    clienteContato.nr_celular2 = txtnrCelular2Contato.Text;

                    String vRet = "";
                    vRet = cadClienteContatoDAO.inserir(clienteContato);

                    if (vRet.Equals(string.Empty))
                    {
                        Conexao.getInstance().commit();
                        Alert.informacao("Registro Salvo com Sucesso!");
                        limpaCamposContatos();
                        carregaGridClienteContato(txtcdClienteContato.Text);
                    }
                    else
                    {
                        Conexao.getInstance().rollback();
                        Alert.erro("Erro ao Gravar Registro! \n" + vRet);
                    }
                }
                catch (Exception erro)
                {
                    Alert.erro("Erro ao inserir o cadastro do contato\n " + erro.Message);
                }
            }
        }

        private void txtdsEmailContato_Validated(object sender, EventArgs e)
        {
            if (!txtdsEmailContato.Text.Equals(String.Empty))
            {
                if (!Valida.validaEmail(txtdsEmailContato.Text))
                {
                    Alert.atencao("E-mail inválido. Verifique o e-mail informado;");
                    txtdsEmailContato.Focus();
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                txtcdContato.Text = Convert.ToString(gvContato.GetRowCellValue(gvContato.FocusedRowHandle, "cd_contato"));
                if (!txtcdContato.Text.Equals(String.Empty))
                {
                    String vSql = String.Format("select ds_contato, ds_email, nr_telefone, nr_celular, nr_celular2 " +
                                        " from clientecontato " +
                                        " where cd_cliente = {0} and cd_contato = {1}", txtcdClienteContato.Text, txtcdContato.Text);
                    Object[] result = Utilidades.consultar(vSql);
                    if (result != null)
                    {
                        txtdsNomeContato.Text = Convert.ToString(result[0]);
                        txtdsEmailContato.Text = Convert.ToString(result[1]);
                        txtnrTelefoneContato.Text = Convert.ToString(result[2]);
                        txtnrCelularContato.Text = Convert.ToString(result[3]);
                        txtnrCelular2Contato.Text = Convert.ToString(result[4]);
                        txtdsNomeContato.Focus();
                    }
                }
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao editar Cliente. \n" + erro.Message);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (gvContato.IsFocusedView == true)
            {
                String cdCliente = Convert.ToString(gvContato.GetRowCellValue(gvContato.FocusedRowHandle, "cd_cliente"));
                String cdContato = Convert.ToString(gvContato.GetRowCellValue(gvContato.FocusedRowHandle, "cd_contato"));
                String dsNome = Convert.ToString(gvContato.GetRowCellValue(gvContato.FocusedRowHandle, "ds_contato"));
                if (!cdCliente.Equals(String.Empty) || !cdContato.Equals(String.Empty))
                {
                    if (txtcdContato.Text.Equals(cdContato))
                    {
                        limpaCamposContatos();
                    }
                    if (Alert.pergunta(String.Format("Contato {0}", dsNome)))
                    {
                        Boolean sucesso = Utilidades.remove("",
                            String.Format("", ""),
                            String.Format(" delete from clientecontato where cd_cliente =  {0} and cd_contato = {1};", cdCliente, cdContato));
                        if (sucesso)
                        {
                            carregaGridClienteContato(txtcdClienteContato.Text);
                        }
                    }
                }
            }  
        }

        private void gcContato_DoubleClick(object sender, EventArgs e)
        {
            toolStripMenuItem1_Click(sender, e);
        }        
    }
}
