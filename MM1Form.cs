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
    public partial class MM1Form : Form
    {
        private double _PromedioLlegadas;
        private double _PromedioServicio;
        private bool _datosValidos = false;
        public MM1Form()
        {
            InitializeComponent();
        }
        private void ConfigurarBoton()
        {
            if (btnMostrarGrafico != null)
            {
                btnMostrarGrafico.Text = "Mostrar Gráficas";
                btnMostrarGrafico.Click -= btnMostrarGrafico_Click;
                btnMostrarGrafico.Click += btnMostrarGrafico_Click;
            }
        }
       
        private void Calculos()
        {
            // 1. Validar que los campos de texto no estén vacíos
            if (string.IsNullOrEmpty(txtLlegadas.Text) || string.IsNullOrEmpty(txtServicio.Text))
            {
                MessageBox.Show("Los datos sobre Promedio de llegadas y de servicio son obligatorios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _datosValidos = false;
                return;
            }

            // 2. Validar unidad de ENTRADA
            if (chEntradaHoras.Checked && chEntradaMinutos.Checked)
            {
                MessageBox.Show("Seleccione SOLO una unidad de ENTRADA (Horas o Minutos)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _datosValidos = false;
                return;
            }

            if (!chEntradaHoras.Checked && !chEntradaMinutos.Checked)
            {
                MessageBox.Show("Seleccione una unidad de ENTRADA (Horas o Minutos)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _datosValidos = false;
                return;
            }

            // 3. Validar unidad de SALIDA
            if (chSalidaHoras.Checked && chSalidaMinutos.Checked)
            {
                MessageBox.Show("Seleccione SOLO una unidad de SALIDA (Horas o Minutos)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _datosValidos = false;
                return;
            }

            if (!chSalidaHoras.Checked && !chSalidaMinutos.Checked)
            {
                MessageBox.Show("Seleccione una unidad de SALIDA (Horas o Minutos)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _datosValidos = false;
                return;
            }

            // 4. Intentar convertir los valores ingresados a double
            if (!double.TryParse(txtLlegadas.Text, out _PromedioLlegadas) || !double.TryParse(txtServicio.Text, out _PromedioServicio))
            {
                MessageBox.Show("Datos incorrectos, revise por favor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _datosValidos = false;
                return;
            }

            // 5. Convertir λ y μ a una unidad base (SIEMPRE a clientes por HORA)
            double lambdaPorHora = _PromedioLlegadas;
            double muPorHora = _PromedioServicio;

            if (chEntradaMinutos.Checked)
            {
                // Si el usuario ingresó valores en "clientes por minuto", convertimos a "clientes por hora"
                lambdaPorHora = _PromedioLlegadas * 60;
                muPorHora = _PromedioServicio * 60;
            }

            // 6. Verificar estabilidad del sistema (λ < μ)
            if (lambdaPorHora >= muPorHora)
            {
                MessageBox.Show($"El sistema no es estable (λ = {lambdaPorHora:F2} ≥ μ = {muPorHora:F2} clientes/hora)",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _datosValidos = false;
                return;
            }

            // 7. Si llegamos aquí, los datos son válidos
            _datosValidos = true;

            // 8. Realizar los cálculos usando SIEMPRE lambdaPorHora y muPorHora
            double porcentajeUso = PorcentajeUsoSistema(lambdaPorHora, muPorHora);
            double porcentajeUsoCien = porcentajeUso * 100;
            double personasSistema = PersonasSistema(lambdaPorHora, muPorHora);
            double personasCola = PersonasCola(personasSistema, porcentajeUso);
            double esperaEnColaHoras = EsperaEnCola(lambdaPorHora, muPorHora);
            double esperaEnSistemaHoras = EsperaEnSistema(lambdaPorHora, muPorHora);
            double sistemaDesocupado = SistemaDesocupado(porcentajeUso);
            double sistemaDesocupadoPorc = sistemaDesocupado * 100;

            // 9. Mostrar resultados según la unidad de SALIDA seleccionada
            txtPorcentajeUso.Text = Math.Round(porcentajeUso, 4).ToString() + " = " + Math.Round(porcentajeUsoCien) + "%";
            txtLs.Text = Math.Round(personasSistema, 4).ToString() + " clientes";
            txtLq.Text = Math.Round(personasCola, 4).ToString() + " clientes";
            txtSistemaOcioso.Text = Math.Round(sistemaDesocupado, 4).ToString() + " = " + Math.Round(sistemaDesocupadoPorc) + "%";

            // Tiempos de espera (depende de la unidad de SALIDA)
            if (chSalidaMinutos.Checked)
            {
                // Convertir de horas a minutos
                txtWq.Text = Math.Round(esperaEnColaHoras * 60, 4).ToString() + " minutos";
                txtWs.Text = Math.Round(esperaEnSistemaHoras * 60, 4).ToString() + " minutos";
            }
            else // chSalidaHoras.Checked
            {
                txtWq.Text = Math.Round(esperaEnColaHoras, 4).ToString() + " horas";
                txtWs.Text = Math.Round(esperaEnSistemaHoras, 4).ToString() + " horas";
            }

            // 10. Calcular probabilidad de N clientes (si el usuario ingresó un número)
            if (!string.IsNullOrEmpty(txtClientes.Text))
            {
                if (int.TryParse(txtClientes.Text, out int clientes))
                {
                    double sistemaNClientes = SistemaNClientes(clientes, porcentajeUso);
                    double sistemaNClientesCien = sistemaNClientes * 100;
                    txtNclientes.Text = Math.Round(sistemaNClientes, 4).ToString() + " = " + Math.Round(sistemaNClientesCien) + "%";
                }
                else
                {
                    MessageBox.Show("Número de clientes incorrecto, revise por favor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNclientes.Text = string.Empty;
                }
            }
            else
            {
                txtNclientes.Text = string.Empty;
            }
        }

        // Métodos de cálculo
        private double PorcentajeUsoSistema(double llegadas, double servicio)
        {
            return llegadas / servicio;
        }

        private double PersonasSistema(double llegadas, double servicio)
        {
            return llegadas / (servicio - llegadas);
        }

        private double PersonasCola(double personasSistema, double porcentajeUso)
        {
            // Lq = Ls - ρ
            return personasSistema - porcentajeUso;
        }

        private double EsperaEnCola(double llegadas, double servicio)
        {
            return llegadas / (servicio * (servicio - llegadas));
        }

        private double EsperaEnSistema(double llegadas, double servicio)
        {
            return 1 / (servicio - llegadas);
        }

        private double SistemaDesocupado(double porcentajeUso)
        {
            return 1 - porcentajeUso;
        }

        private double SistemaNClientes(int clientes, double porcentajeUso)
        {
            return (1 - porcentajeUso) * Math.Pow(porcentajeUso, clientes);
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnMostrarGrafico_Click(object sender, EventArgs e)
        {
            if (!_datosValidos || _PromedioLlegadas == 0 || _PromedioServicio == 0)
            {
                MessageBox.Show("Primero debe realizar cálculos válidos (λ < μ)", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool entradaEnMinutos = chEntradaMinutos.Checked;
            bool salidaEnMinutos = chSalidaMinutos.Checked;

            GraficosMM1Form formGraficos = new GraficosMM1Form(_PromedioLlegadas, _PromedioServicio, entradaEnMinutos, salidaEnMinutos);
            formGraficos.ShowDialog();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Calculos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            txtLlegadas.Clear();
            txtServicio.Clear();
            txtLs.Clear();
            txtLq.Clear();
            txtWq.Clear();
            txtWs.Clear();
            txtPorcentajeUso.Clear();
            txtSistemaOcioso.Clear();
            txtClientes.Clear();
            txtNclientes.Clear();

            // Restablecer CheckBox de entrada
            chEntradaHoras.Checked = false;
            chEntradaMinutos.Checked = false;

            // Restablecer CheckBox de salida
            chSalidaHoras.Checked = false;
            chSalidaMinutos.Checked = false;

            _PromedioLlegadas = 0;
            _PromedioServicio = 0;
            _datosValidos = false;
        }

        private void chEntradaMinutos_CheckedChanged(object sender, EventArgs e)
        {
            if (chEntradaMinutos.Checked)
            {
                chEntradaHoras.Checked = false;
            }
        }

        private void chEntradaHoras_CheckedChanged(object sender, EventArgs e)
        {
            if (chEntradaHoras.Checked)
            {
                chEntradaMinutos.Checked = false;
            }
        }

        private void chSalidaMinutos_CheckedChanged(object sender, EventArgs e)
        {
            if (chSalidaMinutos.Checked)
                chSalidaHoras.Checked = false;
        }

        private void chSalidaHoras_CheckedChanged(object sender, EventArgs e)
        {
            if (chSalidaHoras.Checked)
                chSalidaMinutos.Checked = false;
        }
    }
}
