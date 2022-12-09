using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditordeGrafos
{
    public class Cubeta
    {
        public int TamCubeta;
        public int TamRegistro;

        public int Direccion;
        public int NumOcupados;
        public int SigCubeta;
        public int[] Valores;
        public byte[] Contenido;

        public Cubeta(int TC, int TR)
        {
            this.TamCubeta = TC;
            this.TamRegistro = TR;

            this.Contenido = new byte[TR - 4];
            this.Valores = new int[TamCubeta];
            for (int i = 0; i < TamCubeta; i++)
                Valores[i] = -1;
        }

        public Cubeta(int D, int N, int S, int TC, int TR)
        {
            this.TamCubeta = TC;
            this.TamRegistro = TR;

            this.Direccion = D;
            this.NumOcupados = N;
            this.SigCubeta = S;
            this.Contenido = new byte[TR - 4];
            this.Valores = new int[TamCubeta];
            for (int i = 0; i < TamCubeta; i++)
                Valores[i] = -1;
        }

        public void Ingresar_Ordenar(int V)
        {
            List<int> aux = new List<int>();
            for (int i = 0; i < Valores.Length; i++)
            {
                //MessageBox.Show(Valores[i].ToString());
                aux.Add(Valores[i]);
            }

            aux.Add(V);
            aux.Sort();
            for (int i = 0; i < aux.Count; i++)
                if (aux[i] == -1)
                    aux.RemoveAt(i);
            for (int i = 0; i < NumOcupados; i++)
            {
                //MessageBox.Show(aux[i].ToString());
                Valores[i] = aux[i];
            }

        }

    }
}
