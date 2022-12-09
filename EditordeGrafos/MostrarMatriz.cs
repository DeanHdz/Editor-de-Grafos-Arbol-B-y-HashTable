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
    public partial class MostrarMatriz : Form
    {
        readonly int INF = 1000000000;  //Utilizado para condicionar
        readonly int NumVertices = 27;  //La cantidad de ciudades
        public MostrarMatriz(int[,] MatrizInicial, int[,] MatrizFinal, string[,] MatrizCaminos)
        {
            InitializeComponent();
            Imprime(MatrizInicial, MatrizFinal, MatrizCaminos);
        }

        private void Imprime(int[,] MatrizInicial, int[,] MatrizFinal, string[,] MatrizCaminos)
        {

            dataGridView1.RowHeadersWidth = 120;
            dataGridView2.RowHeadersWidth = 120;
            dataGridView3.RowHeadersWidth = 120;

            //Pasar resultado a las tablas DataGridView1
            for (int i = 0; i < NumVertices; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), ConvertirEntero(i));
            }
            for (int i = 0; i < NumVertices; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = ConvertirEntero(i);
                for (int k = 0; k < NumVertices; k++)
                {
                    if (MatrizInicial[i, k] >= INF)
                        dataGridView1.Rows[i].Cells[k].Value = "INF";
                    else
                        dataGridView1.Rows[i].Cells[k].Value = MatrizInicial[i, k];
                }
            }

            //Pasar resultado a las tablas DataGridView2
            for (int i = 0; i < NumVertices; i++)
            {
                dataGridView2.Columns.Add(i.ToString(), ConvertirEntero(i));
            }
            for (int i = 0; i < NumVertices; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].HeaderCell.Value = ConvertirEntero(i);
                for (int k = 0; k < NumVertices; k++)
                {
                    if (MatrizFinal[i, k] >= INF)
                        dataGridView2.Rows[i].Cells[k].Value = "INF";
                    else
                        dataGridView2.Rows[i].Cells[k].Value = MatrizFinal[i, k];
                }
            }

            //Pasar resultado a las tablas DataGridView3
            for (int i = 0; i < NumVertices; i++)
            {
                dataGridView3.Columns.Add(i.ToString(), ConvertirEntero(i));
            }
            for (int i = 0; i < NumVertices; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].HeaderCell.Value = ConvertirEntero(i);
                for (int k = 0; k < NumVertices; k++)
                {
                    dataGridView3.Rows[i].Cells[k].Value = MatrizCaminos[i, k];
                }
            }
        }

        private string ConvertirEntero(int ciudad)
        {
            if (ciudad == 0)
                return "Jose";
            else if (ciudad == 1)
                return "La Paz";
            else if (ciudad == 2)
                return "Tijuana";
            else if (ciudad == 3)
                return "Nogales";
            else if (ciudad == 4)
                return "Hermosillo";
            else if (ciudad == 5)
                return "Mazatlan";
            else if (ciudad == 6)
                return "Tepic";
            else if (ciudad == 7)
                return "Torreon";
            else if (ciudad == 8)
                return "El Porvenir";
            else if (ciudad == 9)
                return "Juarez";
            else if (ciudad == 10)
                return "Saltillo";
            else if (ciudad == 11)
                return "Piedras Negras";
            else if (ciudad == 12)
                return "Nuevo Laredo";
            else if (ciudad == 13)
                return "Mty";
            else if (ciudad == 14)
                return "Matamoros";
            else if (ciudad == 15)
                return "SLP";
            else if (ciudad == 16)
                return "Qro";
            else if (ciudad == 17)
                return "Tuxpan";
            else if (ciudad == 18)
                return "DF";
            else if (ciudad == 19)
                return "Lazaro Cardenas";
            else if (ciudad == 20)
                return "Acapulco";
            else if (ciudad == 21)
                return "Salina Cruz";
            else if (ciudad == 22)
                return "Veracruz";
            else if (ciudad == 23)
                return "Villahermosa";
            else if (ciudad == 24)
                return "Ciudad Hidalgo";
            else if (ciudad == 25)
                return "Merida";
            else if (ciudad == 26)
                return "Cancun";
            else
                return "ERROR";//error
        }
    }
}
