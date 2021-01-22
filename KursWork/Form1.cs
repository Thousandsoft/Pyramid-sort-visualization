using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursWork
{
    public partial class Form1 : Form
    {
        public int integer = 0;
        Random rnd = new Random();
        public bool isPaused = false;
        public bool firstClick = true;
        TimeSpan LastTime;
        ToolTip tip = new ToolTip();

        public Form1()
        {
            
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pauseContinue_Click(object sender, EventArgs e)
        {


            if (textBox3.Text != "")
            {
                
                if (checkBox1.Checked != true)
                {
                    timerLabel.Text = "";
                    listView1.Items.Clear();
                    listView2.Items.Clear();
                    int count = Convert.ToInt32(textBox3.Text);
                    double[] arr = new Double[count];
                    for (int i = 0; i < count; i++)
                    {
                        listView1.Items.Add(Convert.ToString(rnd.Next(-999, 999)));
                        arr[i] = Convert.ToDouble(listView1.Items[i].Text);
                    }
                    DateTime StartTime = DateTime.Now;
                    HeapSort.sorting(arr, count);
                    DateTime EndTime = DateTime.Now;
                    LastTime = EndTime - StartTime;
                    timerLabel.Text = "Время сортировки: " + (LastTime.TotalSeconds);
                    for (int i = 0; i < count; i++)
                    {
                        listView2.Items.Add(Convert.ToString(arr[i]));
                    }
                }
                else
                {
                    timer1.Interval = 2000;
                    endButton.Enabled = true;
                    cancelButton.Enabled = true;
                    checkBox1.Enabled = false;
                    if (!isPaused && firstClick) { pauseContinue.Image = Properties.Resources.icons8_пауза_96;
                        timerLabel.Text = "";
                        listView1.Items.Clear();
                        listView2.Items.Clear();
                        int count = Convert.ToInt32(textBox3.Text);
                        double[] arr = new Double[count];
                        for (int i = 0; i < count; i++)
                        {
                            listView1.Items.Add(Convert.ToString(rnd.Next(-999, 999)));
                            arr[i] = Convert.ToDouble(listView1.Items[i].Text);
                        }
                        integer = listView1.Items.Count / 2 - 1;
                        firstClick = false;
                        timer1.Enabled = true;
                        
                        

                    }
                    else if (isPaused == false && firstClick == false)
                    {   timer1.Enabled = false;
                        isPaused = true;
                        pauseContinue.Image = Properties.Resources.icons8_play_в_круге_96;
                        cancelButton.Enabled = false;
                        endButton.Enabled = false;
                    }
                    else if (isPaused == true && firstClick == false)
                    {
                        isPaused = !isPaused;
                        timer1.Enabled = true;
                        pauseContinue.Image = Properties.Resources.icons8_пауза_96;
                        
                    }

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tip.SetToolTip(pauseContinue, "Запустить/поставить на паузу");
            tip.SetToolTip(checkBox1, "Включить/выключить режим отображения сортировки по итерациям");
            tip.SetToolTip(textBox3, "Ввод количества случайно генерируемых элементов для сортировки");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                
                if (integer >= 0)
                {
                    sorting(listView1, listView1.Items.Count);
                    integer--;
                }
                else
                {
                    listView2.Items.Insert(0,listView1.Items[0].Text);
                    listView1.Items[0].Remove();
                    integer = listView1.Items.Count / 2 - 1;

                }
            }
            else
            {
                timer1.Enabled = false;
                pauseContinue.Image = Properties.Resources.icons8_play_в_круге_96;
                isPaused = false;
                endButton.Enabled = false;
                cancelButton.Enabled = false;
                firstClick = true;
                checkBox1.Enabled = true;
            }

            
        }
        public int add2pyramid(ListView list, int i, int N)
        {
            int imax;
            double buf;
            
            if ((2 * i + 2) < N)
            {

                if (Convert.ToInt32(list.Items[2 * i + 1].Text) < Convert.ToInt32(list.Items[2 * i + 2].Text)) { imax = 2 * i + 2; /*list.Items[2 * i + 1].ForeColor = Color.Red;*/ }
                else { imax = 2 * i + 1;  }
                }
            else
            {
                list.Items[2 * i + 1].ForeColor = Color.Red;
                imax = 2 * i + 1;
                
            }

            if (imax >= N)
            {
                
                return i;
            }
            if (Convert.ToInt32(list.Items[i].Text) < Convert.ToInt32(list.Items[imax].Text))
            {
                
                list.Items[i].ForeColor = Color.Green;
                list.Items[imax].ForeColor = Color.Red;
                buf = Convert.ToInt32(list.Items[i].Text);
                list.Items[i].Text = list.Items[imax].Text;
                list.Items[imax].Text = Convert.ToString(buf);
                
                if (imax < N / 2) i = imax;
            }
            
            return i;
        }
        public void sorting(ListView list, int len)
        {
            
                
                
            long prev_i = integer;
            integer = add2pyramid(list, integer, len);
            
            if (prev_i != integer) ++integer;
           
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            double num = 0.0;

            if (!(double.TryParse(textBox3.Text, out num)))
            {
                textBox3.Text = "";
            }
            if (textBox3.Text != "")
            {
                pauseContinue.Enabled = true;
            }
            else pauseContinue.Enabled = false;



        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((!(Char.IsDigit(e.KeyChar))) && (textBox3.Text.Length != 0))
            {

                if (e.KeyChar != (char)Keys.Back) e.Handled = true;
            }


            
            if (textBox3.Text == "0" && e.KeyChar == '0')
            {
                textBox3.Text = "0";
            }
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1;

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            timer1.Interval = 2000;
            timer1.Enabled = false;
            endButton.Enabled = false;
            cancelButton.Enabled = false;
            isPaused = false;
            firstClick = true;
            checkBox1.Enabled = true;
            pauseContinue.Image = Properties.Resources.icons8_play_в_круге_96;
        }
    }

        
    
}
