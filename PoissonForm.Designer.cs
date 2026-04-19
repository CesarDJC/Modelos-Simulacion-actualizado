namespace ModelosDiscretosyContinuos
{
    partial class PoissonForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCambiarGrafica = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtConfianza = new System.Windows.Forms.TextBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtK = new System.Windows.Forms.TextBox();
            this.txtXFinal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtXinicio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtResultados2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtResultados = new System.Windows.Forms.TextBox();
            this.txtPoblacion = new System.Windows.Forms.TextBox();
            this.txtP = new System.Windows.Forms.TextBox();
            this.txtN = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLambda = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCambiarGrafica
            // 
            this.btnCambiarGrafica.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarGrafica.Location = new System.Drawing.Point(934, 167);
            this.btnCambiarGrafica.Margin = new System.Windows.Forms.Padding(2);
            this.btnCambiarGrafica.Name = "btnCambiarGrafica";
            this.btnCambiarGrafica.Size = new System.Drawing.Size(136, 37);
            this.btnCambiarGrafica.TabIndex = 46;
            this.btnCambiarGrafica.Text = "G Acumulada";
            this.btnCambiarGrafica.UseVisualStyleBackColor = true;
            this.btnCambiarGrafica.Click += new System.EventHandler(this.btnCambiarGrafica_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(690, 157);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 18);
            this.label8.TabIndex = 45;
            this.label8.Text = "Confianza";
            // 
            // txtConfianza
            // 
            this.txtConfianza.Location = new System.Drawing.Point(782, 155);
            this.txtConfianza.Margin = new System.Windows.Forms.Padding(2);
            this.txtConfianza.Name = "txtConfianza";
            this.txtConfianza.Size = new System.Drawing.Size(116, 20);
            this.txtConfianza.TabIndex = 44;
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Location = new System.Drawing.Point(934, 119);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(136, 37);
            this.btnExcel.TabIndex = 43;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(126, 165);
            this.txtX.Margin = new System.Windows.Forms.Padding(2);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(116, 20);
            this.txtX.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(502, 158);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 18);
            this.label6.TabIndex = 41;
            this.label6.Text = "K";
            this.label6.Visible = false;
            // 
            // txtK
            // 
            this.txtK.Location = new System.Drawing.Point(536, 154);
            this.txtK.Margin = new System.Windows.Forms.Padding(2);
            this.txtK.Name = "txtK";
            this.txtK.Size = new System.Drawing.Size(116, 20);
            this.txtK.TabIndex = 40;
            this.txtK.Visible = false;
            // 
            // txtXFinal
            // 
            this.txtXFinal.Location = new System.Drawing.Point(332, 188);
            this.txtXFinal.Margin = new System.Windows.Forms.Padding(2);
            this.txtXFinal.Name = "txtXFinal";
            this.txtXFinal.Size = new System.Drawing.Size(116, 20);
            this.txtXFinal.TabIndex = 39;
            this.txtXFinal.TextChanged += new System.EventHandler(this.txtXFinal_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(260, 188);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 18);
            this.label5.TabIndex = 38;
            this.label5.Text = "X final";
            // 
            // txtXinicio
            // 
            this.txtXinicio.Location = new System.Drawing.Point(126, 187);
            this.txtXinicio.Margin = new System.Windows.Forms.Padding(2);
            this.txtXinicio.Name = "txtXinicio";
            this.txtXinicio.Size = new System.Drawing.Size(116, 20);
            this.txtXinicio.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 186);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 18);
            this.label4.TabIndex = 36;
            this.label4.Text = "X inicio";
            // 
            // txtResultados2
            // 
            this.txtResultados2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultados2.Location = new System.Drawing.Point(647, 7);
            this.txtResultados2.Margin = new System.Windows.Forms.Padding(2);
            this.txtResultados2.Multiline = true;
            this.txtResultados2.Name = "txtResultados2";
            this.txtResultados2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultados2.Size = new System.Drawing.Size(277, 138);
            this.txtResultados2.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 18);
            this.label3.TabIndex = 34;
            this.label3.Text = "Poblacion (N)";
            this.label3.Visible = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(934, 69);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(136, 43);
            this.btnLimpiar.TabIndex = 33;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnCalcular
            // 
            this.btnCalcular.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalcular.Location = new System.Drawing.Point(934, 7);
            this.btnCalcular.Margin = new System.Windows.Forms.Padding(2);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(136, 47);
            this.btnCalcular.TabIndex = 32;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // formsPlot1
            // 
            this.formsPlot1.DisplayScale = 0F;
            this.formsPlot1.Location = new System.Drawing.Point(608, 220);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(2);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(480, 408);
            this.formsPlot1.TabIndex = 31;
            // 
            // dgvResultados
            // 
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Location = new System.Drawing.Point(7, 215);
            this.dgvResultados.Margin = new System.Windows.Forms.Padding(2);
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.RowHeadersWidth = 82;
            this.dgvResultados.RowTemplate.Height = 33;
            this.dgvResultados.Size = new System.Drawing.Size(597, 413);
            this.dgvResultados.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 137);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 18);
            this.label2.TabIndex = 29;
            this.label2.Text = "Éxito (p)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 18);
            this.label1.TabIndex = 28;
            this.label1.Text = "Muestra(n)";
            // 
            // txtResultados
            // 
            this.txtResultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResultados.Location = new System.Drawing.Point(358, 8);
            this.txtResultados.Margin = new System.Windows.Forms.Padding(2);
            this.txtResultados.Multiline = true;
            this.txtResultados.Name = "txtResultados";
            this.txtResultados.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultados.Size = new System.Drawing.Size(277, 137);
            this.txtResultados.TabIndex = 27;
            // 
            // txtPoblacion
            // 
            this.txtPoblacion.Location = new System.Drawing.Point(130, 15);
            this.txtPoblacion.Margin = new System.Windows.Forms.Padding(2);
            this.txtPoblacion.Name = "txtPoblacion";
            this.txtPoblacion.Size = new System.Drawing.Size(116, 20);
            this.txtPoblacion.TabIndex = 26;
            this.txtPoblacion.Visible = false;
            // 
            // txtP
            // 
            this.txtP.Location = new System.Drawing.Point(126, 138);
            this.txtP.Margin = new System.Windows.Forms.Padding(2);
            this.txtP.Name = "txtP";
            this.txtP.Size = new System.Drawing.Size(116, 20);
            this.txtP.TabIndex = 25;
            // 
            // txtN
            // 
            this.txtN.Location = new System.Drawing.Point(130, 57);
            this.txtN.Margin = new System.Windows.Forms.Padding(2);
            this.txtN.Name = "txtN";
            this.txtN.Size = new System.Drawing.Size(116, 20);
            this.txtN.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 163);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 18);
            this.label7.TabIndex = 47;
            this.label7.Text = "Eventos x";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 102);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 18);
            this.label9.TabIndex = 48;
            this.label9.Text = "Lambda";
            // 
            // txtLambda
            // 
            this.txtLambda.Location = new System.Drawing.Point(126, 102);
            this.txtLambda.Margin = new System.Windows.Forms.Padding(2);
            this.txtLambda.Name = "txtLambda";
            this.txtLambda.Size = new System.Drawing.Size(116, 20);
            this.txtLambda.TabIndex = 49;
            // 
            // PoissonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 642);
            this.Controls.Add(this.txtLambda);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnCambiarGrafica);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtConfianza);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtK);
            this.Controls.Add(this.txtXFinal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtXinicio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtResultados2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.formsPlot1);
            this.Controls.Add(this.dgvResultados);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResultados);
            this.Controls.Add(this.txtPoblacion);
            this.Controls.Add(this.txtP);
            this.Controls.Add(this.txtN);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PoissonForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PoissonForm";
            this.Load += new System.EventHandler(this.PoissonForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCambiarGrafica;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtConfianza;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtK;
        private System.Windows.Forms.TextBox txtXFinal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtXinicio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtResultados2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCalcular;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResultados;
        private System.Windows.Forms.TextBox txtPoblacion;
        private System.Windows.Forms.TextBox txtP;
        private System.Windows.Forms.TextBox txtN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLambda;
    }
}