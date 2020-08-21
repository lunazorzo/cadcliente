using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sistema.Classes.Util
{
    public class LstValores
    {
        #region 'Declarações '
        private DataTable tbDadosLista { get; set; }
        private DataTable tbDadosFiltro { get; set; }
        public ArrayList objRetorno { get; set; }
        public Boolean MultiSelecao { get; set; }
        DevExpress.XtraGrid.GridControl ctrlGrid { get; set; }
        DevExpress.XtraGrid.Views.Grid.GridView gridLst { get; set; }
        DevExpress.XtraEditors.XtraForm frmLista { get; set; }
        #endregion

        public ArrayList ListaValores(Int32 inrLista, String idsWhere, Boolean multiselecao, String idsnovocabecalho = "")
        {
            MultiSelecao = multiselecao;

            frmLista = new DevExpress.XtraEditors.XtraForm();
            Int32 iHeight = 0;
            Int32 iWidth = 0;
            String isqlConsulta = "";

            try
            {
                tbDadosLista = new DataTable("dadosLista");

                if (Conexao.getInstance().getConnection().State == ConnectionState.Open)
                {
                    Object[] consulta = Utilidades.consultar(String.Format(" select ds_titulo,nr_altura,nr_largura,ds_instrucaosql from listavalores where nr_sequencial = {0}", inrLista));
                    if (consulta != null)
                    {
                        if (Convert.ToInt32(consulta[1]) > 0)
                        {
                            iHeight = Convert.ToInt32(consulta[1]);
                        }
                        if (Convert.ToInt32(consulta[2]) > 0)
                        {
                            iWidth = Convert.ToInt32(consulta[2]);
                        }
                        // se foi passado um novo cabeçalho, usa o que o usuário passou
                        if (idsnovocabecalho.Trim().Length > 0)
                            frmLista.Text = idsnovocabecalho;
                        else
                            frmLista.Text = String.Format("({0}) - {1}", inrLista, Convert.ToString(consulta[0]));
                        isqlConsulta = Convert.ToString(consulta[3]);
                    }

                    if (!isqlConsulta.Equals(""))
                    {
                        isqlConsulta = isqlConsulta.Replace(":pDsWhere", idsWhere);
                        NpgsqlCommand cmd = new NpgsqlCommand(isqlConsulta, Conexao.getInstance().getConnection());
                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                        adapter.Fill(tbDadosLista);

                        if (multiselecao)
                        {
                            tbDadosLista.Columns.Add("X", typeof(String)).SetOrdinal(0);
                        }
                    }
                }
                objRetorno = new ArrayList();
                frmLista.ClientSize = new Size(iWidth, iHeight + 10);
                frmLista.Font = new Font("Tahoma", 9, FontStyle.Bold);
                frmLista.WindowState = FormWindowState.Normal;
                frmLista.FormBorderStyle = FormBorderStyle.FixedSingle;
                frmLista.StartPosition = FormStartPosition.CenterParent;
                frmLista.KeyPreview = true;
                frmLista.MaximizeBox = false;
                frmLista.MinimizeBox = false;
                frmLista.ShowInTaskbar = false;
                frmLista.ShowIcon = false;
                frmLista.TopMost = true;

                //Tamnho da Tela
                //frmLista.Size = new Size(700, 700);

                ctrlGrid = new DevExpress.XtraGrid.GridControl();
                gridLst = new DevExpress.XtraGrid.Views.Grid.GridView();

                ctrlGrid.Name = "ctrlGrid";
                ctrlGrid.MainView = gridLst;
                gridLst.Name = "Grid";
                gridLst.GridControl = ctrlGrid;

                ctrlGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridLst });
                ctrlGrid.SetBounds(10, 60, iWidth - 20, iHeight - 85);
                ctrlGrid.DataSource = tbDadosLista;
                ctrlGrid.BindingContext = new BindingContext();
                ctrlGrid.ForceInitialize();
                gridLst.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
                gridLst.OptionsView.ColumnAutoWidth = false;

                List<List<Object>> colunas = Conexao.getInstance().toList(
                    " select nr_coluna,ds_titulocoluna,nm_campoinstrsql,nr_larguracampo,ds_alinhamentocampo " +
                    "   from listavalorescolunas " +
                    "  where nr_sequencial = " + inrLista);
                if (colunas != null)
                {
                    if (multiselecao)
                    {
                        RepositoryItemCheckEdit selectdp = new RepositoryItemCheckEdit();
                        gridLst.Columns[0].ColumnEdit = selectdp;
                        selectdp.NullText = "N";
                        selectdp.ValueChecked = "S";
                        selectdp.ValueUnchecked = "N";
                        selectdp.ValueGrayed = "N";

                        gridLst.Columns[0].Width = 25;
                        gridLst.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gridLst.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        //gridLst.Columns[0].AppearanceHeader.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        gridLst.Columns[0].Caption = "";

                    }

                    foreach (List<Object> col in colunas)
                    {
                        Int32 nrColuna = multiselecao ? Convert.ToInt32(col.ElementAt(0)) + 1 : Convert.ToInt32(col.ElementAt(0));
                        gridLst.Columns[nrColuna - 1].Width = Convert.ToInt32(col.ElementAt(3));
                        gridLst.Columns[nrColuna - 1].OptionsColumn.AllowEdit = false;
                        gridLst.Columns[nrColuna - 1].OptionsColumn.ReadOnly = true;
                        gridLst.Columns[nrColuna - 1].Caption = Convert.ToString(col.ElementAt(1));

                        if ((col.ElementAt(4) != null ? col.ElementAt(4).ToString() : "Centralizado") == "Direita")
                        {
                            gridLst.Columns[nrColuna - 1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                            gridLst.Columns[nrColuna - 1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                            gridLst.Columns[nrColuna - 1].AppearanceHeader.Font = new Font("Tahoma", 9, FontStyle.Bold);

                        }
                        if ((col.ElementAt(4) != null ? col.ElementAt(4).ToString() : "Centralizado") == "Esquerda")
                        {
                            gridLst.Columns[nrColuna - 1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                            gridLst.Columns[nrColuna - 1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                            gridLst.Columns[nrColuna - 1].AppearanceHeader.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        }
                        if ((col.ElementAt(4) != null ? col.ElementAt(4).ToString() : "Centralizado") == "Centralizado")
                        {
                            gridLst.Columns[nrColuna - 1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            gridLst.Columns[nrColuna - 1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            gridLst.Columns[nrColuna - 1].AppearanceHeader.Font = new Font("Tahoma", 9, FontStyle.Bold);
                        }
                    }
                }

                gridLst.GroupPanelText = "Arraste o Título das Colunas para Agrupar.";
                gridLst.Appearance.GroupPanel.Font = new Font("Tahoma", 9, FontStyle.Bold);
                gridLst.OptionsSelection.MultiSelect = multiselecao;

                gridLst.GroupPanelText = "Arraste o Título das Colunas para Agrupar.";
                gridLst.Appearance.GroupPanel.Font = new Font("Tahoma", 9, FontStyle.Bold);
                gridLst.OptionsSelection.MultiSelect = multiselecao;

                LabelControl lbFiltro = new LabelControl();
                LabelControl lbInfo = new LabelControl();
                TextEdit txtdsFiltro = new TextEdit();

                lbFiltro.Text = "Filtro:";
                lbFiltro.Font = new Font("Tahoma", 9, FontStyle.Bold);
                lbFiltro.SetBounds(7, 23, 50, 15);


                lbInfo.Font = new Font("Tahoma", 9, FontStyle.Bold);
                lbInfo.Text = "Duplo Clique no Registro para Confirmar a Seleção";
                lbInfo.SetBounds(10, iHeight - 15, 350, 13);

                txtdsFiltro.Font = new Font("Tahoma", 9, FontStyle.Regular);
                txtdsFiltro.SetBounds(60, 20, 300, 20);
                txtdsFiltro.Properties.CharacterCasing = CharacterCasing.Upper;
                txtdsFiltro.TabIndex = 0;
                txtdsFiltro.TextChanged += new EventHandler(dsFiltroChanged);
                txtdsFiltro.KeyDown += new KeyEventHandler(dsFiltroEnter);

                ctrlGrid.TabIndex = 1;
                ctrlGrid.KeyDown += new KeyEventHandler(dsFiltroEnter);
                if (!multiselecao)
                {
                    ctrlGrid.DoubleClick += new EventHandler(SelecionaRegistro);
                }
                frmLista.KeyDown += new KeyEventHandler(Escape);

                SimpleButton btnConfirmar = new SimpleButton();
                btnConfirmar.SetBounds(iWidth - 110, iHeight - 20, 100, 25);
                btnConfirmar.Text = "Confirmar";
                btnConfirmar.Font = new Font(btnConfirmar.Font, FontStyle.Bold);
                btnConfirmar.Font = new Font("Tahoma", 9, FontStyle.Bold);
                btnConfirmar.Click += new EventHandler(SelecionaRegistro);
                btnConfirmar.Image = Properties.Resources.Apply_16x16;

                //Desabilita a opção de agrupamento 
                gridLst.OptionsView.ShowGroupPanel = false;
                frmLista.Controls.AddRange(new Control[] { ctrlGrid, lbFiltro, txtdsFiltro, lbInfo, btnConfirmar });

                frmLista.ShowDialog();
                frmLista.BringToFront();
            }
            catch (Exception erro)
            {
                Alert.erro("Erro: " + erro.Message);
            }

            return objRetorno;
        }

        public ArrayList ListaValores(Int32 inrLista, String idsWhere, String idsnovocabecalho = "")
        {
            return ListaValores(inrLista, idsWhere, false, idsnovocabecalho);
        }

        private void dsFiltroChanged(object sender, EventArgs e)
        {
            try
            {
                string expressao = "";
                string dsOrdenacao = "";
                for (int i = 0; i < tbDadosLista.Columns.Count; i++)
                {
                    string tipo = tbDadosLista.Columns[i].DataType.ToString();
                    if (tbDadosLista.Columns[i].DataType.ToString().Equals("System.Int32"))
                    {
                        try
                        {
                            Int32 nrExpressao = Convert.ToInt32((sender as Control).Text);
                            expressao = String.Format("{0} {1} = {2} or ", expressao, tbDadosLista.Columns[i].ColumnName, nrExpressao);
                            dsOrdenacao = String.Format("{0} {1},", dsOrdenacao, tbDadosLista.Columns[i].ColumnName);
                        }
                        catch { }
                    }
                    if (tbDadosLista.Columns[i].DataType.ToString().Equals("System.String"))
                    {
                        expressao = String.Format("{0} {1} like '%{2}%' or ", expressao, tbDadosLista.Columns[i].ColumnName, (sender as Control).Text);
                        dsOrdenacao = String.Format("{0} {1},", dsOrdenacao, tbDadosLista.Columns[i].ColumnName);
                    }
                }

                expressao = expressao.Substring(0, expressao.Length - 3);
                dsOrdenacao = dsOrdenacao.Substring(0, dsOrdenacao.Length - 1);

                DataTable dt = new DataTable();
                dt = tbDadosLista.Clone();
                DataRow[] drResults = tbDadosLista.Select(expressao, dsOrdenacao);

                foreach (DataRow dr in drResults)
                {
                    object[] row = dr.ItemArray;
                    dt.Rows.Add(row);
                }

                tbDadosFiltro = dt;
                ctrlGrid.DataSource = tbDadosFiltro;
            }
            catch (Exception erro)
            {
                Alert.erro(erro.Message);
            }

        }

        private void dsFiltroEnter(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    ArrayList rows = new ArrayList();
                    for (int i = 0; i < gridLst.SelectedRowsCount; i++)
                    {
                        if (gridLst.GetSelectedRows()[i] >= 0)
                        {
                            rows.Add(gridLst.GetDataRow(gridLst.GetSelectedRows()[i]));
                        }
                    }
                    objRetorno = rows;
                    frmLista.Close();
                }
            }
            catch (Exception erro)
            {
                Alert.erro(erro.Message);
            }
        }

        private void SelecionaRegistro(object sender, EventArgs e)
        {
            try
            {
                ArrayList rows = new ArrayList();
                if (MultiSelecao)
                {
                    for (int i = 0; i < gridLst.RowCount; i++)
                    {
                        if (Convert.ToString(gridLst.GetRowCellValue(i, "X")).Equals("S"))
                        {
                            rows.Add(gridLst.GetDataRow(i));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < gridLst.SelectedRowsCount; i++)
                    {
                        if (gridLst.GetSelectedRows()[i] >= 0)
                            rows.Add(gridLst.GetDataRow(gridLst.GetSelectedRows()[i]));
                    }
                }
                objRetorno = rows;
                frmLista.Close();
            }
            catch (Exception erro)
            {
                Alert.erro(erro.Message);
            }
        }

        private void Escape(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                frmLista.Close();
            }
        }
    }
}
