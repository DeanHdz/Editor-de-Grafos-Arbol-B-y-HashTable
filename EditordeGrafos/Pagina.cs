using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditordeGrafos
{
    public class Pagina
    {
        public int Direccion;
        public char Tipo;
        public int[] Apuntadores;
        public int[] Claves;

        //Reservado para lectura de bytes a pagina
        public Pagina()
        {
            this.Apuntadores = new int[5];  //Arreglo de 5 apuntadores de paginas o registros
            this.Claves = new int[4];       //Arreglo de 4 claves en orden
            this.Tipo = 'H';
        }

        //Reservado para la creacion de una nueva pagina inicial/raiz
        public Pagina(int Direccion)
        {
            this.Direccion = Direccion;     //Direccion de la pagina
            this.Tipo = 'H';                //Declarar como Hoja asumiendo que se construye desde abajo
            this.Apuntadores = new int[5];  //Arreglo de 5 apuntadores de paginas o registros
            this.Claves = new int[4];       //Arreglo de 4 claves en orden
        }

        public Pagina(int Direccion, char tipo)
        {
            this.Direccion = Direccion;     //Direccion de la pagina
            this.Tipo = tipo;               //Declarar tipo de pagina
            this.Apuntadores = new int[5];  //Arreglo de 5 apuntadores de paginas o registros
            this.Claves = new int[4];       //Arreglo de 4 claves en orden
        }
    }
}
