using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лаба_11
{
    public partial class Form1 : Form
    {
        //Создайте программу, показывающую движение окружности по спирали с плавно изменяющейся скоростью

        private Point pole;//центр спирали
        private Point center;//центр окружности
        private Timer timer = new Timer();//таймер
        private Pen pen = new Pen(Color.Red, 2);//Перо для рисования окружности
        private float diameter = 30f;//Диаметр окружности
        private float a = 20;//Шаг спирали
        private float phi;//угол поворота спирали (в радианах)

        public Form1()
        {
            InitializeComponent();
            pole = new Point(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2);
            timer.Tick += new EventHandler(timer_tick);
            this.Paint += new PaintEventHandler(Form1_Paint);
            timer.Interval = 50;
            timer.Enabled = true;
        }

        private void timer_tick(object sender, EventArgs e)
        {
            center = new Point();
            center.X = (int)((a / (Math.PI * 2)) * phi * Math.Cos(phi));
            center.Y = (int)((a / (Math.PI * 2)) * phi * Math.Sin(phi));
            this.Refresh();
            phi++;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (timer.Enabled)
                using (Graphics g = e.Graphics)
                {
                    //Перенос начала координат в центр клиентской части формы
                    g.TranslateTransform(pole.X, pole.Y);
                    //рисование окружности в заданных координатах
                    g.DrawEllipse(pen, center.X, center.Y, diameter, diameter);
                    /*граничные условия. Если центр окружности выходит за клиентскую 
                    область формы, то начинаем сначала*/
                    center.Offset(pole);
                    if (!this.ClientRectangle.Contains(center)) phi = 0;
                }
        }
    }
}
