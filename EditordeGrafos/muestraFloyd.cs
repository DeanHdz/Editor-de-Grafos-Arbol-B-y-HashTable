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
    public partial class muestraFloyd : Form
    {
        readonly int INF = 1000000000;  //Utilizado para condicionar
        int NumVertices;                //Guarda el valor de graph.count
        int[,] MatrizInicial;           //Matriz bidimensional inicial
        int[,] MatrizFinal;             //Matriz bidimensional con pesos finales
        string[,] MatrizCaminos;        //Matriz con cadenas representando los caminos

        public muestraFloyd(Graph graph)
        {
            InitializeComponent();
            DefinirMatrizInicial(graph);
            Floyd();
        }

        public void DefinirMatrizInicial(Graph graph)
        {
            NumVertices = graph.Count();
            MatrizInicial = new int[NumVertices, NumVertices];          //Crear la matriz inicial
            MatrizFinal = new int[NumVertices, NumVertices];            //Crear la matriz final
            MatrizCaminos = new string[NumVertices, NumVertices];       //Crear la matriz de caminos

            for(int i = 0; i < NumVertices; i++)
            {
                MatrizInicial[i, i] = 0;                                //Dejar valor de 0 en el recorrido de un vertice hacia si mismo
                MatrizFinal[i, i] = 0;
            }

            for(int i=0; i < NumVertices; i++)                            //Dejar con valor INF (infinito) por default
            {
                for(int j=i+1; j < NumVertices; j++)
                {
                    MatrizInicial[i,j] = MatrizInicial[j,i] = INF;
                    MatrizFinal[i, j] = MatrizFinal[j, i] = INF;
                }  
            }

            for (int i=0; i<graph.EdgesList.Count(); i++)                //Ciclo for para rescatar pesos de aristas e insertarlos en la matriz inicial
            {
                if (graph.EdgeIsDirected)   //Manejo para grafos dirigidos
                {
                    int fuente = ConvierteLetra(graph.EdgesList[i].Source.Name);
                    int destino = ConvierteLetra(graph.EdgesList[i].Destiny.Name);
                    int peso = graph.EdgesList[i].Weight;

                    MatrizInicial[fuente, destino] = peso;
                    MatrizFinal[fuente, destino] = peso;
                }
                else                        //Para grafos no dirigidos
                {
                    int fuente = ConvierteLetra(graph.EdgesList[i].Source.Name);
                    int destino = ConvierteLetra(graph.EdgesList[i].Destiny.Name);
                    int peso = graph.EdgesList[i].Weight;

                    MatrizInicial[fuente, destino] = MatrizInicial[destino,fuente] = peso;
                    MatrizFinal[fuente, destino] = MatrizFinal[destino, fuente] = peso;
                }
                
            }
        }

        public void Floyd()
        {
            //Algoritmo de Floyd
            for (int k = 0; k < NumVertices; k++)
            {
                for (int i = 0; i < NumVertices; i++)
                {
                    for (int j = 0; j < NumVertices; j++)
                    {
                        if ( (MatrizFinal[i,k] < INF) && (MatrizFinal[k,j] < INF) && (i != j) && (i != k) && (k != j) && (MatrizFinal[i, k] + MatrizFinal[k, j] < MatrizFinal[i, j]) )
                        {
                            MatrizFinal[i, j] = MatrizFinal[i, k] + MatrizFinal[k, j]; //Guardar resultado en Matriz Final
                            char c = (char)(65+k); //En ASCII 65='A' , 66='B' , ...
                            MatrizCaminos[i, j] = MatrizCaminos[i,k] + MatrizCaminos[k,j] + c ; //Guardar orden de pivote
                        }
                    }
                }
            }

            //Pasar resultado a las tablas DataGridView1
            for (int i = 0; i < NumVertices; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), "*"+(char)(65 + i) + "*");
            }
            for (int i = 0; i < NumVertices; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = (char)(65 + i)+"";
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
                dataGridView2.Columns.Add(i.ToString(), "*" + (char)(65 + i) + "*");
            }
            for (int i = 0; i < NumVertices; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].HeaderCell.Value = (char)(65 + i) + "";
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
                dataGridView3.Columns.Add(i.ToString(), "*" + (char)(65 + i) + "*");
            }
            for (int i = 0; i < NumVertices; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].HeaderCell.Value = (char)(65 + i) + "";
                for (int k = 0; k < NumVertices; k++)
                {
                        dataGridView3.Rows[i].Cells[k].Value = MatrizCaminos[i, k];
                }
            }
        }

        public int ConvierteLetra(string letra)
        {
            int Num = 0;


            if (letra == "A")
            {
                Num = 0;
            }
            else if (letra == "B")
            {
                Num = 1;
            }
            else if (letra == "C")
            {
                Num = 2;
            }
            else if (letra == "D")
            {
                Num = 3;
            }
            else if (letra == "E")
            {
                Num = 4;
            }
            else if (letra == "F")
            {
                Num = 5;
            }
            else if (letra == "G")
            {
                Num = 6;
            }
            else if (letra == "H")
            {
                Num = 7;
            }
            else if (letra == "I")
            {
                Num = 8;
            }
            else if (letra == "J")
            {
                Num = 9;
            }
            else if (letra == "K")
            {
                Num = 10;
            }
            else if (letra == "L")
            {
                Num = 11;
            }
            else if (letra == "M")
            {
                Num = 12;
            }
            else if (letra == "N")
            {
                Num = 13;
            }
            else if (letra == "O")
            {
                Num = 14;
            }
            else if (letra == "P")
            {
                Num = 15;
            }
            else if (letra == "Q")
            {
                Num = 16;
            }
            else if (letra == "R")
            {
                Num = 17;
            }
            else if (letra == "S")
            {
                Num = 18;
            }
            else if (letra == "T")
            {
                Num = 19;
            }
            else if (letra == "U")
            {
                Num = 20;
            }
            else if (letra == "V")
            {
                Num = 21;
            }
            else if (letra == "W")
            {
                Num = 22;
            }
            else if (letra == "X")
            {
                Num = 23;
            }
            else if (letra == "Y")
            {
                Num = 24;
            }
            else if (letra == "Z")
            {
                Num = 25;
            }
            return Num;

        }
    }
}
