using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LoteriaProyecto
{
    public partial class FormPersonalizacion : Form
    {

        private PictureBox[,] casillasPersonalizadas;
        private Carta cartaSeleccionada;
        private JuegoManager juego;
        private Carta[,] matrizPersonalizada;

        public FormPersonalizacion()
        {
            InitializeComponent();

            juego = new JuegoManager();
            casillasPersonalizadas = new PictureBox[5,5];
            
            CargarCartasVisuales();
            CrearTablaPersonalizada();

            matrizPersonalizada = new Carta[5, 5];

        }

        private void FormPersonalizacion_Load(object sender, EventArgs e)
        {

        }

        private void CargarCartasVisuales()
        {
            flpCartas.Controls.Clear();

            foreach (Carta carta in juego.MazoPrincipal.ObtenerListaCompleta())
            {
                PictureBox pic = new PictureBox();

                pic.Width = 70;
                pic.Height = 100;
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pic.BorderStyle = BorderStyle.FixedSingle;
                pic.Cursor = Cursors.Hand;
                pic.Tag = carta;

                if (File.Exists(carta.RutaImagen))
                {
                    pic.Image = Image.FromFile(carta.RutaImagen);
                }

                pic.Click += PicCartaMiniatura_Click;

                flpCartas.Controls.Add(pic);
            }
        }

        private void PicCartaMiniatura_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            cartaSeleccionada = (Carta)pic.Tag;

            picCartaSeleccionada.Image = Image.FromFile(cartaSeleccionada.RutaImagen);
            lblCartaSeleccionada.Text = cartaSeleccionada.Nombre;
        }

        private void CrearTablaPersonalizada()
        {
            int tamañoCasilla = 70;
            int espacio = 5;

            panelTablaPersonalizada.Controls.Clear();

            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    PictureBox pic = new PictureBox();

                    pic.Width = tamañoCasilla;
                    pic.Height = tamañoCasilla;
                    pic.Left = c * (tamañoCasilla + espacio);
                    pic.Top = f * (tamañoCasilla + espacio);
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.BackColor = Color.White;
                    pic.Cursor = Cursors.Hand;
                    pic.Tag = new Point(f, c);

                    pic.Click += PicCasillaPersonalizada_Click;

                    panelTablaPersonalizada.Controls.Add(pic);
                    casillasPersonalizadas[f, c] = pic;
                }
            }
        }

        private void PicCasillaPersonalizada_Click(object sender, EventArgs e)
        {
                    
            if (cartaSeleccionada == null)
            {
                MessageBox.Show("Primero selecciona una carta.");
                return;
            }

            PictureBox pic = (PictureBox)sender;

            pic.Image = Image.FromFile(cartaSeleccionada.RutaImagen);

            Point posicion = (Point)pic.Tag;
            int fila = posicion.X;
            int col = posicion.Y;

            matrizPersonalizada[fila, col] = cartaSeleccionada;
        }

        private void btnGuardarPersonalizacion_Click(object sender, EventArgs e)
        {
            string carpeta = "TablasFavoritas";

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            string nombreTabla = Microsoft.VisualBasic.Interaction.InputBox(
                "Introduce el nombre para guardar tu tabla personalizada:",
                "Guardar Tabla Personalizada",
                "MiTablaPersonalizada"
            );

            if (string.IsNullOrWhiteSpace(nombreTabla)) return;

            List<string> lineas = new List<string>();

            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Carta carta = matrizPersonalizada[f, c];

                    if (carta != null)
                    {
                        lineas.Add($"T1,{f},{c},{carta.Id}");
                    }
                }
            }

            string rutaArchivo = Path.Combine(carpeta, nombreTabla + ".txt");

            File.WriteAllLines(rutaArchivo, lineas);

            MessageBox.Show("Tabla personalizada guardada correctamente.");

            this.Close();
        }
    }
        
    
}
