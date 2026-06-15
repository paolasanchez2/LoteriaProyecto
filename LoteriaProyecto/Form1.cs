using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace LoteriaProyecto
{
    public partial class Form1 : Form
    {

        private List<PictureBox[,]> listaCasillasVisuales;
        private JuegoManager juego;

        
        private PictureBox[,] casillasVisuales;
        private PictureBox[,] casillasVisuales2;

        private RedManager red;
        private bool soyServidor = false;

        public Form1()
        {
            InitializeComponent();
            juego = new JuegoManager();
            casillasVisuales = new PictureBox[5, 5];
            casillasVisuales2 = new PictureBox[5, 5];
            listaCasillasVisuales = new List<PictureBox[,]>();
            CrearTableroEnPantalla();
            CrearTableroEnPantalla2S();
            red = new RedManager();
            red.MensajeRecibido += ProcesarMensajeRed;

            timerCartas.Tick += timerCartas_Tick; //Nuevo
            
        }
        private bool ValidarSeleccionDeTablas()
        {
            if (numCantidadTablas.Value < 1)
            {
                MessageBox.Show("Debes seleccionar al menos 1 tabla.",
                                "Selección Requerida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lstHistorial.Visible = false;
            ActualizarListaFavoritos();
            this.WindowState = FormWindowState.Maximized;

        }

        // Genera los 16 PictureBoxes de forma dinámica en la interfaz
        private void CrearTableroEnPantalla()
        {
            int tamañoCasilla = 65; // Tamaño en píxeles de cada cartita
            int espacio = 5;       // Separación entre cartitas

            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Width = tamañoCasilla;
                    pic.Height = tamañoCasilla;
                    // Calculamos la posición X e Y matemáticamente en la cuadrícula
                    pic.Left = c * (tamañoCasilla + espacio);
                    pic.Top = f * (tamañoCasilla + espacio);
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.BackColor = Color.White;

                    // Guardamos la fila y columna en la propiedad Tag para saber cuál es al dar clic
                    pic.Tag = new Point(f, c);

                    // Asignamos el evento Clic (Tema 1.2.3 / Eventos)
                    pic.Click += PixCasilla_Click;
                    pic.Paint += PicCasilla_Paint;

                    // Lo agregamos al panel y a nuestro arreglo interno
                    panelTablero.Controls.Add(pic);
                    casillasVisuales[f, c] = pic;
                }
            }
        }
        private void CrearTableroEnPantalla2S()
        {
            int tamañoCasilla = 65; // Tamaño en píxeles de cada cartita
            int espacio = 5;       // Separación entre cartitas

            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Width = tamañoCasilla;
                    pic.Height = tamañoCasilla;
                    // Calculamos la posición X e Y matemáticamente en la cuadrícula
                    pic.Left = c * (tamañoCasilla + espacio);
                    pic.Top = f * (tamañoCasilla + espacio);
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.BackColor = Color.White;

                    // Guardamos la fila y columna en la propiedad Tag para saber cuál es al dar clic
                    pic.Tag = new Point(f, c);

                    // Asignamos el evento Clic (Tema 1.2.3 / Eventos)
                    pic.Click += PixCasillaTablero2_Click;
                    pic.Paint += PicCasilla_Paint;

                    // Lo agregamos al panel y a nuestro arreglo interno
                    panelTablero2.Controls.Add(pic);
                    casillasVisuales2[f, c] = pic; 
                }
            }
        }
        // Evento que se dispara cuando el usuario presiona una carta de su tabla
        private void PixCasilla_Click(object sender, EventArgs e)
        {
            if (!ValidarSeleccionDeTablas()) return;
            PictureBox picPresionado = (PictureBox)sender;
            Point posicion = (Point)picPresionado.Tag;
            int fila = posicion.X;
            int col = posicion.Y;
            // --- MODO CONFIGURACIÓN / PERSONALIZACIÓN ---
            if (!juego.EnCurso)
            {
                // 1. Pedimos la siguiente carta del catálogo global
                Carta cartaAsignada = juego.ObtenerSiguienteCartaParaPersonalizar();

                if (cartaAsignada != null)
                {
                    // 2. La registramos en la lógica del tablero 1
                    juego.TableroJugador.AsignarCartaEnPosicion(fila, col, cartaAsignada);

                    // 3. ¡LA MAGIA FALTANTE!: Forzar al PictureBox a cargar y mostrar la nueva foto
                    try
                    {
                        if (System.IO.File.Exists(cartaAsignada.RutaImagen))
                        {
                            // Liberamos la imagen anterior de la memoria para que no se bloquee el archivo
                            if (picPresionado.Image != null) picPresionado.Image.Dispose();

                            // Cargamos la nueva carta seleccionada
                            picPresionado.Image = Image.FromFile(cartaAsignada.RutaImagen);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error visual al personalizar: " + ex.Message);
                    }
                }
                return; // Detiene la ejecución aquí para que no valide gane ni ponga frijolitos
            }
            Carta cartaClickeada = juego.TableroJugador.ObtenerCarta(fila, col);


            // 1. VALIDACIÓN: ¿Es la carta correcta?
            if (juego.CartaActual != null && cartaClickeada.Id == juego.CartaActual.Id)
            {
                // Registrar en la lógica
                juego.TableroJugador.MarcarPosicion(fila, col);
                juego.ControlSonido.ReproducirEfecto("frijolito");

                picPresionado.AccessibleName = "Marcado"; // Usamos esto como una bandera oculta
                picPresionado.Invalidate(); // Fuerza al evento Paint a ejecutarse
                                            // --- EFECTO VISUAL: Dibujar encima de la carta ---
                                            // Forzamos al PictureBox a redibujarse para que ejecute nuestro código de dibujo


                // Verificar gane
                if (juego.TableroJugador.VerificarSiGano(cmbModoJuego.Text))
                {
                    juego.ControlSonido.ReproducirEfecto("ganar");
                    juego.TerminarJuego();
                    timerCartas.Stop();
                    btnSiguiente.Enabled = false;
                    MessageBox.Show("¡¡LOTERÍA!! ¡Felicidades, ganaste!", "Victoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // 2. VALIDACIÓN INCORRECTA: Lanzar mensaje de advertencia
                juego.ControlSonido.ReproducirEfecto("error");

                string mensajeError = (juego.CartaActual == null)
                    ? "¡Aún no ha salido ninguna carta del mazo! Espera a que empiece el juego."
                    : $"La carta '{cartaClickeada.Nombre}' no ha salido. La carta actual es '{juego.CartaActual.Nombre}'.";

                MessageBox.Show(mensajeError, "Carta Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PixCasillaTablero2_Click(object sender, EventArgs e)
        {
            if (numCantidadTablas.Value < 2) return;
            PictureBox picPresionado = (PictureBox)sender;
            Point posicion = (Point)picPresionado.Tag;
            int fila = posicion.X;
            int col = posicion.Y;

            // --- MODO CONFIGURACIÓN / PERSONALIZACIÓN TABLA 2 ---
            if (!juego.EnCurso)
            {
                Carta cartaAsignada = juego.ObtenerSiguienteCartaParaPersonalizar();

                if (cartaAsignada != null)
                {
                    // La registramos en la lógica de la TABLA 2
                    juego.TableroJugador2.AsignarCartaEnPosicion(fila, col, cartaAsignada);

                    // Actualizamos el PictureBox de la tabla 2 al instante
                    try
                    {
                        if (System.IO.File.Exists(cartaAsignada.RutaImagen))
                        {
                            if (picPresionado.Image != null) picPresionado.Image.Dispose();
                            picPresionado.Image = Image.FromFile(cartaAsignada.RutaImagen);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error visual al personalizar tabla 2: " + ex.Message);
                    }
                }
                return;
            }

            // NOTA: Usamos TableroJugador2
            Carta cartaClickeada = juego.TableroJugador2.ObtenerCarta(fila, col);

            if (juego.CartaActual != null && cartaClickeada.Id == juego.CartaActual.Id)
            {
                juego.TableroJugador2.MarcarPosicion(fila, col);
                juego.ControlSonido.ReproducirEfecto("frijolito");

                picPresionado.AccessibleName = "Marcado";
                picPresionado.Invalidate();

                // Verificamos gane en el Tablero 2
                if (juego.TableroJugador2.VerificarSiGano(cmbModoJuego.Text))
                {
                    juego.ControlSonido.ReproducirEfecto("ganar");
                    juego.TerminarJuego();
                    timerCartas.Stop();
                    btnSiguiente.Enabled = false;

                    if (red != null) red.EnviarMensaje("LOTERIA");

                    MessageBox.Show("¡¡LOTERÍA!! ¡Tu segunda tabla ha ganado!", "Victoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                juego.ControlSonido.ReproducirEfecto("error");
                string mensajeError = (juego.CartaActual == null)
                    ? "¡Aún no ha salido ninguna carta del mazo!"
                    : $"La carta '{cartaClickeada.Nombre}' no ha salido. La actual es '{juego.CartaActual.Nombre}'.";

                MessageBox.Show(mensajeError, "Carta Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnIniciar_Click(object sender, EventArgs e)
        {
            int cantidadTablas = (int)numCantidadTablas.Value;

            juego.IniciarNuevoJuego(cantidadTablas);

            CrearTablerosDinamicos(cantidadTablas);

            bool jugarConDosTablas = cantidadTablas >= 2;

            panelTablero2.Visible = jugarConDosTablas;

            // Pintar tabla 1
            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Carta carta = juego.TableroJugador.ObtenerCarta(f, c);
                    casillasVisuales[f, c].BackColor = Color.White;

                    try
                    {
                        if (System.IO.File.Exists(carta.RutaImagen))
                            casillasVisuales[f, c].Image = Image.FromFile(carta.RutaImagen);

                        casillasVisuales[f, c].AccessibleName = "";
                    }
                    catch
                    {
                        casillasVisuales[f, c].Image = null;
                    }
                }
            }

            // Pintar tabla 2 solo si cantidadTablas >= 2
            if (jugarConDosTablas)
            {
                for (int f = 0; f < 5; f++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        Carta carta = juego.TableroJugador2.ObtenerCarta(f, c);
                        casillasVisuales2[f, c].BackColor = Color.White;

                        try
                        {
                            if (System.IO.File.Exists(carta.RutaImagen))
                                casillasVisuales2[f, c].Image = Image.FromFile(carta.RutaImagen);

                            casillasVisuales2[f, c].AccessibleName = "";
                        }
                        catch
                        {
                            casillasVisuales2[f, c].Image = null;
                        }
                    }
                }
            }

            btnSiguiente.Enabled = true;
            picCartaActual.Image = null;
            flpHistorialImagenes.Controls.Clear();

            if (soyServidor)
            {
                string modoSeleccionado = cmbModoJuego.Text;
                int velocidad = (int)numVelocidad.Value;

                red.EnviarMensaje($"INICIAR_PARTIDA|{modoSeleccionado}|{cantidadTablas}|{velocidad}");
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            AvanzarJuego();
        }
        private void AvanzarJuego()
        {
            Carta siguiente = juego.CantarSiguienteCarta();

            if (siguiente != null)
            {
                try
                {
                    if (System.IO.File.Exists(siguiente.RutaImagen))
                        picCartaActual.Image = Image.FromFile(siguiente.RutaImagen);
                    AgregarAlHistorialVisual(siguiente); // <--- REEMPLAZA O AGREGA AQUÍ
                    lstHistorial.Items.Insert(0, $"{siguiente.Id} - {siguiente.Nombre}");
                    // BLINDAJE EXTRA: Solo envía el mensaje si eres servidor Y de verdad hay una conexión activa
                    if (soyServidor && red != null)
                    {
                        red.EnviarMensaje("CARTA:" + siguiente.Id);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al procesar el avance visual: " + ex.Message);
                }
            }
            else
            {
                timerCartas.Stop();
                btnSiguiente.Enabled = false;

                // Solo envía terminación si la red está arriba
                if (red != null)
                {
                    red.EnviarMensaje("LOTERIA");
                }

                MessageBox.Show("El mazo se ha vaciado. ¡Fin del juego!", "Juego Terminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void timerCartas_Tick(object sender, EventArgs e)
        {
            AvanzarJuego();
        }

        private void picCartaActual_Click(object sender, EventArgs e)
        {

        }

        private void PicCasilla_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;

            // Si la bandera oculta dice "Marcado", dibujamos la X roja encima
            if (pic.AccessibleName == "Marcado")
            {
                // Configuramos el pincel para dibujar la X (Color Rojo, grosor de 5 píxeles)
                using (Pen plumaROJA = new Pen(Color.Red, 5))
                {
                    e.Graphics.DrawLine(plumaROJA, 10, 10, pic.Width - 10, pic.Height - 10);
                    e.Graphics.DrawLine(plumaROJA, pic.Width - 10, 10, 10, pic.Height - 10);
                }
            }
        }

        private void btnModoServidor_Click(object sender, EventArgs e)
        {
            soyServidor = true;
            red.IniciarServidor();

            btnModoServidor.Enabled = false;
            btnModoCliente.Enabled = false;

            btnIniciar.Enabled = true; // El servidor controla el mazo
            btnSiguiente.Enabled = true;

            numCantidadTablas.Enabled = true;
            cmbModoJuego.Enabled = true;
            numVelocidad.Enabled = true;

            this.Text = "Lotería - MODO SERVIDOR";
        }

        private void btnModoCliente_Click(object sender, EventArgs e)
        {
            soyServidor = false;
            red.ConectarAlServidor(txtIP.Text);

            gbConfiguracion.Enabled = false;

            btnIniciar.Enabled = false;
            btnSiguiente.Enabled = false;
            btnAutomatico.Enabled = false;
            btnDetenerAutomatico.Enabled = false;

            btnModoServidor.Enabled = false;
            btnModoCliente.Enabled = false;

            this.Text = "Lotería - MODO CLIENTE";
        }

        private void ProcesarMensajeRed(string mensaje)
        {
            
            this.Invoke((MethodInvoker)delegate
            {
               
                if (mensaje.StartsWith("CARTA:"))
                {
                    int idCarta = int.Parse(mensaje.Split(':')[1]);

                    
                    juego.SincronizarCartaPorId(idCarta);
                    AgregarAlHistorialVisual(juego.CartaActual); 
                    lstHistorial.Items.Insert(0, $"{idCarta} - {juego.CartaActual.Nombre}");
           
                    try
                    {
                        string rutaCarpeta = "imagenes";
                        if (System.IO.Directory.Exists(rutaCarpeta))
                        {
                            string[] archivos = System.IO.Directory.GetFiles(rutaCarpeta, idCarta + " - *.jpg");
                            if (archivos.Length > 0)
                            {
                                picCartaActual.Image = Image.FromFile(archivos[0]);
                            }
                        }
                    }
                    catch (Exception) { /* Ignorar si hay problemas de carga visual */ }
                }
                
                if (mensaje == "LOTERIA")
                {
                    juego.TerminarJuego();
                    timerCartas.Stop();
                    btnSiguiente.Enabled = false;
                    MessageBox.Show("¡El otro jugador ha cantado LOTERÍA! Suerte para la próxima.", "Fin del Juego", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                
                
                if (mensaje.StartsWith("INICIAR_PARTIDA|"))
                {
                    string[] datos = mensaje.Split('|');

                    string modoDelServidor = datos[1];
                    string tablasDelServidor = datos[2];
                    int velocidadDelServidor = int.Parse(datos[3]);

                    int cantidadTablasServidor = int.Parse(tablasDelServidor);

                    cmbModoJuego.Text = modoDelServidor;

                    numCantidadTablas.Value = cantidadTablasServidor;

                    panelTablero2.Visible = (cantidadTablasServidor >= 2);

                    numVelocidad.Value = velocidadDelServidor;
                    timerCartas.Interval = velocidadDelServidor * 1000;

                    IniciarTableroCliente();
                }
            });
        }
        private void IniciarTableroCliente()
        {
            lstHistorial.Items.Clear();

            int cantidadTablas = (int)numCantidadTablas.Value;
            juego.IniciarNuevoJuego(cantidadTablas);
            CrearTablerosDinamicos(cantidadTablas);


            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Carta carta = juego.TableroJugador.ObtenerCarta(f, c);
                    casillasVisuales[f, c].BackColor = Color.White;
                    casillasVisuales[f, c].AccessibleName = ""; 

                    try
                    {
                        if (System.IO.File.Exists(carta.RutaImagen))
                            casillasVisuales[f, c].Image = Image.FromFile(carta.RutaImagen);
                    }
                    catch (Exception)
                    {
                        casillasVisuales[f, c].Image = null;
                    }
                }
            }
            // --- DENTRO DE IniciarTableroCliente() (Abajo de tu primer ciclo for) ---
            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Carta carta = juego.TableroJugador2.ObtenerCarta(f, c);
                    casillasVisuales2[f, c].BackColor = Color.White;
                    casillasVisuales2[f, c].AccessibleName = ""; // Limpia marcas anteriores

                    try
                    {
                        if (System.IO.File.Exists(carta.RutaImagen))
                            casillasVisuales2[f, c].Image = Image.FromFile(carta.RutaImagen);
                    }
                    catch (Exception)
                    {
                        casillasVisuales2[f, c].Image = null;
                    }
                }
            }
            picCartaActual.Image = null;
        }

        private void btnGuardarFavorito_Click(object sender, EventArgs e)
        {
            if (!ValidarSeleccionDeTablas()) return;
            string carpetaFavoritos = "TablasFavoritas";
            if (!System.IO.Directory.Exists(carpetaFavoritos))
            {
                System.IO.Directory.CreateDirectory(carpetaFavoritos);
            }

            // Pedimos el nombre para el "Paquete" de 2 tablas
            string nombreTabla = Microsoft.VisualBasic.Interaction.InputBox(
                "Introduce el nombre para guardar tus tablas actuales:",
                "Guardar Par de Tablas",
                "MisDosTablas"
            );

            if (string.IsNullOrWhiteSpace(nombreTabla)) return;

            try
            {
                List<string> lineas = new List<string>();

                // 1. Guardamos los datos de la TABLA 1 (Marcamos con un prefijo "T1")
                for (int f = 0; f < 5; f++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        Carta carta = juego.TableroJugador.ObtenerCarta(f, c);
                        if (carta != null)
                            lineas.Add($"T1,{f},{c},{carta.Id}");
                    }
                }

                // 2. Guardamos los datos de la TABLA 2 (Marcamos con un prefijo "T2")
                for (int f = 0; f < 5; f++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        Carta carta = juego.TableroJugador2.ObtenerCarta(f, c);
                        if (carta != null)
                            lineas.Add($"T2,{f},{c},{carta.Id}");
                    }
                }

                string rutaArchivo = System.IO.Path.Combine(carpetaFavoritos, nombreTabla + ".txt");
                System.IO.File.WriteAllLines(rutaArchivo, lineas);

                MessageBox.Show($"¡Tus dos tablas han sido guardadas en '{nombreTabla}'!", "Favoritos", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ActualizarListaFavoritos();
            }
            catch (Exception ex) { MessageBox.Show("Error al guardar: " + ex.Message); }
        }

        private void ActualizarListaFavoritos()
        {
            string carpetaFavoritos = "TablasFavoritas";
            cmbTablasFavoritas.Items.Clear();

            if (System.IO.Directory.Exists(carpetaFavoritos))
            {
                // Buscamos todos los archivos .txt dentro de la carpeta de favoritos
                string[] archivos = System.IO.Directory.GetFiles(carpetaFavoritos, "*.txt");

                foreach (string archivo in archivos)
                {
                    // Agregamos solo el nombre del archivo (sin la ruta ni el .txt) al ComboBox
                    cmbTablasFavoritas.Items.Add(System.IO.Path.GetFileNameWithoutExtension(archivo));
                }
            }

            // Selecciona el primer elemento por defecto si hay alguno disponible
            if (cmbTablasFavoritas.Items.Count > 0)
            {
                cmbTablasFavoritas.SelectedIndex = 0;
            }
        }
        private void btnCargarFavorito_Click(object sender, EventArgs e)
        {
            
            if (!ValidarSeleccionDeTablas()) return;

            if (cmbTablasFavoritas.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una combinación de la lista.", "Aviso");
                return;
            }

            string nombreArchivoSeleccionado = cmbTablasFavoritas.SelectedItem.ToString();
            string rutaArchivo = System.IO.Path.Combine("TablasFavoritas", nombreArchivoSeleccionado + ".txt");

            if (!System.IO.File.Exists(rutaArchivo)) return;

            try
            {

                bool cargarSegundaTabla = numCantidadTablas.Value >= 2;

                string[] lineas = System.IO.File.ReadAllLines(rutaArchivo);
                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(',');

                    string tipoTablero = datos[0]; // "T1" o "T2"
                    int f = int.Parse(datos[1]);
                    int c = int.Parse(datos[2]);
                    int idCarta = int.Parse(datos[3]);

                    Carta cartaFavorita = juego.BuscarCartaPorIdGlobal(idCarta);

                    // 2. ¡EL FILTRO INTELIGENTE!:
                    if (tipoTablero == "T1")
                    {
                        juego.TableroJugador.AsignarCartaEnPosicion(f, c, cartaFavorita);
                    }
                    else if (tipoTablero == "T2" && cargarSegundaTabla) 
                    {
                        juego.TableroJugador2.AsignarCartaEnPosicion(f, c, cartaFavorita);
                    }
                }

                // 3. Sincronizamos la visibilidad del segundo panel visual en la pantalla
                panelTablero2.Visible = cargarSegundaTabla;

                // Forzamos el refresco de los PictureBoxes en pantalla
                ActualizarPantallaTableroVisual();

                string mensajeExito = cargarSegundaTabla
                    ? $"¡Se cargaron las 2 tablas de '{nombreArchivoSeleccionado}'!"
                    : $"¡Se cargó únicamente la Tabla 1 de '{nombreArchivoSeleccionado}'!";

                MessageBox.Show(mensajeExito, "Favoritos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar favoritos: " + ex.Message); }
        }
        private void ActualizarPantallaTableroVisual()
        {
            // 1. Refresco Tabla 1 (Se queda igual)
            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Carta carta = juego.TableroJugador.ObtenerCarta(f, c);
                    casillasVisuales[f, c].BackColor = Color.White;
                    casillasVisuales[f, c].AccessibleName = "";

                    if (carta != null && System.IO.File.Exists(carta.RutaImagen))
                        casillasVisuales[f, c].Image = Image.FromFile(carta.RutaImagen);
                    else
                        casillasVisuales[f, c].Image = null;
                }
            }

            // 2. Refresco Tabla 2 - ¡BLINDADO CONTRA NULOS!
            // Solo intenta refrescar la segunda tabla si el objeto existe y tiene cartas adentro
            if (juego.TableroJugador2 != null)
            {
                for (int f = 0; f < 5; f++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        Carta carta = juego.TableroJugador2.ObtenerCarta(f, c);
                        casillasVisuales2[f, c].BackColor = Color.White;
                        casillasVisuales2[f, c].AccessibleName = "";

                        if (carta != null && System.IO.File.Exists(carta.RutaImagen))
                            casillasVisuales2[f, c].Image = Image.FromFile(carta.RutaImagen);
                        else
                            casillasVisuales2[f, c].Image = null;
                    }
                }
            }
        }

        private void AgregarAlHistorialVisual(Carta carta)
        {
            if (carta == null) return;

            // Creamos un PictureBox pequeño en tiempo de ejecución para el historial
            PictureBox picHistorial = new PictureBox();
            picHistorial.Width = 60;   // Tamaño miniatura
            picHistorial.Height = 80;
            picHistorial.SizeMode = PictureBoxSizeMode.StretchImage;
            picHistorial.BorderStyle = BorderStyle.FixedSingle;

            try
            {
                if (System.IO.File.Exists(carta.RutaImagen))
                {
                    picHistorial.Image = Image.FromFile(carta.RutaImagen);
                }
            }
            catch { /* Ignorar errores de carga */ }

            // Lo agregamos al principio del panel para que la más reciente salga primero
            flpHistorialImagenes.Controls.Add(picHistorial);
            flpHistorialImagenes.Controls.SetChildIndex(picHistorial, 0);
        }

        private void panelTablero_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAutomatico_Click(object sender, EventArgs e)
        {
            if (!juego.EnCurso)
            {
                MessageBox.Show("Primero inicia una partida.");
                return;
            }


            timerCartas.Interval = (int)numVelocidad.Value * 1000;
            timerCartas.Start();
        }

        private void btnDetenerAutomatico_Click(object sender, EventArgs e)
        {
            timerCartas.Stop();
        }

        private void numVelocidad_ValueChanged(object sender, EventArgs e)
        {
            timerCartas.Interval = (int)numVelocidad.Value * 1000;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbCartasPersonalizar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void btnPersonalizarTabla_Click(object sender, EventArgs e)
        {
            FormPersonalizacion ventana = new FormPersonalizacion();
            ventana.ShowDialog();

            ActualizarListaFavoritos();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void CrearTablerosDinamicos(int cantidadTablas)
        {
            MessageBox.Show("Creando " + cantidadTablas + " tablas");

            flpTableros.Controls.Clear();
            listaCasillasVisuales.Clear();

            int tamañoCasilla = 65;
            int espacio = 5;

            for (int t = 0; t < cantidadTablas; t++)
            {
                var grupoTabla = new System.Windows.Forms.GroupBox();
                grupoTabla.Text = "Tabla " + (t + 1);
                grupoTabla.Width = 380;
                grupoTabla.Height = 410;

                PictureBox[,] casillas = new PictureBox[5, 5];

                for (int f = 0; f < 5; f++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        PictureBox pic = new PictureBox();

                        pic.Width = tamañoCasilla;
                        pic.Height = tamañoCasilla;
                        pic.Left = 15 + c * (tamañoCasilla + espacio);
                        pic.Top = 25 + f * (tamañoCasilla + espacio);
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.BorderStyle = BorderStyle.FixedSingle;
                        pic.BackColor = Color.White;
                        pic.Cursor = Cursors.Hand;

                        Carta carta = juego.TablerosJugador[t].ObtenerCarta(f, c);

                        if (System.IO.File.Exists(carta.RutaImagen))
                        {
                            pic.Image = Image.FromFile(carta.RutaImagen);
                        }

                        pic.Tag = new Point(t, f * 5 + c);
                        pic.Click += PicCasillaDinamica_Click;
                        pic.Paint += PicCasilla_Paint;


                        grupoTabla.Controls.Add(pic);
                        casillas[f, c] = pic;
                    }
                }

                listaCasillasVisuales.Add(casillas);
                flpTableros.Controls.Add(grupoTabla);
            }
        }

        private void PicCasillaDinamica_Click(object sender, EventArgs e)
        {
            PictureBox picPresionado = (PictureBox)sender;
            Point datos = (Point)picPresionado.Tag;

            int indiceTabla = datos.X;
            int posicion = datos.Y;

            int fila = posicion / 5;
            int col = posicion % 5;

            Tablero tablero = juego.TablerosJugador[indiceTabla];
            Carta cartaClickeada = tablero.ObtenerCarta(fila, col);

            if (juego.CartaActual != null && cartaClickeada.Id == juego.CartaActual.Id)
            {
                tablero.MarcarPosicion(fila, col);
                juego.ControlSonido.ReproducirEfecto("frijolito");

                picPresionado.AccessibleName = "Marcado";
                picPresionado.Invalidate();

                if (tablero.VerificarSiGano(cmbModoJuego.Text))
                {
                    juego.ControlSonido.ReproducirEfecto("ganar");
                    juego.TerminarJuego();
                    timerCartas.Stop();
                    btnSiguiente.Enabled = false;

                    if (red != null)
                        red.EnviarMensaje("LOTERIA");

                    MessageBox.Show($"¡¡LOTERÍA!! Ganaste con la Tabla {indiceTabla + 1}.",
                                    "Victoria",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            else
            {
                juego.ControlSonido.ReproducirEfecto("error");

                string mensajeError = (juego.CartaActual == null)
                    ? "¡Aún no ha salido ninguna carta del mazo!"
                    : $"La carta '{cartaClickeada.Nombre}' no ha salido. La actual es '{juego.CartaActual.Nombre}'.";

                MessageBox.Show(mensajeError, "Carta Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}