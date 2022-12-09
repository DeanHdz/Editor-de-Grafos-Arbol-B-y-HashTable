using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditordeGrafos
{
    public class Registro
    {
        public int Direccion;
        public int Clave;
        public int sigRegistro;

        public Registro(){}

        public Registro(int Direccion, int Clave)
        {
            this.Direccion = Direccion; //Direccion en archivo del registro
            this.Clave = Clave;         //Clave del registro
            this.sigRegistro = -1;      //Asumiendo que es el ultimo registro en insertar
        }

        public Registro(Registro copiar)
        {
            this.Direccion = copiar.Direccion;
            this.Clave = copiar.Clave;
            this.sigRegistro = copiar.sigRegistro;
        }

    }
}
