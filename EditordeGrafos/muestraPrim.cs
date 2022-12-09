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
    public partial class muestraPrim : Form
    {
        Graph graph;
        readonly int INF = 1000000000;  //Utilizado para condicionar

        public muestraPrim(Graph graph)
        {
            InitializeComponent();
            this.graph = graph;
            if(graph.Count > 0 && !graph.EdgeIsDirected)
                Prim(graph);
        }

        public void Prim(Graph graph)
        {
            graph.UnselectAllNodes(); //Marca todos los nodos como no visitados
            graph.UnselectAllEdges(); //Marca todos las aristas como no visitados

            String NodoINICIAL = graph.First().Name;

            if (ConvierteLetra(NodoINICIAL) < graph.Count())
            {
                if (graph.IsConnectedU() == true)
                {
                    int indice = ConvierteLetra(NodoINICIAL); //Rescatar indice de nodo inicial
                    int peso = 0; //Peso del arbol
                    List<NodeP> visitados = new List<NodeP>(); //Crear una lista de nodos visitados

                    textBox1.Text = "";            //Limpiar salida
                    graph[indice].Visited = true;  //Marca el vertice inicial como visitado
                    visitados.Add(graph[indice]);

                    while(graph.AllNodesVisited() == false) //Mientras no se hayan visitado todos los nodos sigue la operacion
                    {

                        int menor = INF; //Auxiliar para comparar arista de menor peso
                        int in_origen = -1;   //Rescata el indice del nodo origen de la arista a usar
                        int in_destino = -1;  //Rescata el indice del nodo destino de la arista a usar
                        foreach(NodeP n in visitados)
                        {
                            //MessageBox.Show(n.relations.Count().ToString() + " conectados a: " + n.Name.ToString());
                            foreach(NodeR r in n.relations)
                            {
                                //MessageBox.Show(r.Up.Name.ToString());
                                foreach (Edge e in graph.edgesList)
                                {
                                    if( (string.Compare(e.Source.Name,n.Name) == 0 && string.Compare(e.Destiny.Name,r.Up.Name) == 0) || (string.Compare(e.Source.Name, r.Up.Name) == 0 && string.Compare(e.Destiny.Name, n.Name) == 0) )
                                    {
                                        if(e.Weight < menor && r.Up.Visited == false )
                                        {
                                            menor = e.Weight; //Nueva arista con peso menor
                                            in_origen = ConvierteLetra(n.Name); //Rescata el indice del nodo actual
                                            in_destino = ConvierteLetra(r.Up.Name); //Rescata el indice del nodo a visitar
                                        }
                                    }
                                }
                            }
                        }

                        visitados.Add(graph[in_destino]); //Agregar a la lista de nodos ya visitados
                        graph[in_destino].Visited = true; 
                        peso += menor; //Modificar peso total del arbol
                        textBox1.Text += "(" + graph[in_origen].Name.ToString() + "," + graph[in_destino].Name.ToString() + ") = " + menor.ToString() + Environment.NewLine;
                    }
                    textBox1.Text += Environment.NewLine + "El peso es: " + peso.ToString();
                }
                else
                    textBox1.Text = "El grafo no es conexo (Hay minimo un vertice con grado 0)";
            }
            else
                textBox1.Text = "Inserta un vertice valido";

        }

        public int ConvierteLetra(string letra) //Realiza conversion de numero a letra, al tener un conteo mayor a 25 te deja por default la letra 'A' (Dean)
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
