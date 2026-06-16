using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaProyecto
{
    public class Tablero
    {
        // Tema 5: Arreglos multidimensionales (Matrices de 4x4)
        private Carta[,] matrizCartas;
        private bool[,] matrizMarcados;

        public Tablero()
        {
            matrizCartas = new Carta[5,5];
            matrizMarcados = new bool[5, 5];
        }

        // Llena el tablero de 4x4 con cartas aleatorias que no se repitan en la misma tabla
        public void GenerarTableroAleatorio(Mazo mazo, Random rnd)
        {
            List<Carta> copiaCartas = mazo.ObtenerCartasClonadas();
          //  Random rnd = new Random();

            // Creamos una lista de control para asegurar que los IDs en ESTA tabla sean únicos
            List<int> idsUtilizados = new List<int>();

            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    // Si por alguna razón nos quedamos sin cartas en la carpeta, rompemos el ciclo
                    if (copiaCartas.Count == 0) break;

                    int index;
                    Carta cartaSeleccionada;

                    // ESTE BUCLE ES EL BLINDAJE: 
                    // Saca una carta al azar, si su ID ya salió en esta tabla, busca otra hasta que sea única
                    do
                    {
                        index = rnd.Next(copiaCartas.Count);
                        cartaSeleccionada = copiaCartas[index];

                        // Si solo queda una carta en la copia, rompemos el bucle para evitar ciclos infinitos
                        if (copiaCartas.Count == 1) break;

                    } while (idsUtilizados.Contains(cartaSeleccionada.Id));

                    // Al encontrar una carta única:
                    matrizCartas[f, c] = cartaSeleccionada;
                    matrizMarcados[f, c] = false; // Inicializa limpia

                    // Registramos su ID para que no se vuelva a usar en las casillas siguientes
                    idsUtilizados.Add(cartaSeleccionada.Id);

                    // La removemos de la lista de opciones disponibles
                    copiaCartas.RemoveAt(index);
                }
            }
        }

        public Carta ObtenerCarta(int fila, int col)
        {
            return matrizCartas[fila, col];
        }

        // Intenta marcar una carta si el usuario le da clic
        // Retorna true si la carta coincide con el Id de la que se está buscando (opcional) o simplemente la marca
        public void MarcarPosicion(int fila, int col)
        {
            matrizMarcados[fila, col] = true;
        }

        // Lógica de escaneo de gane en 4x4
 
        public void AsignarCartaEnPosicion(int fila, int col, Carta nuevaCarta)
        {
            matrizCartas[fila, col] = nuevaCarta;
            matrizMarcados[fila, col] = false; 
        }

        public bool VerificarVictoriaValida(string modoJuego, List<int> cartasCantadas)
        {
            if (modoJuego.StartsWith("Tradicional"))
            {
                // Horizontales de 4
                for (int f = 0; f < 5; f++)
                    for (int c = 0; c <= 1; c++)
                        if (ValidarPatron(new Point[] {
                    new Point(f,c), new Point(f,c+1), new Point(f,c+2), new Point(f,c+3)
                }, cartasCantadas)) return true;

                // Verticales de 4
                for (int c = 0; c < 5; c++)
                    for (int f = 0; f <= 1; f++)
                        if (ValidarPatron(new Point[] {
                    new Point(f,c), new Point(f+1,c), new Point(f+2,c), new Point(f+3,c)
                }, cartasCantadas)) return true;

                // Diagonales derecha
                for (int f = 0; f <= 1; f++)
                    for (int c = 0; c <= 1; c++)
                        if (ValidarPatron(new Point[] {
                    new Point(f,c), new Point(f+1,c+1), new Point(f+2,c+2), new Point(f+3,c+3)
                }, cartasCantadas)) return true;

                // Diagonales izquierda
                for (int f = 0; f <= 1; f++)
                    for (int c = 3; c < 5; c++)
                        if (ValidarPatron(new Point[] {
                    new Point(f,c), new Point(f+1,c-1), new Point(f+2,c-2), new Point(f+3,c-3)
                }, cartasCantadas)) return true;

                return false;
            }

            if (modoJuego == "Tabla Llena")
            {
                List<Point> puntos = new List<Point>();

                for (int f = 0; f < 5; f++)
                    for (int c = 0; c < 5; c++)
                        puntos.Add(new Point(f, c));

                return ValidarPatron(puntos.ToArray(), cartasCantadas);
            }

            if (modoJuego == "Cuatro Esquinas")
            {
                return ValidarPatron(new Point[] {
            new Point(0,0), new Point(0,4),
            new Point(4,0), new Point(4,4)
        }, cartasCantadas);
            }

            if (modoJuego == "En L")
            {
                return ValidarPatron(new Point[] {
            new Point(0,0), new Point(1,0), new Point(2,0),
            new Point(3,0), new Point(4,0),
            new Point(4,1), new Point(4,2), new Point(4,3), new Point(4,4)
        }, cartasCantadas);
            }

            return VerificarModoPersonalizadoValido(modoJuego, cartasCantadas);
        }

        private bool ValidarPatron(Point[] puntos, List<int> cartasCantadas)
        {
            foreach (Point p in puntos)
            {
                if (!matrizMarcados[p.X, p.Y])
                    return false;

                Carta carta = ObtenerCarta(p.X, p.Y);

                if (carta == null || !cartasCantadas.Contains(carta.Id))
                    return false;
            }

            return true;
        }

        private bool VerificarModoPersonalizadoValido(string nombreModo, List<int> cartasCantadas)
        {
            string ruta = System.IO.Path.Combine(
                "ModosPersonalizados",
                nombreModo + ".txt");

            if (!System.IO.File.Exists(ruta))
                return false;

            List<Point> puntos = new List<Point>();

            string[] lineas = System.IO.File.ReadAllLines(ruta);

            foreach (string linea in lineas)
            {
                string[] datos = linea.Split(',');

                int f = int.Parse(datos[0]);
                int c = int.Parse(datos[1]);

                puntos.Add(new Point(f, c));
            }

            return ValidarPatron(puntos.ToArray(), cartasCantadas);
        }

    }
}
