using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics canvas;
        Pen p = new Pen(Color.Black, 1);

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            canvas = panel1.CreateGraphics();
            double x0, y0,xf,yf;
            try
            {
                textBox1.Text = textBox1.Text.Replace('.', ',');
                textBox2.Text = textBox2.Text.Replace('.', ',');
                textBox3.Text = textBox1.Text.Replace('.', ',');
                textBox4.Text = textBox2.Text.Replace('.', ',');
                x0 = Convert.ToDouble(textBox1.Text);
                y0 = Convert.ToDouble(textBox2.Text);
                xf = Convert.ToDouble(textBox3.Text);
                yf = Convert.ToDouble(textBox4.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте введенные данные");
                return;
            }

        }
    }
}
