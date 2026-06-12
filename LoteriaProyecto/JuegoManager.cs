using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaProyecto
{
    public class JuegoManager
    {
        public Mazo MazoPrincipal { get; private set; }
        public Tablero TableroJugador { get; private set; }
        public Carta CartaActual { get; private set; }
        public bool EnCurso { get; private set; }
        public Tablero TableroJugador2 { get; private set; }

        private int indicePersonalizar = 0;
        // Creamos una instancia del manejador de sonidos adentro del juego
        public SonidoManager ControlSonido { get; private set; }
        private Random rndGlobal;
        public JuegoManager()
        {
            MazoPrincipal = new Mazo();
            TableroJugador = new Tablero();
            TableroJugador2 = new Tablero(); // <--- AGREGA ESTA LÍNEA AQUÍ
            ControlSonido = new SonidoManager(); // Se inicializa aquí
            EnCurso = false;
            rndGlobal = new Random(); // Inicializamos el Random global
        }

        public void IniciarNuevoJuego()
        {
            MazoPrincipal.Barajar();

            TableroJugador = new Tablero();
            // 3. LE PASAMOS EL RANDOM GLOBAL AL TABLERO 1
            TableroJugador.GenerarTableroAleatorio(MazoPrincipal, rndGlobal);

            TableroJugador2 = new Tablero();
            // 4. LE PASAMOS EL MISMO RANDOM GLOBAL AL TABLERO 2
            TableroJugador2.GenerarTableroAleatorio(MazoPrincipal, rndGlobal);

            EnCurso = true;
            CartaActual = null;
        }

        public Carta CantarSiguienteCarta()
        {
            if (!EnCurso) return null;

            CartaActual = MazoPrincipal.SacarSiguiente();

            if (CartaActual == null)
            {
                EnCurso = false;
            }
            else
            {
                // ¡Aquí es donde ocurre la magia! 
                // Cada vez que sacas una carta, le ordenas a tu SonidoManager que la cante
                ControlSonido.CantarCarta(CartaActual.Nombre);
            }

            return CartaActual;
        }

        public void TerminarJuego()
        {
            EnCurso = false;
            indicePersonalizar = 0; // <-- Opcional: Resetea el carrusel de edición para la siguiente ronda
        }
        // Así debe quedar dentro de JuegoManager.cs
        public void SincronizarCartaPorId(int id)
        {
            // 1. Buscar en el primer tablero
            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Carta car = TableroJugador.ObtenerCarta(f, c);
                    if (car != null && car.Id == id)
                    {
                        CartaActual = car;
                        ControlSonido.CantarCarta(CartaActual.Nombre);
                        return;
                    }
                }
            }

            // 2. Si no la halló, buscar en el segundo tablero
            if (TableroJugador2 != null)
            {
                for (int f = 0; f < 5; f++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        Carta car = TableroJugador2.ObtenerCarta(f, c);
                        if (car != null && car.Id == id)
                        {
                            CartaActual = car;
                            ControlSonido.CantarCarta(CartaActual.Nombre);
                            return;
                        }
                    }
                }
            }

            // 3. Si no la tiene en ningún tablero, objeto genérico
            CartaActual = new Carta(id, "Carta Externa", "", "");
        }
        public Carta BuscarCartaPorIdGlobal(int id)
        {
            // Busca la carta basándose en el ID recorriendo el mazo o la lista base
            return MazoPrincipal.ObtenerListaCompleta().FirstOrDefault(c => c.Id == id);
        }
        public Carta ObtenerSiguienteCartaParaPersonalizar()
        {
            List<Carta> todas = MazoPrincipal.ObtenerListaCompleta();
            if (todas.Count == 0) return null;

            Carta seleccionada = todas[indicePersonalizar];

            // Rotar el índice para la próxima casilla que se presione
            indicePersonalizar = (indicePersonalizar + 1) % todas.Count;

            return seleccionada;
        }
    }
}
