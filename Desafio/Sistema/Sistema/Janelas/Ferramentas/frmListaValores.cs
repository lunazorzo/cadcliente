using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sistema.Classes.Util;
using Sistema.Classes.Ferramentas;

namespace Sistema.Janelas.Ferramentas
{
    public partial class frmListaValores : DevExpress.XtraEditors.XtraForm
    {
        public frmListaValores()
        {
            InitializeComponent();
        }

        private void frmListaValores_Load(object sender, EventArgs e)
        {
            carregaGrid();
            carregaAccessibleName();
        }

        private void frmListaValores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                limpaCampos();
                txtSequencial.Text = Utilidades.getLastId("listavalores", "nr_sequencial");
                txtTituloLista.Focus();
                txtAltura.Text = "400";
                txtLargura.Text = "600";
            }
        }
        public void carregaAccessibleName()
        {
            txtSequencial.AccessibleName = "o Nº Sequencial da Lista";
            txtTituloLista.AccessibleName = "o Título da Lista!";
            txtAltura.AccessibleName = "Valor da Altura!";
            txtLargura.AccessibleName = "Valor da Largura!";
            txtSql.AccessibleName = "Instrução do SQL!";

            txtNumeroColuna.AccessibleName = "o Nº Coluna!";
            txtPosicaoColuna.AccessibleName = "o Nº da Posição da Coluna!";
            txtLarguraColuna.AccessibleName = "a Largura da Coluna!";
            cbAlinhamentoColuna.AccessibleName = "o Alinhamento do Texto da Coluna!";
            txtTituloColuna.AccessibleName = "o Título da Coluna!";
            txtColunaSQL.AccessibleName = "a Coluna do SQL!";
        }

        public void carregaGrid()
        {
            Utilidades.getGrid(GetDataGrid.getDadosListaValores(), gcGridListaValores, "das listas de valores");
        }

        public void carregaGridValores(Int32 nrSequencial)
        {
            Utilidades.getGrid(GetDataGrid.getDadosListaValoresColunas(nrSequencial), gcListaValoresColunas, "coluna valores");
        }

        public void limpaCampos()
        {
            Valida.clear(new Object[] { txtSequencial, txtTituloLista, txtAltura, txtLargura, txtSql });
            txtSequencial.Focus();
        }

        public void limpaCamposColunas()
        {
            Valida.clear(new Object[] { txtNumeroColuna, txtPosicaoColuna, txtLarguraColuna, cbAlinhamentoColuna, txtTituloColuna, txtColunaSQL });
            txtNumeroColuna.Focus();
            txtNumeroColuna.Enabled = true;
            txtPosicaoColuna.Enabled = true;
        }

        public static String retornaAlinhamento(Int32 cdSelecao)
        {
            String retorno = "";
            if (cdSelecao == 0)
            {
                retorno = "Centralizado";
            }
            else if (cdSelecao == 1)
            {
                retorno = "Direita";
            }
            else if (cdSelecao == 2)
            {
                retorno = "Esquerda";
            }
            else if (cdSelecao == 3)
            {
                retorno = "Justificado";
            }
            return retorno;
        }

        public void alteraGridValores(object sender, EventArgs e)
        {
            try
            {
                txtSequencial.Text = Convert.ToString(gvGridListaValores.GetRowCellValue(gvGridListaValores.FocusedRowHandle, "nr_sequencial"));
                if (!txtSequencial.Text.Equals(""))
                {
                    txtSequencial_Validated(sender, e);
                }
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao carregar lista de valores" + erro.Message);
            }
        }

        public void alteraGridValoresColunas(object sender, EventArgs e)
        {
            try
            {
                txtSequencialColuna.Text = Convert.ToString(gvListaValoresColunas.GetRowCellValue(gvListaValoresColunas.FocusedRowHandle, "nr_sequencialcoluna"));
                txtNumeroColuna.Text = Convert.ToString(gvListaValoresColunas.GetRowCellValue(gvListaValoresColunas.FocusedRowHandle, "nr_coluna"));
                txtPosicaoColuna.Text = Convert.ToString(gvListaValoresColunas.GetRowCellValue(gvListaValoresColunas.FocusedRowHandle, "nr_posicaocoluna"));

                if (!txtSequencialColuna.Text.Equals("") || !txtNumeroColuna.Text.Equals("") || !txtPosicaoColuna.Text.Equals(""))
                {
                    Object[] result = GetData.getListaValoresColunas(Convert.ToInt32(txtSequencial.Text), Convert.ToInt32(txtNumeroColuna.Text), Convert.ToInt32(txtPosicaoColuna.Text));
                    if (result != null)
                    {
                        txtSequencialColuna.Text = Convert.ToString(result[0]);
                        txtNumeroColuna.Text = Convert.ToString(result[1]);
                        txtPosicaoColuna.Text = Convert.ToString(result[2]);
                        txtTituloColuna.Text = Convert.ToString(result[3]);
                        txtColunaSQL.Text = Convert.ToString(result[4]);
                        txtLarguraColuna.Text = Convert.ToString(result[5]);
                        if (Convert.ToString(result[6]) == "Centralizado")
                        {
                            cbAlinhamentoColuna.SelectedIndex = 0;
                        }
                        else if (Convert.ToString(result[6]) == "Direita")
                        {
                            cbAlinhamentoColuna.SelectedIndex = 1;
                        }
                        else if (Convert.ToString(result[6]) == "Esquerda")
                        {
                            cbAlinhamentoColuna.SelectedIndex = 2;
                        }
                        else if (Convert.ToString(result[6]) == "Justificado")
                        {
                            cbAlinhamentoColuna.SelectedIndex = 3;
                        }
                        txtNumeroColuna.Enabled = false;
                        txtPosicaoColuna.Enabled = false;
                    }
                }
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao carregar lista de valores" + erro.Message);
            }
        }

        private void txtSequencial_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }

        private void txtLargura_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }

        private void txtNumeroColuna_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }

        private void txtPosicaoColuna_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }

        private void txtLarguraColuna_KeyPress(object sender, KeyPressEventArgs e)
        {
            Valida.keypressSomenteNumero(e);
        }

        private void btnGravarListaValores_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean prossegue = Valida.verificaObrigatorios(new Object[] { txtSequencial, txtTituloLista, txtAltura, txtLargura, txtSql });
                if (prossegue)
                {
                    try
                    {
                        String vret = "";
                        cadListaValores listavalores = new cadListaValores();
                        listavalores.nr_sequencial = Convert.ToInt32(txtSequencial.Text);
                        listavalores.ds_titulo = txtTituloLista.Text;
                        listavalores.vl_altura = Convert.ToInt32(txtAltura.Text);
                        listavalores.vl_largura = Convert.ToInt32(txtLargura.Text);
                        listavalores.ds_sql = txtSql.Text;

                        vret = cadListaValoresDAO.persistLista(listavalores);

                        carregaGrid();
                        limpaCampos();
                    }
                    catch (Exception erro)
                    {
                        Alert.erro("Erro ao Gravar Registro! \n" + erro.Message);
                    }
                }
            }
            catch (Exception erro)
            {
                Alert.erro(String.Format("Erro ao gravar Lista de Valores {0}. {1}", txtTituloLista.Text, erro.Message));
            }
        }

        private void btnGravarColunas_Click(object sender, EventArgs e)
        {            
            Boolean prossegue = Valida.verificaObrigatorios(new Object[] { txtSequencialColuna, txtNumeroColuna, txtPosicaoColuna, txtLarguraColuna,
                        cbAlinhamentoColuna, txtTituloColuna, txtColunaSQL});
            if (prossegue)
            {
                try
                {
                    String vret = "";
                    cadListaValores listavalorescolunas = new cadListaValores();
                    listavalorescolunas.nr_sequencial = Convert.ToInt32(txtSequencialColuna.Text);
                    listavalorescolunas.nr_coluna = Convert.ToInt32(txtNumeroColuna.Text);
                    listavalorescolunas.nr_posicaocoluna = Convert.ToInt32(txtPosicaoColuna.Text);
                    listavalorescolunas.ds_titulocoluna = txtTituloColuna.Text;
                    listavalorescolunas.nm_camposqlcoluna = txtColunaSQL.Text;
                    listavalorescolunas.nr_larguracampocoluna = Convert.ToInt32(txtLarguraColuna.Text);
                    listavalorescolunas.ds_alinhamentocoluna = retornaAlinhamento(cbAlinhamentoColuna.SelectedIndex);

                    vret = cadListaValoresDAO.persistListaColunas(listavalorescolunas);

                    carregaGridValores(Convert.ToInt32(txtSequencialColuna.Text));
                    limpaCamposColunas();
                }
                catch (Exception erro)
                {
                    Alert.erro("Erro ao Gravar Registro! \n" + erro.Message);
                }
            }
        }

        private void txtSequencial_Validated(object sender, EventArgs e)
        {
            if (!txtSequencial.Text.Equals(""))
            {
                Object[] result = GetData.getListaValores(Convert.ToInt32(txtSequencial.Text));
                if (result != null)
                {
                    txtSequencialColuna.Text = txtSequencial.Text;
                    txtTituloLista.Text = Convert.ToString(result[0]);
                    txtAltura.Text = Convert.ToString(result[1]);
                    txtLargura.Text = Convert.ToString(result[2]);
                    txtSql.Text = Convert.ToString(result[3]);
                    carregaGridValores(Convert.ToInt32(txtSequencial.Text));
                }
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            alteraGridValores(sender, e);
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvGridListaValores.IsFocusedView == true)
            {
                String nrSequencial = Convert.ToString(gvGridListaValores.GetRowCellValue(gvGridListaValores.FocusedRowHandle, "nr_sequencial"));
                String dsTitulo = Convert.ToString(gvGridListaValores.GetRowCellValue(gvGridListaValores.FocusedRowHandle, "ds_titulo"));
                if (Alert.pergunta(String.Format(" lista {0}", dsTitulo)))
                {
                    Boolean sucesso = Utilidades.remove("Lista",
                        String.Format("select 1 from listavalorescolunas where nr_sequencial = {0};", nrSequencial),
                        String.Format(" delete from listavalores where nr_sequencial =  {0};", nrSequencial));
                    if (sucesso)
                    {
                        carregaGrid();
                    }
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            alteraGridValoresColunas(sender, e);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (gvListaValoresColunas.IsFocusedView == true)
            {
                String nrSequencial = Convert.ToString(gvListaValoresColunas.GetRowCellValue(gvListaValoresColunas.FocusedRowHandle, "nr_sequencial"));
                String nrColuna = Convert.ToString(gvListaValoresColunas.GetRowCellValue(gvListaValoresColunas.FocusedRowHandle, "nr_coluna"));
                String nrPosicao = Convert.ToString(gvListaValoresColunas.GetRowCellValue(gvListaValoresColunas.FocusedRowHandle, "nr_posicaocoluna"));
                String dsTituloColuna = Convert.ToString(gvListaValoresColunas.GetRowCellValue(gvListaValoresColunas.FocusedRowHandle, "ds_titulocoluna"));

                if (Alert.pergunta(String.Format(" lista coluna {0}", dsTituloColuna)))
                {
                    Boolean sucesso = Utilidades.remove("Coluna", "",
                        String.Format(" delete from listavalorescolunas where nr_sequencial = {0} and nr_coluna = {1} and nr_posicao = {2} ", nrSequencial, nrColuna, nrPosicao));
                    if (sucesso)
                    {
                        carregaGridValores(Convert.ToInt32(nrSequencial));
                    }
                }
            }
        }

        private void gvGridListaValores_DoubleClick(object sender, EventArgs e)
        {
            alteraGridValores(sender, e);
        }

        private void gvListaValoresColunas_DoubleClick(object sender, EventArgs e)
        {
            alteraGridValoresColunas(sender, e);
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            if (!txtSequencialColuna.Text.Equals(String.Empty)) 
            {
                Valida.consultaF9(Convert.ToInt32(txtSequencialColuna.Text), "", "");
            }
            else
            {
                Alert.atencao("É necessário selecionar uma lista de consulta, para que a mesma seja aberta.");
            }
        }

        private void btnVisualizarLista_Click(object sender, EventArgs e)
        {
            if (!txtSequencial.Text.Equals(String.Empty))
            {
                Valida.consultaF9(Convert.ToInt32(txtSequencial.Text), "", "");
            }   
            else
            {
                Alert.atencao("É necessário selecionar uma lista de consulta, para que a mesma seja aberta.");
            }
        }
    }
}