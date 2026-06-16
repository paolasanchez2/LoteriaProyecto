using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LoteriaProyecto
{
    public partial class Form1 : Form
    {

        private List<PictureBox[,]> listaCasillasVisuales;
        private JuegoManager juego;




        private RedManager red;
        private bool soyServidor = false;

        private bool validacionAbierta = false;
        private List<string> reclamantes = new List<string>();
        private List<ComboBox> combosFavoritosPorTabla;

        public Form1()
        {
            InitializeComponent();
            juego = new JuegoManager();

            listaCasillasVisuales = new List<PictureBox[,]>();

            red = new RedManager();
            red.MensajeRecibido += ProcesarMensajeRed;

            timerCartas.Tick += timerCartas_Tick;
            combosFavoritosPorTabla = new List<ComboBox>();
            cmbTipoTabla.SelectedIndex = 0;

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
            CargarModosPersonalizados();

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            int cantidadTablas = (int)numCantidadTablas.Value;

            juego.IniciarNuevoJuego(cantidadTablas);
            CrearTablerosDinamicos(cantidadTablas);
     
            btnSiguiente.Enabled = true;
            picCartaActual.Image = null;
            flpHistorialImagenes.Controls.Clear();

            if (soyServidor)
            {
                string modoSeleccionado = cmbModoJuego.Text;
                int velocidad = (int)numVelocidad.Value;
                string tipoTabla = cmbTipoTabla.Text;

                red.EnviarMensaje($"INICIAR_PARTIDA|{modoSeleccionado}|{cantidadTablas}|{velocidad}|{tipoTabla}");
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (!ValidarTipoDeTablas())
                return;

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

                if (mensaje.StartsWith("CHAT|"))
                {
                    string[] datos = mensaje.Split('|');

                    string nombre = datos[1];
                    string texto = datos[2];

                    txtHistorialChat.AppendText($"{nombre}: {texto}{Environment.NewLine}");

                    return;
                }

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

                if (mensaje.StartsWith("RECLAMO_LOTERIA|"))
                {
                    string jugador = mensaje.Split('|')[1];

                    if (soyServidor)
                    {
                        RegistrarReclamoLoteria(jugador);
                    }

                    return;
                }

                if (mensaje.StartsWith("INICIAR_PARTIDA|"))
                {
                    string[] datos = mensaje.Split('|');

                    string modoDelServidor = datos[1];
                    string tablasDelServidor = datos[2];
                    int velocidadDelServidor = int.Parse(datos[3]);
                    string tipoTablaDelServidor = datos[4];

                    int cantidadTablasServidor = int.Parse(tablasDelServidor);

                    cmbModoJuego.Text = modoDelServidor;

                    numCantidadTablas.Value = cantidadTablasServidor;

                    numVelocidad.Value = velocidadDelServidor;
                    timerCartas.Interval = velocidadDelServidor * 1000;
                    cmbTipoTabla.Text = tipoTablaDelServidor;

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

            string nombreTabla = Microsoft.VisualBasic.Interaction.InputBox(
                "Introduce el nombre para guardar tus tablas actuales:",
                "Guardar Paquete de Tablas",
                "MisTablas"
            );

            if (string.IsNullOrWhiteSpace(nombreTabla)) return;

            try
            {
                List<string> lineas = new List<string>();

                for (int t = 0; t < juego.TablerosJugador.Count; t++)
                {
                    Tablero tablero = juego.TablerosJugador[t];

                    for (int f = 0; f < 5; f++)
                    {
                        for (int c = 0; c < 5; c++)
                        {
                            Carta carta = tablero.ObtenerCarta(f, c);

                            if (carta != null)
                            {
                                lineas.Add($"T{t + 1},{f},{c},{carta.Id}");
                            }
                        }
                    }
                }

                string rutaArchivo = System.IO.Path.Combine(
                    carpetaFavoritos,
                    nombreTabla + ".txt"
                );

                System.IO.File.WriteAllLines(rutaArchivo, lineas);

                MessageBox.Show(
                    $"¡Tus {juego.TablerosJugador.Count} tablas han sido guardadas en '{nombreTabla}'!",
                    "Favoritos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                ActualizarListaFavoritos();
                ActualizarCombosFavoritosPorTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
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



        private void btnAutomatico_Click(object sender, EventArgs e)
        {
            if (!juego.EnCurso)
            {
                MessageBox.Show("Primero inicia una partida.");
                return;
            }

            MessageBox.Show("Estoy validando: " + cmbTipoTabla.Text);

            if (!ValidarTipoDeTablas())
                return;

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
   
        private void btnPersonalizarTabla_Click(object sender, EventArgs e)
        {
            FormPersonalizacion ventana = new FormPersonalizacion();
            ventana.ShowDialog();

            ActualizarListaFavoritos();
            ActualizarCombosFavoritosPorTabla();
        }

        private void CrearTablerosDinamicos(int cantidadTablas)
        {
            combosFavoritosPorTabla.Clear();
            flpTableros.Controls.Clear();
            listaCasillasVisuales.Clear();

            int tamañoCasilla = 65;
            int espacio = 5;

            for (int t = 0; t < cantidadTablas; t++)
            {
                var grupoTabla = new System.Windows.Forms.GroupBox();
                grupoTabla.Text = "Tabla " + (t + 1);
                grupoTabla.Width = 380;
                grupoTabla.Height = 460;

                ComboBox cmbFavoritoTabla = new ComboBox();
                cmbFavoritoTabla.Width = 220;
                cmbFavoritoTabla.Left = 15;
                cmbFavoritoTabla.Top = 25;
                cmbFavoritoTabla.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbFavoritoTabla.Tag = t;

                cmbFavoritoTabla.Items.Add("Aleatoria");

                string carpetaFavoritos = "TablasFavoritas";

                if (System.IO.Directory.Exists(carpetaFavoritos))
                {
                    string[] archivos = System.IO.Directory.GetFiles(carpetaFavoritos, "*.txt");

                    foreach (string archivo in archivos)
                    {
                        cmbFavoritoTabla.Items.Add(
                            System.IO.Path.GetFileNameWithoutExtension(archivo));
                    }
                }

                cmbFavoritoTabla.SelectedIndex = 0;
                cmbFavoritoTabla.SelectedIndexChanged += CmbFavoritoTabla_SelectedIndexChanged;

                grupoTabla.Controls.Add(cmbFavoritoTabla);
                combosFavoritosPorTabla.Add(cmbFavoritoTabla);

                PictureBox[,] casillas = new PictureBox[5, 5];

                for (int f = 0; f < 5; f++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        PictureBox pic = new PictureBox();

                        pic.Width = tamañoCasilla;
                        pic.Height = tamañoCasilla;
                        pic.Left = 15 + c * (tamañoCasilla + espacio);
                        pic.Top = 65 + f * (tamañoCasilla + espacio);
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

            tablero.MarcarPosicion(fila, col);
            juego.ControlSonido.ReproducirEfecto("frijolito");

            picPresionado.AccessibleName = "Marcado";
            picPresionado.Invalidate();

        }

        private void btnLoteria_Click(object sender, EventArgs e)
        {
            timerCartas.Stop();

            bool hayGanador = false;

            foreach (Tablero tablero in juego.TablerosJugador)
            {
                if (tablero.VerificarVictoriaValida(
                        cmbModoJuego.Text,
                        juego.CartasCantadasIds))
                {
                    hayGanador = true;
                    break;
                }
            }

            if (!hayGanador)
            {
                MessageBox.Show(
                    "No tienes una lotería válida.",
                    "Lotería Incorrecta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string jugador = soyServidor ? "Servidor" : "Cliente";

            if (soyServidor)
            {
                RegistrarReclamoLoteria(jugador);
            }
            else
            {
                red.EnviarMensaje("RECLAMO_LOTERIA|" + jugador);

                MessageBox.Show(
                    "Lotería válida enviada al servidor.\n" +
                    "Esperando resultado...");
            }
        }

        private void timerValidacion_Tick(object sender, EventArgs e)
        {
            timerValidacion.Stop();
            validacionAbierta = false;

            if (reclamantes.Count == 0)
            {
                MessageBox.Show("Fin de validación.\nSin ganador.");
            }
            else if (reclamantes.Count == 1)
            {
                MessageBox.Show("Fin de validación.\nGanador: " + reclamantes[0]);
            }
            else
            {
                ResolverEmpateCartaMayor();
            }

            juego.TerminarJuego();
            timerCartas.Stop();
            btnSiguiente.Enabled = false;
        }

        private void CmbFavoritoTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboActual = (ComboBox)sender;

            if (comboActual.SelectedItem == null) return;

            string seleccion = comboActual.SelectedItem.ToString();

            if (seleccion == "Aleatoria") return;

            foreach (ComboBox otroCombo in combosFavoritosPorTabla)
            {
                if (otroCombo == comboActual) continue;

                if (otroCombo.SelectedItem != null &&
                    otroCombo.SelectedItem.ToString() == seleccion)
                {
                    MessageBox.Show(
                        "Esta tabla favorita ya está en uso en otra tabla.",
                        "Tabla repetida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    comboActual.SelectedIndexChanged -= CmbFavoritoTabla_SelectedIndexChanged;
                    comboActual.SelectedIndex = 0;
                    comboActual.SelectedIndexChanged += CmbFavoritoTabla_SelectedIndexChanged;

                    return;
                }
            }

            int indiceTabla = (int)comboActual.Tag;

            CargarFavoritoEnTabla(indiceTabla, seleccion);
        }

        private void CargarFavoritoEnTabla(int indiceTabla, string nombreFavorito)
        {
            string rutaArchivo = System.IO.Path.Combine(
                "TablasFavoritas",
                nombreFavorito + ".txt");

            if (!System.IO.File.Exists(rutaArchivo)) return;

            string[] lineas = System.IO.File.ReadAllLines(rutaArchivo);

            foreach (string linea in lineas)
            {
                string[] datos = linea.Split(',');

                // Formato actual: T1,f,c,id
                int f = int.Parse(datos[1]);
                int c = int.Parse(datos[2]);
                int idCarta = int.Parse(datos[3]);

                Carta cartaFavorita = juego.BuscarCartaPorIdGlobal(idCarta);

                juego.TablerosJugador[indiceTabla]
                    .AsignarCartaEnPosicion(f, c, cartaFavorita);

                PictureBox pic =
                    listaCasillasVisuales[indiceTabla][f, c];

                pic.AccessibleName = "";

                if (System.IO.File.Exists(cartaFavorita.RutaImagen))
                {
                    pic.Image = Image.FromFile(cartaFavorita.RutaImagen);
                }
            }
        }

        private void ActualizarCombosFavoritosPorTabla()
        {
            foreach (ComboBox combo in combosFavoritosPorTabla)
            {
                string seleccionActual = combo.SelectedItem?.ToString();

                combo.Items.Clear();
                combo.Items.Add("Aleatoria");

                string carpetaFavoritos = "TablasFavoritas";

                if (System.IO.Directory.Exists(carpetaFavoritos))
                {
                    string[] archivos = System.IO.Directory.GetFiles(carpetaFavoritos, "*.txt");

                    foreach (string archivo in archivos)
                    {
                        combo.Items.Add(System.IO.Path.GetFileNameWithoutExtension(archivo));
                    }
                }

                if (!string.IsNullOrEmpty(seleccionActual) &&
                    combo.Items.Contains(seleccionActual))
                {
                    combo.SelectedItem = seleccionActual;
                }
                else
                {
                    combo.SelectedIndex = 0;
                }
            }
        }

        private void btnCargarPaquete_Click(object sender, EventArgs e)
        {
            if (cmbTablasFavoritas.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un paquete de tablas.");
                return;
            }

            string nombrePaquete = cmbTablasFavoritas.SelectedItem.ToString();
            string rutaArchivo = System.IO.Path.Combine("TablasFavoritas", nombrePaquete + ".txt");

            if (!System.IO.File.Exists(rutaArchivo))
            {
                MessageBox.Show("No se encontró el archivo del paquete.");
                return;
            }

            try
            {
                string[] lineas = System.IO.File.ReadAllLines(rutaArchivo);

                int cantidadTablas = 0;

                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(',');
                    string tipoTabla = datos[0]; // T1, T2, T3...

                    int numeroTabla = int.Parse(tipoTabla.Replace("T", ""));
                    if (numeroTabla > cantidadTablas)
                        cantidadTablas = numeroTabla;
                }

                numCantidadTablas.Value = cantidadTablas;

                juego.IniciarNuevoJuego(cantidadTablas);
                CrearTablerosDinamicos(cantidadTablas);

                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(',');

                    string tipoTabla = datos[0];
                    int indiceTabla = int.Parse(tipoTabla.Replace("T", "")) - 1;

                    int f = int.Parse(datos[1]);
                    int c = int.Parse(datos[2]);
                    int idCarta = int.Parse(datos[3]);

                    Carta carta = juego.BuscarCartaPorIdGlobal(idCarta);

                    juego.TablerosJugador[indiceTabla].AsignarCartaEnPosicion(f, c, carta);

                    PictureBox pic = listaCasillasVisuales[indiceTabla][f, c];

                    pic.AccessibleName = "";

                    if (System.IO.File.Exists(carta.RutaImagen))
                    {
                        pic.Image = Image.FromFile(carta.RutaImagen);
                    }

                    pic.Invalidate();
                }

                MessageBox.Show($"Paquete '{nombrePaquete}' cargado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar paquete: " + ex.Message);
            }
        }

        private void btnCrearModoJuego_Click(object sender, EventArgs e)
        {
            FormModoJuego ventana = new FormModoJuego();

            if (ventana.ShowDialog() == DialogResult.OK)
            {
                CargarModosPersonalizados();
            }
        }

        private void CargarModosPersonalizados()
        {
            cmbModoJuego.Items.Clear();

            cmbModoJuego.Items.Add("Tradicional (Horizontal, Vertical, Diagonal)");
            cmbModoJuego.Items.Add("Tabla Llena");
            cmbModoJuego.Items.Add("En L");
            cmbModoJuego.Items.Add("Cuatro Esquinas");

            string carpeta = "ModosPersonalizados";

            if (System.IO.Directory.Exists(carpeta))
            {
                string[] archivos = System.IO.Directory.GetFiles(carpeta, "*.txt");

                foreach (string archivo in archivos)
                {
                    string nombreModo = System.IO.Path.GetFileNameWithoutExtension(archivo);

                    if (!cmbModoJuego.Items.Contains(nombreModo))
                    {
                        cmbModoJuego.Items.Add(nombreModo);
                    }
                }
            }

            if (cmbModoJuego.Items.Count > 0)
            {
                cmbModoJuego.SelectedIndex = 0;
            }
        }

        private void ResolverEmpateCartaMayor()
        {
            Random rnd = new Random();

            string ganador = "";
            int cartaMayor = -1;
            string resultado = "Empate detectado.\nDesempate por carta mayor:\n\n";

            foreach (string jugador in reclamantes)
            {
                Carta carta = juego.MazoPrincipal.ObtenerListaCompleta()
                    [rnd.Next(juego.MazoPrincipal.ObtenerListaCompleta().Count)];

                resultado += $"{jugador}: {carta.Nombre} ({carta.Id})\n";

                if (carta.Id > cartaMayor)
                {
                    cartaMayor = carta.Id;
                    ganador = jugador;
                }
            }

            resultado += $"\nGanador por carta mayor: {ganador}";

            MessageBox.Show(resultado, "Desempate", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void RegistrarReclamoLoteria(string jugador)
        {
            if (!reclamantes.Contains(jugador))
            {
                reclamantes.Add(jugador);
            }

            if (!validacionAbierta)
            {
                validacionAbierta = true;

                timerValidacion.Stop();
                timerValidacion.Interval = 10000;
                timerValidacion.Start();

                MessageBox.Show(
                    "Lotería válida detectada.\n" +
                    "Esperando 10 segundos por otros jugadores...");
            }
        }

        private void btnEnviarChat_Click(object sender, EventArgs e)
        {
            string mensaje = txtMensajeChat.Text.Trim();

            if (mensaje == "")
                return;

            string nombre = soyServidor ? "Servidor" : "Cliente";

            txtHistorialChat.AppendText($"{nombre}: {mensaje}{Environment.NewLine}");

            red.EnviarMensaje($"CHAT|{nombre}|{mensaje}");

            txtMensajeChat.Clear();
            txtMensajeChat.Focus();
        }

        private void txtMensajeChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Evita el sonido "ding"

                btnEnviarChat.PerformClick();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private bool ValidarTipoDeTablas()
        {
            string tipoTabla = cmbTipoTabla.Text;

            if (tipoTabla == "Cartas dobles")
                return true;

            for (int i = 0; i < juego.TablerosJugador.Count; i++)
            {
                if (juego.TablerosJugador[i].TieneCartasDuplicadas())
                {
                    MessageBox.Show(
                        $"La Tabla {i + 1} contiene cartas repetidas.\n\n" +
                        "El modo 'Cartas únicas' no permite cartas duplicadas.\n" +
                        "Cambia esa tabla o selecciona 'Cartas dobles'.",
                        "Tabla no permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }
            }

            return true;
        }
    }
}