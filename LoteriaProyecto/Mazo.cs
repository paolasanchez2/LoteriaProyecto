using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaProyecto
{
    public class Mazo
    {
        // Tema 5: Uso de arreglos/listas para agrupar datos
        private List<Carta> cartas;
        private int indiceActual;

        public Mazo()
        {
            cartas = new List<Carta>();
            indiceActual = 0;
            CargarMazoDinamico();
        }

        // Método privado para llenar el mazo inicial
        private void CargarMazoDinamico()
        {
            string rutaCarpeta = "imagenes";

            try
            {
                if (Directory.Exists(rutaCarpeta))
                {
                    // Buscamos todos los archivos .jpg en la carpeta
                    string[] archivos = Directory.GetFiles(rutaCarpeta, "*.jpg");

                    foreach (string archivo in archivos)
                    {
                        // Ejemplo de 'archivo': "imagenes\\30 - El camarón.jpg"
                        string nombreArchivo = Path.GetFileNameWithoutExtension(archivo); // "30 - El camarón"

                        // Separamos el número del nombre usando el guion
                        string[] partes = nombreArchivo.Split('-');

                        if (partes.Length >= 2)
                        {
                            int id = int.Parse(partes[0].Trim());       // 30
                            string nombre = partes[1].Trim();          // "El camarón"

                            // Agregamos la carta usando el mismo archivo .jpg como ruta
                            // Nota: Como no tenemos 54 audios separados, pasamos un string vacío o lo ignoramos
                            cartas.Add(new Carta(id, nombre, archivo, ""));
                        }
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("La carpeta 'imagenes' no existe en la ruta del programa.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al cargar las imágenes: " + ex.Message);
            }
        }

        // Método para revolver las cartas (Algoritmo Fisher-Yates)
        public void Barajar()
        {
            Random rnd = new Random();
            int n = cartas.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Carta valor = cartas[k];
                cartas[k] = cartas[n];
                cartas[n] = valor;
            }
            indiceActual = 0;
        }

        // Retorna la siguiente carta a "cantar"
        public Carta SacarSiguiente()
        {
            if (indiceActual < cartas.Count)
            {
                Carta cartaMazo = cartas[indiceActual];
                indiceActual++;
                return cartaMazo;
            }
            return null;
        }

        // Clonar una lista de cartas para poder generar tableros aleatorios sin alterar el mazo original
        public List<Carta> ObtenerCartasClonadas()
        {
            return new List<Carta>(cartas);
        }
        public List<Carta> ObtenerListaCompleta()
        {
            return cartas; // O el nombre que le hayas dado a tu lista base de 54 cartas
        }
    }
}
