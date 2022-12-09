namespace EditordeGrafos
{
    partial class HashTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HashTable));
            this.label5 = new System.Windows.Forms.Label();
            this.DGV_Cubetas = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_rb = new System.Windows.Forms.TextBox();
            this.DGV_Direcciones = new System.Windows.Forms.DataGridView();
            this.Posición = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dirección = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B_Insertar = new System.Windows.Forms.Button();
            this.NUP_Clave = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_rpc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_mod = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_EOF = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TSB_CrearArchivo = new System.Windows.Forms.ToolStripButton();
            this.TSB_Abrir = new System.Windows.Forms.ToolStripButton();
            this.TSB_ImportarDispersion = new System.Windows.Forms.ToolStripButton();
            this.label6 = new System.Windows.Forms.Label();
            this.TB_cu = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TB_dis = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Cubetas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Direcciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_Clave)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(448, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "(Cubetas)";
            // 
            // DGV_Cubetas
            // 
            this.DGV_Cubetas.AllowUserToAddRows = false;
            this.DGV_Cubetas.AllowUserToDeleteRows = false;
            this.DGV_Cubetas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Cubetas.Location = new System.Drawing.Point(256, 176);
            this.DGV_Cubetas.Name = "DGV_Cubetas";
            this.DGV_Cubetas.ReadOnly = true;
            this.DGV_Cubetas.Size = new System.Drawing.Size(440, 268);
            this.DGV_Cubetas.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(497, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Registro (Bytes):";
            // 
            // TB_rb
            // 
            this.TB_rb.Location = new System.Drawing.Point(587, 89);
            this.TB_rb.Name = "TB_rb";
            this.TB_rb.ReadOnly = true;
            this.TB_rb.Size = new System.Drawing.Size(102, 20);
            this.TB_rb.TabIndex = 24;
            // 
            // DGV_Direcciones
            // 
            this.DGV_Direcciones.AllowUserToAddRows = false;
            this.DGV_Direcciones.AllowUserToDeleteRows = false;
            this.DGV_Direcciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_Direcciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Direcciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Posición,
            this.Dirección});
            this.DGV_Direcciones.Location = new System.Drawing.Point(12, 37);
            this.DGV_Direcciones.Name = "DGV_Direcciones";
            this.DGV_Direcciones.ReadOnly = true;
            this.DGV_Direcciones.Size = new System.Drawing.Size(237, 407);
            this.DGV_Direcciones.TabIndex = 23;
            // 
            // Posición
            // 
            this.Posición.HeaderText = "Posición";
            this.Posición.Name = "Posición";
            this.Posición.ReadOnly = true;
            // 
            // Dirección
            // 
            this.Dirección.HeaderText = "Dirección";
            this.Dirección.Name = "Dirección";
            this.Dirección.ReadOnly = true;
            // 
            // B_Insertar
            // 
            this.B_Insertar.Location = new System.Drawing.Point(355, 118);
            this.B_Insertar.Name = "B_Insertar";
            this.B_Insertar.Size = new System.Drawing.Size(75, 23);
            this.B_Insertar.TabIndex = 22;
            this.B_Insertar.Text = "Insertar";
            this.B_Insertar.UseVisualStyleBackColor = true;
            this.B_Insertar.Click += new System.EventHandler(this.B_Insertar_Click);
            // 
            // NUP_Clave
            // 
            this.NUP_Clave.Location = new System.Drawing.Point(436, 119);
            this.NUP_Clave.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NUP_Clave.Name = "NUP_Clave";
            this.NUP_Clave.Size = new System.Drawing.Size(131, 20);
            this.NUP_Clave.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Registro por cubeta:";
            // 
            // TB_rpc
            // 
            this.TB_rpc.Location = new System.Drawing.Point(393, 89);
            this.TB_rpc.Name = "TB_rpc";
            this.TB_rpc.ReadOnly = true;
            this.TB_rpc.Size = new System.Drawing.Size(100, 20);
            this.TB_rpc.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(506, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Mod:";
            // 
            // TB_mod
            // 
            this.TB_mod.Location = new System.Drawing.Point(543, 34);
            this.TB_mod.Name = "TB_mod";
            this.TB_mod.ReadOnly = true;
            this.TB_mod.Size = new System.Drawing.Size(100, 20);
            this.TB_mod.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(506, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "EOF:";
            // 
            // TB_EOF
            // 
            this.TB_EOF.Location = new System.Drawing.Point(543, 63);
            this.TB_EOF.Name = "TB_EOF";
            this.TB_EOF.ReadOnly = true;
            this.TB_EOF.Size = new System.Drawing.Size(100, 20);
            this.TB_EOF.TabIndex = 15;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSB_CrearArchivo,
            this.TSB_Abrir,
            this.TSB_ImportarDispersion});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(701, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TSB_CrearArchivo
            // 
            this.TSB_CrearArchivo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSB_CrearArchivo.Image = ((System.Drawing.Image)(resources.GetObject("TSB_CrearArchivo.Image")));
            this.TSB_CrearArchivo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_CrearArchivo.Name = "TSB_CrearArchivo";
            this.TSB_CrearArchivo.Size = new System.Drawing.Size(83, 22);
            this.TSB_CrearArchivo.Text = "Crear Archivo";
            this.TSB_CrearArchivo.Click += new System.EventHandler(this.TSB_CrearArchivo_Click);
            // 
            // TSB_Abrir
            // 
            this.TSB_Abrir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSB_Abrir.Image = ((System.Drawing.Image)(resources.GetObject("TSB_Abrir.Image")));
            this.TSB_Abrir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_Abrir.Name = "TSB_Abrir";
            this.TSB_Abrir.Size = new System.Drawing.Size(81, 22);
            this.TSB_Abrir.Text = "Abrir Archivo";
            this.TSB_Abrir.Click += new System.EventHandler(this.TSB_Abrir_Click);
            // 
            // TSB_ImportarDispersion
            // 
            this.TSB_ImportarDispersion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSB_ImportarDispersion.Image = ((System.Drawing.Image)(resources.GetObject("TSB_ImportarDispersion.Image")));
            this.TSB_ImportarDispersion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB_ImportarDispersion.Name = "TSB_ImportarDispersion";
            this.TSB_ImportarDispersion.Size = new System.Drawing.Size(187, 22);
            this.TSB_ImportarDispersion.Text = "Importar Archivo Dispersion (.txt)";
            this.TSB_ImportarDispersion.Click += new System.EventHandler(this.TSB_ImportarDispersion_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(267, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Tamaño Cubeta (Bytes): ";
            // 
            // TB_cu
            // 
            this.TB_cu.Location = new System.Drawing.Point(393, 60);
            this.TB_cu.Name = "TB_cu";
            this.TB_cu.ReadOnly = true;
            this.TB_cu.Size = new System.Drawing.Size(100, 20);
            this.TB_cu.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(255, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Tamaño Dispersión (Bytes):";
            // 
            // TB_dis
            // 
            this.TB_dis.Location = new System.Drawing.Point(393, 34);
            this.TB_dis.Name = "TB_dis";
            this.TB_dis.ReadOnly = true;
            this.TB_dis.Size = new System.Drawing.Size(100, 20);
            this.TB_dis.TabIndex = 28;
            // 
            // HashTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TB_cu);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TB_dis);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DGV_Cubetas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_rb);
            this.Controls.Add(this.DGV_Direcciones);
            this.Controls.Add(this.B_Insertar);
            this.Controls.Add(this.NUP_Clave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_rpc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_mod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_EOF);
            this.Controls.Add(this.toolStrip1);
            this.Name = "HashTable";
            this.Text = "HashTable";
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Cubetas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Direcciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUP_Clave)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView DGV_Cubetas;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox TB_rb;
        public System.Windows.Forms.DataGridView DGV_Direcciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn Posición;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dirección;
        private System.Windows.Forms.Button B_Insertar;
        public System.Windows.Forms.NumericUpDown NUP_Clave;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox TB_rpc;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox TB_mod;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox TB_EOF;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TSB_CrearArchivo;
        private System.Windows.Forms.ToolStripButton TSB_Abrir;
        private System.Windows.Forms.ToolStripButton TSB_ImportarDispersion;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox TB_cu;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox TB_dis;
    }
}