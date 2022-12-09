using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditordeGrafos
{
    public partial class muestraDFS : Form
    {
        //De muestraGrados se rescata la forma de determinar adyacencia y el conversion de nombre y numero del vertice. (Dean)
        public List<List<int>> ListAdyacencia = new List<List<int>>();
        public List<int> auxiliar = new List<int>();
        
        public HashSet<string> pares;


        public List<NodeP> OrdenFinal = new List<NodeP>();
        public int BAJO;

        public readonly int NIL = -1;
        public static int time;

        public List<NodeP> Componente;
        public List<NodeP> ComponenteAux;
        public Dictionary<string, int> Bajos;
        public Dictionary<string, int> Bps;

        Graph graph;


        public List<List<int>> adList = new List<List<int>>();
        public HashSet<string> pairs = new HashSet<string>();
        public HashSet<int> conexComponentsHash = new HashSet<int>();

        public muestraDFS(Graph graph)
        {
            InitializeComponent();
            this.graph = graph;
            //Determinar Aristas de Retroceso
            Retroceso(graph);
            //Determinar Elementos Conexos
            ComponentesConexos();
            //SCC();
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

        //Listo
        #region DFS

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
                DFS(graph);
        }

        // Funcion para hacer recorriemiento DFS, utiliza DFSUtil()
        public void DFS(Graph graph)
        {
            ListAdyacencia = new List<List<int>>();
            //Igual que en muestraGrados utiliza el mismo procedimiento para determinar adyacencia
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

                textBox1.Text = "";

                // Inicia marcando como visita a cada vertice a 0
                bool[] visited = new bool[graph.Count()];

                for (int j = 0; j < graph.Count(); j++)
                    visited[j] = false;

                if (ConvierteLetra(textBox2.Text[0].ToString()) < graph.Count())
                {
                    DFSUtil(graph, ConvierteLetra(textBox2.Text[0].ToString()), visited);
                    // LLama la funcion recursiva para iterar por cada vertice uno por uno
                    for (int k = 0; k < graph.Count(); k++)
                        if (visited[k] == false)
                        {
                        DFSUtil(graph, k, visited);
                        }
                }
                else
                    textBox1.Text = "Inserta un vertice valido";

        }

        void DFSUtil(Graph graph, int v, bool[] visited)
        {
            //El nodo actual se marca como ya visitado para no volver a repetir su escritura
            visited[v] = true;
            textBox1.Text += (char)(v+65) + " "; //Cambio de num vertice a letra

            // Se aplica recursividad para todo vertice adyacente
            for(int i=0; i<graph.Count(); i++)
            {
                if (!visited[i] && ListAdyacencia[v][i] > 0)
                {
                    DFSUtil(graph, i, visited);
                }
            }
        }
        #endregion

        //Listo
        #region ARISTAS DE RETROCESO
        //Iniciar DFS Utilizando el primer nodo
        public void Retroceso(Graph graph)
        {
            if (graph.Count > 0)
            {
                pares = new HashSet<string>(); //Reiniciar los pares
                ListAdyacencia = new List<List<int>>();
                //Igual que en muestraGrados utiliza el mismo procedimiento para determinar adyacencia
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

                // Inicia marcando como visita a cada vertice a 0
                bool[] visited = new bool[graph.Count()];
                string[] origen = new string[graph.Count()]; //De que nodo se origina la partida/visita de cada nodo

                for (int j = 0; j < graph.Count(); j++)
                    visited[j] = false;

                //Rescatar el primer nodo de la lista de nodos
                String NodoINICIAL = graph.First().Name;

                if (ConvierteLetra(NodoINICIAL) < graph.Count())
                {
                    origen[ConvierteLetra(NodoINICIAL)] = "Ninguno";
                    R_Util(graph, ConvierteLetra(NodoINICIAL), visited, origen);
                    // LLama la funcion recursiva para iterar por cada vertice uno por uno
                    for (int k = 0; k < graph.Count(); k++)
                        if (visited[k] == false)
                        {
                            origen[k] = "Ninguno";
                            R_Util(graph, k, visited, origen);
                        }
                    textBox3.Text += Environment.NewLine + Environment.NewLine + "Aristas de Retroceso: ";
                    for (int m = 0; m < pares.Count; m++)
                        textBox3.Text += Environment.NewLine + pares.ElementAt(m);
                }
                else
                    textBox3.Text = "Inserta un vertice valido";
            }
            else
                textBox3.Text = "No hay nodos";
        }

        void R_Util(Graph graph, int v, bool[] visited, string[] origen)
        {
            //El nodo actual se marca como ya visitado para no volver a repetir su escritura
            visited[v] = true;
            textBox3.Text += (char)(v + 65) + " "; //Cambio de num vertice a letra

            // Se aplica recursividad para todo vertice adyacente
            for (int i = 0; i < graph.Count(); i++)
            {
                if (!visited[i] && ListAdyacencia[v][i] > 0)
                {
                    origen[i] = ((char)(v + 65)) + "";
                    R_Util(graph, i, visited, origen);
                }
                else if (visited[i] && ListAdyacencia[v][i] > 0 && (((char)(i + 65)) + "") != origen[v])
                {
                    String conv;
                    //Insertar par de vertices para indicar retroceso
                    if ( ((char)(v + 65)) > ((char)(i + 65)) )
                        conv = ((char)(v + 65)) + "-" + ((char)(i + 65));
                    else
                        conv = ((char)(i + 65)) + "-" + ((char)(v + 65));
                    pares.Add(conv);
                }
            }
        }
        #endregion


        #region ELEMENTOS CONEXOS

        //Utilidad DFS elementos conexos
        public void EC_Util(int v, string[] origen, HashSet<string> retroceso)
        {
            OrdenFinal.Add(graph[v]);
            //Bps[graph[v].Name] = OrdenFinal.Count;
            graph[v].Visited = true;
            textBox4.Text += (char)(v + 65) + " "; //Cambio de num vertice a letra

            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].Visited == false && ListAdyacencia[v][i] > 0)
                {
                    origen[i] = graph[v].Name;
                    EC_Util(i,origen,retroceso);
                }
                else if (graph[i].Visited == true && ListAdyacencia[v][i] > 0 && graph[i].Name != origen[v])
                {
                    String conv;
                    //Insertar par de vertices para indicar retroceso
                    if (((char)(v + 65)) > ((char)(i + 65)))
                        conv = ((char)(v + 65)) + "-" + ((char)(i + 65));
                    else
                        conv = ((char)(i + 65)) + "-" + ((char)(v + 65));
                    retroceso.Add(conv);
                }
            }
        }

        public void ComponentesConexos()
        {
            int mayor = 0;
            for (int i = 1; i < graph.Count(); i++)//Sacar el nodo de mayor grado
            {
                if (graph[i].Degree > graph[mayor].Degree)
                    mayor = i;
            }

            graph.UnselectAllNodes();//Hacer DFS y enumerar orden de los nodos recorridos
            OrdenFinal.Clear();

            string[] origen = new string[graph.Count()]; //guarda el origen de la visita de cada nodo
            HashSet<string> retroceso = new HashSet<string>();

            EC_Util(mayor, origen, retroceso);
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].Visited == false)
                {
                    EC_Util(i, origen, retroceso);
                }
            }
            textBox4.Text += Environment.NewLine + Environment.NewLine + "Componentes conexos: " + Environment.NewLine;

            Conexos(); //llama a la funcion que imprime los componentes conexos

            HashSet<NodeP> Articulacion = new HashSet<NodeP>();

            graph.UnselectAllNodes();
            foreach (NodeP p in graph)
            {
                if(p.Degree > 1)
                {
                    graph.UnselectAllNodes();
                    p.Visited = true;
                    grafoConexo(p.relations[0].Up);
                    if (!graph.AllNodesVisited())
                    {
                        Articulacion.Add(p);
                    }
                }
                
            }
            textBox4.Text += Environment.NewLine + "Puntos de articulación:" + Environment.NewLine;
            foreach (NodeP p in Articulacion)
            {

                textBox4.Text += "(" + p.Name + ") ";
            }
        }


        private void Conexos()
        {
            foreach (NodeP p in OrdenFinal)
            {
                for (int i = 0; i < OrdenFinal.Count; i++)
                {
                    if (p == OrdenFinal[i])
                    {
                        p.bajo = i;
                        p.bp = i;
                    }
                }
            }
            foreach (NodeP p in OrdenFinal)
            {
                p.Visited = false;
            }

            BajosFun(OrdenFinal[0]);

            foreach (int i in conexComponentsHash)
            {
                textBox4.Text += Environment.NewLine;
                foreach (NodeP p in OrdenFinal)
                {
                    if (p.bajo == i)
                    {
                        textBox4.Text += p.Name + " ";
                    }
                }
            }
            textBox4.Text += Environment.NewLine;
        }

        private void BajosFun(NodeP p)
        {
            p.Visited = true;
            foreach (NodeR r in p.relations)
            {
                foreach (Edge ed in graph.EdgesList)
                {
                    if ((ed.Source == p && ed.Destiny == r.Up) || ed.Source == r.Up && ed.Destiny == p)
                    {
                        if (r.Up.Visited && !ed.Visited)
                        {
                            ed.Visited = true;
                            if (p.bajo > r.Up.bp)
                            {
                                p.bajo = r.Up.bp;
                            }
                        }
                        else
                        {
                            if (!ed.Visited)
                            {
                                ed.Visited = true;
                                BajosFun(r.Up);
                                if (p.bajo > r.Up.bajo)
                                {
                                    p.bajo = r.Up.bajo;
                                }
                            }
                        }
                    }
                }
            }
            conexComponentsHash.Add(p.bajo);
        }

        private bool ExisteRetroceso(HashSet<string> retroceso, NodeP a, NodeP b)
        {
            if (retroceso.Contains(a.Name + "-" + b.Name) || retroceso.Contains(b.Name + "-" + a.Name))
                return true;
            else
                return false;
        }
        
        /*
        private List<NodeP> BuscarNodo(NodeP actual, NodeP destino, List<NodeP> corrido)
        {
            corrido.Add(destino);
            List<List<NodeP>> resultados = new List<List<NodeP>>(actual.relations.Count);

            for(int i=0; i < actual.relations.Count)
            {
                if(actual.relations.Up.Name)
            }

            for(int i=0; i<actual.relations.Count; i++)
            {
                BuscarNodo(actual.relations.Up, destino,);
            }
        }
        */

        private void grafoConexo(NodeP p)
        {
            if (!p.Visited)
            {
                p.Visited = true;
                foreach (NodeR r in p.relations)
                {
                    grafoConexo(r.Up);
                }
            }
        }

        private void grafoConexo2(NodeP p)
        {
            if (!p.Visited)
            {
                p.relations.OrderBy(NodeR => NodeR.Up.Name);
                p.Visited = true;
                textBox4.Text += p.Name + " ";
                //textBox1.Text += p.Name;
                foreach (NodeR r in p.relations)
                {
                    grafoConexo2(r.Up);
                }
            }
        }

        private void subConexo(NodeP p, HashSet<NodeP> nodosArticulados)
        {
            graph.UnselectAllNodes();
            foreach (NodeP i in nodosArticulados)
            {
                p.Visited = true;
                if (i != p)
                {
                    grafoConexo(i);
                }
            }
            p.Visited = false;
            grafoConexo2(p);

            textBox4.Text += p.Name + " " + Environment.NewLine;

        }

        private string subConexo2(NodeP inicio, HashSet<NodeP> descartados)
        {
            string aux = "";
            foreach (NodeP p in descartados)
            {
                p.Visited = true;
            }
            foreach (NodeP p in graph)
            {
                if (!p.Visited)
                {
                    p.Visited = true;
                    foreach (NodeR r in p.relations)
                    {
                        if (r.Up == inicio)
                        {
                            aux = p.Name + r.Up.Name;
                            p.Visited = true;
                            return aux;
                            //descartados.Add(inicio);
                        }
                        else
                        {
                            aux = subConexo2(inicio, descartados);
                        }
                        if (aux != "")
                        {
                            p.Visited = true;
                            aux = p.Name + aux;
                        }
                    }
                }
            }
            return aux;

        }

        #endregion

    }

}
