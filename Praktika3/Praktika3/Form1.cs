using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using  Library;
using static Library.Class;


namespace Praktika3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                H = Convert.ToDouble(textBoxH0.Text);
                Tg0 = Convert.ToDouble(textBoxTg0.Text);
                Tm0 = Convert.ToDouble(textBoxTm0.Text);
                Ug = Convert.ToDouble(textBoxWg.Text);
                crteplo = Convert.ToDouble(textBoxCrteplo.Text);
                rasxod = Convert.ToDouble(textBoxRasxod.Text);
                teploM = Convert.ToDouble(textBoxTeploM.Text);
                Vkof = Convert.ToDouble(textBoxAlfaV.Text);
                d = Convert.ToDouble(textBoxD.Text);

            }
            catch (FormatException)
            {
                MessageBox.Show("Неверно введено число. \nВнимательно проверьте введённые значения");
            }
            if (Class.H == 0 || Class.Ug == 0 || Class.crteplo == 0 ||
                Class.rasxod == 0 || Class.teploM == 0 ||
                Class.Vkof == 0 || Class.d == 0 ) 
            {
                MessageBox.Show("Это значение не может равняться нулю");
            }
            else 
            {
                Class.m = Class.M(Class.teploM, Class.rasxod, Class.Ug, Class.crteplo, Class.d);
                Class.y0 = Class.Y0(Class.Vkof, Class.H, Class.Ug, Class.crteplo);
                Class.e1 = Class.E1(Class.m, Class.y0);
                textBoxM.Text = Convert.ToString(Class.m);
                textBoxY0.Text = Convert.ToString(Class.y0);
                textBoxE1.Text = Convert.ToString(Class.e1);
                DataTable dt = new DataTable();
                double x = 0;
                int razm = Convert.ToInt32(Class.H * 2 + 1);
                double[] y = new double[razm];
                double[] e2 = new double[razm];
                double[] e3 = new double[razm];
                double[] d1 = new double[razm];
                double[] d2 = new double[razm];
                t1 = new double[razm];
                t2 = new double[razm];
                T = new double[razm];
               

                for (int i = 0; i <= Class.H * 2; i++)
                {
                    dt.Columns.Add(Convert.ToString(x));
                    x += 0.5;
                }

                for (int i = 0; i < 8; i++)
                {
                    dt.Rows.Add();
                }
                x = 0;

                for (int i = 0; i <= Class.H * 2; i++)
                {
                    y[i] = Class.Y(Class.Vkof, Class.Ug, x, Class.crteplo);
                    e2[i] = Class.E2(Class.m, y[i]);
                    e3[i] = Class.E3(Class.m, y[i]);
                    d1[i] = Class.D1(Class.y0, e2[i], Class.m);
                    d2[i] = Class.D2(Class.y0, e3[i], Class.m);
                    t1[i] = T1(Tm0, Tg0, d1[i]);
                    t2[i] = T2(Tm0, Tg0, d2[i]);
                    T[i] = t1[i] - t2[i];
                    

                    x += 0.5;
                    dt.Rows[0][i] = y[i];
                    dt.Rows[1][i] = e2[i];
                    dt.Rows[2][i] = e3[i];
                    dt.Rows[3][i] = d1[i];
                    dt.Rows[4][i] = d2[i];
                    dt.Rows[5][i] = t1[i];
                    dt.Rows[6][i] = t2[i];
                    dt.Rows[7][i] = T[i];

                }
                dataGridView1.DataSource = dt;
                dataGridView1.Invalidate();
                tabControl1.SelectedTab = tabControl1.TabPages["TabPage2"];
            }
        }

        //График
        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            double x = 0;
            for (int i = 0; i < H*2+1; i++)
            {
                double y = T[i];
                chart1.Series[0].Points.AddXY(x, y);
                x += 0.5;
            }

            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
           
            for (int i = 0; i < H * 2 + 1; i++)
            {
                double y = t1[i];
                chart2.Series[0].Points.AddXY(x, y);
                x += 0.5;
            }

            chart2.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            for (int i = 0; i < H * 2 + 1; i++)
            {
                double y = t2[i];
                chart2.Series[1].Points.AddXY(x, y);
                x += 0.5;
            }
            tabControl1.SelectedTab = tabControl1.TabPages["TabPage3"];



        }

        private void textBoxH0_KeyPress(object sender, KeyPressEventArgs e)
        {

        char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }

        }

        private void textBoxTm0_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void textBoxTg0_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void textBoxWg_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void textBoxCrteplo_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void textBoxRasxod_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void textBoxTeploM_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void textBoxAlfaV_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void textBoxD_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void разностьТемпературОкатышейИГазаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            double x = 0;
            for (int i = 0; i < H * 2 + 1; i++)
            {
                double y = T[i];
                chart1.Series[0].Points.AddXY(x, y);
                x += 0.5;
                tabControl1.SelectedTab = tabControl1.TabPages["TabPage3"];
            }

        }

        
    }
}
    



