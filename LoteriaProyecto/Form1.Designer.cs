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
            this.flpHistorialImagenes = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTablasFavoritas = new System.Windows.Forms.ComboBox();
            this.btnAutomatico = new System.Windows.Forms.Button();
            this.btnDetenerAutomatico = new System.Windows.Forms.Button();
            this.numVelocidad = new System.Windows.Forms.NumericUpDown();
            this.gbConfiguracion = new System.Windows.Forms.GroupBox();
            this.numCantidadTablas = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbControlesJuego = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnPersonalizarTabla = new System.Windows.Forms.Button();
            this.flpTableros = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLoteria = new System.Windows.Forms.Button();
            this.timerValidacion = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picCartaActual)).BeginInit();
            this.flpHistorialImagenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVelocidad)).BeginInit();
            this.gbConfiguracion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidadTablas)).BeginInit();
            this.gbControlesJuego.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(6, 41);
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
            this.btnSiguiente.Location = new System.Drawing.Point(6, 79);
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
            this.picCartaActual.Location = new System.Drawing.Point(9, 42);
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
            this.panelTablero.Visible = false;
            this.panelTablero.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTablero_Paint);
            // 
            // timerCartas
            // 
            this.timerCartas.Interval = 3000;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(6, 80);
            this.txtIP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIP.Multiline = true;
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(267, 49);
            this.txtIP.TabIndex = 4;
            // 
            // btnModoServidor
            // 
            this.btnModoServidor.BackColor = System.Drawing.Color.White;
            this.btnModoServidor.Location = new System.Drawing.Point(6, 133);
            this.btnModoServidor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnModoServidor.Name = "btnModoServidor";
            this.btnModoServidor.Size = new System.Drawing.Size(263, 48);
            this.btnModoServidor.TabIndex = 5;
            this.btnModoServidor.Text = "Crear Partida (Servidor)";
            this.btnModoServidor.UseVisualStyleBackColor = false;
            this.btnModoServidor.Click += new System.EventHandler(this.btnModoServidor_Click);
            // 
            // btnModoCliente
            // 
            this.btnModoCliente.Location = new System.Drawing.Point(7, 189);
            this.btnModoCliente.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnModoCliente.Name = "btnModoCliente";
            this.btnModoCliente.Size = new System.Drawing.Size(262, 54);
            this.btnModoCliente.TabIndex = 6;
            this.btnModoCliente.Text = "Unirse a Partida (Cliente)";
            this.btnModoCliente.UseVisualStyleBackColor = true;
            this.btnModoCliente.Click += new System.EventHandler(this.btnModoCliente_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 22);
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
            this.cmbModoJuego.Location = new System.Drawing.Point(6, 50);
            this.cmbModoJuego.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbModoJuego.Name = "cmbModoJuego";
            this.cmbModoJuego.Size = new System.Drawing.Size(266, 30);
            this.cmbModoJuego.TabIndex = 8;
            // 
            // panelTablero2
            // 
            this.panelTablero2.Location = new System.Drawing.Point(559, 13);
            this.panelTablero2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTablero2.Name = "panelTablero2";
            this.panelTablero2.Size = new System.Drawing.Size(521, 536);
            this.panelTablero2.TabIndex = 4;
            this.panelTablero2.Visible = false;
            // 
            // btnGuardarFavorito
            // 
            this.btnGuardarFavorito.Location = new System.Drawing.Point(175, 35);
            this.btnGuardarFavorito.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGuardarFavorito.Name = "btnGuardarFavorito";
            this.btnGuardarFavorito.Size = new System.Drawing.Size(153, 82);
            this.btnGuardarFavorito.TabIndex = 9;
            this.btnGuardarFavorito.Text = "Guardar Tabla en Favoritos";
            this.btnGuardarFavorito.UseVisualStyleBackColor = true;
            this.btnGuardarFavorito.Click += new System.EventHandler(this.btnGuardarFavorito_Click);
            // 
            // btnCargarFavorito
            // 
            this.btnCargarFavorito.Location = new System.Drawing.Point(175, 125);
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
            this.lstHistorial.ItemHeight = 22;
            this.lstHistorial.Location = new System.Drawing.Point(3, 4);
            this.lstHistorial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstHistorial.Name = "lstHistorial";
            this.lstHistorial.Size = new System.Drawing.Size(134, 92);
            this.lstHistorial.TabIndex = 11;
            // 
            // flpHistorialImagenes
            // 
            this.flpHistorialImagenes.AutoScroll = true;
            this.flpHistorialImagenes.Controls.Add(this.lstHistorial);
            this.flpHistorialImagenes.Location = new System.Drawing.Point(9, 473);
            this.flpHistorialImagenes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flpHistorialImagenes.Name = "flpHistorialImagenes";
            this.flpHistorialImagenes.Size = new System.Drawing.Size(304, 299);
            this.flpHistorialImagenes.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 22);
            this.label2.TabIndex = 15;
            this.label2.Text = "Modo de juego";
            // 
            // cmbTablasFavoritas
            // 
            this.cmbTablasFavoritas.FormattingEnabled = true;
            this.cmbTablasFavoritas.Location = new System.Drawing.Point(346, 48);
            this.cmbTablasFavoritas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbTablasFavoritas.Name = "cmbTablasFavoritas";
            this.cmbTablasFavoritas.Size = new System.Drawing.Size(172, 30);
            this.cmbTablasFavoritas.TabIndex = 16;
            // 
            // btnAutomatico
            // 
            this.btnAutomatico.Location = new System.Drawing.Point(6, 116);
            this.btnAutomatico.Name = "btnAutomatico";
            this.btnAutomatico.Size = new System.Drawing.Size(224, 37);
            this.btnAutomatico.TabIndex = 17;
            this.btnAutomatico.Text = "Iniciar automático";
            this.btnAutomatico.UseVisualStyleBackColor = true;
            this.btnAutomatico.Click += new System.EventHandler(this.btnAutomatico_Click);
            // 
            // btnDetenerAutomatico
            // 
            this.btnDetenerAutomatico.Location = new System.Drawing.Point(6, 156);
            this.btnDetenerAutomatico.Name = "btnDetenerAutomatico";
            this.btnDetenerAutomatico.Size = new System.Drawing.Size(224, 38);
            this.btnDetenerAutomatico.TabIndex = 18;
            this.btnDetenerAutomatico.Text = "Detener automático";
            this.btnDetenerAutomatico.UseVisualStyleBackColor = true;
            this.btnDetenerAutomatico.Click += new System.EventHandler(this.btnDetenerAutomatico_Click);
            // 
            // numVelocidad
            // 
            this.numVelocidad.Location = new System.Drawing.Point(11, 176);
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
            this.numVelocidad.Size = new System.Drawing.Size(85, 28);
            this.numVelocidad.TabIndex = 19;
            this.numVelocidad.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numVelocidad.ValueChanged += new System.EventHandler(this.numVelocidad_ValueChanged);
            // 
            // gbConfiguracion
            // 
            this.gbConfiguracion.Controls.Add(this.numCantidadTablas);
            this.gbConfiguracion.Controls.Add(this.label4);
            this.gbConfiguracion.Controls.Add(this.label3);
            this.gbConfiguracion.Controls.Add(this.label2);
            this.gbConfiguracion.Controls.Add(this.numVelocidad);
            this.gbConfiguracion.Controls.Add(this.cmbModoJuego);
            this.gbConfiguracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbConfiguracion.Location = new System.Drawing.Point(1744, 13);
            this.gbConfiguracion.Name = "gbConfiguracion";
            this.gbConfiguracion.Size = new System.Drawing.Size(283, 215);
            this.gbConfiguracion.TabIndex = 20;
            this.gbConfiguracion.TabStop = false;
            this.gbConfiguracion.Text = "Configuración";
            // 
            // numCantidadTablas
            // 
            this.numCantidadTablas.Location = new System.Drawing.Point(9, 109);
            this.numCantidadTablas.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidadTablas.Name = "numCantidadTablas";
            this.numCantidadTablas.Size = new System.Drawing.Size(120, 28);
            this.numCantidadTablas.TabIndex = 27;
            this.numCantidadTablas.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 22);
            this.label4.TabIndex = 28;
            this.label4.Text = "Cantidad de tablas";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 22);
            this.label3.TabIndex = 20;
            this.label3.Text = "Velocidad por carta";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // gbControlesJuego
            // 
            this.gbControlesJuego.Controls.Add(this.btnIniciar);
            this.gbControlesJuego.Controls.Add(this.btnSiguiente);
            this.gbControlesJuego.Controls.Add(this.btnDetenerAutomatico);
            this.gbControlesJuego.Controls.Add(this.btnAutomatico);
            this.gbControlesJuego.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbControlesJuego.Location = new System.Drawing.Point(1745, 234);
            this.gbControlesJuego.Name = "gbControlesJuego";
            this.gbControlesJuego.Size = new System.Drawing.Size(282, 253);
            this.gbControlesJuego.TabIndex = 21;
            this.gbControlesJuego.TabStop = false;
            this.gbControlesJuego.Text = "Controles de juego";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtIP);
            this.groupBox3.Controls.Add(this.btnModoServidor);
            this.groupBox3.Controls.Add(this.btnModoCliente);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(1744, 516);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(280, 248);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Conexión";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.picCartaActual);
            this.groupBox4.Controls.Add(this.flpHistorialImagenes);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(1340, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(360, 783);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Información de la partida ";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnPersonalizarTabla);
            this.groupBox5.Controls.Add(this.btnGuardarFavorito);
            this.groupBox5.Controls.Add(this.btnCargarFavorito);
            this.groupBox5.Controls.Add(this.cmbTablasFavoritas);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(12, 705);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(524, 218);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tablas favoritas";
            // 
            // btnPersonalizarTabla
            // 
            this.btnPersonalizarTabla.Location = new System.Drawing.Point(6, 35);
            this.btnPersonalizarTabla.Name = "btnPersonalizarTabla";
            this.btnPersonalizarTabla.Size = new System.Drawing.Size(150, 82);
            this.btnPersonalizarTabla.TabIndex = 17;
            this.btnPersonalizarTabla.Text = "Personalizar Tablas";
            this.btnPersonalizarTabla.UseVisualStyleBackColor = true;
            this.btnPersonalizarTabla.Click += new System.EventHandler(this.btnPersonalizarTabla_Click);
            // 
            // flpTableros
            // 
            this.flpTableros.AutoScroll = true;
            this.flpTableros.Location = new System.Drawing.Point(12, 12);
            this.flpTableros.Name = "flpTableros";
            this.flpTableros.Size = new System.Drawing.Size(1284, 654);
            this.flpTableros.TabIndex = 27;
            // 
            // btnLoteria
            // 
            this.btnLoteria.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoteria.Location = new System.Drawing.Point(814, 693);
            this.btnLoteria.Name = "btnLoteria";
            this.btnLoteria.Size = new System.Drawing.Size(482, 71);
            this.btnLoteria.TabIndex = 28;
            this.btnLoteria.Text = "Loteria\r\n";
            this.btnLoteria.UseVisualStyleBackColor = true;
            this.btnLoteria.Click += new System.EventHandler(this.btnLoteria_Click);
            // 
            // timerValidacion
            // 
            this.timerValidacion.Interval = 1000;
            this.timerValidacion.Tick += new System.EventHandler(this.timerValidacion_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(2036, 1024);
            this.Controls.Add(this.btnLoteria);
            this.Controls.Add(this.flpTableros);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbControlesJuego);
            this.Controls.Add(this.gbConfiguracion);
            this.Controls.Add(this.panelTablero2);
            this.Controls.Add(this.panelTablero);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Loteria";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCartaActual)).EndInit();
            this.flpHistorialImagenes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numVelocidad)).EndInit();
            this.gbConfiguracion.ResumeLayout(false);
            this.gbConfiguracion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidadTablas)).EndInit();
            this.gbControlesJuego.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.FlowLayoutPanel flpHistorialImagenes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTablasFavoritas;
        private System.Windows.Forms.Button btnAutomatico;
        private System.Windows.Forms.Button btnDetenerAutomatico;
        private System.Windows.Forms.NumericUpDown numVelocidad;
        private System.Windows.Forms.GroupBox gbConfiguracion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbControlesJuego;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnPersonalizarTabla;
        private System.Windows.Forms.NumericUpDown numCantidadTablas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flpTableros;
        private System.Windows.Forms.Button btnLoteria;
        private System.Windows.Forms.Timer timerValidacion;
    }
}

