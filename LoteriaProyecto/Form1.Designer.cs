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
            ((System.ComponentModel.ISupportInitialize)(this.picCartaActual)).BeginInit();
            this.flpHistorialImagenes.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(1094, 42);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(125, 27);
            this.btnIniciar.TabIndex = 0;
            this.btnIniciar.Text = "Iniciar Juego";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(1094, 75);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(125, 28);
            this.btnSiguiente.TabIndex = 1;
            this.btnSiguiente.Text = "Siguiente Carta";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // picCartaActual
            // 
            this.picCartaActual.Location = new System.Drawing.Point(1094, 203);
            this.picCartaActual.Name = "picCartaActual";
            this.picCartaActual.Size = new System.Drawing.Size(232, 351);
            this.picCartaActual.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCartaActual.TabIndex = 2;
            this.picCartaActual.TabStop = false;
            this.picCartaActual.Click += new System.EventHandler(this.picCartaActual_Click);
            // 
            // panelTablero
            // 
            this.panelTablero.Location = new System.Drawing.Point(12, 32);
            this.panelTablero.Name = "panelTablero";
            this.panelTablero.Size = new System.Drawing.Size(523, 459);
            this.panelTablero.TabIndex = 3;
            // 
            // timerCartas
            // 
            this.timerCartas.Interval = 3000;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(1342, 429);
            this.txtIP.Multiline = true;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(243, 40);
            this.txtIP.TabIndex = 4;
            // 
            // btnModoServidor
            // 
            this.btnModoServidor.Location = new System.Drawing.Point(1351, 492);
            this.btnModoServidor.Name = "btnModoServidor";
            this.btnModoServidor.Size = new System.Drawing.Size(193, 38);
            this.btnModoServidor.TabIndex = 5;
            this.btnModoServidor.Text = "Crear Partida (Servidor)";
            this.btnModoServidor.UseVisualStyleBackColor = true;
            this.btnModoServidor.Click += new System.EventHandler(this.btnModoServidor_Click);
            // 
            // btnModoCliente
            // 
            this.btnModoCliente.Location = new System.Drawing.Point(1351, 536);
            this.btnModoCliente.Name = "btnModoCliente";
            this.btnModoCliente.Size = new System.Drawing.Size(193, 43);
            this.btnModoCliente.TabIndex = 6;
            this.btnModoCliente.Text = "Unirse a Partida (Cliente)";
            this.btnModoCliente.UseVisualStyleBackColor = true;
            this.btnModoCliente.Click += new System.EventHandler(this.btnModoCliente_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1348, 400);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
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
            this.cmbModoJuego.Location = new System.Drawing.Point(1108, 145);
            this.cmbModoJuego.Name = "cmbModoJuego";
            this.cmbModoJuego.Size = new System.Drawing.Size(121, 24);
            this.cmbModoJuego.TabIndex = 8;
            // 
            // panelTablero2
            // 
            this.panelTablero2.Location = new System.Drawing.Point(541, 32);
            this.panelTablero2.Name = "panelTablero2";
            this.panelTablero2.Size = new System.Drawing.Size(523, 459);
            this.panelTablero2.TabIndex = 4;
            // 
            // btnGuardarFavorito
            // 
            this.btnGuardarFavorito.Location = new System.Drawing.Point(12, 531);
            this.btnGuardarFavorito.Name = "btnGuardarFavorito";
            this.btnGuardarFavorito.Size = new System.Drawing.Size(105, 66);
            this.btnGuardarFavorito.TabIndex = 9;
            this.btnGuardarFavorito.Text = "Guardar Tabla en Favoritos";
            this.btnGuardarFavorito.UseVisualStyleBackColor = true;
            this.btnGuardarFavorito.Click += new System.EventHandler(this.btnGuardarFavorito_Click);
            // 
            // btnCargarFavorito
            // 
            this.btnCargarFavorito.Location = new System.Drawing.Point(123, 531);
            this.btnCargarFavorito.Name = "btnCargarFavorito";
            this.btnCargarFavorito.Size = new System.Drawing.Size(136, 66);
            this.btnCargarFavorito.TabIndex = 10;
            this.btnCargarFavorito.Text = "Cargar Tabla Favorita";
            this.btnCargarFavorito.UseVisualStyleBackColor = true;
            this.btnCargarFavorito.Click += new System.EventHandler(this.btnCargarFavorito_Click);
            // 
            // lstHistorial
            // 
            this.lstHistorial.FormattingEnabled = true;
            this.lstHistorial.ItemHeight = 16;
            this.lstHistorial.Location = new System.Drawing.Point(3, 3);
            this.lstHistorial.Name = "lstHistorial";
            this.lstHistorial.Size = new System.Drawing.Size(120, 84);
            this.lstHistorial.TabIndex = 11;
            // 
            // rbUnaTabla
            // 
            this.rbUnaTabla.AutoSize = true;
            this.rbUnaTabla.Location = new System.Drawing.Point(1400, 64);
            this.rbUnaTabla.Name = "rbUnaTabla";
            this.rbUnaTabla.Size = new System.Drawing.Size(92, 20);
            this.rbUnaTabla.TabIndex = 12;
            this.rbUnaTabla.TabStop = true;
            this.rbUnaTabla.Text = "Una Tabla";
            this.rbUnaTabla.UseVisualStyleBackColor = true;
            // 
            // rbDosTablas
            // 
            this.rbDosTablas.AutoSize = true;
            this.rbDosTablas.Location = new System.Drawing.Point(1400, 107);
            this.rbDosTablas.Name = "rbDosTablas";
            this.rbDosTablas.Size = new System.Drawing.Size(99, 20);
            this.rbDosTablas.TabIndex = 13;
            this.rbDosTablas.TabStop = true;
            this.rbDosTablas.Text = "Dos Tablas";
            this.rbDosTablas.UseVisualStyleBackColor = true;
            // 
            // flpHistorialImagenes
            // 
            this.flpHistorialImagenes.AutoScroll = true;
            this.flpHistorialImagenes.Controls.Add(this.lstHistorial);
            this.flpHistorialImagenes.Location = new System.Drawing.Point(1332, 145);
            this.flpHistorialImagenes.Name = "flpHistorialImagenes";
            this.flpHistorialImagenes.Size = new System.Drawing.Size(270, 239);
            this.flpHistorialImagenes.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1105, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Modo de juego";
            // 
            // cmbTablasFavoritas
            // 
            this.cmbTablasFavoritas.FormattingEnabled = true;
            this.cmbTablasFavoritas.Location = new System.Drawing.Point(288, 530);
            this.cmbTablasFavoritas.Name = "cmbTablasFavoritas";
            this.cmbTablasFavoritas.Size = new System.Drawing.Size(121, 24);
            this.cmbTablasFavoritas.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1755, 646);
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
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCartaActual)).EndInit();
            this.flpHistorialImagenes.ResumeLayout(false);
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
    }
}

