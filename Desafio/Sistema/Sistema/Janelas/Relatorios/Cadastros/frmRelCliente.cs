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
using FastReport;
using Sistema.Classes.Util;

namespace Sistema.Janelas.Relatorios.Cadastros
{
    public partial class frmRelCliente : DevExpress.XtraEditors.XtraForm
    {
        public frmRelCliente()
        {
            InitializeComponent();
        }

        private void frmRelCliente_Load(object sender, EventArgs e)
        {
            cbOrdenacao.SelectedIndex = 0;
        }

        private void frmRelCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                txtcdCliente.Text = Valida.consultaF9(5, "", "cd_cliente");
                txtcdCliente_Validated(sender, e);
            }
            if (e.KeyCode == Keys.F12)
            {
                btnImprimir_Click(sender, e);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            String vWhere = String.Format("where cliente.dt_registro between '1900-01-01 00:00:00' and '{0}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            String orderby = " ";
            if (!txtcdCliente.Text.Equals(String.Empty))
            {
                vWhere += " and cliente.cd_cliente = " + Convert.ToInt32(txtcdCliente.Text);
            }
            if (cbOrdenacao.SelectedIndex == 0)
            {
                orderby += " cliente.cd_cliente asc, ";
            }
            if (cbOrdenacao.SelectedIndex == 1)
            {
                orderby += " cliente.cd_cliente desc, ";
            }
            if (cbOrdenacao.SelectedIndex == 2)
            {
                orderby += " cliente.ds_nome asc, ";
            }
            if (cbOrdenacao.SelectedIndex == 3)
            {                
                orderby += " cliente.ds_nome desc, ";
            }

            Report report = GetReport.loadReport("RelClienteContato.frx");

            String vSql = "select " +
                            "       cliente.cd_cliente as cliente_cd_cliente, " +
                            "       clientecontato.cd_contato as clientecontato_cd_contato, " +
                            "       cliente.ds_nome as cliente_ds_nome, " +
                            "       cliente.nr_telefone as cliente_nr_telefone, " +
                            "       cliente.nr_celular as cliente_nr_celular, " +
                            "       cliente.nr_celular2 as cliente_nr_celular2, " +
                            "       cliente.ds_email as cliente_ds_email, " +
                            "       clientecontato.ds_contato as clientecontato_ds_contato, " +
                            "       clientecontato.nr_telefone as clientecontato_nr_telefone, " +
                            "       clientecontato.nr_celular as clientecontato_nr_celular, " +
                            "       clientecontato.nr_celular2 as clientecontato_nr_celular2, " +
                            "       clientecontato.ds_email as clientecontato_ds_email " +
                            " from cliente " +
                            " left join clientecontato on clientecontato.cd_cliente = cliente.cd_cliente " + vWhere +
                            " order by " + orderby + "clientecontato.cd_contato ";
            
            GetReport.buildReport("tabClienteContato", vSql, report);
            GetReport.abreVisualizador(report);
        }

        private void txtcdCliente_Validated(object sender, EventArgs e)
        {
            if (!txtcdCliente.Text.Equals(String.Empty))
            {
                String vSql = String.Format("select ds_nome from cliente  where cd_cliente = {0}", txtcdCliente.Text);
                Object[] result = Utilidades.consultar(vSql);
                if (result != null)
                {
                    txtdsCliente.Text = Convert.ToString(result[0]);
                }
            }
            else
            {
                txtdsCliente.Text = "";
            }
        }

        private void txtcdCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }
    }
}