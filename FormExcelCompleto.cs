using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelDataReader;
using System.Text;

namespace ModelosDiscretosyContinuos
{
    public partial class FormExcelCompleto : Form
    {
        private DataTable datosOriginales;
        private string[] encabezados;
        private int N, K, n;
        private string columnaSeleccionada;
        private string estadoSeleccionado;
        private double mediaCalculada;
        private double pCalculada;
        private double desviacionCalculada;

        // Controles
        private DataGridView dgvDatos;
        private DataGridView dgvFrecuencias;
        private ComboBox cmbColumnas;
        private ComboBox cmbCategorias;
        private Button btnCargarArchivo;
        private Button btnEnviarAForm1;
        private Button btnLimpiar;
        private Button btnCalcularFrecuencias;
        private Label lblTotalRegistros;
        private Label lblExitos;
        private Label lblMedia;
        private Label lblProbabilidad;
        private Label lblDesviacion;
        private TextBox txtN, txtK, txtn;
        private TextBox txtMedia, txtProbabilidad, txtDesviacion;
        private GroupBox gbSeleccion;
        private GroupBox gbParametros;
        private GroupBox gbEstadisticos;
        private GroupBox gbFrecuencias;
        private OpenFileDialog openFileDialog;
        public FormExcelCompleto()
        {
            
            InicializarComponentes();
        }


        private void InicializarComponentes()
        {
            this.Text = "Análisis de Datos - Distribuciones";
            this.Width = 1000;
            this.Height = 850;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Microsoft Sans Serif", 8.25f);


            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos Excel (*.xlsx;*.xls)|*.xlsx;*.xls|Archivos CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*";
            openFileDialog.Title = "Seleccionar archivo de datos";

       
            btnCargarArchivo = new Button();
            btnCargarArchivo.Text = "Cargar Archivo Excel/CSV";
            btnCargarArchivo.Location = new Point(20, 20);
            btnCargarArchivo.Size = new Size(180, 30);
            btnCargarArchivo.Click += BtnCargarArchivo_Click;
            this.Controls.Add(btnCargarArchivo);


            gbSeleccion = new GroupBox();
            gbSeleccion.Text = "Selección de Datos";
            gbSeleccion.Location = new Point(220, 20);
            gbSeleccion.Size = new Size(400, 140);
            this.Controls.Add(gbSeleccion);

            Label lblColumnas = new Label();
            lblColumnas.Text = "Columna:";
            lblColumnas.Location = new Point(15, 30);
            lblColumnas.Size = new Size(80, 20);
            gbSeleccion.Controls.Add(lblColumnas);

            cmbColumnas = new ComboBox();
            cmbColumnas.Location = new Point(100, 27);
            cmbColumnas.Size = new Size(180, 21);
            cmbColumnas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColumnas.SelectedIndexChanged += CmbColumnas_SelectedIndexChanged;
            gbSeleccion.Controls.Add(cmbColumnas);

     
            Label lblCategorias = new Label();
            lblCategorias.Text = "Categoría (éxito):";
            lblCategorias.Location = new Point(15, 65);
            lblCategorias.Size = new Size(80, 20);
            gbSeleccion.Controls.Add(lblCategorias);

            cmbCategorias = new ComboBox();
            cmbCategorias.Location = new Point(100, 62);
            cmbCategorias.Size = new Size(180, 21);
            cmbCategorias.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategorias.SelectedIndexChanged += CmbCategorias_SelectedIndexChanged;
            gbSeleccion.Controls.Add(cmbCategorias);


            lblTotalRegistros = new Label();
            lblTotalRegistros.Text = "N (total): 0";
            lblTotalRegistros.Location = new Point(300, 30);
            lblTotalRegistros.Size = new Size(90, 20);
            gbSeleccion.Controls.Add(lblTotalRegistros);

            lblExitos = new Label();
            lblExitos.Text = "K (éxitos): 0";
            lblExitos.Location = new Point(300, 65);
            lblExitos.Size = new Size(90, 20);
            gbSeleccion.Controls.Add(lblExitos);

        
            gbParametros.Text = "Parámetros del Modelo";
            gbParametros.Location = new Point(20, 170);
            gbParametros.Size = new Size(400, 120);
            this.Controls.Add(gbParametros);

   
            Label lblNPoblacion = new Label();
            lblNPoblacion.Text = "N (población):";
            lblNPoblacion.Location = new Point(15, 30);
            lblNPoblacion.Size = new Size(80, 20);
            gbParametros.Controls.Add(lblNPoblacion);

            txtN = new TextBox();
            txtN.Location = new Point(100, 27);
            txtN.Size = new Size(80, 20);
            txtN.ReadOnly = true;
            txtN.BackColor = SystemColors.Control;
            gbParametros.Controls.Add(txtN);

            Label lblKExitos = new Label();
            lblKExitos.Text = "K (éxitos):";
            lblKExitos.Location = new Point(15, 60);
            lblKExitos.Size = new Size(80, 20);
            gbParametros.Controls.Add(lblKExitos);

            txtK = new TextBox();
            txtK.Location = new Point(100, 57);
            txtK.Size = new Size(80, 20);
            txtK.ReadOnly = true;
            txtK.BackColor = SystemColors.Control;
            gbParametros.Controls.Add(txtK);

      
            Label lblNMuestra = new Label();
            lblNMuestra.Text = "n (muestra):";
            lblNMuestra.Location = new Point(15, 90);
            lblNMuestra.Size = new Size(80, 20);
            gbParametros.Controls.Add(lblNMuestra);

            txtn = new TextBox();
            txtn.Location = new Point(100, 87);
            txtn.Size = new Size(80, 20);
            txtn.Text = "";
            gbParametros.Controls.Add(txtn);

         
            gbEstadisticos = new GroupBox();
            gbEstadisticos.Text = "Estadísticos Descriptivos";
            gbEstadisticos.Location = new Point(440, 170);
            gbEstadisticos.Size = new Size(380, 120);
            this.Controls.Add(gbEstadisticos);

   
            Label lblMediaLabel = new Label();
            lblMediaLabel.Text = "Media:";
            lblMediaLabel.Location = new Point(15, 30);
            lblMediaLabel.Size = new Size(80, 20);
            gbEstadisticos.Controls.Add(lblMediaLabel);

            txtMedia = new TextBox();
            txtMedia.Location = new Point(100, 27);
            txtMedia.Size = new Size(80, 20);
            txtMedia.ReadOnly = true;
            txtMedia.BackColor = SystemColors.Control;
            gbEstadisticos.Controls.Add(txtMedia);

            Label lblProbabilidadLabel = new Label();
            lblProbabilidadLabel.Text = "p (éxito):";
            lblProbabilidadLabel.Location = new Point(200, 30);
            lblProbabilidadLabel.Size = new Size(80, 20);
            gbEstadisticos.Controls.Add(lblProbabilidadLabel);

            txtProbabilidad = new TextBox();
            txtProbabilidad.Location = new Point(280, 27);
            txtProbabilidad.Size = new Size(80, 20);
            txtProbabilidad.ReadOnly = true;
            txtProbabilidad.BackColor = SystemColors.Control;
            gbEstadisticos.Controls.Add(txtProbabilidad);

 
            Label lblDesviacionLabel = new Label();
            lblDesviacionLabel.Text = "Desviación:";
            lblDesviacionLabel.Location = new Point(15, 60);
            lblDesviacionLabel.Size = new Size(80, 20);
            gbEstadisticos.Controls.Add(lblDesviacionLabel);

            txtDesviacion = new TextBox();
            txtDesviacion.Location = new Point(100, 57);
            txtDesviacion.Size = new Size(80, 20);
            txtDesviacion.ReadOnly = true;
            txtDesviacion.BackColor = SystemColors.Control;
            gbEstadisticos.Controls.Add(txtDesviacion);

      
            dgvDatos = new DataGridView();
            dgvDatos.Location = new Point(20, 300);
            dgvDatos.Size = new Size(940, 200);
            dgvDatos.ReadOnly = true;
            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.AllowUserToDeleteRows = false;
            dgvDatos.RowHeadersVisible = true;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.BackgroundColor = SystemColors.Window;
            this.Controls.Add(dgvDatos);

     
            gbFrecuencias = new GroupBox();
            gbFrecuencias.Text = "Tabla de Distribución de Frecuencias";
            gbFrecuencias.Location = new Point(20, 510);
            gbFrecuencias.Size = new Size(940, 220);
            this.Controls.Add(gbFrecuencias);

            dgvFrecuencias = new DataGridView();
            dgvFrecuencias.Location = new Point(10, 20);
            dgvFrecuencias.Size = new Size(920, 150);
            dgvFrecuencias.ReadOnly = true;
            dgvFrecuencias.AllowUserToAddRows = false;
            dgvFrecuencias.ColumnCount = 6;
            dgvFrecuencias.Columns[0].Name = "Intervalo";
            dgvFrecuencias.Columns[1].Name = "Frecuencia (F)";
            dgvFrecuencias.Columns[2].Name = "Frecuencia Relativa (FR)";
            dgvFrecuencias.Columns[3].Name = "FR Acumulada";
            dgvFrecuencias.Columns[4].Name = "Marca de Clase";
            dgvFrecuencias.Columns[5].Name = "F * Marca";
            gbFrecuencias.Controls.Add(dgvFrecuencias);


            btnEnviarAForm1 = new Button();
            btnEnviarAForm1.Text = "Calcular en Form1";
            btnEnviarAForm1.Location = new Point(20, 740);
            btnEnviarAForm1.Size = new Size(150, 30);
            btnEnviarAForm1.Click += BtnEnviarAForm1_Click;
            this.Controls.Add(btnEnviarAForm1);


            btnLimpiar = new Button();
            btnLimpiar.Text = "Limpiar Todo";
            btnLimpiar.Location = new Point(180, 740);
            btnLimpiar.Size = new Size(150, 30);
            btnLimpiar.Click += BtnLimpiar_Click;
            this.Controls.Add(btnLimpiar);
        }

        private void BtnCargarArchivo_Click(object sender, EventArgs e)
        {
            MatarProcesosExcelFantasma();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string archivo = openFileDialog.FileName;
                    CargarDatos(archivo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar archivo: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       

        private void CargarDatos(string archivo)
        {
            datosOriginales = new DataTable();

            if (archivo.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                CargarCSV(archivo);
            }
            else
            {
                CargarExcel(archivo);
            }

           
            encabezados = new string[datosOriginales.Columns.Count];
            for (int i = 0; i < datosOriginales.Columns.Count; i++)
            {
                encabezados[i] = datosOriginales.Columns[i].ColumnName;
            }

         
            dgvDatos.DataSource = datosOriginales;

         
            cmbColumnas.Items.Clear();
            cmbColumnas.Items.AddRange(encabezados);
            if (encabezados.Length > 0)
                cmbColumnas.SelectedIndex = 0;

        
            cmbCategorias.Items.Clear();
            cmbCategorias.Text = "";

            MessageBox.Show($"Archivo cargado correctamente.\nTotal de registros: {datosOriginales.Rows.Count}",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CargarCSV(string archivo)
        {
            string[] lineas = File.ReadAllLines(archivo);
            if (lineas.Length == 0) return;

            string[] encabezadosCSV = lineas[0].Split(',');

            foreach (string encabezado in encabezadosCSV)
            {
                datosOriginales.Columns.Add(encabezado.Trim());
            }

            for (int i = 1; i < lineas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineas[i])) continue;

                string[] valores = lineas[i].Split(',');
                DataRow fila = datosOriginales.NewRow();

                for (int j = 0; j < valores.Length && j < datosOriginales.Columns.Count; j++)
                {
                    fila[j] = valores[j].Trim();
                }

                datosOriginales.Rows.Add(fila);
            }
        }

        private void MatarProcesosExcelFantasma()
        {
            try
            {
                var procesos = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                foreach (var proc in procesos)
                {
                  
                    if (string.IsNullOrEmpty(proc.MainWindowTitle))
                    {
                        try { proc.Kill(); } catch { }
                    }
                }
            }
            catch { }
        }

        private void CargarExcel(string archivo)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

      
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var stream = File.Open(archivo, FileMode.Open, FileAccess.Read))
                {
                   
                    var conf = new ExcelReaderConfiguration
                    {
                        
                        FallbackEncoding = Encoding.UTF8,
                        AutodetectSeparators = new char[] { ',', ';', '\t', '|' }
                    };

                    using (var reader = ExcelReaderFactory.CreateReader(stream, conf))
                    {
                      
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                           
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true, 
                                ReadHeaderRow = (rowReader) =>
                                {
                                    
                                }
                            }
                        });

                    
                        if (result.Tables.Count > 0)
                        {
                            datosOriginales = result.Tables[0];

                            MessageBox.Show($"Archivo Excel cargado con ExcelDataReader.\n" +
                                           $"Filas: {datosOriginales.Rows.Count}\n" +
                                           $"Columnas: {datosOriginales.Columns.Count}",
                                           "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("El archivo no contiene hojas de datos", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Excel: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void BtnCalcularFrecuencias_Click(object sender, EventArgs e)
        {
            if (cmbColumnas.SelectedItem == null || datosOriginales == null)
            {
                MessageBox.Show("Seleccione una columna primero", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string columna = cmbColumnas.SelectedItem.ToString();
            CalcularTablaFrecuencias(columna);
        }

        private void CalcularTablaFrecuencias(string columnaNumerica)
        {
            if (datosOriginales == null || !datosOriginales.Columns.Contains(columnaNumerica))
                return;

           
            List<double> valores = new List<double>();
            foreach (DataRow row in datosOriginales.Rows)
            {
                if (double.TryParse(row[columnaNumerica]?.ToString(), out double valor))
                {
                    valores.Add(valor);
                }
            }

            if (valores.Count == 0)
            {
                MessageBox.Show("La columna seleccionada no contiene valores numéricos válidos.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

    
            mediaCalculada = valores.Average();
            pCalculada = (double)K / N;
            desviacionCalculada = CalcularDesviacionEstandar(valores);


            txtMedia.Text = mediaCalculada.ToString("F4");
            txtProbabilidad.Text = pCalculada.ToString("F4");
            txtDesviacion.Text = desviacionCalculada.ToString("F4");

          
            valores.Sort();

          
            int numIntervalos = (int)Math.Ceiling(1 + 3.322 * Math.Log10(valores.Count));

            double min = valores.Min();
            double max = valores.Max();
            double rango = max - min;
            double anchoIntervalo = rango / numIntervalos;

            if (anchoIntervalo == 0) anchoIntervalo = 1; 


            double[] limites = new double[numIntervalos + 1];
            for (int i = 0; i <= numIntervalos; i++)
            {
                limites[i] = min + i * anchoIntervalo;
            }

        
            int[] frecuencias = new int[numIntervalos];
            foreach (double valor in valores)
            {
                for (int i = 0; i < numIntervalos; i++)
                {
                    if (valor >= limites[i] && (i == numIntervalos - 1 ? valor <= limites[i + 1] : valor < limites[i + 1]))
                    {
                        frecuencias[i]++;
                        break;
                    }
                }
            }

        
            dgvFrecuencias.Rows.Clear();
            double frAcumulada = 0;
            int totalValores = valores.Count;

            for (int i = 0; i < numIntervalos; i++)
            {
                string intervalo = $"{limites[i]:F2} - {limites[i + 1]:F2}";
                int frecuencia = frecuencias[i];
                double fr = (double)frecuencia / totalValores;
                frAcumulada += fr;
                double marcaClase = (limites[i] + limites[i + 1]) / 2;
                double fMarca = frecuencia * marcaClase;

                dgvFrecuencias.Rows.Add(
                    intervalo,
                    frecuencia,
                    fr.ToString("F4"),
                    frAcumulada.ToString("F4"),
                    marcaClase.ToString("F2"),
                    fMarca.ToString("F2")
                );
            }

    
            double totalFMarca = 0;
            for (int i = 0; i < numIntervalos; i++)
            {
                double marcaClase = (limites[i] + limites[i + 1]) / 2;
                totalFMarca += frecuencias[i] * marcaClase;
            }

            dgvFrecuencias.Rows.Add("TOTAL", totalValores, "1.0000", "", "", totalFMarca.ToString("F2"));

     
            dgvFrecuencias.Rows[dgvFrecuencias.Rows.Count - 1].DefaultCellStyle.Font =
                new Font(dgvFrecuencias.Font, FontStyle.Bold);
        }

        private double CalcularDesviacionEstandar(List<double> valores)
        {
            double media = valores.Average();
            double suma = 0;
            foreach (double valor in valores)
            {
                suma += Math.Pow(valor - media, 2);
            }
            return Math.Sqrt(suma / (valores.Count - 1));
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            this.ClientSize = new System.Drawing.Size(274, 229);
            this.Name = "FormExcelCompleto";
            this.Load += new System.EventHandler(this.FormExcelCompleto_Load);
            this.ResumeLayout(false);

        }

        private void FormExcelCompleto_Load(object sender, EventArgs e)
        {

        }

        private void CmbColumnas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbColumnas.SelectedItem == null || datosOriginales == null) return;

            columnaSeleccionada = cmbColumnas.SelectedItem.ToString();

            bool esNumerica = false;
            if (datosOriginales.Rows.Count > 0)
            {
                string primerValor = datosOriginales.Rows[0][columnaSeleccionada]?.ToString() ?? "";
                esNumerica = double.TryParse(primerValor, out _);
            }

            if (esNumerica)
            {
         
                DialogResult result = MessageBox.Show(
                    "La columna seleccionada contiene valores numéricos.\n" +
                    "¿Desea generar una tabla de distribución de frecuencias?\n\n" +
                    "Sí: Generar tabla de frecuencias\n" +
                    "No: Usar valores individuales",
                    "Análisis de datos",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
              
                    CalcularTablaFrecuencias(columnaSeleccionada);

            
                    MostrarDialogoSeleccionIntervalo();
                }
                else
                {
                 
                    dgvFrecuencias.Rows.Clear();
                    cmbCategorias.Enabled = false;
                    cmbCategorias.Items.Clear();
                    cmbCategorias.Text = "Seleccione otra columna";

           
                    K = 0;
                    txtK.Clear();
                    txtProbabilidad.Clear();
                }
            }
            else
            {
    
                dgvFrecuencias.Rows.Clear(); 

                var valoresUnicos = datosOriginales.AsEnumerable()
                    .Select(row => row[columnaSeleccionada]?.ToString() ?? "")
                    .Where(val => !string.IsNullOrEmpty(val))
                    .Distinct()
                    .ToArray();

                cmbCategorias.Enabled = true;
                cmbCategorias.Items.Clear();
                cmbCategorias.Items.AddRange(valoresUnicos);
                if (valoresUnicos.Length > 0)
                    cmbCategorias.SelectedIndex = 0;
            }
        }
       
        private void MostrarInterpretacionLikert()
        {
            
            if (N == 0 || mediaCalculada == 0) return;

            bool esLikert = mediaCalculada >= 1 && mediaCalculada <= 5;
            if (!esLikert) return;

            string mensaje = "═══════════════════════════════════════════\n" +
                            "   ANÁLISIS DE SATISFACCIÓN DEL CLIENTE\n" +
                            "═══════════════════════════════════════════\n\n" +
                            $"Variable analizada: {columnaSeleccionada}\n" +
                            $"Total de clientes (N): {N}\n" +
                            $"Clientes satisfechos (K): {K}\n" +
                            $"Probabilidad de éxito (p): {pCalculada:F2} ({pCalculada * 100:F1}%)\n" +
                            $"Media de satisfacción: {mediaCalculada:F2}\n\n" +
                            "───────────────────────────────────────────\n" +
                            "INTERPRETACIÓN:\n" +
                            "• Se considera 'éxito' = puntuación ≥ 4\n" +
                            "• Clientes satisfechos: aquellos con valor 4 o 5\n" +
                            $"• El {(mediaCalculada >= 4 ? "✅ CUMPLE" : "❌ NO CUMPLE")} " +
                            $"con el objetivo de media ≥ 4.0\n" +
                            $"• {K} de {N} clientes están satisfechos\n" +
                            $"• Probabilidad de encontrar un cliente satisfecho: {pCalculada * 100:F1}%\n" +
                            "───────────────────────────────────────────\n\n" +
                            "¿Desea usar estos datos para cálculos probabilísticos?";

            DialogResult result = MessageBox.Show(mensaje, "Análisis de Satisfacción",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
              
                txtn.Focus();
            }
        }
        private void MostrarDialogoSeleccionIntervalo()
        {
            if (dgvFrecuencias.Rows.Count == 0) return;

            Form dialogo = new Form();
            dialogo.Text = "Seleccionar Intervalo de Éxito";
            dialogo.Width = 500;
            dialogo.Height = 250;
            dialogo.StartPosition = FormStartPosition.CenterParent;
            dialogo.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialogo.MaximizeBox = false;
            dialogo.MinimizeBox = false;

            Label lblInstruccion = new Label();
            lblInstruccion.Text = "Seleccione el intervalo que desea considerar como 'éxito':\n" +
                                  "(Este será el valor K para los cálculos)";
            lblInstruccion.Location = new Point(20, 20);
            lblInstruccion.Size = new Size(440, 40);
            dialogo.Controls.Add(lblInstruccion);

            ComboBox cmbIntervalos = new ComboBox();
            cmbIntervalos.Location = new Point(20, 70);
            cmbIntervalos.Size = new Size(440, 25);
            cmbIntervalos.DropDownStyle = ComboBoxStyle.DropDownList;

         
            for (int i = 0; i < dgvFrecuencias.Rows.Count - 1; i++)
            {
                string intervalo = dgvFrecuencias.Rows[i].Cells[0].Value?.ToString() ?? "";
                string frecuencia = dgvFrecuencias.Rows[i].Cells[1].Value?.ToString() ?? "";
                string fr = dgvFrecuencias.Rows[i].Cells[2].Value?.ToString() ?? "";
                cmbIntervalos.Items.Add($"{intervalo} (F={frecuencia}, FR={fr})");
            }

            if (cmbIntervalos.Items.Count > 0)
                cmbIntervalos.SelectedIndex = 0;

            dialogo.Controls.Add(cmbIntervalos);

            Button btnAceptar = new Button();
            btnAceptar.Text = "Aceptar";
            btnAceptar.Location = new Point(150, 130);
            btnAceptar.Size = new Size(100, 30);
            btnAceptar.DialogResult = DialogResult.OK;
            dialogo.Controls.Add(btnAceptar);

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(270, 130);
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.DialogResult = DialogResult.Cancel;
            dialogo.Controls.Add(btnCancelar);

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                string seleccion = cmbIntervalos.SelectedItem?.ToString() ?? "";
                int indice = cmbIntervalos.SelectedIndex;

                if (indice >= 0 && indice < dgvFrecuencias.Rows.Count - 1)
                {
                 
                    string kStr = dgvFrecuencias.Rows[indice].Cells[1].Value?.ToString() ?? "0";
                    string frStr = dgvFrecuencias.Rows[indice].Cells[2].Value?.ToString() ?? "0";

                    K = int.Parse(kStr);
                    double fr = double.Parse(frStr);
                    N = datosOriginales.Rows.Count;

                   
                    estadoSeleccionado = seleccion;
                    lblTotalRegistros.Text = $"N (total): {N}";
                    lblExitos.Text = $"K (éxitos): {K}";
                    txtN.Text = N.ToString();
                    txtK.Text = K.ToString();

                   
                    txtProbabilidad.Text = fr.ToString("F4");
                    pCalculada = fr;

                    MessageBox.Show(
                        $"Intervalo seleccionado: {seleccion}\n" +
                        $"Éxitos (K) = {K}\n" +
                        $"Probabilidad (p) = {fr:F4} ({fr * 100:F1}%)",
                        "Éxito definido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    
                }
            }
        }
        private void CmbCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategorias.SelectedItem == null || datosOriginales == null) return;

            estadoSeleccionado = cmbCategorias.SelectedItem.ToString();

            N = datosOriginales.Rows.Count;
            K = datosOriginales.AsEnumerable()
                .Count(row => (row[columnaSeleccionada]?.ToString() ?? "") == estadoSeleccionado);

            lblTotalRegistros.Text = $"N (total): {N}";
            lblExitos.Text = $"K (éxitos): {K}";
            txtN.Text = N.ToString();
            txtK.Text = K.ToString();

         
            pCalculada = (double)K / N;
            txtProbabilidad.Text = pCalculada.ToString("F4");
        }

        private void BtnEnviarAForm1_Click(object sender, EventArgs e)
        {
      
            if (datosOriginales == null)
            {
                MessageBox.Show("Primero debe cargar un archivo", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(columnaSeleccionada))
            {
                MessageBox.Show("Debe seleccionar una columna", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtn.Text, out n) || n <= 0)
            {
                MessageBox.Show("Ingrese un valor válido para n (tamaño de muestra)", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (n > N)
            {
                MessageBox.Show($"La muestra (n) no puede ser mayor que la población (N={N})", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string modelo;
            double p = 0;

            if (n >= 0.2 * N)
            {
                modelo = "Hipergeométrica";
                
                if (K == 0)
                {
                    MessageBox.Show("Para usar Hipergeométrica necesita definir K (éxitos).\n" +
                        "Seleccione una categoría o intervalo de éxito.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                modelo = "Binomial";
                p = (double)K / N;  
            }

        
            Form1 form1 = Application.OpenForms["Form1"] as Form1;
            if (form1 != null)
            {
                form1.RecibirDatosDesdeExcel(N, K, n, p, modelo, columnaSeleccionada,
                    estadoSeleccionado ?? "Valor seleccionado");
                this.Close();
            }
            else
            {
                MessageBox.Show("No se encontró el formulario principal", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            dgvDatos.DataSource = null;
            dgvFrecuencias.Rows.Clear();
            datosOriginales = null;
            cmbColumnas.Items.Clear();
            cmbCategorias.Items.Clear();
            cmbCategorias.Enabled = true;
           
            lblTotalRegistros.Text = "N (total): 0";
            lblExitos.Text = "K (éxitos): 0";
            txtN.Clear();
            txtK.Clear();
            txtn.Clear();
            txtMedia.Clear();
            txtProbabilidad.Clear();
            txtDesviacion.Clear();
            columnaSeleccionada = "";
            estadoSeleccionado = "";
            N = K = n = 0;
            mediaCalculada = pCalculada = desviacionCalculada = 0;
       
        }

    }
}