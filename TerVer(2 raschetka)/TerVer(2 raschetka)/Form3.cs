using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerVer_2_raschetka_
{
    public partial class Form3 : Form
    {
        //double Mx, Dx;
        //int n;
        double U;
        int k;
        public Form3(double U, int k)
        {
            this.U = U;
            this.k = k;
            InitializeComponent();
            //Form1 f1 = new Form1();
            //n = f1.n;
            //Mx = f1.Mx;
            //Dx = f1.Dx;
            button1.Enabled = false;
            //button2.Enabled = false;
            label8.Text = Convert.ToString(U);
            label6.Text = Convert.ToString(k);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form1 f1 = new Form1();
            //double a0 = Convert.ToDouble(textBox3.Text);
            //double U = Math.Sqrt(n) * (Mx - a0) / Math.Sqrt(Dx);
            // double U = Math.Sqrt(f.n) * (f.Mx - a0) / Math.Sqrt(f.Dx);
            //double U = Math.Sqrt(200) * (0.5 - a0) / Math.Sqrt(0.64);
            //double U = f1.CriticalPoint();
            //label8.Text = Convert.ToString(U);
            if ((-Convert.ToDouble(textBox2.Text) >= U) || (Convert.ToDouble(textBox2.Text) <= U)) textBox4.Text = "Нет оснований принимать гипотезу Ho с данным уровнем значимости";
            else textBox4.Text = "Гипотеза Но не может быть отвергнута при данном уровне значимости";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //double lvl = (2 - Convert.ToDouble(textBox1.Text)) / 2;
            //label5.Text = Convert.ToString(lvl);
            //label5.Text = "";
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength != 0 && textBox2.TextLength != 0) button1.Enabled = true;
            else button1.Enabled = false;
            if (textBox1.TextLength != 0)
            {
                //Form1 f1 = new Form1();
                //double a0 = Convert.ToDouble(textBox3.Text);
                // double U = Math.Sqrt(f.n) * (f.Mx - a0) / Math.Sqrt(f.Dx);
                //double U = Math.Sqrt(200) * (0.5 - a0) / Math.Sqrt(0.64);
                //double U = Math.Sqrt(n) * (Mx - a0) / Math.Sqrt(Dx);
                //double U = f1.CriticalPoint();
                double lvl = (2 - Convert.ToDouble(textBox1.Text)) / 2;
                label4.Text = "(квантиль уровня " + Convert.ToString(lvl) + " )";
            }
            else label4.Text = "(квантиль уровня )";
            textBox4.Text = "";
        }

        //private void textBox3_TextChanged(object sender, EventArgs e)
        //{
        //    if (textBox1.TextLength != 0 && textBox2.TextLength != 0 && textBox3.TextLength != 0) button1.Enabled = true;
        //    else button1.Enabled = false;
            
        //}

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
            if (textBox1.TextLength != 0 && textBox2.TextLength != 0) button1.Enabled = true;
            else button1.Enabled = false;
            textBox4.Text = "";
        }
    }
}
