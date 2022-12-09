using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditordeGrafos
{
    public class TablaDireccion
    {
        public int NumZocalos;
        public int RegistrosPorCubeta;
        public int TamRegistro;
        public int SigVacio;
        public int EOFInicial;
        public int[] Zocalos;

        public TablaDireccion() { }

        public TablaDireccion(int N, int R, int T, int S, int E)
        {
            this.NumZocalos = N;
            this.RegistrosPorCubeta = R;
            this.TamRegistro = T;
            this.SigVacio = S;
            this.EOFInicial = E;
            this.Zocalos = new int[N];
            for (int i = 0; i < N; i++)
                Zocalos[i] = -1;
        }

    }
}
