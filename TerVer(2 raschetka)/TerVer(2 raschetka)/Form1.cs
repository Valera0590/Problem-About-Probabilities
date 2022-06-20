using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*class NormalRandom : Random
{
    // сохранённое предыдущее значение
    double prevSample = double.NaN;
    protected override double Sample()
    {
        // есть предыдущее значение? возвращаем его
        if (!double.IsNaN(prevSample))
        {
            double result = prevSample;
            prevSample = double.NaN;
            return result;
        }

        // нет? вычисляем следующие два
        // Marsaglia polar method из википедии
        double u, v, s;
        do
        {
            u = 2 * base.Sample() - 1;
            v = 2 * base.Sample() - 1; // [-1, 1)
            //u = base.Sample() * (5 - 3) + 3;
            //v = base.Sample() * (5 - 3) + 3; // [3, 5)
            s = u * u + v * v;
        }
        while (u <= -1 || v <= -1 || s >= 1 || s == 0);
        double r = Math.Sqrt(-2 * Math.Log(s) / s);

        prevSample = r * v;
        return r * u;
    }
}*/

namespace TerVer_2_raschetka_
{
    public partial class Form1 : Form
    {
        public double Mx = 0, Dx = 0, Mo = 0, Med = 0, As = 0, Ex = 0, SumSqr = 0;
        public int step , temp;
        public int n = 200;
        public int valueInterval = (int)(Math.Log(200,2) + 1);
        public List<double> val = new List<double>();      //список ошибок
        public List<double> errors = new List<double>();      //список ошибок дополнительный
        public List<int> intervals = new List<int>();      //список интервалов

       
        private void button2_Click(object sender, EventArgs e)  //проверка гипотезы о равенстве мат ожидания нулю
        {
            Form3 f3 = new Form3(CriticalPoint(), n - 1);
            f3.Show();
        }

        private void b_rand_err_Click(object sender, EventArgs e)
        {
            int n1=0, n2=0, i = 0, KS = 1;
            bool series1 = false, series2 = false;
            double Z=0;
            do
            {
                if (errors[i] > Med)
                {
                    n1++;
                    series1 = true;
                } else if(errors[i] < Med)
                {
                    n2++;
                    series2 = true;
                }
                i++;
            } while ((i < n) && (errors[i] == Med));
            for (; i < n; i++)
            {
                if (errors[i] > Med)
                {
                    n1++;
                    if(series2==true)
                    {
                        KS++;
                        series1 = true;
                        series2 = false;
                    }
                }
                else if (errors[i] < Med)
                {
                    n2++;
                    if (series1 == true)
                    {
                        KS++;
                        series2 = true;
                        series1 = false;
                    }
                }
            }
            Z = ((double)KS - (double)2 * n1 * n2 / (n1 + n2) - 3 / 2.0) / Math.Sqrt((double)2 * n1 * n2 * (2 * n1 * n2 - (n1 + n2)) / (Math.Pow(n1 + n2, 2) * (n1 + n2 - 1)));
            Form4 f4 = new Form4(Z);
            f4.Show();
        }

        private void b_type_rasp_err_Click(object sender, EventArgs e)
        {
            double start = val[0]+step/2.0;
            double Z = 0, fz, ni, Xi_search=0;
            for (int i = 0; i < valueInterval; i++, start+=step)
            {
                if (intervals[i] != 0)
                {
                    Z = (start - Mx) / Math.Sqrt(Dx);
                    fz = Math.Exp(-Math.Pow(Z, 2) / 2) / Math.Sqrt(Math.PI * 2);
                    ni = step * n * fz / Math.Sqrt(Dx);
                    Xi_search += Math.Pow(intervals[i] - ni, 2) / intervals[i];
                }
            }

            Form5 f5 = new Form5(Xi_search, intervals.Count-3);
            f5.Show();
        }

        public Form1()
        {
            InitializeComponent();
            GridFill();
            tb_OMM.Text ="a = " + Convert.ToString(Math.Round(Mx,3)) + "   σ = " + Convert.ToString(Math.Round(Math.Sqrt(Dx),3));
            tb_OMP.Text = tb_OMM.Text;
            tb_doverit.Text = "(" + Convert.ToString(Math.Round(Mx - 2.33 * Math.Sqrt(Dx) / Math.Sqrt(200),13))
                + " ; " + Convert.ToString(Math.Round(Mx + 2.33 * Math.Sqrt(Dx) / Math.Sqrt(200), 13)) + ")";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /*private double GetNormalDistributedValue(int value, int cyclesCount = 220)
        {
            Random r = new Random();
            double result = 0;
            for (int i = 0; i <= cyclesCount; i++)
            {
                result += r.Next(value * 2);
            }
            result /= cyclesCount;
            return result;
        }*/

        /*private double Power(double x, int pow)     //метод возведения числа в целую степень
        {
            double prod = x;
            while (pow > 1)
            {
                prod *= x;
                pow--;
            }
            return prod;
        }*/

        public double CriticalPoint(double a0=0)
        {
            double U = Math.Sqrt(n) * (Mx - a0) / Math.Sqrt(Dx);
            return U;
        }

        private double CentralMoment(List<double> X, double Mx, int n, int k)   //метод нахождения центрального момента к-го порядка
        {
            double v=0;
            foreach (double xi in X)    v += Math.Pow(xi-Mx, k);
            v /= n;
            return v;
        }

        public void GridFill()  //метод заполнения таблицы
        {
            
            Random randomValue = new Random();
            //Random r = new Random();
            double value1, integr, result = 3552;//, z1 = 0, z2 = 0, x, y;
            
            List<string[]> data = new List<string[]>();
            for (int i = 0; i < n; i++)
            {
                data.Add(new string[3]);
                integr = 0;
                /*if (i % 2 == 0)
                {
                    x = r.NextDouble();
                    y = r.NextDouble();
                    if (x != 0 & y != 0)
                    {
                        z1 = Math.Cos(2 * Math.PI * y) * Math.Sqrt(-2 * Math.Log10(x)) * (5 - 3) + 3;
                        z2 = Math.Sin(2 * Math.PI * y) * Math.Sqrt(-2 * Math.Log10(x)) * (5 - 3) + 3;
                    }
                }*/
                value1 = randomValue.NextDouble() * (5 - 3) + 3;    //*(b-a) + a
                for (int j = 0; j < 100; j++)
                {
                    value1 = randomValue.NextDouble() * (5 - 3) + 3;
                    integr += Math.Pow(2 * value1 + 4, 3);
                    //if (i % 2 == 0) integr += Power(2*z1+4, 3);
                    //else integr += Power(2 * z2 + 4, 3);
                }
                integr *= 2/100.0;
                data[data.Count - 1][0] = Convert.ToString(i + 1);
                data[data.Count - 1][1] = Convert.ToString(integr);
                data[data.Count - 1][2] = Convert.ToString(result-integr);
                
                
                //if(i%2==0)  data[data.Count - 1][2] = Convert.ToString(z1);
                //else data[data.Count - 1][2] = Convert.ToString(z2);
            }
            /*double Sred=0;
            for (int i1 = 0; i1 < n; i1++) Sred += Convert.ToDouble(data[i1][1]);
                textBox6.Text = Convert.ToString(Sred/n);*/
            foreach (string[] s in data)
            {
                dataGridView1.Rows.Add(s);
            }
            SndFunc(data, n);
        }
        
        private void SndFunc(List<string[]> data1, int n)
        {
            
            foreach(string[] str in data1)
            {
                val.Add(double.Parse(str[2]));
                errors.Add(double.Parse(str[2]));
                SumSqr += Math.Pow(double.Parse(str[2]), 2);
            }
            Mx = val.Sum() / n;
            SumSqr /= n;    //мат ожидание от X^2
            Dx = SumSqr - Math.Pow(Mx, 2);
            As = CentralMoment(val, Mx, n, 3) / (Math.Pow(Math.Sqrt(Dx), 3));
            Ex = CentralMoment(val, Mx, n, 4) / Math.Pow(Dx, 2) - 3;
            textBox1.Text = Convert.ToString(Mx);
            textBox2.Text = Convert.ToString(Dx);
            val.Sort();
            Med = (val[n / 2 - 1] + val[n / 2]) / 2;
            //valueInterval = (int)Math.Ceiling((val[n - 1] - val[0]) / step);
            step = (int) Math.Ceiling((val[n - 1] - val[0]) / valueInterval) + 1;  //нахождение шага интервала (кол-во интервалов - )
            for (int i = 0; i < valueInterval; i++)
            {
                intervals.Add(0);
            }
            /*foreach (double err in val)     //деление на интервалы
            {
                if(err < 0) temp = 5 + (int)Math.Ceiling(err/step);
                else temp = 5 + (int)Math.Floor(err / step);
                if (temp < 0 || temp > 9) temp = Math.Abs(temp) - 1;
                intervals[temp]++;
            }*/
            int j = 0;
            for (int i = 0; i < valueInterval; i++)     //деление на интервалы
            {
                while((j<n) && (val[0] + (i)*step >= val[j]))
                {
                    intervals[i]++;
                    j++;
                }
            }
            while (j < n)
            {
                intervals[intervals.Count - 1]++;   //если не все ошибки распределились по интервалам
                j++;
            }
            //Находим моду по самому высокому интервалу
            temp = intervals[0];
            j = 0;
            for(int i = 1; i < valueInterval; i++)
            {
                if (intervals[i] > temp)
                {
                    temp = intervals[i];
                    j = i;      //индекс самого высокого интервала
                }
            }
            //tb_OMM.Text = Convert.ToString(intervals.Sum());
            Mo = val[0] + (double)j * step + (double)step / 2;
            textBox3.Text = Convert.ToString(Mo);
            textBox4.Text = Convert.ToString(Med);
            textBox5.Text = Convert.ToString(As);
            textBox6.Text = Convert.ToString(Ex);
            //GetBarChart(val[0], step, intervals, Dx, Mx);       //вызов метода построения гистограммы
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            //this.Hide();
            f2.GetBarChart(val[0], step, intervals, Dx, Mx);       //вызов метода построения гистограммы
            f2.Show();
            
        }
    }
}
