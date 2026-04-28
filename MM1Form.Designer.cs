namespace ModelosDiscretosyContinuos
{
    partial class MM1Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLlegadas = new System.Windows.Forms.TextBox();
            this.txtServicio = new System.Windows.Forms.TextBox();
            this.txtClientes = new System.Windows.Forms.TextBox();
            this.txtLs = new System.Windows.Forms.TextBox();
            this.txtLq = new System.Windows.Forms.TextBox();
            this.txtWq = new System.Windows.Forms.TextBox();
            this.txtWs = new System.Windows.Forms.TextBox();
            this.txtPorcentajeUso = new System.Windows.Forms.TextBox();
            this.txtSistemaOcioso = new System.Windows.Forms.TextBox();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnMostrarGrafico = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNclientes = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chEntradaMinutos = new System.Windows.Forms.CheckBox();
            this.chEntradaHoras = new System.Windows.Forms.CheckBox();
            this.chSalidaMinutos = new System.Windows.Forms.CheckBox();
            this.chSalidaHoras = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(73, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Promedio de llegadas (λ)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(73, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Promedio de servicio (μ)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(73, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Especificar clientes para (pn)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(73, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Número de clientes en el sistema (Ls)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(73, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(229, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Número de clientes en la cola (Lq)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(73, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(224, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tiempo de espera en la cola (Wq)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(73, 350);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(246, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Tiempo de espera en el sistema (Ws)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(73, 400);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(227, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "Porcentaje de uso del sistema (ρ )";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(73, 443);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(350, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "Probabilidad de que el sistema esté desocupado (ρ0)";
            // 
            // txtLlegadas
            // 
            this.txtLlegadas.Location = new System.Drawing.Point(329, 65);
            this.txtLlegadas.Name = "txtLlegadas";
            this.txtLlegadas.Size = new System.Drawing.Size(126, 20);
            this.txtLlegadas.TabIndex = 9;
            // 
            // txtServicio
            // 
            this.txtServicio.Location = new System.Drawing.Point(329, 103);
            this.txtServicio.Name = "txtServicio";
            this.txtServicio.Size = new System.Drawing.Size(126, 20);
            this.txtServicio.TabIndex = 10;
            // 
            // txtClientes
            // 
            this.txtClientes.Location = new System.Drawing.Point(329, 142);
            this.txtClientes.Name = "txtClientes";
            this.txtClientes.Size = new System.Drawing.Size(126, 20);
            this.txtClientes.TabIndex = 11;
            // 
            // txtLs
            // 
            this.txtLs.Location = new System.Drawing.Point(338, 217);
            this.txtLs.Name = "txtLs";
            this.txtLs.Size = new System.Drawing.Size(126, 20);
            this.txtLs.TabIndex = 12;
            this.txtLs.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // txtLq
            // 
            this.txtLq.Location = new System.Drawing.Point(338, 263);
            this.txtLq.Name = "txtLq";
            this.txtLq.Size = new System.Drawing.Size(126, 20);
            this.txtLq.TabIndex = 13;
            // 
            // txtWq
            // 
            this.txtWq.Location = new System.Drawing.Point(338, 307);
            this.txtWq.Name = "txtWq";
            this.txtWq.Size = new System.Drawing.Size(126, 20);
            this.txtWq.TabIndex = 14;
            // 
            // txtWs
            // 
            this.txtWs.Location = new System.Drawing.Point(338, 350);
            this.txtWs.Name = "txtWs";
            this.txtWs.Size = new System.Drawing.Size(126, 20);
            this.txtWs.TabIndex = 15;
            // 
            // txtPorcentajeUso
            // 
            this.txtPorcentajeUso.Location = new System.Drawing.Point(338, 400);
            this.txtPorcentajeUso.Name = "txtPorcentajeUso";
            this.txtPorcentajeUso.Size = new System.Drawing.Size(126, 20);
            this.txtPorcentajeUso.TabIndex = 16;
            // 
            // txtSistemaOcioso
            // 
            this.txtSistemaOcioso.Location = new System.Drawing.Point(440, 443);
            this.txtSistemaOcioso.Name = "txtSistemaOcioso";
            this.txtSistemaOcioso.Size = new System.Drawing.Size(120, 20);
            this.txtSistemaOcioso.TabIndex = 17;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalcular.Location = new System.Drawing.Point(604, 238);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(104, 41);
            this.btnCalcular.TabIndex = 19;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(604, 308);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(104, 41);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnMostrarGrafico
            // 
            this.btnMostrarGrafico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMostrarGrafico.Location = new System.Drawing.Point(604, 376);
            this.btnMostrarGrafico.Name = "btnMostrarGrafico";
            this.btnMostrarGrafico.Size = new System.Drawing.Size(104, 39);
            this.btnMostrarGrafico.TabIndex = 21;
            this.btnMostrarGrafico.Text = "Mostrar";
            this.btnMostrarGrafico.UseVisualStyleBackColor = true;
            this.btnMostrarGrafico.Click += new System.EventHandler(this.btnMostrarGrafico_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(73, 490);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(361, 15);
            this.label10.TabIndex = 22;
            this.label10.Text = "Probabilidad de que haya n clientes en el sistema (ρn) ";
            // 
            // txtNclientes
            // 
            this.txtNclientes.Location = new System.Drawing.Point(440, 490);
            this.txtNclientes.Name = "txtNclientes";
            this.txtNclientes.Size = new System.Drawing.Size(120, 20);
            this.txtNclientes.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(487, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 15);
            this.label11.TabIndex = 24;
            this.label11.Text = "Datos de entrada";
            // 
            // chEntradaMinutos
            // 
            this.chEntradaMinutos.AutoSize = true;
            this.chEntradaMinutos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chEntradaMinutos.Location = new System.Drawing.Point(490, 79);
            this.chEntradaMinutos.Name = "chEntradaMinutos";
            this.chEntradaMinutos.Size = new System.Drawing.Size(134, 17);
            this.chEntradaMinutos.TabIndex = 25;
            this.chEntradaMinutos.Text = "Clientes por minuto";
            this.chEntradaMinutos.UseVisualStyleBackColor = true;
            this.chEntradaMinutos.CheckedChanged += new System.EventHandler(this.chEntradaMinutos_CheckedChanged);
            // 
            // chEntradaHoras
            // 
            this.chEntradaHoras.AutoSize = true;
            this.chEntradaHoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chEntradaHoras.Location = new System.Drawing.Point(490, 109);
            this.chEntradaHoras.Name = "chEntradaHoras";
            this.chEntradaHoras.Size = new System.Drawing.Size(122, 17);
            this.chEntradaHoras.TabIndex = 26;
            this.chEntradaHoras.Text = "Clientes por hora";
            this.chEntradaHoras.UseVisualStyleBackColor = true;
            this.chEntradaHoras.CheckedChanged += new System.EventHandler(this.chEntradaHoras_CheckedChanged);
            // 
            // chSalidaMinutos
            // 
            this.chSalidaMinutos.AutoSize = true;
            this.chSalidaMinutos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chSalidaMinutos.Location = new System.Drawing.Point(699, 79);
            this.chSalidaMinutos.Name = "chSalidaMinutos";
            this.chSalidaMinutos.Size = new System.Drawing.Size(70, 17);
            this.chSalidaMinutos.TabIndex = 27;
            this.chSalidaMinutos.Text = "Minutos";
            this.chSalidaMinutos.UseVisualStyleBackColor = true;
            this.chSalidaMinutos.CheckedChanged += new System.EventHandler(this.chSalidaMinutos_CheckedChanged);
            // 
            // chSalidaHoras
            // 
            this.chSalidaHoras.AutoSize = true;
            this.chSalidaHoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chSalidaHoras.Location = new System.Drawing.Point(699, 109);
            this.chSalidaHoras.Name = "chSalidaHoras";
            this.chSalidaHoras.Size = new System.Drawing.Size(59, 17);
            this.chSalidaHoras.TabIndex = 28;
            this.chSalidaHoras.Text = "Horas";
            this.chSalidaHoras.UseVisualStyleBackColor = true;
            this.chSalidaHoras.CheckedChanged += new System.EventHandler(this.chSalidaHoras_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(680, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 15);
            this.label12.TabIndex = 29;
            this.label12.Text = "Datos de salida";
            // 
            // MM1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 528);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.chSalidaHoras);
            this.Controls.Add(this.chSalidaMinutos);
            this.Controls.Add(this.chEntradaHoras);
            this.Controls.Add(this.chEntradaMinutos);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtNclientes);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnMostrarGrafico);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.txtSistemaOcioso);
            this.Controls.Add(this.txtPorcentajeUso);
            this.Controls.Add(this.txtWs);
            this.Controls.Add(this.txtWq);
            this.Controls.Add(this.txtLq);
            this.Controls.Add(this.txtLs);
            this.Controls.Add(this.txtClientes);
            this.Controls.Add(this.txtServicio);
            this.Controls.Add(this.txtLlegadas);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MM1Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MM1Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLlegadas;
        private System.Windows.Forms.TextBox txtServicio;
        private System.Windows.Forms.TextBox txtClientes;
        private System.Windows.Forms.TextBox txtLs;
        private System.Windows.Forms.TextBox txtLq;
        private System.Windows.Forms.TextBox txtWq;
        private System.Windows.Forms.TextBox txtWs;
        private System.Windows.Forms.TextBox txtPorcentajeUso;
        private System.Windows.Forms.TextBox txtSistemaOcioso;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnMostrarGrafico;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNclientes;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chEntradaMinutos;
        private System.Windows.Forms.CheckBox chEntradaHoras;
        private System.Windows.Forms.CheckBox chSalidaMinutos;
        private System.Windows.Forms.CheckBox chSalidaHoras;
        private System.Windows.Forms.Label label12;
    }
}