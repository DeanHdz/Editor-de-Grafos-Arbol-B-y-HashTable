namespace EditordeGrafos
{
    partial class NuevaTabla
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
            this.B_Crear = new System.Windows.Forms.Button();
            this.NUP_tam = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.NUP_cap = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NUP_mod = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_tam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_cap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_mod)).BeginInit();
            this.SuspendLayout();
            // 
            // B_Crear
            // 
            this.B_Crear.Location = new System.Drawing.Point(47, 183);
            this.B_Crear.Name = "B_Crear";
            this.B_Crear.Size = new System.Drawing.Size(75, 23);
            this.B_Crear.TabIndex = 13;
            this.B_Crear.Text = "Crear";
            this.B_Crear.UseVisualStyleBackColor = true;
            this.B_Crear.Click += new System.EventHandler(this.B_Crear_Click);
            // 
            // NUP_tam
            // 
            this.NUP_tam.Location = new System.Drawing.Point(26, 137);
            this.NUP_tam.Name = "NUP_tam";
            this.NUP_tam.Size = new System.Drawing.Size(131, 20);
            this.NUP_tam.TabIndex = 12;
            this.NUP_tam.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Tamaño de registros (Bytes):";
            // 
            // NUP_cap
            // 
            this.NUP_cap.Location = new System.Drawing.Point(26, 85);
            this.NUP_cap.Name = "NUP_cap";
            this.NUP_cap.Size = new System.Drawing.Size(131, 20);
            this.NUP_cap.TabIndex = 10;
            this.NUP_cap.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Capacidad de cubeta: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Mod (Numero de zócalos):";
            // 
            // NUP_mod
            // 
            this.NUP_mod.Location = new System.Drawing.Point(26, 37);
            this.NUP_mod.Name = "NUP_mod";
            this.NUP_mod.Size = new System.Drawing.Size(131, 20);
            this.NUP_mod.TabIndex = 7;
            this.NUP_mod.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // NuevaTabla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 232);
            this.Controls.Add(this.B_Crear);
            this.Controls.Add(this.NUP_tam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NUP_cap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NUP_mod);
            this.Name = "NuevaTabla";
            this.Text = "NuevaTabla";
            ((System.ComponentModel.ISupportInitialize)(this.NUP_tam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_cap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_mod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_Crear;
        private System.Windows.Forms.NumericUpDown NUP_tam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NUP_cap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NUP_mod;
    }
}