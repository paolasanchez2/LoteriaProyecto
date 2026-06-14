using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoteriaProyecto
{
    public partial class Form1 : Form
    {

        // Instancia global del controlador del juego
        private JuegoManager juego;

        // Arreglo bidimensional de PictureBoxes para la interfaz visual (Tema 5)
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
            CrearTableroEnPantalla();
            CrearTableroEnPantalla2S();
            red = new RedManager();
            red.MensajeRecibido += ProcesarMensajeRed;

            timerCartas.Tick += timerCartas_Tick; //Nuevo
        }
        private bool ValidarSeleccionDeTablas()
        {
            // Si ninguno de los dos RadioButtons está marcado, alertamos al usuario
            if (!rbUnaTabla.Checked && !rbDosTablas.Checked)
            {
                MessageBox.Show("Por favor, selecciona primero con cuántas tablas deseas jugar (1 Tabla o 2 Tablas) antes de realizar esta acción.",
                                "Selección Requerida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false; // Validación fallida
            }
            return true; // Todo en orden
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lstHistorial.Visible = false;
            ActualizarListaFavoritos();
           
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
            if (!rbDosTablas.Checked) return;
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
            if (!ValidarSeleccionDeTablas()) return;
            juego.IniciarNuevoJuego();

            // 1. Verificar cuántas tablas seleccionó el usuario
            bool jugarConDosTablas = rbDosTablas.Checked; // O desde un ComboBox: cmbTablas.Text == "2"

            // Mostrar u ocultar el panel visual de la segunda tabla
            panelTablero2.Visible = jugarConDosTablas;

            // Generar y pintar la primera tabla (Siempre se hace)
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
                    catch { casillasVisuales[f, c].Image = null; }
                }
            }

            // Generar y pintar la segunda tabla SOLO si el usuario la seleccionó
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
                        catch { casillasVisuales2[f, c].Image = null; }
                    }
                }
            }

            btnSiguiente.Enabled = true;
            picCartaActual.Image = null;
            flpHistorialImagenes.Controls.Clear(); // Limpiamos el historial visual (Punto 2)

            if (soyServidor)
            {
                string modoSeleccionado = cmbModoJuego.Text;
                string numTablas = jugarConDosTablas ? "2" : "1";

                // Mandamos el mensaje con el formato: INICIAR_PARTIDA:ModoJuego:NumTablas
                red.EnviarMensaje($"INICIAR_PARTIDA:{modoSeleccionado}:{numTablas}");
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
            btnIniciar.Enabled = true; // El servidor controla el mazo
            btnSiguiente.Enabled = false;
            this.Text = "Lotería - MODO SERVIDOR";
        }

        private void btnModoCliente_Click(object sender, EventArgs e)
        {
            soyServidor = false;
            red.ConectarAlServidor(txtIP.Text);
            btnIniciar.Enabled = false; // El cliente no puede iniciar ni barajar
            btnSiguiente.Enabled = false;
            this.Text = "Lotería - MODO CLIENTE";
        }

        private void ProcesarMensajeRed(string mensaje)
        {
            // C# requiere usar Invoke porque la red corre en un hilo separado a la interfaz gráfica
            this.Invoke((MethodInvoker)delegate
            {
                // CASO 1: Llegó una nueva carta desde el servidor
                if (mensaje.StartsWith("CARTA:"))
                {
                    int idCarta = int.Parse(mensaje.Split(':')[1]);

                    // Le ordenamos al juego local del cliente que se sincronice con el ID enviado
                    juego.SincronizarCartaPorId(idCarta);
                    AgregarAlHistorialVisual(juego.CartaActual); // <--- REEMPLAZA O AGREGA AQUÍ
                    lstHistorial.Items.Insert(0, $"{idCarta} - {juego.CartaActual.Nombre}");

                    // Buscamos visualmente el archivo en la carpeta local para mostrarlo en la pantalla del cliente
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
                // CASO 2: El contrincante cantó ¡Lotería! antes que tú
                if (mensaje == "LOTERIA")
                {
                    juego.TerminarJuego();
                    timerCartas.Stop();
                    btnSiguiente.Enabled = false;
                    MessageBox.Show("¡El otro jugador ha cantado LOTERÍA! Suerte para la próxima.", "Fin del Juego", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Agrega este nuevo bloque IF junto a los otros casos de ProcesarMensajeRed
                // Busca esto dentro de ProcesarMensajeRed:
                // Modifica el bloque del inicio de partida dentro de ProcesarMensajeRed:
                if (mensaje.StartsWith("INICIAR_PARTIDA:"))
                {
                    string[] datos = mensaje.Split(':');
                    string modoDelServidor = datos[1];
                    string tablasDelServidor = datos[2]; // "1" o "2"

                    // Sincronizamos los componentes del cliente automáticamente
                    cmbModoJuego.Text = modoDelServidor;
                    if (tablasDelServidor == "2") rbDosTablas.Checked = true;
                    else rbUnaTabla.Checked = true;

                    // Controlamos la visibilidad del panel del cliente igual que en el servidor
                    panelTablero2.Visible = (tablasDelServidor == "2");

                    // Inicializamos el tablero del cliente
                    IniciarTableroCliente();
                }
            });
        }
        private void IniciarTableroCliente()
        {
            lstHistorial.Items.Clear();
            // El cliente inicializa su lógica local
            juego.IniciarNuevoJuego();

            // Pintamos las imágenes en la pantalla del cliente
            for (int f = 0; f < 5; f++)
            {
                for (int c = 0; c < 5; c++)
                {
                    Carta carta = juego.TableroJugador.ObtenerCarta(f, c);
                    casillasVisuales[f, c].BackColor = Color.White;
                    casillasVisuales[f, c].AccessibleName = ""; // Limpia marcas anteriores

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
            // 1. Validar que haya seleccionado una opción (1 o 2 tablas)
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
                // Guardamos en una variable booleana si el usuario quiere cargar la segunda tabla
                bool cargarSegundaTabla = rbDosTablas.Checked;

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
                    else if (tipoTablero == "T2" && cargarSegundaTabla) // <-- SOLO si rbDosTablas está activo
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
    }
}