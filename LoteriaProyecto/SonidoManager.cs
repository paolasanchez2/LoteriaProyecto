using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaProyecto
{
    public class SonidoManager
    {
        private SpeechSynthesizer sintetizador;

        public SonidoManager()
        {
            sintetizador = new SpeechSynthesizer();
            // Configurar para que hable en español si el Windows está en español
            sintetizador.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, new System.Globalization.CultureInfo("es-MX"));
        }

        // 1. CANTAR LA CARTA (Text to Speech)
        public void CantarCarta(string nombreCarta)
        {
            try
            {
                // El sintetizador hablará de forma asíncrona para no congelar la pantalla del juego
                sintetizador.SpeakAsync(nombreCarta);
            }
            catch (Exception ex)
            {
                // Tema 2.9: Manejo de excepciones
                Console.WriteLine("Error con el TTS: " + ex.Message);
            }
        }

        // 2. SONIDOS GENERALES DEL JUEGO
        public void ReproducirEfecto(string accion)
        {
            try
            {
                string rutaArchivo = "";

                // Dependiendo de lo que pase en el juego, asignamos el archivo
                switch (accion)
                {
                    case "frijolito":
                        rutaArchivo = "sonidos/sonidoClick.wav";
                        break;
                    case "ganar":
                        rutaArchivo = "sonidos/SonidoVctoria.wav";
                        break;
                    case "error":
                        break;
                }

                if (System.IO.File.Exists(rutaArchivo))
                {
                    SoundPlayer player = new SoundPlayer(rutaArchivo);
                    player.Play();
                }
            }
            catch (Exception ex)
            {
                // Evita que el juego se cierre si no encuentra los archivos de sonido
                System.Windows.Forms.MessageBox.Show("Error de audio: " + ex.Message);
            }
        }
    }
}
