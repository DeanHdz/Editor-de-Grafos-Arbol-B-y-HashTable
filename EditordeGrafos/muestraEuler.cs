using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace EditordeGrafos
{
    public partial class muestraEuler : Form
    {
        Graph graph;
        List<Edge> ari = new List<Edge>();
        List<NodeP> nod = new List<NodeP>();

        public muestraEuler(Graph graph)
        {
            InitializeComponent();
            this.graph = graph;
            graph.UnselectAllEdges();
            if (graph.Count > 1)
            {
                if (graph.EdgeIsDirected) //Para grafo dirigido
                    EulerDirigido(graph);
                else
                    EulerNoDirigido(graph); //Para grafo no dirigido
            }
            else
                textBox1.Text = "Faltan por agregar nodos";
        }

        public void EulerNoDirigido(Graph graph) 
        {
            string output;
            graph.UnselectAllEdges();   //Marcar todas las aristas como no visitadas
            int res = 2;                //Determinar circuito, camino o ninguno
            int ini=0, fin=0;               //Guardar posicion de nodos impares primero y ultimo

            //Comprobar si tiene circuito
            for(int i=0; i<graph.Count();i++)
            {
                if (graph[i].Degree % 2 != 0)
                {
                    res--; //reduce res a 1 para verificar en lugar de circuito tiene camino
                    break; 
                }
            }
            if(res == 2) //Si es 2 tiene circuito, hacer calculo de circuito en textbox
            {
                label1.Text = "(No Dirigido)Es circuito: ";
                Circuito(ari, graph.First(), graph.First(), false, nod);
                output = Secuencia_String(nod);
                textBox1.Text = output;
            }
            else //sino, Comprobar si tiene camino
            {
                int conteo = 0;
                for(int i=0; i < graph.Count(); i++)
                {
                    if (graph[i].Degree % 2 != 0) //Vertice con grado impar (maximo debe haber 2)
                    {
                        conteo++;
                        if (conteo == 1)
                            ini = i;
                        else if (conteo == 2)
                            fin = i;
                        else
                            break;
                    }
                }

                if (conteo != 2)
                    res--;

                if (res == 1) //Si es 1 tiene camino
                {
                    label1.Text = "(No Dirigido)Es camino: ";
                    Camino(ari, graph[ini], nod);
                    output = Secuencia_String(nod);
                    textBox1.Text = output;
                }
                else //sino, es ninguno
                {
                    label1.Text = "No tiene circuito ni camino";
                }
            }
        }

        public void EulerDirigido(Graph graph)
        {
            string output;
            graph.UnselectAllEdges();   //Marcar todas las aristas como no visitadas
            int res = 2;                //Determinar circuito, camino o ninguno
            int ini=0, fin=0;               //Guardar posicion de nodos impares inicial y final

            //Comprobar si tiene circuito
            for (int i = 0; i < graph.Count(); i++)
            {
                if (graph[i].DegreeEx != graph[i].DegreeIn)
                {
                    res--; //reduce res a 1 para verificar en lugar de circuito tiene camino
                    break; 
                }
            }
            if (res == 2) //Si es 2 tiene circuito
            {
                label1.Text = "(Dirigido)Es circuito: ";
                Circuito(ari, graph.First(), graph.First(), false, nod);
                output = Secuencia_String(nod);
                textBox1.Text = output;
            }
            else //sino, Comprobar si tiene camino
            {
                int conteo = 0;
                for (int i = 0; i < graph.Count(); i++)
                {
                    if (graph[i].DegreeIn == graph[i].DegreeEx - 1) //Vertice con grado impar (maximo debe haber 2)
                    {
                        conteo++;
                        ini = i;
                    }
                    else if (graph[i].DegreeIn == graph[i].DegreeEx + 1) //El otro vertice impar
                    {
                        conteo++;
                        fin = i;
                    }
                }

                if (conteo != 2)
                    res--;

                if (res == 1) //Si es 1 tiene camino
                {
                    label1.Text = "(Dirigido)Es camino: ";
                    Camino(ari, graph[ini], nod);
                    output = Secuencia_String(nod);
                    textBox1.Text = output;
                }
                else //sino, es ninguno
                {
                    label1.Text = "No tiene circuito ni camino";
                }
            }
        }

        private void Camino(List<Edge> circuito, NodeP actual, List<NodeP> SN)
        {
            Edge arista = new Edge();
            SN.Add(actual); //Nodo de donde partir, se agregar a la secuencia de nodos recorridos
            foreach (NodeR r in actual.relations)
            {
                arista = graph.GetEdge(actual, r.Up);
                if (!arista.Visited)
                {
                    arista.Visited = true;
                    circuito.Add(arista);
                    Camino(circuito, r.Up, SN);
                    if (circuito.Count != graph.edgesList.Count)
                    {
                        circuito.Remove(arista);
                        SN.RemoveAt(SN.Count - 1);
                        arista.Visited = false;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private bool Circuito(List<Edge> circuito, NodeP inicial, NodeP actual, bool Ini_halla, List<NodeP> SN)
        {
            Edge arista = new Edge();
            SN.Add(actual); //Nodo de donde partir, se agregar a la secuencia de nodos recorridos
            foreach (NodeR r in actual.relations)
            {
                arista = graph.GetEdge(actual, r.Up);
                if (!arista.Visited)
                {
                    arista.Visited = true;
                    circuito.Add(arista);
                    Ini_halla = Circuito(circuito, inicial, r.Up, Ini_halla, SN);
                    if (circuito.Count != graph.edgesList.Count)
                    {
                        circuito.Remove(arista);
                        SN.RemoveAt(SN.Count - 1);
                        arista.Visited = false;
                        Ini_halla = false;
                    }
                    else
                    {
                        if (!Ini_halla)
                        {
                            circuito.Remove(arista);
                            SN.Remove(actual);
                            arista.Visited = false;
                        }
                        return Ini_halla;
                    }
                }
            }

            if (actual == inicial)
                Ini_halla = true;
            else
                SN.Remove(actual);

            return Ini_halla;
        }

        private string Secuencia_String(List<NodeP> Nodos)
        {
            string output = "";
            foreach (NodeP node in Nodos)
            {
                output += node.Name + " ";
            }
            return output;
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
