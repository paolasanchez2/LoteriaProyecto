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

        public List<Tablero> TablerosJugador { get; private set; }

        public List<int> CartasCantadasIds { get; private set; }

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

            TablerosJugador = new List<Tablero>();
            CartasCantadasIds = new List<int>();
        }

        public Carta CantarSiguienteCarta()
        {
            if (!EnCurso) return null;

            CartaActual = MazoPrincipal.SacarSiguiente();

            if (CartaActual != null)
            {
                CartasCantadasIds.Add(CartaActual.Id);
            }

            if (CartaActual == null)
            {
                EnCurso = false;
            }
            else
            {
                ControlSonido.CantarCarta(CartaActual.Nombre);
            }

            return CartaActual;
        }

        public void TerminarJuego()
        {
            EnCurso = false;
           
        }
        // Así debe quedar dentro de JuegoManager.cs
        public void SincronizarCartaPorId(int id)
        {
            if (!CartasCantadasIds.Contains(id))
                CartasCantadasIds.Add(id);

            foreach (Tablero tablero in TablerosJugador)
            {
                for (int f = 0; f < 5; f++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        Carta car = tablero.ObtenerCarta(f, c);

                        if (car != null && car.Id == id)
                        {
                            CartaActual = car;
                            ControlSonido.CantarCarta(CartaActual.Nombre);
                            return;
                        }
                    }
                }
            }

            Carta cartaGlobal = BuscarCartaPorIdGlobal(id);

            if (cartaGlobal != null)
            {
                CartaActual = cartaGlobal;
                ControlSonido.CantarCarta(CartaActual.Nombre);
            }
            else
            {
                CartaActual = new Carta(id, "Carta Externa", "", "");
            }
        }
        public Carta BuscarCartaPorIdGlobal(int id)
        {
            // Busca la carta basándose en el ID recorriendo el mazo o la lista base
            return MazoPrincipal.ObtenerListaCompleta().FirstOrDefault(c => c.Id == id);
        }
        
        public void IniciarNuevoJuego(int cantidadTablas)
        {
            MazoPrincipal.Barajar();
            CartasCantadasIds.Clear();

            TablerosJugador = new List<Tablero>();

            for (int i = 0; i < cantidadTablas; i++)
            {
                Tablero nuevoTablero = new Tablero();
                nuevoTablero.GenerarTableroAleatorio(MazoPrincipal, rndGlobal);
                TablerosJugador.Add(nuevoTablero);
            }

            // Compatibilidad con código viejo
            if (TablerosJugador.Count > 0)
                TableroJugador = TablerosJugador[0];

            if (TablerosJugador.Count > 1)
                TableroJugador2 = TablerosJugador[1];

            EnCurso = true;
            CartaActual = null;

            
        }
    }
}
