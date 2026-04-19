using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelosDiscretosyContinuos
{
    public partial class PoissonForm : Form
    {
        private PoissonModel poisson;

    
        private bool mostrandoGraficaAcumulada = false;
        private double[] ultimosValoresX;
        private double[] ultimasProbabilidades;
        private double[] ultimasProbabilidadesAcumuladas;
        private double ultimaLambda;
        public PoissonForm()
        {
            InitializeComponent();
            ConfigurarTabla();
            ConfigurarGrafico();
        }

        private void ConfigurarTabla()
        {
            dgvResultados.Columns.Clear();
            dgvResultados.Columns.Add("x", "x");
            dgvResultados.Columns.Add("P_x", "P(x)");
            dgvResultados.Columns.Add("P_Acumulado", "P(x) Acumulado");
            dgvResultados.Columns.Add("Porcentaje", "%");
            dgvResultados.Columns.Add("Porcentaje_A", "% Acumulado");

            dgvResultados.Columns["P_x"].DefaultCellStyle.Format = "F4";
            dgvResultados.Columns["P_Acumulado"].DefaultCellStyle.Format = "F4";
            dgvResultados.Columns["Porcentaje"].DefaultCellStyle.Format = "F2";
            dgvResultados.Columns["Porcentaje_A"].DefaultCellStyle.Format = "F2";
        }

        private void ConfigurarGrafico()
        {
            formsPlot1.Plot.Title("Distribución Poisson");
            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("Probabilidad P(X=x)");
            formsPlot1.Refresh();
        }

        private bool ValidarLambda(out double lambda)
        {
            lambda = 0;

            bool hayLambda = !string.IsNullOrWhiteSpace(txtLambda.Text);
            bool hayN = !string.IsNullOrWhiteSpace(txtN.Text);
            bool hayP = !string.IsNullOrWhiteSpace(txtP.Text);

            double lambdaInput = 0;
            int n = 0;
            double p = 0;

            if (hayLambda && !double.TryParse(txtLambda.Text, out lambdaInput))
            {
                MessageBox.Show("Lambda debe ser un número válido",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (hayN && (!int.TryParse(txtN.Text, out n) || n <= 0))
            {
                MessageBox.Show("n debe ser un entero positivo",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (hayP && !double.TryParse(txtP.Text, out p))
            {
                MessageBox.Show("p debe ser un número válido",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Solo Lambda
            if (hayLambda && !hayN && !hayP)
            {
                if (lambdaInput <= 0)
                {
                    MessageBox.Show("Lambda debe ser mayor que 0",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                lambda = lambdaInput;
                return true;
            }

            //  Solo n y p
            if (!hayLambda && hayN && hayP)
            {
                if (p >= 0.10 || n * p >= 10)
                {
                    MessageBox.Show(
                        "No se puede aplicar Poisson.\nSe abrirá el modelo Binomial.",
                        "Cambio de modelo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Hide();

                    Form1 formBinomial = new Form1(n, p);
                    formBinomial.ShowDialog();
                    this.Close();

                    return false;
                }

                lambda = n * p;

                txtLambda.Text = lambda.ToString("F4");
                return true;
            }

            // Están los tres
            if (hayLambda && hayN && hayP)
            {
                if (p >= 0.10 || n * p >= 10)
                {
                    MessageBox.Show(
                        "No se puede aplicar Poisson.\nSe abrirá el modelo Binomial.",
                        "Cambio de modelo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.Hide();

                    Form1 formBinomial = new Form1(n, p);
                    formBinomial.ShowDialog();
                    this.Close();

                    return false;
                }

                double lambdaNP = n * p;

                if (Math.Abs(lambdaInput - lambdaNP) > 0.0001)
                {
                    MessageBox.Show("Lambda no coincide con n*p.\nSe usará λ = n*p.",
                        "Ajuste automático", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                lambda = lambdaNP;
                txtLambda.Text = lambda.ToString("F4");
                return true;
            }

            // datos incompletos
            MessageBox.Show("Debe ingresar Lambda o ambos valores n y p.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return false;
        }
        private int ObtenerXMaximo()
        {
            if (string.IsNullOrWhiteSpace(txtN.Text))
                return 20;

            if (!int.TryParse(txtN.Text, out int n) || n <= 0)
                return 20;

            return n;
        }

        private bool ValidarNivelConfianza()
        {
            if (string.IsNullOrWhiteSpace(txtConfianza.Text))
            {
                return false;
            }

            if (!double.TryParse(txtConfianza.Text, out double confianza))
            {
                MessageBox.Show("El nivel de confianza debe ser un número válido",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfianza.Focus();
                return false;
            }

            if (confianza < 0 || confianza > 100)
            {
                MessageBox.Show("El nivel de confianza debe estar entre 0 y 100",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfianza.Focus();
                return false;
            }

            return true;
        }


        private void MarcarFilaSegunConfianza()
        {

            foreach (DataGridViewRow row in dgvResultados.Rows)
            {
                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                row.DefaultCellStyle.Font = new Font(dgvResultados.Font, System.Drawing.FontStyle.Regular);
            }

            if (!ValidarNivelConfianza()) return;

            double confianza = double.Parse(txtConfianza.Text) / 100;

            int filaAMarcar = -1;


            for (int i = 0; i < dgvResultados.Rows.Count; i++)
            {
                DataGridViewRow row = dgvResultados.Rows[i];

                if (row.Cells["Porcentaje_A"].Value != null)
                {
                    double porcentajeAcumulado;
                    if (double.TryParse(row.Cells["Porcentaje_A"].Value.ToString(), out porcentajeAcumulado))
                    {
                        double acumuladoDecimal = porcentajeAcumulado / 100;

                        if (acumuladoDecimal <= confianza)
                        {
                            filaAMarcar = i;
                        }
                    }
                }
            }


            if (filaAMarcar >= 0)
            {

                dgvResultados.Rows[filaAMarcar].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                dgvResultados.Rows[filaAMarcar].DefaultCellStyle.Font =
                    new Font(dgvResultados.Font, System.Drawing.FontStyle.Bold);


                double porcentajeFinal = double.Parse(dgvResultados.Rows[filaAMarcar].Cells["Porcentaje_A"].Value.ToString());
                MostrarInfoConfianza(filaAMarcar, confianza * 100, porcentajeFinal);
            }


        }



        // Mostrar información sobre el nivel de confianza
        private void MostrarInfoConfianza(int filaIndice, double confianzaPorcentaje, double acumuladoPorcentaje)
        {
            DataGridViewRow fila = dgvResultados.Rows[filaIndice];
            string x = fila.Cells["x"].Value.ToString();
            string prob = fila.Cells["P_x"].Value.ToString();
            string acumulado = fila.Cells["Porcentaje_A"].Value.ToString();

            //string mensaje = $"Nivel de confianza: {confianzaPorcentaje}%\n" +
            //                $"Se alcanza en X = {x}\n" +
            //                $"Probabilidad acumulada: {acumulado}%\n" +
            //                $"Interpretación: Con una confianza del {confianzaPorcentaje}%, " +
            //                $"se aceptan hasta {x} defectos en el lote.";

            //// Puedes mostrarlo en un Label adicional o en txtResultados2
            //if (txtResultados2 != null)
            //{
            //    txtResultados2.Text = mensaje;
            //}
        }
        private void txtXFinal_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (!ValidarLambda(out double lambda))
                return;

            int xMax = ObtenerXMaximo();

            int xInicio = -1;
            int xFinal = -1;

            bool hayX = !string.IsNullOrWhiteSpace(txtX.Text);
            bool hayRango = !string.IsNullOrWhiteSpace(txtXinicio.Text) &&
                            !string.IsNullOrWhiteSpace(txtXFinal.Text);

            if (!hayX && !hayRango)
            {
                MessageBox.Show("Debe ingresar un valor de X o un rango de X.",
                    "Dato requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

    
            if (hayX && hayRango)
            {
                MessageBox.Show("No puede usar X exacta y rango al mismo tiempo.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // X única
            if (hayX)
            {
                if (!int.TryParse(txtX.Text, out int xUnico) || xUnico < 0)
                {
                    MessageBox.Show("X debe ser un entero no negativo",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                xInicio = xUnico;
                xFinal = xUnico;
            }

            // Rango
            if (hayRango)
            {
                if (!int.TryParse(txtXinicio.Text, out xInicio) || xInicio < 0 ||
                    !int.TryParse(txtXFinal.Text, out xFinal) || xFinal < 0)
                {
                    MessageBox.Show("Ingrese valores válidos para el rango",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (xInicio > xFinal)
                {
                    MessageBox.Show("X Inicio no puede ser mayor que X Final",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            CalcularPoisson(lambda, xInicio, xFinal, xMax);
        }


        private void CalcularPoisson(double lambda, int xInicio, int xFinal, int xMax)
        {
            dgvResultados.Rows.Clear();
            formsPlot1.Plot.Clear();

            poisson = new PoissonModel(lambda);
            ultimaLambda = lambda;

            var lista = poisson.CalcularRango(xInicio, xFinal);

  
            var listaCompleta = poisson.CalcularRango(0, xMax);

            double acumuladoTabla = 0;
            double probabilidadRango = 0;

            List<double> valoresXGrafica = new List<double>();
            List<double> probabilidadesGrafica = new List<double>();


            foreach (var item in lista)
            {
                acumuladoTabla = item.acumulado;
                probabilidadRango += item.prob;

                dgvResultados.Rows.Add(
                    item.x,
                    item.prob.ToString("F4"),
                    item.acumulado.ToString("F4"),
                    (item.prob * 100).ToString("F2"),
                    (item.acumulado * 100).ToString("F2")
                );
            }

            foreach (var item in listaCompleta)
            {
                valoresXGrafica.Add(item.x);
                probabilidadesGrafica.Add(item.prob);
            }


            ultimasProbabilidadesAcumuladas = new double[probabilidadesGrafica.Count];
            double acum = 0;
            for (int i = 0; i < probabilidadesGrafica.Count; i++)
            {
                acum += probabilidadesGrafica[i];
                ultimasProbabilidadesAcumuladas[i] = acum;
            }

            txtResultados.Clear();
            txtResultados.Text =
                $"Lambda (λ) = {lambda:F4}{Environment.NewLine}" +
                $"Media = {lambda:F2}{Environment.NewLine}" +
                $"Varianza = {lambda:F2}{Environment.NewLine}" +
                $"Desviación Estándar = {Math.Sqrt(lambda):F4}{Environment.NewLine}" +
                $"Sesgo = {1 / Math.Sqrt(lambda):F4}{Environment.NewLine}" +
                $"Curtosis = {1 / lambda:F4}{Environment.NewLine}" +
                $"{Environment.NewLine}Probabilidad del rango = {probabilidadRango:F4}" +
                $"{Environment.NewLine}Porcentaje = {(probabilidadRango * 100):F2}%";

            double sesgo = 1 / Math.Sqrt(lambda);
            double curtosis = 1 / lambda;
            MostrarMensajesForma(sesgo, curtosis);
            

            GraficarSimple(valoresXGrafica.ToArray(), probabilidadesGrafica.ToArray(), lambda);

            MarcarFilaSegunConfianza();
        }

        private void MostrarMensajesForma(double sesgo, double curtosis)
        {
            string mensajeSesgo = "";
            string mensajeCurtosis = "";

            if (sesgo < 0)
                mensajeSesgo = "Sesgo Negativo: Curva con asimetría a la izquierda";
            else if (sesgo == 0)
                mensajeSesgo = "Sesgo neutro: Curva simétrica";
            else
                mensajeSesgo = "Sesgo Positivo: Curva con asimetría a la derecha";

            if (curtosis < 0)
                mensajeCurtosis = "Curtosis Platicúrtica (Curva aplanada)";
            else if (curtosis == 0)
                mensajeCurtosis = "Curtosis Mesocúrtica (Campana de Gauss)";
            else
                mensajeCurtosis = "Curtosis Leptocúrtica (Curva elevada)";

            txtResultados2.Text = $"{mensajeSesgo}{Environment.NewLine}{Environment.NewLine}{mensajeCurtosis}";
        }

        private void GraficarSimple(double[] valoresX, double[] probabilidades, double lambda)
        {
       
            ultimosValoresX = valoresX;
            ultimasProbabilidades = probabilidades;
            mostrandoGraficaAcumulada = false;
            btnCambiarGrafica.Text = "Ver Gráfica Acumulada";

            formsPlot1.Plot.Clear();

            double[] porcentajes = probabilidades.Select(x => x * 100).ToArray();

            formsPlot1.Plot.Add.Bars(valoresX, porcentajes);

            for (int i = 0; i < valoresX.Length; i++)
            {
                var texto = formsPlot1.Plot.Add.Text(
                    $"{porcentajes[i]:0.00}",
                    valoresX[i],
                    porcentajes[i] + 0.8
                );
                texto.LabelFontSize = 11;
                texto.Alignment = Alignment.LowerCenter;
            }

            formsPlot1.Plot.Title($"Distribución Poisson (λ={lambda:F2})");
            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("p %");

            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();
        }

        private void GraficarAcumulado()
        {
            if (ultimosValoresX == null || ultimasProbabilidadesAcumuladas == null) return;

            formsPlot1.Plot.Clear();

            double[] porcentajesAcum = ultimasProbabilidadesAcumuladas.Select(x => x * 100).ToArray();

            formsPlot1.Plot.Add.Bars(ultimosValoresX, porcentajesAcum);

            for (int i = 0; i < ultimosValoresX.Length; i++)
            {
                var texto = formsPlot1.Plot.Add.Text(
                    $"{porcentajesAcum[i]:0.00}",
                    ultimosValoresX[i],
                    porcentajesAcum[i] + 0.8
                );
                texto.LabelFontSize = 11;
                texto.Alignment = Alignment.LowerCenter;
            }

            formsPlot1.Plot.Title($"Distribución Poisson ACUMULADA (λ={ultimaLambda:F2})");
            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("Probabilidad Acumulada %");

            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();
        }

        private void btnCambiarGrafica_Click(object sender, EventArgs e)
        {
            if (ultimosValoresX == null) return;

            if (mostrandoGraficaAcumulada)
            {
                GraficarSimple(ultimosValoresX, ultimasProbabilidades, ultimaLambda);
                btnCambiarGrafica.Text = "Ver Gráfica Acumulada";
                mostrandoGraficaAcumulada = false;
            }
            else
            {
                GraficarAcumulado();
                btnCambiarGrafica.Text = "Ver Gráfica Normal";
                mostrandoGraficaAcumulada = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtLambda.Clear();
            txtN.Clear();
            txtX.Clear();
            txtXinicio.Clear();
            txtXFinal.Clear();
            txtResultados.Clear();
            txtResultados2.Clear();
            txtP.Clear();

            dgvResultados.Rows.Clear();
            formsPlot1.Plot.Clear();
            ConfigurarGrafico();

            ultimosValoresX = null;
            ultimasProbabilidades = null;
            ultimasProbabilidadesAcumuladas = null;
            mostrandoGraficaAcumulada = false;
       
        }

        private void PoissonForm_Load(object sender, EventArgs e)
        {

        }
    }
}
