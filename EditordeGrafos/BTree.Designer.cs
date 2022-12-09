namespace EditordeGrafos
{
    partial class BTree
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_clave = new System.Windows.Forms.TextBox();
            this.btn_Insertar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_e = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_r = new System.Windows.Forms.TextBox();
            this.tb_p = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Ndat = new System.Windows.Forms.Button();
            this.btn_Cdat = new System.Windows.Forms.Button();
            this.btn_Ctxt = new System.Windows.Forms.Button();
            this.dGV = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_u = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_Raiz = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clave: ";
            // 
            // tb_clave
            // 
            this.tb_clave.Location = new System.Drawing.Point(75, 53);
            this.tb_clave.Name = "tb_clave";
            this.tb_clave.Size = new System.Drawing.Size(187, 20);
            this.tb_clave.TabIndex = 1;
            // 
            // btn_Insertar
            // 
            this.btn_Insertar.Location = new System.Drawing.Point(280, 51);
            this.btn_Insertar.Name = "btn_Insertar";
            this.btn_Insertar.Size = new System.Drawing.Size(75, 23);
            this.btn_Insertar.TabIndex = 2;
            this.btn_Insertar.Text = "Insertar";
            this.btn_Insertar.UseVisualStyleBackColor = true;
            this.btn_Insertar.Click += new System.EventHandler(this.btn_Insertar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(782, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "EOF: ";
            // 
            // tb_e
            // 
            this.tb_e.Location = new System.Drawing.Point(822, 49);
            this.tb_e.Name = "tb_e";
            this.tb_e.ReadOnly = true;
            this.tb_e.Size = new System.Drawing.Size(144, 20);
            this.tb_e.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(781, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tam. Registro: ";
            // 
            // tb_r
            // 
            this.tb_r.Location = new System.Drawing.Point(866, 10);
            this.tb_r.Name = "tb_r";
            this.tb_r.ReadOnly = true;
            this.tb_r.Size = new System.Drawing.Size(100, 20);
            this.tb_r.TabIndex = 6;
            // 
            // tb_p
            // 
            this.tb_p.Location = new System.Drawing.Point(1057, 10);
            this.tb_p.Name = "tb_p";
            this.tb_p.ReadOnly = true;
            this.tb_p.Size = new System.Drawing.Size(100, 20);
            this.tb_p.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(972, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tam. Página: ";
            // 
            // btn_Ndat
            // 
            this.btn_Ndat.Location = new System.Drawing.Point(26, 8);
            this.btn_Ndat.Name = "btn_Ndat";
            this.btn_Ndat.Size = new System.Drawing.Size(122, 23);
            this.btn_Ndat.TabIndex = 9;
            this.btn_Ndat.Text = "Nuevo arbol B+ (.dat)";
            this.btn_Ndat.UseVisualStyleBackColor = true;
            this.btn_Ndat.Click += new System.EventHandler(this.btn_Ndat_Click);
            // 
            // btn_Cdat
            // 
            this.btn_Cdat.Location = new System.Drawing.Point(173, 8);
            this.btn_Cdat.Name = "btn_Cdat";
            this.btn_Cdat.Size = new System.Drawing.Size(120, 23);
            this.btn_Cdat.TabIndex = 10;
            this.btn_Cdat.Text = "Cargar arbol B+ (.dat)";
            this.btn_Cdat.UseVisualStyleBackColor = true;
            this.btn_Cdat.Click += new System.EventHandler(this.btn_Cdat_Click);
            // 
            // btn_Ctxt
            // 
            this.btn_Ctxt.Location = new System.Drawing.Point(319, 8);
            this.btn_Ctxt.Name = "btn_Ctxt";
            this.btn_Ctxt.Size = new System.Drawing.Size(114, 23);
            this.btn_Ctxt.TabIndex = 11;
            this.btn_Ctxt.Text = "Cargar arbol B+ (.txt)";
            this.btn_Ctxt.UseVisualStyleBackColor = true;
            this.btn_Ctxt.Click += new System.EventHandler(this.btn_Ctxt_Click);
            // 
            // dGV
            // 
            this.dGV.AllowUserToAddRows = false;
            this.dGV.AllowUserToDeleteRows = false;
            this.dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            this.dGV.Location = new System.Drawing.Point(13, 92);
            this.dGV.Name = "dGV";
            this.dGV.Size = new System.Drawing.Size(1144, 503);
            this.dGV.TabIndex = 12;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "DIR";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "T";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "AP";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Clave";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "AP";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Clave";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "AP";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Clave";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "AP";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Clave";
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "AP";
            this.Column11.Name = "Column11";
            // 
            // tb_u
            // 
            this.tb_u.Location = new System.Drawing.Point(1057, 49);
            this.tb_u.Name = "tb_u";
            this.tb_u.ReadOnly = true;
            this.tb_u.Size = new System.Drawing.Size(100, 20);
            this.tb_u.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(973, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Ultimo Registro: ";
            // 
            // TB_Raiz
            // 
            this.TB_Raiz.Location = new System.Drawing.Point(675, 10);
            this.TB_Raiz.Name = "TB_Raiz";
            this.TB_Raiz.ReadOnly = true;
            this.TB_Raiz.Size = new System.Drawing.Size(100, 20);
            this.TB_Raiz.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(590, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Dirección Raíz: ";
            // 
            // BTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 607);
            this.Controls.Add(this.TB_Raiz);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_u);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dGV);
            this.Controls.Add(this.btn_Ctxt);
            this.Controls.Add(this.btn_Cdat);
            this.Controls.Add(this.btn_Ndat);
            this.Controls.Add(this.tb_p);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_r);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_e);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Insertar);
            this.Controls.Add(this.tb_clave);
            this.Controls.Add(this.label1);
            this.Name = "BTree";
            this.Text = "BTree";
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_clave;
        private System.Windows.Forms.Button btn_Insertar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_e;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_r;
        private System.Windows.Forms.TextBox tb_p;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Ndat;
        private System.Windows.Forms.Button btn_Cdat;
        private System.Windows.Forms.Button btn_Ctxt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.TextBox tb_u;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DataGridView dGV;
        private System.Windows.Forms.TextBox TB_Raiz;
        private System.Windows.Forms.Label label6;
    }
}