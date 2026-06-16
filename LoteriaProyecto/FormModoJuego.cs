using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoteriaProyecto
{
    public partial class FormModoJuego : Form
    {

        private bool[,] patron = new bool[5, 5];
        private PictureBox[,] casillas = new PictureBox[5, 5];





        public FormModoJuego()
        {
            InitializeComponent();
            CrearCuadricula();
        }

        private void FormModoJuego_Load(object sender, EventArgs e)
        {

        }

        private void CrearCuadricula()
        {
            int tamaño = 50;
            int espacio = 5;

            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    PictureBox pic = new PictureBox();

                    pic.Width = tamaño;
                    pic.Height = tamaño;
                    pic.Left = 20 + c * (tamaño + espacio);
                    pic.Top = 20 + f * (tamaño + espacio);

                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.BackColor = Color.White;

                    pic.Tag = new Point(f, c);

                    pic.Click += PicPatron_Click;

                    Controls.Add(pic);

                    casillas[f, c] = pic;
                }
            }
        }

        private void PicPatron_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            Point pos = (Point)pic.Tag;

            int f = pos.X;
            int c = pos.Y;

            patron[f, c] = !patron[f, c];

            pic.BackColor =
                patron[f, c]
                ? Color.LimeGreen
                : Color.White;
        }

        private void btnGuardarModo_Click(object sender, EventArgs e)
        {
            
            
            
            if (string.IsNullOrWhiteSpace(txtNombreModo.Text))
            {
                MessageBox.Show("Escribe un nombre.");
                return;
            }

            string carpeta = "ModosPersonalizados";

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string ruta =
                Path.Combine(
                    carpeta,
                    txtNombreModo.Text + ".txt");

            List<string> lineas = new List<string>();

            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    if (patron[f, c])
                    {
                        lineas.Add($"{f},{c}");
                    }
                }
            }

            File.WriteAllLines(ruta, lineas);  
            MessageBox.Show("Modo guardado.");

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
