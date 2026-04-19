using MathNet.Numerics.Distributions;
using ScottPlot.MultiplotLayouts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;

namespace ModelosDiscretosyContinuos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            configurarTabla();
            ConfigurarGrafico();
        }

        public Form1(int n, double p)
        {
            InitializeComponent();
            configurarTabla();
            ConfigurarGrafico();
            nRecibido = n;
            pRecibido = p;
        }
        private BinomialModel binomial = new BinomialModel();

        private bool mostrandoGraficaAcumulada = false;

        private double[] ultimosValoresX;
        private double[] ultimasProbabilidades;
        private double[] ultimasProbabilidadesAcumuladas;
        private int ultimoN;
        private double ultimaP;
        private int ultimoNHiper;
        private int ultimoKHiper;
        private bool esModoBinomial = true;
        private int nRecibido;
        private double pRecibido;


        private void ConfigurarGrafico()
        {
            formsPlot1.Plot.Title("Distribución Binomial", size: 14);
            formsPlot1.Plot.XLabel("Número de éxitos (k)", size: 11);
            formsPlot1.Plot.YLabel("Probabilidad P(X=k)", size: 11);
            formsPlot1.Refresh();
        }
        private void configurarTabla()
        {
            dgvResultados.Columns.Clear();
            dgvResultados.Columns.Add("x", "x");
            dgvResultados.Columns.Add("P_x", "P(x)");
            dgvResultados.Columns.Add("P_Acumulado", "P(x)Acumulado");
            dgvResultados.Columns.Add("Porcentaje", "%");
            dgvResultados.Columns.Add("Porcentaje_A", "% Acumulado");


            dgvResultados.Columns["P_x"].DefaultCellStyle.Format = "F4";
            dgvResultados.Columns["P_Acumulado"].DefaultCellStyle.Format = "F4";
            dgvResultados.Columns["Porcentaje"].DefaultCellStyle.Format = "F2";
            dgvResultados.Columns["Porcentaje_A"].DefaultCellStyle.Format = "F2";



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

        private void CalcularProbabilidadFinita(int n, double p, int N, int xInicio, int xFinal)
        {
            dgvResultados.Rows.Clear();
            formsPlot1.Plot.Clear();


            var resultado = this.binomial.CalcularProbabilidad(n, p, xInicio, xFinal);


            var resultadoCompleto = this.binomial.CalcularProbabilidad(n, p, 0, n);

            var binomial = resultado.binomial;
            var valoresX = resultado.valoresX;
            var probabilidades = resultado.probabilidades;
            var probabilidadRango = resultado.probabilidadRango;

            double acumuladoTabla = 0;
            for (int i = 0; i < valoresX.Length; i++)
            {
                acumuladoTabla += probabilidades[i];
                dgvResultados.Rows.Add(
                    valoresX[i],
                    probabilidades[i].ToString("F4"),
                    acumuladoTabla.ToString("F4"),
                    (probabilidades[i] * 100).ToString("F2"),
                    (acumuladoTabla * 100).ToString("F2")
                );
            }

            double fc = this.binomial.CalcularCorreccionPoblacionFinita(n, N);
            double curtosis = this.binomial.CalcularCurtosis(n, p);
            double sesgo = this.binomial.CalcularSesgo(n, p);

            MostrarResultados(binomial, curtosis, sesgo, fc);

            txtResultados.Text +=
                $"{Environment.NewLine}Probabilidad del rango = {probabilidadRango:F4}" +
                $"{Environment.NewLine}Porcentaje = {(probabilidadRango * 100):F2}%";

    
            GraficarSimple(resultadoCompleto.valoresX, resultadoCompleto.probabilidades, n, p);

            ultimasProbabilidadesAcumuladas = new double[valoresX.Length];
            double acum = 0;
            for (int i = 0; i < valoresX.Length; i++)
            {
                acum += probabilidades[i];
                ultimasProbabilidadesAcumuladas[i] = acum;
            }
     
            MarcarFilaSegunConfianza();
        }

        private void CalcularProbabilidadInfinita(int n, double p, int xInicio, int xFinal)
        {
            dgvResultados.Rows.Clear();
            formsPlot1.Plot.Clear();

            var resultado = this.binomial.CalcularProbabilidad(n, p, xInicio, xFinal);

            var resultadoCompleto = this.binomial.CalcularProbabilidad(n, p, 0, n);

            var binomial = resultado.binomial;
            var valoresX = resultado.valoresX;
            var probabilidades = resultado.probabilidades;
            var probabilidadRango = resultado.probabilidadRango;

            // Mostrar tabla (solo rango)
            double acumuladoTabla = 0;
            for (int i = 0; i < valoresX.Length; i++)
            {
                acumuladoTabla += probabilidades[i];
                dgvResultados.Rows.Add(
                    valoresX[i],
                    probabilidades[i].ToString("F4"),
                    acumuladoTabla.ToString("F4"),
                    (probabilidades[i] * 100).ToString("F2"),
                    (acumuladoTabla * 100).ToString("F2")
                );
            }

            double curtosis = this.binomial.CalcularCurtosis(n, p);
            double sesgo = this.binomial.CalcularSesgo(n, p);

            MostrarResultados(binomial, curtosis, sesgo);

            txtResultados.Text +=
                $"{Environment.NewLine}Probabilidad del rango = {probabilidadRango:F4}" +
                $"{Environment.NewLine}Porcentaje = {(probabilidadRango * 100):F2}%";

      
            GraficarSimple(resultadoCompleto.valoresX, resultadoCompleto.probabilidades, n, p);

       
            ultimasProbabilidadesAcumuladas = new double[valoresX.Length];
            double acum = 0;
            for (int i = 0; i < valoresX.Length; i++)
            {
                acum += probabilidades[i];
                ultimasProbabilidadesAcumuladas[i] = acum;
            }
        
            MarcarFilaSegunConfianza();
        }
        private void CalcularHipergeometrica(int N, int K, int n, int xInicio, int xFinal)
        {
            dgvResultados.Rows.Clear();
            formsPlot1.Plot.Clear();

            HipergeometricaModel hiper = new HipergeometricaModel(N, K, n);

            var lista = hiper.CalcularRango(xInicio, xFinal);

            
            int xMin = Math.Max(0, n - (N - K));
            int xMax = Math.Min(n, K);
            var listaCompleta = hiper.CalcularRango(xMin, xMax);

            double acumuladoTabla = 0;
            double probabilidadRango = 0;

            List<double> valoresXTabla = new List<double>();
            List<double> probabilidadesTabla = new List<double>();
            List<double> valoresXGrafica = new List<double>();
            List<double> probabilidadesGrafica = new List<double>();

            foreach (var item in lista)
            {
                acumuladoTabla += item.prob;
                probabilidadRango += item.prob;

                dgvResultados.Rows.Add(
                    item.x,
                    item.prob.ToString("F4"),
                    acumuladoTabla.ToString("F4"),
                    (item.prob * 100).ToString("F2"),
                    (acumuladoTabla * 100).ToString("F2")
                );

                valoresXTabla.Add(item.x);
                probabilidadesTabla.Add(item.prob);
            }

          
            foreach (var item in listaCompleta)
            {
                valoresXGrafica.Add(item.x);
                probabilidadesGrafica.Add(item.prob);
            }

            double media = hiper.Media();
            double desviacion = hiper.DesviacionEstandar();
            double sesgo = hiper.Sesgo();
            double curtosis = hiper.Curtosis();

            txtResultados.Clear();
            txtResultados.Text =
                $"Media= {media:F2}{Environment.NewLine}" +
                $"Desviación Estándar= {desviacion:F2}{Environment.NewLine}" +
                $"Curtosis= {curtosis:F4}{Environment.NewLine}" +
                $"Sesgo= {sesgo:F4}{Environment.NewLine}" +
                $"{Environment.NewLine}Probabilidad del rango= {probabilidadRango:F4}" +
                $"{Environment.NewLine}Porcentaje= {(probabilidadRango * 100):F2}%";

            MostrarMensajesForma(sesgo, curtosis);

         
            GraficarHiper(valoresXGrafica.ToArray(), probabilidadesGrafica.ToArray(), N, K, n);

       
            ultimasProbabilidadesAcumuladas = new double[valoresXGrafica.Count];
            double acum = 0;
            for (int i = 0; i < valoresXGrafica.Count; i++)
            {
                acum += probabilidadesGrafica[i];
                ultimasProbabilidadesAcumuladas[i] = acum;
            }
     
            MarcarFilaSegunConfianza();
        }

        private void MostrarMensajesForma(double sesgo, double curtosis)
        {
            string MensajeSesgo = "";
            string MensajeCurtosis = "";

            if (sesgo < 0)
                MensajeSesgo = "Sesgo Negativo: Curva con asimetría a la izquierda";
            else if (sesgo == 0)
                MensajeSesgo = "Sesgo neutro: Curva simétrica";
            else
                MensajeSesgo = "Sesgo Positivo: Curva con asimetría a la derecha";

            if (curtosis < 0)
                MensajeCurtosis = "Curtosis Platicúrtica (Curva aplanada)";
            else if (curtosis == 0)
                MensajeCurtosis = "Curtosis Mesocúritca (Campana de Gauss)";
            else
                MensajeCurtosis = "Curtosis Leptocúrtica (Curva elevada)";

            txtResultados2.Text =
                $"{MensajeSesgo}{Environment.NewLine}{Environment.NewLine}" +
                $"{MensajeCurtosis}";
        }

        private void MostrarResultados(Binomial binomial, double curtosis, double sesgo, double correccion=0)
        {
            txtResultados.Clear();
            txtResultados.Clear();
            if (correccion == 0)
            {
                string resultados = $"Media= {binomial.Mean:F2}{Environment.NewLine}" +
                $"Desviación Estándar= {binomial.StdDev:F2}{Environment.NewLine}"
                + $"Curtosis= {curtosis}{Environment.NewLine}"
                + $"Sesgo= {sesgo}{Environment.NewLine}";
                txtResultados.Text = resultados;

            }
            else
            {
                string resultados = $"Media= {binomial.Mean:F2}{Environment.NewLine}" +
                $"Desviación Estándar= {(correccion*binomial.StdDev):F2}{Environment.NewLine}"
                + $"Curtosis= {curtosis}{Environment.NewLine}"
                + $"Sesgo= {sesgo}{Environment.NewLine}"
                + $"Factor de corrección= {correccion:F3}{Environment.NewLine}";

                txtResultados.Text = resultados;


            }

            string MensajeSesgo = "";
            string MensajeCurtosis = "";


            if (sesgo<0)
            {
                MensajeSesgo ="Sesgo Negativo:Curva con asimetría a la izquierda";
            }
            else if (sesgo == 0)
            {
                MensajeSesgo = "Sesgo neutro: Curva simétrica";
            }
            else
            {
                MensajeSesgo = "Sesgo Positivo: Curva con asimetría a la derecha";
            }


            if (curtosis < 0)
            {
                MensajeCurtosis = "Curtosis Platicúrtica (Curva aplanada)";
            }
            else if (curtosis == 0)
            {
                MensajeCurtosis = "Curtosis Mesocúritca (Campana de Gauss)";
            }
            else
            {
                MensajeCurtosis= "Curtosis Leptocúrtica (Curva elevada)";
            }

            txtResultados2.Text = $"{MensajeSesgo} {Environment.NewLine} {Environment.NewLine}"+
                $"{MensajeCurtosis} {Environment.NewLine}";
            

        }
       


        private bool EsPoblacionIfinita(int n, int N)
        {
            if (N == 0) return false;

            return (((double)n / N) >= 0.05);           
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            int n;
            double p = 0;
            int N = 0;
            int K = 0;

            int xInicio = 0;
            int xFinal = 0;
            int xUnico = -1;

            if (string.IsNullOrEmpty(txtN.Text))
            {
                MessageBox.Show("El campo n no puede estar vacío",
                    "Vacío detectado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtN.Text, out n) || n <= 0)
            {
                MessageBox.Show("n debe ser un número entero positivo",
                    "Error en n", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtN.Focus();
                txtN.SelectAll();
                return;
            }

 
            bool usarXUnica = false;

            if (!string.IsNullOrWhiteSpace(txtX.Text))
            {
                if (!int.TryParse(txtX.Text, out xUnico))
                {
                    MessageBox.Show("X debe ser un entero válido",
                        "Error en X", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (xUnico < 0 || xUnico > n)
                {
                    MessageBox.Show("X debe estar entre 0 y n",
                        "Error en X", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                usarXUnica = true;
                xInicio = xUnico;
                xFinal = xUnico;
            }

       
            if (!usarXUnica)
            {
                if (string.IsNullOrWhiteSpace(txtXinicio.Text) &&
                    string.IsNullOrWhiteSpace(txtXFinal.Text))
                {
                    xInicio = 0;
                    xFinal = n;
                }
                else if (!string.IsNullOrWhiteSpace(txtXinicio.Text) &&
                         !string.IsNullOrWhiteSpace(txtXFinal.Text))
                {
                    if (!int.TryParse(txtXinicio.Text, out xInicio) ||
                        !int.TryParse(txtXFinal.Text, out xFinal))
                    {
                        MessageBox.Show("Ingrese valores válidos para XInicio y XFinal",
                            "Error en rango", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (xInicio < 0 || xFinal > n || xInicio > xFinal)
                    {
                        MessageBox.Show("El rango debe estar entre 0 y n, y xInicio <= xFinal",
                            "Error en rango", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Debe llenar ambos campos de rango o dejarlos vacíos",
                        "Error en rango", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

         
            bool hayX = !string.IsNullOrWhiteSpace(txtX.Text);
            bool hayRangoInicio = !string.IsNullOrWhiteSpace(txtXinicio.Text);
            bool hayRangoFinal = !string.IsNullOrWhiteSpace(txtXFinal.Text);

            if (hayX && (hayRangoInicio || hayRangoFinal))
            {
                MessageBox.Show("No puede usar X y rango al mismo tiempo. " +
                    "Ingrese solo X o solo el rango.",
                    "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            bool tienePoblacion = !string.IsNullOrWhiteSpace(txtPoblacion.Text);

            if (tienePoblacion)
            {
                if (!int.TryParse(txtPoblacion.Text, out N) || N <= 0)
                {
                    MessageBox.Show("La población debe ser un entero positivo",
                        "Error en N", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (N < n)
                {
                    MessageBox.Show("La población no puede ser menor que la muestra",
                        "Error lógico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

         
            bool tieneK = !string.IsNullOrWhiteSpace(txtK.Text);
            bool tieneP = !string.IsNullOrWhiteSpace(txtP.Text);

            if (tienePoblacion)
            {
            
                if (n >= 0.2 * N)
                {
                  
                    if (!tieneK)
                    {
                        MessageBox.Show($"n = {n} es ≥ 20% de N = {N}\n" +
                                        "Por la regla del 20%, corresponde usar DISTRIBUCIÓN HIPERGEOMÉTRICA.\n\n" +
                                        "Para usar Hipergeométrica necesita K (número de éxitos en la población).\n" +
                                        "Por favor ingrese K.",
                                        "Se requiere K",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtK.Focus();
                        return;
                    }

                 
                    if (!int.TryParse(txtK.Text, out K) || K < 0)
                    {
                        MessageBox.Show("K debe ser un entero positivo",
                            "Error en K", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (K > N)
                    {
                        MessageBox.Show("K no puede ser mayor que N",
                            "Error lógico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    
                    MessageBox.Show($"n = {n} es ≥ 20% de N = {N}\n" +
                                    "Calculando con Distribución Hipergeométrica",
                                    "Modelo Hipergeométrico",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CalcularHipergeometrica(N, K, n, xInicio, xFinal);
                    return;
                }
                else
                {
                  
                    if (tieneK)
                    {
                      
                        if (!int.TryParse(txtK.Text, out K) || K < 0)
                        {
                            MessageBox.Show("K debe ser un entero positivo",
                                "Error en K", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (K > N)
                        {
                            MessageBox.Show("K no puede ser mayor que N",
                                "Error lógico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        double pSugerida = (double)K / N;

                        if (tieneP)
                        {
                            double pIngresada;
                            if (double.TryParse(txtP.Text, out pIngresada))
                            {
                                MessageBox.Show($"n = {n} es menor al 20% de N = {N}\n" +
                                                "Por la regla del 20%, corresponde usar DISTRIBUCIÓN BINOMIAL.\n\n" +
                                                $"K ingresado = {K}\n" +
                                                $"p sugerido (K/N) = {pSugerida:F4}\n" +
                                                $"p ingresado = {pIngresada:F4}\n\n" +
                                                "Se usará el p que usted ingresó.",
                                                "Usando Binomial",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                p = pIngresada;
                            }
                        }
                        else
                        {
                            // No tiene p, usamos el sugerido
                            MessageBox.Show($"n = {n} es menor al 20% de N = {N}\n" +
                                            "Por la regla del 20%, corresponde usar DISTRIBUCIÓN BINOMIAL.\n\n" +
                                            $"Usando p = K/N = {pSugerida:F4}",
                                            "Usando Binomial",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            p = pSugerida;
                        }
                    }
                    else
                    {
                    
                        if (!tieneP)
                        {
                            MessageBox.Show($"n = {n} es menor al 20% de N = {N}\n" +
                                            "Por la regla del 20%, corresponde usar DISTRIBUCIÓN BINOMIAL.\n\n" +
                                            "Para usar Binomial necesita p (probabilidad de éxito).\n" +
                                            "Por favor ingrese p.",
                                            "Se requiere p",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtP.Focus();
                            return;
                        }

                    
                        if (!double.TryParse(txtP.Text, out p))
                        {
                            MessageBox.Show("p debe ser un número decimal",
                                "Error en p", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (p < 0 || p > 1)
                        {
                            MessageBox.Show("p debe estar entre 0 y 1",
                                "Error en p", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (EsPoblacionIfinita(n, N))
                    {
                        MessageBox.Show("La muestra es mayor o igual al 5% de la población.\n" +
                                      "Se aplicará Binomial con corrección por población finita.",
                                      "Binomial Finita",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CalcularProbabilidadFinita(n, p, N, xInicio, xFinal);
                    }
                    else
                    {
                        CalcularProbabilidadInfinita(n, p, xInicio, xFinal);
                    }
                    return;
                }
            }
            else
            {
              
                if (!tieneP)
                {
                    MessageBox.Show("No ha ingresado población (N).\n" +
                                    "Para usar distribución Binomial necesita p (probabilidad de éxito).\n" +
                                    "Por favor ingrese p.",
                                    "Se requiere p",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtP.Focus();
                    return;
                }

            
                if (!double.TryParse(txtP.Text, out p))
                {
                    MessageBox.Show("p debe ser un número decimal",
                        "Error en p", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (p < 0 || p > 1)
                {
                    MessageBox.Show("p debe estar entre 0 y 1",
                        "Error en p", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

             
                CalcularProbabilidadInfinita(n, p, xInicio, xFinal);
                return;
            }
        }
        private void ResetearCampos()
        {
            txtK.Enabled = true;
            txtK.Clear();
            txtP.Enabled = true;
            txtP.Clear();
            txtPoblacion.Clear();
            txtN.Clear();
        }
        private void GraficarSimple(double[] valoresK, double[] probabilidades, int n, double p)
        {
          
            ultimosValoresX = valoresK;
            ultimasProbabilidades = probabilidades;
            ultimoN = n;
            ultimaP = p;
            esModoBinomial = true;
            mostrandoGraficaAcumulada = false;
            btnCambiarGrafica.Text = "Ver Gráfica Acumulada";

            formsPlot1.Plot.Clear();

            double[] porcentajes = probabilidades.Select(x => x * 100).ToArray();

            formsPlot1.Plot.Add.Bars(valoresK, porcentajes);

            for (int i = 0; i < valoresK.Length; i++)
            {
                var texto = formsPlot1.Plot.Add.Text(
                    $"{porcentajes[i]:0.00}",
                    valoresK[i],
                    porcentajes[i] + 0.8
                );

                texto.LabelFontSize = 11;
                texto.Alignment = Alignment.LowerCenter;
            }

            double q = 1 - p;

            formsPlot1.Plot.Title($"Distribución Binomial (n={n}, p={p}, q={q})");
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

            if (esModoBinomial)
            {
                formsPlot1.Plot.Title($"Distribución Binomial ACUMULADA (n={ultimoN}, p={ultimaP})");
            }
            else
            {
                formsPlot1.Plot.Title($"Distribución Hipergeométrica ACUMULADA");
            }

            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("Probabilidad Acumulada %");

            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();
        }

       
    

        private void ActualizarTextoBoton()
        {
          
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn && btn.Text.Contains("Gráfica"))
                {
                    btn.Text = mostrandoGraficaAcumulada ?
                        "Ver Gráfica Normal" :
                        "Ver Gráfica Acumulada";
                    break;
                }
            }
        }


        private void GraficarHiper(double[] valoresX, double[] probabilidades, int N, int K, int n)
        {
          
            ultimosValoresX = valoresX;
            ultimasProbabilidades = probabilidades;
            ultimoNHiper = N;
            ultimoKHiper = K;
            ultimoN = n;
            esModoBinomial = false;
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

            double p = (double)K / N;
            double q = 1 - p;

            formsPlot1.Plot.Title($"Distribución Hipergeométrica (N={N}, K={K}, n={n}, p={p:F2}, q={q:F2})");
            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("p %");

            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();
        }
        private void GraficarHiperAcumulada(double[] valoresX, double[] probabilidades, int N, int K, int n)
        {
            formsPlot1.Plot.Clear();

            
            double[] porcentajesAcum = new double[probabilidades.Length];
            double acum = 0;
            for (int i = 0; i < probabilidades.Length; i++)
            {
                acum += probabilidades[i];
                porcentajesAcum[i] = acum * 100;
            }

            formsPlot1.Plot.Add.Bars(valoresX, porcentajesAcum);

            for (int i = 0; i < valoresX.Length; i++)
            {
                var texto = formsPlot1.Plot.Add.Text(
                    $"{porcentajesAcum[i]:0.00}",
                    valoresX[i],
                    porcentajesAcum[i] + 0.8
                );

                texto.LabelFontSize = 11;
                texto.Alignment = Alignment.LowerCenter;
            }

            formsPlot1.Plot.Title($"Distribución Hipergeométrica ACUMULADA (N={N}, K={K}, n={n})");
            formsPlot1.Plot.XLabel("x");
            formsPlot1.Plot.YLabel("Probabilidad Acumulada %");

            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();
        }

       

        public void RecibirDatosDesdeExcel(int N, int K, int n, double p, string modelo, string columna, string categoria)
        {
            // Mostrar mensaje de confirmación
            MessageBox.Show($"Datos recibidos desde archivo:\n\n" +
                            $"Columna: {columna}\n" +
                            $"Categoría éxito: {categoria}\n" +
                            $"N = {N}\n" +
                            $"K = {K}\n" +
                            $"n = {n}\n" +
                            $"Modelo a usar: {modelo}" +
                            (modelo == "Binomial" ? $"\np = {p:F4}" : ""),
                            "Datos cargados", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtPoblacion.Text = N.ToString();
            txtN.Text = n.ToString();  

            if (modelo == "Hipergeométrica")
            {
                txtK.Text = K.ToString();
                txtK.Enabled = true;    
                txtP.Clear();
                txtP.Enabled = false;
            }
            else 
            {
                txtK.Clear();
                txtK.Enabled = false;      
                txtP.Text = p.ToString("F4");
                txtP.Enabled = true;
            }

            txtX.Clear();
            txtXinicio.Clear();
            txtXFinal.Clear();


            txtX.Focus();

            MessageBox.Show($"Los campos han sido llenados automáticamente.\n" +
                            $"Ahora ingrese el valor de X (o rango) y presione Calcular.",
                            "Listo para calcular", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Limpiar()
        {
            txtN.Clear();
            txtP.Clear();
            txtPoblacion.Clear();
            txtResultados.Clear();
            txtResultados2.Clear();
            txtXinicio.Clear();
            txtXFinal.Clear();
            txtConfianza.Clear();
            txtK.Clear();

            txtK.Enabled = true;
            txtP.Enabled = true;

            dgvResultados.Rows.Clear();
            formsPlot1.Plot.Clear();
            ConfigurarGrafico();

            ultimosValoresX = null;
            ultimasProbabilidades = null;
            ultimasProbabilidadesAcumuladas = null;
            mostrandoGraficaAcumulada = false;
            ActualizarTextoBoton();
        }
        private void dgvResultados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtN.Text = nRecibido.ToString();
            txtP.Text = pRecibido.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FormExcelCompleto formExcelCompleto = new FormExcelCompleto();
            formExcelCompleto.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCambiarGrafica_Click_1(object sender, EventArgs e)
        {
            if (ultimosValoresX == null) return;

            if (mostrandoGraficaAcumulada)
            {
               
                if (esModoBinomial)
                {
                    GraficarSimple(ultimosValoresX, ultimasProbabilidades, ultimoN, ultimaP);
                }
                else
                {
                    GraficarHiper(ultimosValoresX, ultimasProbabilidades, ultimoNHiper, ultimoKHiper, ultimoN);
                }
                btnCambiarGrafica.Text = "Ver Gráfica Acumulada";
                mostrandoGraficaAcumulada = false;
            }
            else
            {
            
                if (esModoBinomial)
                {
                    GraficarAcumulado();
                }
                else
                {
                    GraficarHiperAcumulada(ultimosValoresX, ultimasProbabilidades, ultimoNHiper, ultimoKHiper, ultimoN);
                }
                btnCambiarGrafica.Text = "Ver Gráfica Normal";
                mostrandoGraficaAcumulada = true;
            }
        }
    }
}
