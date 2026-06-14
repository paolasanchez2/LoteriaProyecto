namespace LoteriaProyecto
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.picCartaActual = new System.Windows.Forms.PictureBox();
            this.panelTablero = new System.Windows.Forms.Panel();
            this.timerCartas = new System.Windows.Forms.Timer(this.components);
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnModoServidor = new System.Windows.Forms.Button();
            this.btnModoCliente = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbModoJuego = new System.Windows.Forms.ComboBox();
            this.panelTablero2 = new System.Windows.Forms.Panel();
            this.btnGuardarFavorito = new System.Windows.Forms.Button();
            this.btnCargarFavorito = new System.Windows.Forms.Button();
            this.lstHistorial = new System.Windows.Forms.ListBox();
            this.rbUnaTabla = new System.Windows.Forms.RadioButton();
            this.rbDosTablas = new System.Windows.Forms.RadioButton();
            this.flpHistorialImagenes = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTablasFavoritas = new System.Windows.Forms.ComboBox();
            this.btnAutomatico = new System.Windows.Forms.Button();
            this.btnDetenerAutomatico = new System.Windows.Forms.Button();
            this.numVelocidad = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.picCartaActual)).BeginInit();
            this.flpHistorialImagenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVelocidad)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(1231, 52);
            this.btnIniciar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(224, 34);
            this.btnIniciar.TabIndex = 0;
            this.btnIniciar.Text = "Iniciar Juego";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(1231, 94);
            this.btnSiguiente.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(224, 35);
            this.btnSiguiente.TabIndex = 1;
            this.btnSiguiente.Text = "Siguiente Carta";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // picCartaActual
            // 
            this.picCartaActual.Location = new System.Drawing.Point(1231, 341);
            this.picCartaActual.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picCartaActual.Name = "picCartaActual";
            this.picCartaActual.Size = new System.Drawing.Size(261, 418);
            this.picCartaActual.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCartaActual.TabIndex = 2;
            this.picCartaActual.TabStop = false;
            this.picCartaActual.Click += new System.EventHandler(this.picCartaActual_Click);
            // 
            // panelTablero
            // 
            this.panelTablero.Location = new System.Drawing.Point(12, 13);
            this.panelTablero.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTablero.Name = "panelTablero";
            this.panelTablero.Size = new System.Drawing.Size(524, 536);
            this.panelTablero.TabIndex = 3;
            this.panelTablero.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTablero_Paint);
            // 
            // timerCartas
            // 
            this.timerCartas.Interval = 3000;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(1510, 536);
            this.txtIP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIP.Multiline = true;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(273, 49);
            this.txtIP.TabIndex = 4;
            // 
            // btnModoServidor
            // 
            this.btnModoServidor.Location = new System.Drawing.Point(1520, 615);
            this.btnModoServidor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnModoServidor.Name = "btnModoServidor";
            this.btnModoServidor.Size = new System.Drawing.Size(217, 48);
            this.btnModoServidor.TabIndex = 5;
            this.btnModoServidor.Text = "Crear Partida (Servidor)";
            this.btnModoServidor.UseVisualStyleBackColor = true;
            this.btnModoServidor.Click += new System.EventHandler(this.btnModoServidor_Click);
            // 
            // btnModoCliente
            // 
            this.btnModoCliente.Location = new System.Drawing.Point(1520, 670);
            this.btnModoCliente.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnModoCliente.Name = "btnModoCliente";
            this.btnModoCliente.Size = new System.Drawing.Size(217, 54);
            this.btnModoCliente.TabIndex = 6;
            this.btnModoCliente.Text = "Unirse a Partida (Cliente)";
            this.btnModoCliente.UseVisualStyleBackColor = true;
            this.btnModoCliente.Click += new System.EventHandler(this.btnModoCliente_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1516, 500);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Escribe tu IP";
            // 
            // cmbModoJuego
            // 
            this.cmbModoJuego.FormattingEnabled = true;
            this.cmbModoJuego.Items.AddRange(new object[] {
            "Tradicional (Línea de 4: Horizontal, Vertical, Diagonal)",
            "Tabla Llena",
            "En L (Fila 0, Columna 0 y sus bordes)",
            "Cuatro Esquinas"});
            this.cmbModoJuego.Location = new System.Drawing.Point(1231, 295);
            this.cmbModoJuego.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbModoJuego.Name = "cmbModoJuego";
            this.cmbModoJuego.Size = new System.Drawing.Size(136, 28);
            this.cmbModoJuego.TabIndex = 8;
            // 
            // panelTablero2
            // 
            this.panelTablero2.Location = new System.Drawing.Point(600, 13);
            this.panelTablero2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTablero2.Name = "panelTablero2";
            this.panelTablero2.Size = new System.Drawing.Size(521, 536);
            this.panelTablero2.TabIndex = 4;
            // 
            // btnGuardarFavorito
            // 
            this.btnGuardarFavorito.Location = new System.Drawing.Point(14, 664);
            this.btnGuardarFavorito.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGuardarFavorito.Name = "btnGuardarFavorito";
            this.btnGuardarFavorito.Size = new System.Drawing.Size(118, 82);
            this.btnGuardarFavorito.TabIndex = 9;
            this.btnGuardarFavorito.Text = "Guardar Tabla en Favoritos";
            this.btnGuardarFavorito.UseVisualStyleBackColor = true;
            this.btnGuardarFavorito.Click += new System.EventHandler(this.btnGuardarFavorito_Click);
            // 
            // btnCargarFavorito
            // 
            this.btnCargarFavorito.Location = new System.Drawing.Point(138, 664);
            this.btnCargarFavorito.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCargarFavorito.Name = "btnCargarFavorito";
            this.btnCargarFavorito.Size = new System.Drawing.Size(153, 82);
            this.btnCargarFavorito.TabIndex = 10;
            this.btnCargarFavorito.Text = "Cargar Tabla Favorita";
            this.btnCargarFavorito.UseVisualStyleBackColor = true;
            this.btnCargarFavorito.Click += new System.EventHandler(this.btnCargarFavorito_Click);
            // 
            // lstHistorial
            // 
            this.lstHistorial.FormattingEnabled = true;
            this.lstHistorial.ItemHeight = 20;
            this.lstHistorial.Location = new System.Drawing.Point(3, 4);
            this.lstHistorial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstHistorial.Name = "lstHistorial";
            this.lstHistorial.Size = new System.Drawing.Size(134, 104);
            this.lstHistorial.TabIndex = 11;
            // 
            // rbUnaTabla
            // 
            this.rbUnaTabla.AutoSize = true;
            this.rbUnaTabla.Location = new System.Drawing.Point(1575, 80);
            this.rbUnaTabla.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbUnaTabla.Name = "rbUnaTabla";
            this.rbUnaTabla.Size = new System.Drawing.Size(107, 24);
            this.rbUnaTabla.TabIndex = 12;
            this.rbUnaTabla.TabStop = true;
            this.rbUnaTabla.Text = "Una Tabla";
            this.rbUnaTabla.UseVisualStyleBackColor = true;
            // 
            // rbDosTablas
            // 
            this.rbDosTablas.AutoSize = true;
            this.rbDosTablas.Location = new System.Drawing.Point(1575, 134);
            this.rbDosTablas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbDosTablas.Name = "rbDosTablas";
            this.rbDosTablas.Size = new System.Drawing.Size(114, 24);
            this.rbDosTablas.TabIndex = 13;
            this.rbDosTablas.TabStop = true;
            this.rbDosTablas.Text = "Dos Tablas";
            this.rbDosTablas.UseVisualStyleBackColor = true;
            // 
            // flpHistorialImagenes
            // 
            this.flpHistorialImagenes.AutoScroll = true;
            this.flpHistorialImagenes.Controls.Add(this.lstHistorial);
            this.flpHistorialImagenes.Location = new System.Drawing.Point(1498, 181);
            this.flpHistorialImagenes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flpHistorialImagenes.Name = "flpHistorialImagenes";
            this.flpHistorialImagenes.Size = new System.Drawing.Size(304, 299);
            this.flpHistorialImagenes.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1227, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Modo de juego";
            // 
            // cmbTablasFavoritas
            // 
            this.cmbTablasFavoritas.FormattingEnabled = true;
            this.cmbTablasFavoritas.Location = new System.Drawing.Point(324, 662);
            this.cmbTablasFavoritas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTablasFavoritas.Name = "cmbTablasFavoritas";
            this.cmbTablasFavoritas.Size = new System.Drawing.Size(136, 28);
            this.cmbTablasFavoritas.TabIndex = 16;
            // 
            // btnAutomatico
            // 
            this.btnAutomatico.Location = new System.Drawing.Point(1231, 134);
            this.btnAutomatico.Name = "btnAutomatico";
            this.btnAutomatico.Size = new System.Drawing.Size(224, 37);
            this.btnAutomatico.TabIndex = 17;
            this.btnAutomatico.Text = "Iniciar automático";
            this.btnAutomatico.UseVisualStyleBackColor = true;
            this.btnAutomatico.Click += new System.EventHandler(this.btnAutomatico_Click);
            // 
            // btnDetenerAutomatico
            // 
            this.btnDetenerAutomatico.Location = new System.Drawing.Point(1231, 177);
            this.btnDetenerAutomatico.Name = "btnDetenerAutomatico";
            this.btnDetenerAutomatico.Size = new System.Drawing.Size(224, 38);
            this.btnDetenerAutomatico.TabIndex = 18;
            this.btnDetenerAutomatico.Text = "Detener automático";
            this.btnDetenerAutomatico.UseVisualStyleBackColor = true;
            this.btnDetenerAutomatico.Click += new System.EventHandler(this.btnDetenerAutomatico_Click);
            // 
            // numVelocidad
            // 
            this.numVelocidad.Location = new System.Drawing.Point(1231, 221);
            this.numVelocidad.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVelocidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numVelocidad.Name = "numVelocidad";
            this.numVelocidad.Size = new System.Drawing.Size(85, 26);
            this.numVelocidad.TabIndex = 19;
            this.numVelocidad.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numVelocidad.ValueChanged += new System.EventHandler(this.numVelocidad_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1974, 808);
            this.Controls.Add(this.numVelocidad);
            this.Controls.Add(this.btnDetenerAutomatico);
            this.Controls.Add(this.btnAutomatico);
            this.Controls.Add(this.cmbTablasFavoritas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flpHistorialImagenes);
            this.Controls.Add(this.rbDosTablas);
            this.Controls.Add(this.rbUnaTabla);
            this.Controls.Add(this.btnCargarFavorito);
            this.Controls.Add(this.btnGuardarFavorito);
            this.Controls.Add(this.panelTablero2);
            this.Controls.Add(this.cmbModoJuego);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnModoCliente);
            this.Controls.Add(this.btnModoServidor);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.panelTablero);
            this.Controls.Add(this.picCartaActual);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnIniciar);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCartaActual)).EndInit();
            this.flpHistorialImagenes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numVelocidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.PictureBox picCartaActual;
        private System.Windows.Forms.Panel panelTablero;
        private System.Windows.Forms.Timer timerCartas;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnModoServidor;
        private System.Windows.Forms.Button btnModoCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbModoJuego;
        private System.Windows.Forms.Panel panelTablero2;
        private System.Windows.Forms.Button btnGuardarFavorito;
        private System.Windows.Forms.Button btnCargarFavorito;
        private System.Windows.Forms.ListBox lstHistorial;
        private System.Windows.Forms.RadioButton rbUnaTabla;
        private System.Windows.Forms.RadioButton rbDosTablas;
        private System.Windows.Forms.FlowLayoutPanel flpHistorialImagenes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTablasFavoritas;
        private System.Windows.Forms.Button btnAutomatico;
        private System.Windows.Forms.Button btnDetenerAutomatico;
        private System.Windows.Forms.NumericUpDown numVelocidad;
    }
}

