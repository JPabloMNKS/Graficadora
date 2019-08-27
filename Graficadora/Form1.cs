using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficadora
{
    public partial class Form1 : Form
    {
        // Drag Window Panel
        private bool draggable;
        private int mouseX;
        private int mouseY;

        // Variables de la aplicacion
        Pen lapiz1 = new Pen(Color.DarkRed);
        Pen lapiz2 = new Pen(Color.Green);

        double[] valores = new double[20000];
        double puntoX1 = 0;
        double puntoX2 = 0;
        double puntoY1 = 0;
        double puntoY2 = 0;
        int con = 0;

        String funciones;



        public Form1()
        {
            InitializeComponent();
        }

        // Drag Window
        private void PanelTop_MouseDown(object sender, MouseEventArgs e)
        {
            draggable = true;
            mouseX = Cursor.Position.X - this.Left;
            mouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggable)
            {
                this.Left = Cursor.Position.X - mouseX;
                this.Top = Cursor.Position.Y - mouseY;
            }
        }
        private void PanelTop_MouseUp(object sender, MouseEventArgs e)
        {
            draggable = false;
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Application

        private void GraficarFunciones()
        {
            Graphics dibujo = this.pictureBox1.CreateGraphics();
            int xcentro = pictureBox1.Width / 2;
            int ycentro = pictureBox1.Height / 2;

            dibujo.TranslateTransform(xcentro, ycentro);
            dibujo.ScaleTransform(1, -1);
            if(textBox_Coordenada.Text == "")
            {
                MessageBox.Show("Ingrese un valor", "Valor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                for(int i = -xcentro; i< xcentro; i += 8)
                {
                    dibujo.DrawLine(lapiz1, 5, i, -5, i);
                    dibujo.DrawLine(lapiz1, i, 5, i, -5);
                }
                funciones = comboBox1.Text;
                if (funciones == "Funciones")
                {
                    MessageBox.Show("Seleccione una funcion", "Funciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    con = 0;
                    for(double x = xcentro * -1; x < xcentro * 2; x += 0.1)
                    {
                        switch (funciones)
                        {
                            case "Cos(x)":
                                valores[con] = Math.Cos(x);
                                break;
                            case "Sin(x)":
                                valores[con] = Math.Sin(x);
                                break;
                            case "Tan(x)":
                                valores[con] = Math.Tan(x);
                                break;
                            case "x^2":
                                valores[con] = Math.Pow(x, 2);
                                break;
                        }
                        con++;
                    }
                    con = 1;
                    for(double xx = xcentro * -1 + 0.1; xx < xcentro * 2; xx += 0.1)
                    {
                        // sacamos coordenadas 1
                        puntoX1 = (xx - 0.1) * (pictureBox1.Width / (Convert.ToInt32(textBox_Coordenada.Text) * 2));
                        puntoY1 = valores[con - 1] * ycentro;

                        // sacamos coordenadas 2
                        puntoX2 = xx * (pictureBox1.Width / (Convert.ToInt32(textBox_Coordenada.Text) * 2));
                        puntoY2 = valores[con] * ycentro;
                        dibujo.DrawLine(lapiz2, Convert.ToSingle(puntoX1), Convert.ToSingle(puntoY1), Convert.ToSingle(puntoX2), Convert.ToSingle(puntoY2));
                        con++;
                    }
                }

            }
        }


        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int xcentro = pictureBox1.Width / 2;
            int ycentro = pictureBox1.Height / 2;
            e.Graphics.TranslateTransform(xcentro, ycentro);
            e.Graphics.ScaleTransform(1, -1);

            Point p1 = new Point(xcentro*-1, xcentro*2);

            e.Graphics.DrawLine(lapiz1, xcentro * -1, 0, xcentro * 2, 0); // Eje X
            e.Graphics.DrawLine(lapiz1, 0, ycentro, 0, ycentro * -1);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            funciones = comboBox1.Text;
            pictureBox1.Image = null;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GraficarFunciones();
        }

        private void BtnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                pictureBox1.Image = null;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                pictureBox1.Image = null;
            }
        }
    }
}
