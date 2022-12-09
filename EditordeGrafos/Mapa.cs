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
    public partial class Mapa : Form
    {
        readonly int INF = 1000000000;  //Utilizado para condicionar
        readonly int NumVertices = 27;  //La cantidad de ciudades
        int[,] MatrizInicial;           //Matriz bidimensional inicial
        int[,] MatrizFinal;             //Matriz bidimensional con pesos finales
        string[,] MatrizCaminos;        //Matriz con cadenas representando los caminos
        public HashSet<string> destinos;//Set para guardar destinos a llegar


        public Mapa()
        {
            InitializeComponent();
            destinos = new HashSet<string>(); //Inicializar vacio el set de destinos seleccionados
            DefinirMatriz();
            Floyd();
        }

        private void DefinirMatriz()
        {
            //Definir las matrices inicial y final con los mismos valores iniciales

            //MatrizInicial = new int[NumVertices, NumVertices];          //Crear la matriz inicial
            //MatrizFinal = new int[NumVertices, NumVertices];            //Crear la matriz final
            MatrizCaminos = new string[NumVertices, NumVertices];       //Crear la matriz de caminos

            this.MatrizInicial = new int[,]
            {
                { 0, 0, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { 0, 0, 1135, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, 1135, 0, 590, 685, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, 590, 0, 231, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, 685, 231, 0, 790, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, 790, 0, 245, 400, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, 245, 0, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 645, 485, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, 400, INF, 0, INF, 740, 245, INF, INF, INF, INF, INF, 700, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, 0, 72, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, 740, 72, 0, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, 245, INF, INF, 0, 355, 270, 70, INF, 365, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 355, 0, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 270, INF, 0, 100, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 70, INF, 100, 0, 285, INF, INF, 600, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 285, 0, INF, INF, 570, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 365, INF, INF, INF, INF, 0, 200, 400, INF, 490, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, 700, INF, INF, INF, INF, INF, INF, INF, 200, 0, INF, 175, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 600, 570, 400, INF, 0, 240, INF, INF, INF, 240, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, 645, INF, INF, INF, INF, INF, INF, INF, INF, INF, 175, 240, 0, INF, 315, 560, 200, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, 485, INF, INF, INF, INF, INF, INF, INF, INF, 490, INF, INF, INF, 0, 275, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 315, 275, 0, 500, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 560, INF, 500, 0, 390, 300, 400, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 240, 200, INF, INF, 390, 0, 390, 700, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 300, 390, 0, INF, 520, 815},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 400, 700, INF, 0, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 520, INF, 0, 282},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 815, INF, 282, 0}
            };

            this.MatrizFinal = new int[,]
            {
                { 0, 0, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { 0, 0, 1135, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, 1135, 0, 590, 685, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, 590, 0, 231, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, 685, 231, 0, 790, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, 790, 0, 245, 400, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, 245, 0, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 645, 485, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, 400, INF, 0, INF, 740, 245, INF, INF, INF, INF, INF, 700, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, 0, 72, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, 740, 72, 0, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, 245, INF, INF, 0, 355, 270, 70, INF, 365, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 355, 0, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 270, INF, 0, 100, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 70, INF, 100, 0, 285, INF, INF, 600, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 285, 0, INF, INF, 570, INF, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 365, INF, INF, INF, INF, 0, 200, 400, INF, 490, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, 700, INF, INF, INF, INF, INF, INF, INF, 200, 0, INF, 175, INF, INF, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 600, 570, 400, INF, 0, 240, INF, INF, INF, 240, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, 645, INF, INF, INF, INF, INF, INF, INF, INF, INF, 175, 240, 0, INF, 315, 560, 200, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, 485, INF, INF, INF, INF, INF, INF, INF, INF, 490, INF, INF, INF, 0, 275, INF, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 315, 275, 0, 500, INF, INF, INF, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 560, INF, 500, 0, 390, 300, 400, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 240, 200, INF, INF, 390, 0, 390, 700, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 300, 390, 0, INF, 520, 815},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 400, 700, INF, 0, INF, INF},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 520, INF, 0, 282},
                { INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, INF, 815, INF, 282, 0}
            };

        }

        private void Floyd()
        {
            //Algoritmo de Floyd
            for (int k = 0; k < NumVertices; k++)
            {
                for (int i = 0; i < NumVertices; i++)
                {
                    for (int j = 0; j < NumVertices; j++)
                    {
                        if ((MatrizFinal[i, k] < INF) && (MatrizFinal[k, j] < INF) && (i != j) && (i != k) && (k != j) && (MatrizFinal[i, k] + MatrizFinal[k, j] < MatrizFinal[i, j]))
                        {
                            MatrizFinal[i, j] = MatrizFinal[i, k] + MatrizFinal[k, j]; //Guardar resultado en Matriz Final
                            string c = ConvertirEntero(k);
                            MatrizCaminos[i, j] = MatrizCaminos[i, k] + MatrizCaminos[k, j] + "-" + c; //Guardar orden de pivote
                        }
                    }
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

        private int ConvertirString(string ciudad)
        {
            if (ciudad == "Jose")
                return 0;
            else if (ciudad == "La Paz")
                return 1;
            else if (ciudad == "Tijuana")
                return 2;
            else if (ciudad == "Nogales")
                return 3;
            else if (ciudad == "Hermosillo")
                return 4;
            else if (ciudad == "Mazatlan")
                return 5;
            else if (ciudad == "Tepic")
                return 6;
            else if (ciudad == "Torreon")
                return 7;
            else if (ciudad == "El Porvenir")
                return 8;
            else if (ciudad == "Juarez")
                return 9;
            else if (ciudad == "Saltillo")
                return 10;
            else if (ciudad == "Piedras Negras")
                return 11;
            else if (ciudad == "Nuevo Laredo")
                return 12;
            else if (ciudad == "Mty")
                return 13;
            else if (ciudad == "Matamoros")
                return 14;
            else if (ciudad == "SLP")
                return 15;
            else if (ciudad == "Qro")
                return 16;
            else if (ciudad == "Tuxpan")
                return 17;
            else if (ciudad == "DF")
                return 18;
            else if (ciudad == "Lazaro Cardenas")
                return 19;
            else if (ciudad == "Acapulco")
                return 20;
            else if (ciudad == "Salina Cruz")
                return 21;
            else if (ciudad == "Veracruz")
                return 22;
            else if (ciudad == "Villahermosa")
                return 23;
            else if (ciudad == "Ciudad Hidalgo")
                return 24;
            else if (ciudad == "Merida")
                return 25;
            else if (ciudad == "Cancun")
                return 26;
            else
                return -1;//error
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Imprimir recorrido

            if (dGV.Rows.Count < 2)
                MessageBox.Show("Elige 2 o mas destinos");
            else if (dGV.SelectedRows.Count == 0)
                MessageBox.Show("Elige un punto de partida");
            else
            {
                int DistanciaAcumulado = 0;
                DataGridViewRow row = dGV.SelectedRows[0];
                String Actual = row.Cells[0].Value.ToString();
                richTextBox1.Text = "";
                HashSet<string> daux = destinos;
                bool[] visited = new bool[daux.Count];

                for (int i = 0; i < daux.Count - 1; i++)
                {

                    for (int j = 0; j < daux.Count; j++)
                    {
                        if (Actual == daux.ElementAt(j))
                            visited[j] = true;
                    }

                    richTextBox1.Text += " " + Actual + " ";
                    int MasCercano = INF;
                    string CiudadCercano = "";

                    for (int j = 0; j < daux.Count; j++)
                    {
                        if (MatrizFinal[ConvertirString(Actual), ConvertirString(daux.ElementAt(j))] < MasCercano && !visited[j])
                        {
                            MasCercano = MatrizFinal[ConvertirString(Actual), ConvertirString(daux.ElementAt(j))];
                            CiudadCercano = daux.ElementAt(j);
                        }
                    }

                    richTextBox1.Text += "(Intermedio)" + MatrizCaminos[ConvertirString(Actual), ConvertirString(CiudadCercano)] + "(Fin Int.)" + "(Distancia:" + MatrizFinal[ConvertirString(Actual), ConvertirString(CiudadCercano)] +")";
                    DistanciaAcumulado += MatrizFinal[ConvertirString(Actual), ConvertirString(CiudadCercano)];

                    Actual = CiudadCercano;
                }
                richTextBox1.Text += " " + Actual;

                richTextBox1.Text += " (DISTANCIA TOTAL: " + DistanciaAcumulado.ToString() +")";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Mostrar matrices

            MostrarMatriz form = new MostrarMatriz(MatrizInicial, MatrizFinal, MatrizCaminos);
            form.ShowDialog();
        }

        private void ReiniciarTabla()
        {
            dGV.Rows.Clear();
            for (int i = 0; i < destinos.Count(); i++)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(dGV);
                r.Cells[0].Value = destinos.ElementAt(i);
                dGV.Rows.Add(r);
            }
        }

        #region Checks
        private void Jose_CheckedChanged(object sender, EventArgs e)
        {
            if (Jose.Checked == true)
                destinos.Add("Jose");
            else
                destinos.Remove("Jose");
            ReiniciarTabla();
        }
        private void La_Paz_CheckedChanged(object sender, EventArgs e)
        {
            if (La_Paz.Checked == true)
                destinos.Add("La Paz");
            else
                destinos.Remove("La Paz");
            ReiniciarTabla();
        }
        private void Tijuana_CheckedChanged(object sender, EventArgs e)
        {
            if (Tijuana.Checked == true)
                destinos.Add("Tijuana");
            else
                destinos.Remove("Tijuana");
            ReiniciarTabla();
        }

        private void Nogales_CheckedChanged(object sender, EventArgs e)
        {
            if (Nogales.Checked == true)
                destinos.Add("Nogales");
            else
                destinos.Remove("Nogales");
            ReiniciarTabla();
        }

        private void Juarez_CheckedChanged(object sender, EventArgs e)
        {
            if (Juarez.Checked == true)
                destinos.Add("Juarez");
            else
                destinos.Remove("Juarez");
            ReiniciarTabla();
        }

        private void El_Porvenir_CheckedChanged(object sender, EventArgs e)
        {
            if (El_Porvenir.Checked == true)
                destinos.Add("El Porvenir");
            else
                destinos.Remove("El Porvenir");
            ReiniciarTabla();
        }

        private void Hermosillo_CheckedChanged(object sender, EventArgs e)
        {
            if (Hermosillo.Checked == true)
                destinos.Add("Hermosillo");
            else
                destinos.Remove("Hermosillo");
            ReiniciarTabla();
        }

        private void Piedras_Negras_CheckedChanged(object sender, EventArgs e)
        {
            if (Piedras_Negras.Checked == true)
                destinos.Add("Piedras Negras");
            else
                destinos.Remove("Piedras Negras");
            ReiniciarTabla();
        }

        private void Nuevo_Laredo_CheckedChanged(object sender, EventArgs e)
        {
            if (Nuevo_Laredo.Checked == true)
                destinos.Add("Nuevo Laredo");
            else
                destinos.Remove("Nuevo Laredo");
            ReiniciarTabla();
        }

        private void Matamoros_CheckedChanged(object sender, EventArgs e)
        {
            if (Matamoros.Checked == true)
                destinos.Add("Matamoros");
            else
                destinos.Remove("Matamoros");
            ReiniciarTabla();
        }

        private void Mty_CheckedChanged(object sender, EventArgs e)
        {
            if (Mty.Checked == true)
                destinos.Add("Mty");
            else
                destinos.Remove("Mty");
            ReiniciarTabla();
        }

        private void Torreon_CheckedChanged(object sender, EventArgs e)
        {
            if (Torreon.Checked == true)
                destinos.Add("Torreon");
            else
                destinos.Remove("Torreon");
            ReiniciarTabla();
        }

        private void Saltillo_CheckedChanged(object sender, EventArgs e)
        {
            if (Saltillo.Checked == true)
                destinos.Add("Saltillo");
            else
                destinos.Remove("Saltillo");
            ReiniciarTabla();
        }

        private void Mazatlan_CheckedChanged(object sender, EventArgs e)
        {
            if (Mazatlan.Checked == true)
                destinos.Add("Mazatlan");
            else
                destinos.Remove("Mazatlan");
            ReiniciarTabla();
        }

        private void SLP_CheckedChanged(object sender, EventArgs e)
        {
            if (SLP.Checked == true)
                destinos.Add("SLP");
            else
                destinos.Remove("SLP");
            ReiniciarTabla();
        }

        private void Tepic_CheckedChanged(object sender, EventArgs e)
        {
            if (Tepic.Checked == true)
                destinos.Add("Tepic");
            else
                destinos.Remove("Tepic");
            ReiniciarTabla();
        }

        private void Tuxpan_CheckedChanged(object sender, EventArgs e)
        {
            if (Tuxpan.Checked == true)
                destinos.Add("Tuxpan");
            else
                destinos.Remove("Tuxpan");
            ReiniciarTabla();
        }

        private void Qro_CheckedChanged(object sender, EventArgs e)
        {
            if (Qro.Checked == true)
                destinos.Add("Qro");
            else
                destinos.Remove("Qro");
            ReiniciarTabla();
        }

        private void DF_CheckedChanged(object sender, EventArgs e)
        {
            if (DF.Checked == true)
                destinos.Add("DF");
            else
                destinos.Remove("DF");
            ReiniciarTabla();
        }

        private void Veracruz_CheckedChanged(object sender, EventArgs e)
        {
            if (Veracruz.Checked == true)
                destinos.Add("Veracruz");
            else
                destinos.Remove("Veracruz");
            ReiniciarTabla();
        }

        private void Lazaro_Cardenas_CheckedChanged(object sender, EventArgs e)
        {
            if (Lazaro_Cardenas.Checked == true)
                destinos.Add("Lazaro Cardenas");
            else
                destinos.Remove("Lazaro Cardenas");
            ReiniciarTabla();
        }

        private void Acapulco_CheckedChanged(object sender, EventArgs e)
        {
            if (Acapulco.Checked == true)
                destinos.Add("Acapulco");
            else
                destinos.Remove("Acapulco");
            ReiniciarTabla();
        }

        private void Salina_Cruz_CheckedChanged(object sender, EventArgs e)
        {
            if (Salina_Cruz.Checked == true)
                destinos.Add("Salina Cruz");
            else
                destinos.Remove("Salina Cruz");
            ReiniciarTabla();
        }

        private void Ciudad_Hidalgo_CheckedChanged(object sender, EventArgs e)
        {
            if (Ciudad_Hidalgo.Checked == true)
                destinos.Add("Ciudad Hidalgo");
            else
                destinos.Remove("Ciudad Hidalgo");
            ReiniciarTabla();
        }

        private void Villahermosa_CheckedChanged(object sender, EventArgs e)
        {
            if (Villahermosa.Checked == true)
                destinos.Add("Villahermosa");
            else
                destinos.Remove("Villahermosa");
            ReiniciarTabla();
        }

        private void Merida_CheckedChanged(object sender, EventArgs e)
        {
            if (Merida.Checked == true)
                destinos.Add("Merida");
            else
                destinos.Remove("Merida");
            ReiniciarTabla();
        }

        private void Cancun_CheckedChanged(object sender, EventArgs e)
        {
            if (Cancun.Checked == true)
                destinos.Add("Cancun");
            else
                destinos.Remove("Cancun");
            ReiniciarTabla();
        }

        #endregion


    }
}
