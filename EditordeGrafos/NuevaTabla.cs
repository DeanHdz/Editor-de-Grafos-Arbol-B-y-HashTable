using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditordeGrafos
{
    public partial class NuevaTabla : Form
    {
        HashTable Padre;
        public NuevaTabla(HashTable Padre)
        {
            InitializeComponent();
            this.Padre = Padre;
        }

        private void B_Crear_Click(object sender, EventArgs e)
        {
            if (NUP_mod.Value > 1 && NUP_cap.Value > 0 && NUP_tam.Value > 3)
            {
                //Iniciar creacion de Archivo
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "dat files (*.dat)|*.dat";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (sfd.FileName.Contains(".dat"))
                    {
                        Padre.Archivo = sfd.FileName;
                        Padre.NumZocalos = (int)NUP_mod.Value;
                        Padre.RegistrosPorCubeta = (int)NUP_cap.Value;
                        Padre.TamRegistro = (int)NUP_tam.Value;
                        System.IO.FileStream fs = System.IO.File.Create(sfd.FileName);
                        Padre.TB_EOF.Text = fs.Length.ToString(); //EOF en textbox
                        Padre.TB_mod.Text = ((int)NUP_mod.Value).ToString(); //zocalos en textbox
                        Padre.TB_rpc.Text = ((int)NUP_cap.Value).ToString(); //registros por cubeta en textbox
                        Padre.TB_rb.Text = ((int)NUP_tam.Value).ToString(); //tamaño registro en textbox
                        //Mostrar tamaño de Dispersion y cubeta
                        Padre.TB_dis.Text = (20 + (Padre.NumZocalos * 4)).ToString();
                        Padre.TB_cu.Text = (12 + (Padre.TamRegistro * Padre.RegistrosPorCubeta)).ToString();

                        TablaDireccion td = new TablaDireccion(Padre.NumZocalos, Padre.RegistrosPorCubeta, Padre.TamRegistro, 0, 0);
                        fs.Close();
                        Padre.Escribir_TablaDirecciones(Padre.Archivo, td);
                        Padre.Grid_TablaDirecciones(td);
                        Padre.Grid_Cubetas(null);
                        this.Close(); //Cerrar form
                    }
                }


            }
            else
                MessageBox.Show("Utilizar un Mod mayor a 1, Capacidad mayor de 0 y tamaño de registro mayor a 3.");

        }
    }
}
