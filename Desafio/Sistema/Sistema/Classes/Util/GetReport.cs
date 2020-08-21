using FastReport;
using Npgsql;
using Sistema.Janelas.Ferramentas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Sistema.Classes.Util
{
    public class GetReport
    {
        internal static DataTable createDataTableFromSQL(string itablename, string vsql, string sortColumn = "")
        {
            DataTable vret = null;
            if (itablename == null || vsql == null)
                return null;

            NpgsqlDataReader ireader;
            vret = new DataTable(itablename);
            ireader = new NpgsqlCommand(vsql, Conexao.getInstance().getConnection(), Conexao.getInstance().getTransaction()).ExecuteReader();

            for (int i = 0; i < ireader.FieldCount; i++)
            {
                DataColumn col = new DataColumn() { ColumnName = ireader.GetName(i), DataType = ireader.GetFieldType(i) };
                vret.Columns.Add(col);
            }
            if (ireader.HasRows)
            {
                while (ireader.Read())
                {
                    DataRow dr = vret.NewRow();
                    for (int i = 0; i < ireader.FieldCount; i++)
                    {
                        dr[ireader.GetName(i).ToString()] = ireader.GetValue(i);
                    }
                    vret.Rows.Add(dr);
                }
            }
            ireader.Close();
            ireader.Dispose();

            if (sortColumn.Trim().Length > 0)
            {
                vret.DefaultView.Sort = sortColumn;
                vret = vret.DefaultView.ToTable();
            }
            return vret;
        }

        public static Report loadReport(string irelatorioname)
        {
            Report report = new Report();
            try
            {
                report.Load(String.Format("{0}\\Relatorios\\{1}", Ficheiro.getLocalExecutavel(), irelatorioname));
            }
            catch (Exception ex)
            {
                Alert.erro("Erro ao gerar o relatorio \n" + ex.Message);
            }
            return report;
        }

        public static void buildReport(string itablename, string vsql, Report report)
        {
            try
            {
                DataTable tab = createDataTableFromSQL(itablename, vsql);
                report.RegisterData(tab, itablename);
                report.GetDataSource(itablename).Enabled = true;
            }
            catch (Exception erro)
            {
                Alert.erro("Erro ao gerar o relatorio \n" + erro.Message);
            }
        }

        public static void abreVisualizador(Report report, Boolean abreEditor = false)
        {
            using (frmVisualizador visualiza = new frmVisualizador() { relatorio = report })
            {
                if (abreEditor)
                {
                    report.Design();
                }
                visualiza.ShowDialog();
            }
        }

        public static void imprimeDireto(Report report)
        {
            report.PrintSettings.ShowDialog = false;
            report.Print();
        }
    }
}

