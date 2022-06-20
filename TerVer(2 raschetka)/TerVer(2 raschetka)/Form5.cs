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
    public partial class Form5 : Form
    {
        double Xi2;
        int k;
        public Form5(double Xi2, int k)
        {
            this.Xi2 = Xi2;
            this.k = k;
            InitializeComponent();
            button1.Enabled = false;
            label8.Text = Convert.ToString(k);
            label5.Text = Convert.ToString(Xi2);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength != 0 && textBox2.TextLength != 0) button1.Enabled = true;
            else button1.Enabled = false;
            if (textBox1.TextLength != 0)
            {

                double lvl = 1 - Convert.ToDouble(textBox1.Text);
                label4.Text = "(квантиль уровня " + Convert.ToString(lvl) + " )";
            }
            else label4.Text = "(квантиль уровня )";
            textBox4.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength != 0 && textBox2.TextLength != 0) button1.Enabled = true;
            else button1.Enabled = false;
            textBox4.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(textBox2.Text) > Xi2) textBox4.Text = "Гипотеза Hо принимается с данным уровнем значимости";
            else textBox4.Text = "Гипотеза Ho отвергается с данным уровнем значимости";
        }
    }
}
