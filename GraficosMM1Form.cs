using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ModelosDiscretosyContinuos
{
    public partial class GraficosMM1Form : Form
    {
        private Chart chartVsLambda;
        private Chart chartVsMu;
        private double _PromedioLlegadas;
        private double _PromedioServicio;
        private bool _entradaEnMinutos;
        private bool _salidaEnMinutos;
        public GraficosMM1Form(double promedioLlegadas, double promedioServicio, bool entradaEnMinutos, bool salidaEnMinutos)
        {
            InitializeComponent();
            _PromedioLlegadas = promedioLlegadas;
            _PromedioServicio = promedioServicio;
            _entradaEnMinutos = entradaEnMinutos;
            _salidaEnMinutos = salidaEnMinutos;
            ConfigurarFormulario();
            CrearGraficos();
            GenerarGraficos();
        }
        private void ConfigurarFormulario()
        {
            this.Text = "Gráficas del Sistema MM1 - Variación de λ y μ";
            this.Size = new Size(1000, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.MinimumSize = new Size(900, 650);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
        }

        private void CrearGraficos()
        {
            // ========== GRÁFICO 1: Variación de λ (tasa de llegada) ==========
            chartVsLambda = new Chart();
            chartVsLambda.Size = new Size(this.ClientSize.Width - 40, 320);
            chartVsLambda.Location = new Point(10, 10);
            chartVsLambda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            chartVsLambda.BackColor = Color.White;

            ChartArea chartAreaLambda = new ChartArea();
            chartAreaLambda.AxisX.Title = "Tasa de llegada (λ)";
            chartAreaLambda.AxisX.TitleFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            chartAreaLambda.AxisY.Title = "Valor";
            chartAreaLambda.AxisY.TitleFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            chartAreaLambda.BackColor = Color.WhiteSmoke;
            chartVsLambda.ChartAreas.Add(chartAreaLambda);

            // Series para gráfico vs λ
            Series serieLsLambda = new Series("Ls (Clientes en sistema)");
            serieLsLambda.ChartType = SeriesChartType.Line;
            serieLsLambda.BorderWidth = 3;
            serieLsLambda.Color = Color.Blue;
            serieLsLambda.MarkerStyle = MarkerStyle.Circle;
            serieLsLambda.MarkerSize = 6;

            Series serieLqLambda = new Series("Lq (Clientes en cola)");
            serieLqLambda.ChartType = SeriesChartType.Line;
            serieLqLambda.BorderWidth = 3;
            serieLqLambda.Color = Color.Purple;
            serieLqLambda.MarkerStyle = MarkerStyle.Triangle;
            serieLqLambda.MarkerSize = 6;

            Series serieWqLambda = new Series("Wq (Tiempo espera cola)");
            serieWqLambda.ChartType = SeriesChartType.Line;
            serieWqLambda.BorderWidth = 3;
            serieWqLambda.Color = Color.Red;
            serieWqLambda.MarkerStyle = MarkerStyle.Square;
            serieWqLambda.MarkerSize = 6;

            Series serieWsLambda = new Series("Ws (Tiempo espera sistema)");
            serieWsLambda.ChartType = SeriesChartType.Line;
            serieWsLambda.BorderWidth = 3;
            serieWsLambda.Color = Color.Green;
            serieWsLambda.MarkerStyle = MarkerStyle.Diamond;
            serieWsLambda.MarkerSize = 6;

            chartVsLambda.Series.Add(serieLsLambda);
            chartVsLambda.Series.Add(serieLqLambda);
            chartVsLambda.Series.Add(serieWqLambda);
            chartVsLambda.Series.Add(serieWsLambda);

            Title tituloLambda = new Title("Comportamiento del sistema VARIANDO λ");
            tituloLambda.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            chartVsLambda.Titles.Add(tituloLambda);

            Legend legendLambda = new Legend();
            legendLambda.Docking = Docking.Bottom;
            legendLambda.Font = new Font("Microsoft Sans Serif", 8);
            chartVsLambda.Legends.Add(legendLambda);

            this.Controls.Add(chartVsLambda);

            // ========== GRÁFICO 2: Variación de μ (tasa de servicio) ==========
            chartVsMu = new Chart();
            chartVsMu.Size = new Size(this.ClientSize.Width - 40, 320);
            chartVsMu.Location = new Point(10, 350);
            chartVsMu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            chartVsMu.BackColor = Color.White;

            ChartArea chartAreaMu = new ChartArea();
            chartAreaMu.AxisX.Title = "Tasa de servicio (μ)";
            chartAreaMu.AxisX.TitleFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            chartAreaMu.AxisY.Title = "Valor";
            chartAreaMu.AxisY.TitleFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            chartAreaMu.BackColor = Color.WhiteSmoke;
            chartVsMu.ChartAreas.Add(chartAreaMu);

            // Series para gráfico vs μ
            Series serieLsMu = new Series("Ls (Clientes en sistema)");
            serieLsMu.ChartType = SeriesChartType.Line;
            serieLsMu.BorderWidth = 3;
            serieLsMu.Color = Color.Blue;
            serieLsMu.MarkerStyle = MarkerStyle.Circle;
            serieLsMu.MarkerSize = 6;

            Series serieLqMu = new Series("Lq (Clientes en cola)");
            serieLqMu.ChartType = SeriesChartType.Line;
            serieLqMu.BorderWidth = 3;
            serieLqMu.Color = Color.Purple;
            serieLqMu.MarkerStyle = MarkerStyle.Triangle;
            serieLqMu.MarkerSize = 6;

            Series serieWqMu = new Series("Wq (Tiempo espera cola)");
            serieWqMu.ChartType = SeriesChartType.Line;
            serieWqMu.BorderWidth = 3;
            serieWqMu.Color = Color.Red;
            serieWqMu.MarkerStyle = MarkerStyle.Square;
            serieWqMu.MarkerSize = 6;

            Series serieWsMu = new Series("Ws (Tiempo espera sistema)");
            serieWsMu.ChartType = SeriesChartType.Line;
            serieWsMu.BorderWidth = 3;
            serieWsMu.Color = Color.Green;
            serieWsMu.MarkerStyle = MarkerStyle.Diamond;
            serieWsMu.MarkerSize = 6;

            chartVsMu.Series.Add(serieLsMu);
            chartVsMu.Series.Add(serieLqMu);
            chartVsMu.Series.Add(serieWqMu);
            chartVsMu.Series.Add(serieWsMu);

            Title tituloMu = new Title("Comportamiento del sistema VARIANDO μ");
            tituloMu.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            chartVsMu.Titles.Add(tituloMu);

            Legend legendMu = new Legend();
            legendMu.Docking = Docking.Bottom;
            legendMu.Font = new Font("Microsoft Sans Serif", 8);
            chartVsMu.Legends.Add(legendMu);

            this.Controls.Add(chartVsMu);
        }

        private void GenerarGraficos()
        {
            if (_PromedioLlegadas > 0 && _PromedioServicio > 0 && _PromedioLlegadas < _PromedioServicio)
            {
                GenerarGraficoVsLambda();
                GenerarGraficoVsMu();
            }
        }

        private void GenerarGraficoVsLambda()
        {
            try
            {
                chartVsLambda.Series["Ls (Clientes en sistema)"].Points.Clear();
                chartVsLambda.Series["Lq (Clientes en cola)"].Points.Clear();
                chartVsLambda.Series["Wq (Tiempo espera cola)"].Points.Clear();
                chartVsLambda.Series["Ws (Tiempo espera sistema)"].Points.Clear();

                double lambdaBase = _PromedioLlegadas;
                double muBase = _PromedioServicio;

                if (_entradaEnMinutos)
                {
                    lambdaBase = _PromedioLlegadas * 60;
                    muBase = _PromedioServicio * 60;
                }

                double mu = muBase;
                double paso = mu / 20;

                string unidad = _salidaEnMinutos ? "clientes/min" : "clientes/hora";
                double muMostrar = _salidaEnMinutos ? mu / 60 : mu;

                for (double lambda = 0.1; lambda < mu; lambda += paso)
                {
                    if (lambda >= mu) break;

                    double rho = lambda / mu;
                    double ls = lambda / (mu - lambda);
                    double lq = ls - rho;
                    double wq = lambda / (mu * (mu - lambda));
                    double ws = 1 / (mu - lambda);

                    double lambdaMostrar = _salidaEnMinutos ? lambda / 60 : lambda;

                    chartVsLambda.Series["Ls (Clientes en sistema)"].Points.AddXY(Math.Round(lambdaMostrar, 4), ls);
                    chartVsLambda.Series["Lq (Clientes en cola)"].Points.AddXY(Math.Round(lambdaMostrar, 4), lq);
                    chartVsLambda.Series["Wq (Tiempo espera cola)"].Points.AddXY(Math.Round(lambdaMostrar, 4), wq);
                    chartVsLambda.Series["Ws (Tiempo espera sistema)"].Points.AddXY(Math.Round(lambdaMostrar, 4), ws);
                }

                chartVsLambda.Titles[0].Text = $"Comportamiento variando λ (μ fijo = {Math.Round(muMostrar, 4)} {unidad})";
                chartVsLambda.ChartAreas[0].AxisX.Title = $"Tasa de llegada (λ) en {unidad}";
                chartVsLambda.ChartAreas[0].AxisY.Minimum = 0;
                chartVsLambda.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar gráfico vs λ: {ex.Message}", "Error");
            }
        }

        private void GenerarGraficoVsMu()
        {
            try
            {
                chartVsMu.Series["Ls (Clientes en sistema)"].Points.Clear();
                chartVsMu.Series["Lq (Clientes en cola)"].Points.Clear();
                chartVsMu.Series["Wq (Tiempo espera cola)"].Points.Clear();
                chartVsMu.Series["Ws (Tiempo espera sistema)"].Points.Clear();

                double lambdaBase = _PromedioLlegadas;
                if (_entradaEnMinutos)
                    lambdaBase = _PromedioLlegadas * 60;

                double lambda = lambdaBase;
                double muMin = lambda + 0.1;
                double muMax = lambda * 5;
                double paso = (muMax - muMin) / 20;

                string unidad = _salidaEnMinutos ? "clientes/min" : "clientes/hora";
                double lambdaMostrar = _salidaEnMinutos ? lambda / 60 : lambda;

                for (double mu = muMin; mu <= muMax; mu += paso)
                {
                    if (mu <= lambda) continue;

                    double rho = lambda / mu;
                    double ls = lambda / (mu - lambda);
                    double lq = ls - rho;
                    double wq = lambda / (mu * (mu - lambda));
                    double ws = 1 / (mu - lambda);

                    double muMostrar = _salidaEnMinutos ? mu / 60 : mu;

                    chartVsMu.Series["Ls (Clientes en sistema)"].Points.AddXY(Math.Round(muMostrar, 4), ls);
                    chartVsMu.Series["Lq (Clientes en cola)"].Points.AddXY(Math.Round(muMostrar, 4), lq);
                    chartVsMu.Series["Wq (Tiempo espera cola)"].Points.AddXY(Math.Round(muMostrar, 4), wq);
                    chartVsMu.Series["Ws (Tiempo espera sistema)"].Points.AddXY(Math.Round(muMostrar, 4), ws);
                }

                chartVsMu.Titles[0].Text = $"Comportamiento variando μ (λ fijo = {Math.Round(lambdaMostrar, 4)} {unidad})";
                chartVsMu.ChartAreas[0].AxisX.Title = $"Tasa de servicio (μ) en {unidad}";
                chartVsMu.ChartAreas[0].AxisY.Minimum = 0;
                chartVsMu.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar gráfico vs μ: {ex.Message}", "Error");
            }
        }

        private void GraficosMM1Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (chartVsLambda != null)
            {
                chartVsLambda.Series.Clear();
                chartVsLambda.Dispose();
            }
            if (chartVsMu != null)
            {
                chartVsMu.Series.Clear();
                chartVsMu.Dispose();
            }
        }

        private void GraficosMM1Form_Load(object sender, EventArgs e)
        {

        }
    }
}
