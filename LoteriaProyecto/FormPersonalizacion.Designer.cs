namespace LoteriaProyecto
{
    partial class FormPersonalizacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpCartas = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTablaPersonalizada = new System.Windows.Forms.Panel();
            this.picCartaSeleccionada = new System.Windows.Forms.PictureBox();
            this.lblCartaSeleccionada = new System.Windows.Forms.Label();
            this.btnGuardarPersonalizacion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCartaSeleccionada)).BeginInit();
            this.SuspendLayout();
            // 
            // flpCartas
            // 
            this.flpCartas.AutoScroll = true;
            this.flpCartas.Location = new System.Drawing.Point(751, 54);
            this.flpCartas.Name = "flpCartas";
            this.flpCartas.Size = new System.Drawing.Size(400, 500);
            this.flpCartas.TabIndex = 0;
            // 
            // panelTablaPersonalizada
            // 
            this.panelTablaPersonalizada.Location = new System.Drawing.Point(39, 54);
            this.panelTablaPersonalizada.Name = "panelTablaPersonalizada";
            this.panelTablaPersonalizada.Size = new System.Drawing.Size(580, 548);
            this.panelTablaPersonalizada.TabIndex = 1;
            // 
            // picCartaSeleccionada
            // 
            this.picCartaSeleccionada.Location = new System.Drawing.Point(751, 608);
            this.picCartaSeleccionada.Name = "picCartaSeleccionada";
            this.picCartaSeleccionada.Size = new System.Drawing.Size(120, 170);
            this.picCartaSeleccionada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCartaSeleccionada.TabIndex = 0;
            this.picCartaSeleccionada.TabStop = false;
            // 
            // lblCartaSeleccionada
            // 
            this.lblCartaSeleccionada.AutoSize = true;
            this.lblCartaSeleccionada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCartaSeleccionada.Location = new System.Drawing.Point(747, 570);
            this.lblCartaSeleccionada.Name = "lblCartaSeleccionada";
            this.lblCartaSeleccionada.Size = new System.Drawing.Size(258, 22);
            this.lblCartaSeleccionada.TabIndex = 3;
            this.lblCartaSeleccionada.Text = "Ninguna carta seleccionada";
            // 
            // btnGuardarPersonalizacion
            // 
            this.btnGuardarPersonalizacion.Location = new System.Drawing.Point(39, 618);
            this.btnGuardarPersonalizacion.Name = "btnGuardarPersonalizacion";
            this.btnGuardarPersonalizacion.Size = new System.Drawing.Size(140, 104);
            this.btnGuardarPersonalizacion.TabIndex = 4;
            this.btnGuardarPersonalizacion.Text = "Guardar Tabla";
            this.btnGuardarPersonalizacion.UseVisualStyleBackColor = true;
            this.btnGuardarPersonalizacion.Click += new System.EventHandler(this.btnGuardarPersonalizacion_Click);
            // 
            // FormPersonalizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1427, 822);
            this.Controls.Add(this.btnGuardarPersonalizacion);
            this.Controls.Add(this.lblCartaSeleccionada);
            this.Controls.Add(this.picCartaSeleccionada);
            this.Controls.Add(this.panelTablaPersonalizada);
            this.Controls.Add(this.flpCartas);
            this.Name = "FormPersonalizacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormPersonalizacion";
            this.Load += new System.EventHandler(this.FormPersonalizacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCartaSeleccionada)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpCartas;
        private System.Windows.Forms.Panel panelTablaPersonalizada;
        private System.Windows.Forms.PictureBox picCartaSeleccionada;
        private System.Windows.Forms.Label lblCartaSeleccionada;
        private System.Windows.Forms.Button btnGuardarPersonalizacion;
    }
}