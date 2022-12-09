using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditordeGrafos {
    [Serializable()]

    public class Graph : List<NodeP> {
        private bool edgeNamesVisible;
        private bool edgeWeightVisible;
        private bool edgeIsDirected;
        private int nodeRadio;
        private int nodeBorderWidth;
        private int edgeWidth;
        private Color edgeColor;
        private Color nodeColor;
        private Color nodeBorderColor;
        private bool letter;
        private string name;
        public List<Edge> edgesList;


        public List<Edge> EdgesList
        {
            get { return edgesList; }
            set { edgesList = value; }
        }

        public bool Letter {
            get { return letter; }
            set { letter = value; }
        }

        public bool EdgeIsDirected {
            get { return edgeIsDirected; }
            set { edgeIsDirected = value; }
        }

        public int EdgeWidth {
            get { return edgeWidth; }
            set { edgeWidth = value; }
        }

        public Color NodeBorderColor {
            get { return nodeBorderColor; }
            set { nodeBorderColor = value; }
        }

        public int NodeBorderWidth {
            get { return nodeBorderWidth; }
            set { nodeBorderWidth = value; }
        }

        public Color EdgeColor {
            get { return edgeColor; }
            set { edgeColor = value; }
        }

        public Color NodeColor {
            get { return nodeColor; }
            set { nodeColor = value; }
        }

        public int NodeRadio {
            get { return nodeRadio; }
            set { nodeRadio = value; }
        }

        public bool EdgeNamesVisible {
            get { return edgeNamesVisible; }
            set { edgeNamesVisible = value; }
        }

        public bool EdgeWeightVisible {
            get { return edgeWeightVisible; }
            set { edgeWeightVisible = value; }
        }
        

        public Graph() {
            EdgesList = new List<Edge>();
            edgeColor = Color.Black;
            letter = true;
            edgeNamesVisible = false;
            edgeWeightVisible = false;
            nodeColor = Color.White;
            nodeRadio = 30;
            nodeBorderWidth = 1;
            edgeWidth = 1;
            nodeBorderColor = Color.Black;
        }

        public Graph(Graph gr) {

            EdgesList = new List<Edge>();
            edgeColor = gr.EdgeColor;
            nodeColor = gr.nodeColor;
            nodeRadio = gr.NodeRadio;
            Edge k = new Edge();
            NodeP aux1, aux2;

            nodeBorderWidth = 1;
            edgeWidth = 1;
            nodeBorderColor = Color.Black;
            edgeNamesVisible = gr.EdgeNamesVisible;
            edgeWeightVisible = gr.EdgeWeightVisible;
            letter = gr.Letter;

            foreach (NodeP n in gr) {
                this.Add(new NodeP(n));
            }

            foreach (NodeP n in gr) {
                aux1 = Find(delegate (NodeP bu) { if (bu.Name == n.Name) return true; else return false; });
                foreach (NodeR rel in n.relations) {
                    aux2 = Find(delegate (NodeP je) { if (je.Name == rel.Up.Name) return true; else return false; });
                    aux1.InsertRelation(aux2, EdgesList.Count, false);
                }
            }
            //Agregar Aristas 
            foreach (Edge ar in gr.EdgesList) {
                aux1 = Find(delegate (NodeP bu) { if (bu.Name == ar.Source.Name) return true; else return false; });
                aux2 = Find(delegate (NodeP bu) { if (bu.Name == ar.Destiny.Name) return true; else return false; });
                k = new Edge(aux1, aux2, ar.Name) {
                    Weight = ar.Weight
                };
                //Manda llamar la funcion para añadir la arista
                AddEdge(k);
            }
        }

        public void AddNode(NodeP n) {
            Add(n);
        }

        public void AddEdge(Edge A) {
            EdgesList.Add(A);
        }

        public void RemoveEdge(Edge ar) {
            NodeR rel;
            rel = ar.Source.relations.Find(delegate (NodeR np) { if (np.Up.Name == ar.Destiny.Name) return true; else return false; });

            if (rel != null) {
                ar.Source.relations.Remove(rel);
                ar.Source.Degree--;
                ar.Destiny.Degree--;
                ar.Source.DegreeEx--;
                ar.Destiny.DegreeIn--;
            }
            if (!edgeIsDirected) {
                rel = ar.Destiny.relations.Find(delegate (NodeR np) { if (np.Up.Name == ar.Source.Name) return true; else return false; });

                if (rel != null) {
                    ar.Destiny.relations.Remove(rel);
                    ar.Destiny.DegreeEx--;
                    ar.Source.DegreeIn--;
                }
            }
            EdgesList.Remove(ar);
        }

        public void RemoveNode(NodeP rem) {
            NodeR rel;
            List<Edge> remove;
            remove = new List<Edge>();

            foreach (NodeP a in this) {
                rel = a.relations.Find(delegate (NodeR np) { if (np.Up.Name == rem.Name) return true; else return false; });
                if (rel != null) {
                    a.relations.Remove(rel);
                    a.Degree--;
                    a.DegreeEx--;
                    if (!edgeIsDirected || edgeIsDirected) {
                        a.DegreeIn--;
                    }
                }
            }
            remove = EdgesList.FindAll(delegate (Edge ar) { if (ar.Source.Name == rem.Name || ar.Destiny.Name == rem.Name) return true; else return false; });
            if (remove != null)
                foreach (Edge re in remove) {
                    EdgesList.Remove(re);
                }
            this.Remove(rem);
        }

        // Regresa si dos nodos está conectados
        public NodeR Connected(NodeP a, NodeP b) {
            for (int i = 0; i < a.relations.Count; i++) {
                if (a.relations[i].Up == b) {
                    return a.relations[i];
                }
            }
            return null;
        }

        // Regresa la arista entre dos nodos que si se sabe que tiene aristas
        public Edge GetEdge(NodeP a, NodeP b) {
            for (int i = 0; i < this.EdgesList.Count; i++) {
                if (this.EdgesList[i].Source.Name == a.Name && this.EdgesList[i].Destiny.Name == b.Name ||
                        this.EdgesList[i].Source.Name == b.Name && this.EdgesList[i].Destiny.Name == a.Name) {
                    return (EdgesList[i]);
                }
            }
            return (null);
        }

        // Deselecciona todos los nodos
        public void UnselectAllNodes() {
            for (int k = 0; k < Count; k++) {
                this[k].Visited = false;
            }
        }

        // Deselecciona todas las aristas
        public void UnselectAllEdges() {
            foreach (Edge ed in EdgesList) {
                ed.Visited = false;
            }
        }

        // Verifica si el grafo es regular
        public bool IsRegular() {
            foreach (NodeP np in this) {
                if (np.Degree < Count - 1) {
                    return false;
                }
            }
            return true;
        }

        // Verifica que el grafo no dirigido este conectado
        public bool IsConnectedU() {
            foreach (NodeP np in this) {
                if (np.Degree == 0) {
                    return false;
                }
            }
            return true;
        }

        // Verifica que todas las aristas estén visitadas
        public bool AllEdgesVisited() {
            foreach (Edge ed in EdgesList) {
                if (ed.Visited == false) {
                    return false;
                }
            }
            return true;
        }

        // Verifica que todos los nodos estén visitados
        public bool AllNodesVisited() {
            foreach (NodeP np in this) {
                if (np.Visited == false) {
                    return false;
                }
            }
            return true;
        }

    }
}
