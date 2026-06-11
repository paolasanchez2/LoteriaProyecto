using System;
using System.Collections.Generic;
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
            matrizCartas = new Carta[4, 4];
            matrizMarcados = new bool[4, 4];
        }

        // Llena el tablero de 4x4 con cartas aleatorias que no se repitan en la misma tabla
        public void GenerarTableroAleatorio(Mazo mazo, Random rnd)
        {
            List<Carta> copiaCartas = mazo.ObtenerCartasClonadas();
          //  Random rnd = new Random();

            // Creamos una lista de control para asegurar que los IDs en ESTA tabla sean únicos
            List<int> idsUtilizados = new List<int>();

            for (int f = 0; f < 4; f++)
            {
                for (int c = 0; c < 4; c++)
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
        public bool VerificarLineasTradicionales()
        {
            // 1. Validar Filas
            for (int f = 0; f < 4; f++)
            {
                if (matrizMarcados[f, 0] && matrizMarcados[f, 1] && matrizMarcados[f, 2] && matrizMarcados[f, 3])
                    return true;
            }

            // 2. Validar Columnas
            for (int c = 0; c < 4; c++)
            {
                if (matrizMarcados[0, c] && matrizMarcados[1, c] && matrizMarcados[2, c] && matrizMarcados[3, c])
                    return true;
            }

            // 3. Diagonal de izquierda a derecha
            if (matrizMarcados[0, 0] && matrizMarcados[1, 1] && matrizMarcados[2, 2] && matrizMarcados[3, 3])
                return true;

            // 4. Diagonal de derecha a izquierda
            if (matrizMarcados[0, 3] && matrizMarcados[1, 2] && matrizMarcados[2, 1] && matrizMarcados[3, 0])
                return true;

            return false;
        }
        public bool VerificarSiGano(string modoJuego)
        {
            switch (modoJuego)
            {
                case "Tabla Llena":
                    // Recorre toda la matriz; si encuentra un false, no ha ganado
                    for (int f = 0; f < 4; f++)
                        for (int c = 0; c < 4; c++)
                            if (!matrizMarcados[f, c]) return false;
                    return true;

                case "Cuatro Esquinas":
                    return matrizMarcados[0, 0] && matrizMarcados[0, 3] &&
                           matrizMarcados[3, 0] && matrizMarcados[3, 3];

                case "En L":
                    // Ejemplo básico de L (columna 0 completa + fila 3 completa)
                    return (matrizMarcados[0, 0] && matrizMarcados[1, 0] && matrizMarcados[2, 0] && matrizMarcados[3, 0] &&
                            matrizMarcados[3, 1] && matrizMarcados[3, 2] && matrizMarcados[3, 3]);

                default: // Tradicional (El algoritmo que ya tienes de filas, columnas y diagonales)
                         // Aquí pegas tu lógica actual de escaneo por líneas de 4
                    return VerificarLineasTradicionales();
            }
        }
        // Añade esto para poder cargar tus tablas favoritas después
        public void AsignarCartaEnPosicion(int fila, int col, Carta nuevaCarta)
        {
            matrizCartas[fila, col] = nuevaCarta;
            matrizMarcados[fila, col] = false; // Nos aseguramos de que empiece sin frijolito
        }
    }
}
