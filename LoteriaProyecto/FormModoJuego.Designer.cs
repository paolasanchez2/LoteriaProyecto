namespace LoteriaProyecto
{
    partial class FormModoJuego
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
            this.txtNombreModo = new System.Windows.Forms.TextBox();
            this.btnGuardarModo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNombreModo
            // 
            this.txtNombreModo.Location = new System.Drawing.Point(465, 77);
            this.txtNombreModo.Name = "txtNombreModo";
            this.txtNombreModo.Size = new System.Drawing.Size(183, 26);
            this.txtNombreModo.TabIndex = 0;
            // 
            // btnGuardarModo
            // 
            this.btnGuardarModo.Location = new System.Drawing.Point(465, 143);
            this.btnGuardarModo.Name = "btnGuardarModo";
            this.btnGuardarModo.Size = new System.Drawing.Size(195, 139);
            this.btnGuardarModo.TabIndex = 1;
            this.btnGuardarModo.Text = "Guardar";
            this.btnGuardarModo.UseVisualStyleBackColor = true;
            this.btnGuardarModo.Click += new System.EventHandler(this.btnGuardarModo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(461, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre de la forma de ganar:";
            // 
            // FormModoJuego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 571);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGuardarModo);
            this.Controls.Add(this.txtNombreModo);
            this.Name = "FormModoJuego";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormModoJuego";
            this.Load += new System.EventHandler(this.FormModoJuego_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombreModo;
        private System.Windows.Forms.Button btnGuardarModo;
        private System.Windows.Forms.Label label1;
    }
}