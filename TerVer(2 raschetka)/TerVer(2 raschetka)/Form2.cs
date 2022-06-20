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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void GetBarChart(double from, int step, List<int> massY, double sig2, double a)    //метод построения гистограммы
        {
            Form1 f1 = new Form1();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart2.Series[0].Points.Clear();
            double beg = from;
                //Деление по оси Х на интервалы 
            chart1.ChartAreas[0].AxisX.Interval = step;
            chart2.ChartAreas[0].AxisX.Interval = step;
            chart1.ChartAreas[0].AxisX.Minimum = from;
            chart1.ChartAreas[0].AxisX.Maximum = from + step * f1.valueInterval;
            chart2.ChartAreas[0].AxisX.Minimum = from;
            chart2.ChartAreas[0].AxisX.Maximum = from + step * f1.valueInterval;
            //Деление по оси Y на интервалы
            chart1.ChartAreas[0].AxisY.Interval = 0.0005;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 0.006;
            chart2.ChartAreas[0].AxisY.Interval = 0.0005;
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisY.Maximum = 0.006;

            chart1.Series[1].Points.AddXY(from, 1 / (Math.Sqrt(2 * Math.PI * sig2)) * Math.Exp(-Math.Pow(from - a, 2) / (2 * sig2)));
            for (int i = 0; i < massY.Count; i++)
            {
                chart1.Series[0].Points.AddXY(beg + step / 2, massY[i] / (double)(step * f1.n));
                chart2.Series[0].Points.AddXY(beg + step / 2, massY[i] / (double)(step * f1.n));
                chart1.Series[1].Points.AddXY(beg, 1 / (Math.Sqrt(2 * Math.PI * sig2)) * Math.Exp(-Math.Pow(beg - a, 2) / (2 * sig2)));
                beg += step;
            }
            
            chart1.Series[1].Points.AddXY(from + step * (f1.valueInterval + 1), 1 / (Math.Sqrt(2 * Math.PI * sig2)) * Math.Exp(-Math.Pow(from + step * (f1.valueInterval + 1) - a, 2) / (2 * sig2)));
            /*for (int i = (int)from+step/2; i <= (int)(from + step * 11); i+=step)
            {
                //chart2.Series[0].Points.AddXY(i, 1 / (Math.Sqrt(2 * Math.PI * sig2)) * Math.Exp(-Math.Pow(i - a, 2) / (2 * sig2)));
                chart1.Series[1].Points.AddXY(i, 1 / (Math.Sqrt(2 * Math.PI * sig2)) * Math.Exp(-Math.Pow(i - a, 2) / (2 * sig2)));

            }*/
        }

        private void chart2_Click(object sender, EventArgs e)
        {
            
        }

       
    }
}
