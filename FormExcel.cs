using Microsoft.Office.Interop.Excel;
using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;

using System.Windows.Forms.DataVisualization.Charting;

namespace ModelosDiscretosyContinuos
{
    public partial class FormExcel : Form
    {

        private int N;
        private int K;
        private int n;

        private HipergeometricaModel hiper;

        DataGridView dgv;
        System.Windows.Forms.DataVisualization.Charting.Chart chart;
        System.Windows.Forms.Button btnExportar;

        public FormExcel(int N, int K, int n)
        {
            this.N = N;
            this.K = K;
            this.n = n;

            hiper = new HipergeometricaModel(N, K, n);

            InitializeComponent();
            CrearComponentes();
            GenerarTablaYGrafica();
        }

        private void CrearComponentes()
        {
            this.Width = 950;
            this.Height = 600;
            this.Text = "Tabla y Gráfica Hipergeométrica";

            dgv = new DataGridView();
            dgv.Width = 400;
            dgv.Height = 450;
            dgv.Left = 10;
            dgv.Top = 10;
            dgv.ColumnCount = 3;

            dgv.Columns[0].Name = "X";
            dgv.Columns[1].Name = "P(X)";
            dgv.Columns[2].Name = "Acumulada";

            this.Controls.Add(dgv);

            // Chart
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart.Width = 450;
            chart.Height = 450;
            chart.Left = 450;
            chart.Top = 10;

            System.Windows.Forms.DataVisualization.Charting.ChartArea area = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            chart.ChartAreas.Add(area);

            System.Windows.Forms.DataVisualization.Charting.Series serie = new System.Windows.Forms.DataVisualization.Charting.Series();
            serie.ChartType = SeriesChartType.Column;
            serie.Name = "Probabilidad";

            chart.Series.Add(serie);

            this.Controls.Add(chart);

            // =exportar
            btnExportar = new System.Windows.Forms.Button();
            btnExportar.Text = "Exportar a Excel";
            btnExportar.Width = 200;
            btnExportar.Height = 40;
            btnExportar.Top = 480;
            btnExportar.Left = 10;

            btnExportar.Click += BtnExportar_Click;

            this.Controls.Add(btnExportar);
        }

        private void GenerarTablaYGrafica()
        {
            dgv.Rows.Clear();
            chart.Series[0].Points.Clear();

            double acumulada = 0;

            int xmin = Math.Max(0, n - (N - K));
            int xmax = Math.Min(n, K);

            for (int x = xmin; x <= xmax; x++)
            {
                double prob = hiper.Probabilidad(x);
                acumulada += prob;

                dgv.Rows.Add(
                    x,
                    prob.ToString("F4"),
                    acumulada.ToString("F4")
                );


                chart.Series[0].Points.AddXY(x, prob);
            }
        }



        private void BtnExportar_Click(object sender, EventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb = excel.Workbooks.Add();
            Excel.Worksheet ws = wb.ActiveSheet;

            ws.Cells[1, 1] = "X";
            ws.Cells[1, 2] = "P(X)";
            ws.Cells[1, 3] = "Acumulada";

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells[0].Value != null)
                {
                    ws.Cells[i + 2, 1] = dgv.Rows[i].Cells[0].Value;
                    ws.Cells[i + 2, 2] = dgv.Rows[i].Cells[1].Value;
                    ws.Cells[i + 2, 3] = dgv.Rows[i].Cells[2].Value;
                }
            }

            excel.Visible = true;
        }

        private void FormExcel_Load(object sender, EventArgs e)
        {

        }
    }
}
