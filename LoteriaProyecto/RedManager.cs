using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
namespace LoteriaProyecto
{
    public class RedManager
    {
        private TcpListener servidor;
        private TcpClient cliente;
        private NetworkStream flujoDatos;
        private Thread hiloEscucha;
        private bool esServidor;

        // Evento para avisarle al Form1 que llegó un mensaje desde la red
        public event Action<string> MensajeRecibido;

        // 1. INICIAR COMO SERVIDOR (El que canta las cartas)
        public void IniciarServidor(int puerto = 8888)
        {
            try
            {
                esServidor = true;
                servidor = new TcpListener(IPAddress.Any, puerto);
                servidor.Start();

                // Hilo para esperar a que el cliente se conecte sin congelar la app
                Thread hiloConexion = new Thread(() =>
                {
                    cliente = servidor.AcceptTcpClient();
                    flujoDatos = cliente.GetStream();

                    // Una vez conectado, empezamos a escuchar mensajes
                    hiloEscucha = new Thread(EscucharMensajes);
                    hiloEscucha.Start();

                    MessageBox.Show("¡Jugador conectado! Ya pueden iniciar.", "Red");
                });
                hiloConexion.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar servidor: " + ex.Message);
            }
        }

        // 2. INICIAR COMO CLIENTE (El que se conecta a la IP del Servidor)
        public void ConectarAlServidor(string ipServidor, int puerto = 8888)
        {
            try
            {
                esServidor = false;
                cliente = new TcpClient();
                cliente.Connect(ipServidor, puerto);
                flujoDatos = cliente.GetStream();

                // Empezamos a escuchar los mensajes que envíe el servidor
                hiloEscucha = new Thread(EscucharMensajes);
                hiloEscucha.Start();

                MessageBox.Show("Conectado exitosamente al servidor.", "Red");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar al servidor: " + ex.Message);
            }
        }

        // 3. ENVIAR MENSAJE A TRAVÉS DE LA RED
        public void EnviarMensaje(string mensaje)
        {
            try
            {
                if (flujoDatos != null && cliente.Connected)
                {
                    byte[] bytesEnviar = Encoding.UTF8.GetBytes(mensaje);
                    flujoDatos.Write(bytesEnviar, 0, bytesEnviar.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar: " + ex.Message);
            }
        }

        // 4. BUCLE EN SEGUNDO PLANO PARA RECIBIR MENSAJES
        private void EscucharMensajes()
        {
            byte[] buffer = new byte[1024];
            int bytesLeidos;

            try
            {
                while (flujoDatos != null && (bytesLeidos = flujoDatos.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string mensaje = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);

                    // Disparamos el evento para que el Formulario procese el mensaje
                    MensajeRecibido?.Invoke(mensaje);
                }
            }
            catch (Exception)
            {
                // Se desconectó la red o terminó el juego
            }
        }

        public void CerrarConexiones()
        {
            try { flujoDatos?.Close(); } catch { }
            try { cliente?.Close(); } catch { }
            try { servidor?.Stop(); } catch { }
            try { hiloEscucha?.Abort(); } catch { }
        }
    }
}