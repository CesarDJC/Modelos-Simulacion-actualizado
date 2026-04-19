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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        Form1 form1= new Form1();

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultar el formulario actual

            Form1 form1 = new Form1(); 
            form1.ShowDialog(); 

            this.Show(); 

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Remove(pictureBox1);
            this.Controls.Add(pictureBox1);
            pictureBox1.SendToBack();

            this.Controls.SetChildIndex(pictureBox1, 0); 
            foreach (Control control in this.Controls)
            {
                if (control != pictureBox1)
                {
                    control.BringToFront();
                }
            }

         
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label1.ForeColor = Color.Red;
            button1.BackColor = Color.Yellow; 
        }

        private void Menu_Load_1(object sender, EventArgs e)
        {

        }

        private void btnPoisson_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultar el formulario actual

            PoissonForm formPoisson = new PoissonForm();
            formPoisson.ShowDialog();

            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultar el formulario actual
            MM1Form mm1= new MM1Form();
            mm1.ShowDialog();

            this.Show();
        }
    }
}
