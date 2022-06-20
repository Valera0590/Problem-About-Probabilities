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
    public partial class Form4 : Form
    {
        double Z;
        public Form4(double Z)
        {
            this.Z = Z;
            InitializeComponent();
            button1.Enabled = false;
            label8.Text = Convert.ToString(Math.Abs(Z));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(textBox2.Text) >= Math.Abs(Z)) textBox4.Text = "Гипотеза Hо принимается с данным уровнем значимости";
            else textBox4.Text = "Нет оснований принимать гипотезу Ho с данным уровнем значимости";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength != 0 && textBox2.TextLength != 0) button1.Enabled = true;
            else button1.Enabled = false;
            if (textBox1.TextLength != 0)
            {
                
                double lvl = (2 - Convert.ToDouble(textBox1.Text)) / 2;
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
    }
}
