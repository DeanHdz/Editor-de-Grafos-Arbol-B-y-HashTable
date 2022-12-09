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
    public partial class muestramatriz : Form
    {


        public List<List<int>> ListAdyacencia = new List<List<int>>();
        public List<int> auxiliar = new List<int>();
        public string name;
        public List<int> inDegree = new List<int>();
        int j = 65;
        public muestramatriz(Graph graph)
        {
            InitializeComponent();
            MuestraMatriz(graph);
            CreaGrid(graph);

        }

        //Matriz de Adyacencias 
        public void MuestraMatriz(Graph graph)
        {
            for (int j = 0; j < graph.Count; j++)
            {
                auxiliar = new List<int>();
                for (int i = 0; i < graph.Count; i++)
                {
                    auxiliar.Add(0);
                }
                ListAdyacencia.Add(auxiliar);
            }

            if (graph.Count > 0)
            {
                foreach (Edge Ar in graph.edgesList)
                {
                    if (!graph.EdgeIsDirected)  //si el grafo no es dirigido
                    {
                        if (ConvierteLetra(Ar.Source.Name) == ConvierteLetra(Ar.Destiny.Name))  // Verificamos si es una oreja
                        {
                            ListAdyacencia[ConvierteLetra(Ar.Source.Name)][ConvierteLetra(Ar.Destiny.Name)] = 1; //agregamos un 1 a la matriz de adyacencia 
                            ListAdyacencia[ConvierteLetra(Ar.Destiny.Name)][ConvierteLetra(Ar.Source.Name)] = 1;
                        }
                        else  //En caso de no cumplir lo anterior se trata de aristas que no estan conectadas al mismo nodo
                        {
                            ListAdyacencia[ConvierteLetra(Ar.Source.Name)][ConvierteLetra(Ar.Destiny.Name)]++;  //si no es oreja incrementamos en 1 el valor de la matriz
                            ListAdyacencia[ConvierteLetra(Ar.Destiny.Name)][ConvierteLetra(Ar.Source.Name)]++;
                        }

                    }
                    else  // para los grafos dirigidos
                    {
                        if (ConvierteLetra(Ar.Source.Name) == ConvierteLetra(Ar.Destiny.Name))
                        {
                            ListAdyacencia[ConvierteLetra(Ar.Destiny.Name)][ConvierteLetra(Ar.Source.Name)] = 1;  //Si es oreja solo agregamos un 1 a la matriz 

                        }
                        else
                        {
                            ListAdyacencia[ConvierteLetra(Ar.Source.Name)][ConvierteLetra(Ar.Destiny.Name)]++;  // caso contrario, aumentamos en 1 el dato que ya tiene la matriz
                        }

                    }
                }
            }
        }




        public void CreaGrid(Graph graph)
        {

            List<string> cadena = new List<string>();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Size = new Size(800, 400);

            //dataGridView1.Columns.Add(" ", " ");
            foreach (NodeP c in graph)
            {
                dataGridView1.Columns.Add(c.Name, c.Name);

            }


            for (int N = 0; N < graph.Count; N++)
            {
                dataGridView1.Rows.Add();

                cadena.Add(graph[N].Name);
                cadena = new List<string>();

                for (int M = 0; M < graph.Count; M++)
                {
                    cadena.Add(ListAdyacencia[N][M].ToString());
                    dataGridView1.Rows[N].Cells[M].Value = ListAdyacencia[N][M].ToString();
                    dataGridView1.Rows[N].HeaderCell.Value = graph[N].Name + "     ";
                    dataGridView1.RowHeadersWidth = 50;
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

    
   


  
