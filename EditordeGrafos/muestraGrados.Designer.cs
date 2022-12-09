namespace EditordeGrafos
{
    partial class muestraGrados
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.muestraGradosIn = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.muestraGradosSalida = new System.Windows.Forms.TextBox();
            this.gradoTotalG = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.MuestraVerticesEspeciales = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(111, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Grados de entrada: ";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // muestraGradosIn
            // 
            this.muestraGradosIn.Location = new System.Drawing.Point(12, 38);
            this.muestraGradosIn.Multiline = true;
            this.muestraGradosIn.Name = "muestraGradosIn";
            this.muestraGradosIn.Size = new System.Drawing.Size(111, 160);
            this.muestraGradosIn.TabIndex = 6;
            this.muestraGradosIn.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(152, 12);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(106, 20);
            this.textBox3.TabIndex = 7;
            this.textBox3.Text = "Grados de salida: ";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // muestraGradosSalida
            // 
            this.muestraGradosSalida.Location = new System.Drawing.Point(152, 38);
            this.muestraGradosSalida.Multiline = true;
            this.muestraGradosSalida.Name = "muestraGradosSalida";
            this.muestraGradosSalida.Size = new System.Drawing.Size(106, 160);
            this.muestraGradosSalida.TabIndex = 8;
            this.muestraGradosSalida.TextChanged += new System.EventHandler(this.muestraGradosSalida_TextChanged);
            // 
            // gradoTotalG
            // 
            this.gradoTotalG.Location = new System.Drawing.Point(461, 12);
            this.gradoTotalG.Name = "gradoTotalG";
            this.gradoTotalG.Size = new System.Drawing.Size(133, 20);
            this.gradoTotalG.TabIndex = 9;
            this.gradoTotalG.Text = "Grado Total del grafo: ";
            this.gradoTotalG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gradoTotalG.TextChanged += new System.EventHandler(this.gradoTotalG_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(286, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(144, 20);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "Vertices Especiales:";
            // 
            // MuestraVerticesEspeciales
            // 
            this.MuestraVerticesEspeciales.Location = new System.Drawing.Point(286, 38);
            this.MuestraVerticesEspeciales.Multiline = true;
            this.MuestraVerticesEspeciales.Name = "MuestraVerticesEspeciales";
            this.MuestraVerticesEspeciales.Size = new System.Drawing.Size(144, 160);
            this.MuestraVerticesEspeciales.TabIndex = 11;
            // 
            // muestraGrados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MuestraVerticesEspeciales);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.gradoTotalG);
            this.Controls.Add(this.muestraGradosSalida);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.muestraGradosIn);
            this.Controls.Add(this.textBox1);
            this.Name = "muestraGrados";
            this.Text = "muestraGrados";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox muestraGradosIn;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox muestraGradosSalida;
        private System.Windows.Forms.TextBox gradoTotalG;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox MuestraVerticesEspeciales;
    }
}