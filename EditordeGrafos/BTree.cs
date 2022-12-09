using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EditordeGrafos
{
    public partial class BTree : Form
    {
        public String Archivo;
        Stack<Pagina> HistorialPaginas;
        int dirA; // Que rescata

        public BTree()
        {
            InitializeComponent();

            tb_r.Text = (sizeof(int)*3).ToString();                         //int Direccion, clave y sigRegistro en Registro
            tb_p.Text = (sizeof(int)*10 + sizeof(char)).ToString();         //int Direccion, 5 apuntadores, 4 claves y un char tipo
            tb_e.Text = "Sin cargar";
            tb_u.Text = "Sin cargar";
        }

        //Conversiones correctas, no tocar
        #region Conversion Int32 <-> bytes
        public static byte[] ConvertInt32ToByteArray(Int32 I32)
        {
            return BitConverter.GetBytes(I32);
        }

        public static Int32 ConvertByteArrayToInt32(byte[] b)
        {
            return BitConverter.ToInt32(b, 0);
        }
        #endregion

        #region Conversion Char <-> bytes
        public static byte[] ConvertCharToByteArray(Char c)
        {
            return BitConverter.GetBytes(c);
        }

        public static Int32 ConvertByteArrayToChar(byte[] b)
        {
            return BitConverter.ToChar(b, 0);
        }
        #endregion

        #region Conversion Página <-> bytes

        public static byte[] ConvertirPaginaToByteArray(Pagina p)
        {
            byte[] auxBytes;
            byte[] bytes = new byte[42];

            auxBytes = ConvertInt32ToByteArray(p.Direccion);
            auxBytes.CopyTo(bytes, 0);
            auxBytes = ConvertCharToByteArray(p.Tipo);
            auxBytes.CopyTo(bytes, 4);
            auxBytes = ConvertInt32ToByteArray(p.Apuntadores[0]);
            auxBytes.CopyTo(bytes, 6);
            auxBytes = ConvertInt32ToByteArray(p.Claves[0]);
            auxBytes.CopyTo(bytes, 10);
            auxBytes = ConvertInt32ToByteArray(p.Apuntadores[1]);
            auxBytes.CopyTo(bytes, 14);
            auxBytes = ConvertInt32ToByteArray(p.Claves[1]);
            auxBytes.CopyTo(bytes, 18);
            auxBytes = ConvertInt32ToByteArray(p.Apuntadores[2]);
            auxBytes.CopyTo(bytes, 22);
            auxBytes = ConvertInt32ToByteArray(p.Claves[2]);
            auxBytes.CopyTo(bytes, 26);
            auxBytes = ConvertInt32ToByteArray(p.Apuntadores[3]);
            auxBytes.CopyTo(bytes, 30);
            auxBytes = ConvertInt32ToByteArray(p.Claves[3]);
            auxBytes.CopyTo(bytes, 34);
            auxBytes = ConvertInt32ToByteArray(p.Apuntadores[4]);
            auxBytes.CopyTo(bytes, 38);

            return bytes;
        }

        public static Pagina ConvertByteArrayToPagina(byte[] b)
        {
            Pagina p = new Pagina();

            p.Direccion = BitConverter.ToInt32(b, 0);
            p.Tipo = BitConverter.ToChar(b, 4);
            p.Apuntadores[0] = BitConverter.ToInt32(b, 6);
            p.Claves[0] = BitConverter.ToInt32(b, 10);
            p.Apuntadores[1] = BitConverter.ToInt32(b, 14);
            p.Claves[1] = BitConverter.ToInt32(b, 18);
            p.Apuntadores[2] = BitConverter.ToInt32(b, 22);
            p.Claves[2] = BitConverter.ToInt32(b, 26);
            p.Apuntadores[3] = BitConverter.ToInt32(b, 30);
            p.Claves[3] = BitConverter.ToInt32(b, 34);
            p.Apuntadores[4] = BitConverter.ToInt32(b, 38);

            return p;
        }

        #endregion

        #region Conversion Registro <-> bytes

        public static byte[] ConvertirRegistroToByteArray(Registro reg)
        {
            byte[] auxBytes;
            byte[] bytes = new byte[12];

            auxBytes = ConvertInt32ToByteArray(reg.Direccion);
            auxBytes.CopyTo(bytes, 0);
            auxBytes = ConvertInt32ToByteArray(reg.Clave);
            auxBytes.CopyTo(bytes, 4);
            auxBytes = ConvertInt32ToByteArray(reg.sigRegistro);
            auxBytes.CopyTo(bytes, 8);

            return bytes;
        }

        public static Registro ConvertByteArrayToRegistro(byte[] b)
        {
            Registro reg = new Registro();

            reg.Direccion = BitConverter.ToInt32(b, 0);
            reg.Clave = BitConverter.ToChar(b, 4);
            reg.sigRegistro = BitConverter.ToInt32(b, 8);
            
            return reg;
        }

        #endregion

        //Listo...
        #region botones
        private void btn_Ndat_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "dat files (*.dat)|*.dat";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (sfd.FileName.Contains(".dat"))
                {
                    this.Archivo = sfd.FileName;    //Guardar ruta del archivo en donde operar
                    System.IO.FileStream fs = System.IO.File.Create(Archivo); //Crear el archivo
                    dGV.Rows.Clear();               //Despejar contenido en tabla
                    byte[] bytes = ConvertInt32ToByteArray(4);  //El primer entero (primeros 4 bytes) representan la posicion de Raiz
                    fs.Write(bytes, 0, bytes.Length);           //Insertar en archivo la posicion de la pagina raiz, siempre inicia en 4
                    fs.Close();
                    Pagina p = new Pagina(4);
                    TB_Raiz.Text = 4.ToString();    //Imprimir direccion de raiz
                    _ = FileInsertaPagina(p);
                }
            }
        }

        private void btn_Cdat_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "dat files (*.dat)|*.dat|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.Contains(".dat"))
                {
                    this.Archivo = ofd.FileName;
                    dGV.Rows.Clear();
                    tb_e.Text = System.IO.File.ReadAllText(Archivo).Length.ToString(); //Determinar EOF y escribir en Textbox
                    ImprimirArchivoCargado();
                }
            }
        }

        private void btn_Ctxt_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.Contains(".txt"))
                {
                    String ArchivoTxt = ofd.FileName;   //Rescatar ruta del archivo de texto
                    ofd.FileName = ofd.FileName.Remove(ofd.FileName.Length - 4, 4); //Modificar la extension para crear un nuevo .dat
                    ofd.FileName += ".dat";
                    this.Archivo = ofd.FileName;    //Guardar ruta del archivo en donde operar
                    dGV.Rows.Clear();               //Despejar contenido en tabla
                    tb_e.Text = "Sin cargar";
                    tb_u.Text = "Sin cargar";
                    System.IO.FileStream fs = System.IO.File.Create(Archivo); //Crear el archivo
                    TB_Raiz.Text = 4.ToString();    //Imprimir direccion de raiz
                    byte[] bytes = ConvertInt32ToByteArray(4);  //El primer entero (primeros 4 bytes) representan la posicion de Raiz
                    fs.Write(bytes, 0, bytes.Length);           //Insertar en archivo la posicion de la pagina raiz, siempre inicia en 5
                    fs.Close();
                    Pagina p = new Pagina(4);
                    _ = FileInsertaPagina(p);
                    CargarArchivoTexto(ArchivoTxt);

                }
            }
        }

        private void btn_Insertar_Click(object sender, EventArgs e)
        {
            if (tb_clave.Text != "" && Int32.Parse(tb_clave.Text) > 0)
            {
                
                if(File.Exists(Archivo))
                {
                    Registro reg = new Registro(Int32.Parse(tb_e.Text), Int32.Parse(tb_clave.Text));
                    Insertar_en_arbol(reg);
                    tb_clave.Text = "";
                }
                else
                    MessageBox.Show("No existe el archivo");
            }
            else
                MessageBox.Show("Campo clave vacio o valor de 0 siendo introducido");
        }

        #endregion

        //Listo...
        #region FileInsert
        private bool FileInsertaPagina(Pagina p)
        {
            if (System.IO.File.Exists(Archivo))
            {
                if(p.Tipo == 'R')
                    TB_Raiz.Text = p.Direccion.ToString();    //Imprimir direccion de raiz

                FileStream fs = File.Open(Archivo, FileMode.Open);
                byte[] bytes = ConvertirPaginaToByteArray(p);
                fs.Position = fs.Length;
                fs.Write(bytes, 0, bytes.Length);
                tb_e.Text = fs.Length.ToString(); //EOF en textbox
                fs.Close();
                GridInsertaPagina(p); //Lo movi 3 lineas abajo

                return true;
            }
            else
            {
                MessageBox.Show("El archivo: " + Archivo + " no existe");
                return false;
            }
        }

        private bool FileInsertaRegistro(Registro reg)
        {
            if (System.IO.File.Exists(Archivo))
            {
                //Actualizar registro anterior (sigRegistro) con la direccion del actual a insertar
                FileStream fs = File.Open(Archivo, FileMode.Open);

                //Modificar apuntador de SigRegistro de ultimo registro insertado 
                if(tb_u.Text != "Sin cargar" && tb_u.Text != "-1")
                {
                    Registro ureg;
                    fs.Position = Int32.Parse(tb_u.Text.ToString());
                    byte[] bytes2 = new byte[12];
                    fs.Read(bytes2, 0, 12);
                    ureg = ConvertByteArrayToRegistro(bytes2);
                    ureg.sigRegistro = (int)fs.Length;

                    byte[] bytes3;
                    bytes3 = ConvertirRegistroToByteArray(ureg);
                    fs.Position = ureg.Direccion;
                    fs.Write(bytes3, 0, bytes3.Length);
                    GridActualizaRegistro(ureg);
                }

                byte[] bytes = ConvertirRegistroToByteArray(reg);
                fs.Position = fs.Length;
                long ultRegPos = fs.Length;
                fs.Write(bytes, 0, bytes.Length);
                GridInsertaRegistro(reg);
                tb_e.Text = fs.Length.ToString(); //EOF en textbox
                tb_u.Text = ultRegPos.ToString();
                fs.Close();

                return true;
            }
            else
            {
                MessageBox.Show("El archivo: " + Archivo + " no existe");
                return false;
            }
        }

        private bool FileModificaPagina(int direccion, Pagina p)
        {
            if (System.IO.File.Exists(Archivo))
            {
                FileStream fs = File.Open(Archivo, FileMode.Open);
                byte[] bytes = ConvertirPaginaToByteArray(p);
                fs.Position = direccion;
                fs.Write(bytes, 0, bytes.Length);
                GridActualizaPagina(p);
                fs.Close();

                return true;
            }
            else
            {
                MessageBox.Show("El archivo: " + Archivo + " no existe");
                return false;
            }
        }

        private bool FileModificaRegistro(int direccion, Registro reg)
        {
            if (System.IO.File.Exists(Archivo))
            {
                FileStream fs = File.Open(Archivo, FileMode.Open);
                byte[] bytes = ConvertirRegistroToByteArray(reg);
                fs.Position = direccion;
                fs.Write(bytes, 0, bytes.Length);
                GridActualizaRegistro(reg);
                fs.Close();

                return true;
            }
            else
            {
                MessageBox.Show("El archivo: " + Archivo + " no existe");
                return false;
            }
        }

        private bool FileModificaRaiz(Pagina Raiz)
        {
            if (System.IO.File.Exists(Archivo))
            {
                FileStream fs = File.Open(Archivo, FileMode.Open);
                byte[] bytes = ConvertInt32ToByteArray(Raiz.Direccion);  //El primer entero (primeros 4 bytes) representan la posicion de Raiz
                fs.Position = 0;
                fs.Write(bytes, 0, bytes.Length);             //Insertar en archivo la posicion de la pagina raiz, siempre inicia en 5
                fs.Close();
                return true;
            }
            else
            {
                MessageBox.Show("El archivo: " + Archivo + " no existe");
                return false;
            }
        }
        #endregion

        //Soporte de division de intermedios
        #region LogicaArbolB+

        //Parece listo
        public void Insertar_en_arbol(Registro reg)
        {
            HistorialPaginas = new Stack<Pagina>();
            Pagina raiz = LeerArbolArchivo();
            //MessageBox.Show(raiz.Direccion.ToString() + " " + raiz.Tipo.ToString());
            baja(raiz, reg);
        }

        //Parece listo
        public void baja(Pagina p, Registro reg)
        {
            if(p.Tipo == 'H')
            {
                Insertar_en_Pagina(p, reg);
            }
            else
            {
                HistorialPaginas.Push(p); //Agregar al historial de padres
                int posicion = BuscarClave(p, reg.Clave);
                Pagina pb = LeerPaginaArchivo(p.Apuntadores[posicion]);
                baja(pb,reg);
            }
        }

        //Parece listo
        public void Insertar_en_Pagina(Pagina p, Registro reg)
        {
            //Verificar que no se meta un dato anteriormente ya insertado
            if (p.Claves[0] == reg.Clave || p.Claves[1] == reg.Clave || p.Claves[2] == reg.Clave || p.Claves[3] == reg.Clave)
            {
                MessageBox.Show("La clave ya fue introducida anteriormente");
                return;
            }
            else
            {
                int m = CalcularM(p);
                //Si ya esta lleno la hoja, es necesario pasar el dato de enmedio al padre (pagina)
                if (m == 4)
                {
                    //Funcionando
                    if(HistorialPaginas.Count == 0) //Si no se tiene un padre, se crea una nueva raiz y hoja
                    {
                        LLenoYnoPadre(reg,p);
                    }

                    else if(HistorialPaginas.Count != 0)
                    {
                        //Funcionando
                        if(CalcularM(HistorialPaginas.Peek()) != 4)
                        {
                            LLenoPeroNoElPadre(reg, p);

                        }
                        else if(CalcularM(HistorialPaginas.Peek()) == 4)
                        {
                            //Lleno y el padre igual, pueden ser multiples
                            LlenoTambienPadre(reg, p);
                        }
                    }
                }
                else
                {
                    int i;
                    for (i = 0; i < m && p.Claves[i] < reg.Clave; i++){} //Buscar lugar donde se debe insertar
                    for(int j = m; j > i; j--) //Recorrer los registros acorde a donde se insertara
                    {
                        p.Claves[j] = p.Claves[j-1];
                        p.Apuntadores[j] = p.Apuntadores[j-1];
                    }
                    p.Claves[i] = reg.Clave;
                    p.Apuntadores[i] = reg.Direccion;
                    FileInsertaRegistro(reg);
                    FileModificaPagina(p.Direccion, p);
                }
            }
        }


        //Parece Listo
        public void LLenoPeroNoElPadre(Registro reg, Pagina p)
        {
            //MessageBox.Show("LLpnep: " + p.Direccion.ToString() + " " + HistorialPaginas.Peek().Direccion.ToString());
            //Funcionando
            if (CalcularM(HistorialPaginas.Peek()) != 4 && HistorialPaginas.Count != 0)
            {
                Pagina Hoja;
                if (p.Tipo != 'H')
                {
                    Hoja = new Pagina(Int32.Parse(tb_e.Text), 'I');
                    p.Tipo = 'I';
                }
                else
                {
                    FileInsertaRegistro(reg);
                    Hoja = new Pagina(Int32.Parse(tb_e.Text));
                    //La Hoja apunta a la nueva hoja
                    Hoja.Apuntadores[4] = p.Apuntadores[4];
                    p.Apuntadores[4] = Hoja.Direccion;
                }

                FileInsertaPagina(Hoja);
                Pagina Padre = HistorialPaginas.Pop(); //Recuperar ultimo padre

                int i; //Verificar donde se tiene que poner el nuevo registro
                for (i = 0; i < 4; i++)
                {
                    if (reg.Clave < p.Claves[i])
                        break;
                }

                if (Hoja.Tipo == 'H')
                {
                    switch (i)
                    {
                        case 0:

                            Hoja.Claves[0] = p.Claves[1];
                            Hoja.Claves[1] = p.Claves[2];
                            Hoja.Claves[2] = p.Claves[3];
                            Hoja.Apuntadores[0] = p.Apuntadores[1];
                            Hoja.Apuntadores[1] = p.Apuntadores[2];
                            Hoja.Apuntadores[2] = p.Apuntadores[3];

                            p.Claves[1] = p.Claves[0];
                            p.Apuntadores[1] = p.Apuntadores[0];
                            p.Claves[0] = reg.Clave;
                            p.Apuntadores[0] = reg.Direccion;
                            break;
                        case 1:

                            Hoja.Claves[0] = p.Claves[1];
                            Hoja.Claves[1] = p.Claves[2];
                            Hoja.Claves[2] = p.Claves[3];
                            Hoja.Apuntadores[0] = p.Apuntadores[1];
                            Hoja.Apuntadores[1] = p.Apuntadores[2];
                            Hoja.Apuntadores[2] = p.Apuntadores[3];

                            p.Claves[1] = reg.Clave;
                            p.Apuntadores[1] = reg.Direccion;
                            break;
                        case 2:

                            Hoja.Claves[0] = reg.Clave;
                            Hoja.Claves[1] = p.Claves[2];
                            Hoja.Claves[2] = p.Claves[3];

                            Hoja.Apuntadores[0] = reg.Direccion;
                            Hoja.Apuntadores[1] = p.Apuntadores[2];
                            Hoja.Apuntadores[2] = p.Apuntadores[3];
                            break;
                        case 3:

                            Hoja.Claves[0] = p.Claves[2];
                            Hoja.Claves[1] = reg.Clave;
                            Hoja.Claves[2] = p.Claves[3];

                            Hoja.Apuntadores[0] = p.Apuntadores[2];
                            Hoja.Apuntadores[1] = reg.Direccion;
                            Hoja.Apuntadores[2] = p.Apuntadores[3];
                            break;
                        case 4:

                            Hoja.Claves[0] = p.Claves[2];
                            Hoja.Claves[1] = p.Claves[3];
                            Hoja.Claves[2] = reg.Clave;

                            Hoja.Apuntadores[0] = p.Apuntadores[2];
                            Hoja.Apuntadores[1] = p.Apuntadores[3];
                            Hoja.Apuntadores[2] = reg.Direccion;
                            break;
                    }

                    //Independientemente del caso, estos se ejecutan despues
                    p.Claves[2] = 0;
                    p.Claves[3] = 0;
                    p.Apuntadores[2] = 0;
                    p.Apuntadores[3] = 0;

                    int eme = CalcularM(Padre);
                    int j;
                    for (j = 0; j < eme && Padre.Claves[j] < Hoja.Claves[0]; j++) { }
                    for (int k = eme; k > j; k--)
                    {
                        Padre.Claves[k] = Padre.Claves[k - 1];
                        Padre.Apuntadores[k + 1] = Padre.Apuntadores[k];
                    }
                    Padre.Claves[j] = Hoja.Claves[0];
                    Padre.Apuntadores[j] = p.Direccion;
                    Padre.Apuntadores[j + 1] = Hoja.Direccion;
                }
                else //Si el intermedio se llena y tiene que pasar al padre (ya sea padre intermedio o raiz)
                {
                    //REVISAR
                    //En caso de que sea intermedio o primer raiz
                    int NumeroMEDIO = -111111;
                    switch (i)
                    {
                        case 0:
                            NumeroMEDIO = p.Claves[1];

                            Hoja.Claves[0] = p.Claves[2];
                            Hoja.Claves[1] = p.Claves[3];

                            Hoja.Apuntadores[0] = p.Apuntadores[2];
                            Hoja.Apuntadores[1] = p.Apuntadores[3];
                            Hoja.Apuntadores[2] = p.Apuntadores[4];

                            p.Claves[1] = p.Claves[0];
                            p.Claves[0] = reg.Clave;

                            p.Apuntadores[2] = p.Apuntadores[1];
                            p.Apuntadores[1] = dirA;

                            break;
                        case 1:
                            NumeroMEDIO = p.Claves[1];

                            Hoja.Claves[0] = p.Claves[2];
                            Hoja.Claves[1] = p.Claves[3];

                            Hoja.Apuntadores[0] = p.Apuntadores[2];
                            Hoja.Apuntadores[1] = p.Apuntadores[3];
                            Hoja.Apuntadores[2] = p.Apuntadores[4];

                            p.Claves[1] = reg.Clave;

                            p.Apuntadores[2] = dirA;

                            break;
                        case 2:
                            NumeroMEDIO = reg.Clave;

                            Hoja.Claves[0] = p.Claves[2];
                            Hoja.Claves[1] = p.Claves[3];

                            Hoja.Apuntadores[0] = dirA;
                            Hoja.Apuntadores[1] = p.Apuntadores[3];
                            Hoja.Apuntadores[2] = p.Apuntadores[4];

                            break;
                        case 3:
                            NumeroMEDIO = p.Claves[2];

                            Hoja.Claves[0] = reg.Clave;
                            Hoja.Claves[1] = p.Claves[3];

                            Hoja.Apuntadores[0] = p.Apuntadores[3];
                            Hoja.Apuntadores[1] = dirA;
                            Hoja.Apuntadores[2] = p.Apuntadores[4];

                            break;
                        case 4:
                            NumeroMEDIO = p.Claves[2];

                            Hoja.Claves[0] = p.Claves[3];
                            Hoja.Claves[1] = reg.Clave;

                            Hoja.Apuntadores[0] = p.Apuntadores[3];
                            Hoja.Apuntadores[1] = p.Apuntadores[4];
                            Hoja.Apuntadores[2] = dirA;

                            break;
                    }

                    //Esto se ejecuta despues
                    p.Claves[2] = 0;
                    p.Claves[3] = 0;
                    p.Apuntadores[3] = 0;
                    p.Apuntadores[4] = 0;

                    int eme = CalcularM(Padre);
                    int j;
                    for (j = 0; j < eme && Padre.Claves[j] < Hoja.Claves[0]; j++) { }
                    for (int k = eme; k > j; k--)
                    {
                        Padre.Claves[k] = Padre.Claves[k - 1];
                        Padre.Apuntadores[k + 1] = Padre.Apuntadores[k];
                    }
                    Padre.Claves[j] = NumeroMEDIO;
                    Padre.Apuntadores[j] = p.Direccion;
                    Padre.Apuntadores[j + 1] = Hoja.Direccion;
                }

                //Independientemente de los 2 casos, se actualizan
                FileModificaPagina(p.Direccion, p);
                FileModificaPagina(Hoja.Direccion, Hoja);
                FileModificaPagina(Padre.Direccion, Padre);

            }
        }


        //Parece listo
        public void LLenoYnoPadre(Registro reg, Pagina p)
        {
            //MessageBox.Show("LLynp: " + p.Direccion.ToString());
            //Insercion del registro y 2 paginas a archivo
            Pagina Hoja;
            if(p.Tipo != 'H')
            {
                Hoja = new Pagina(Int32.Parse(tb_e.Text), 'I');
                p.Tipo = 'I';
            }
            else
            {
                FileInsertaRegistro(reg);
                Hoja = new Pagina(Int32.Parse(tb_e.Text));
            }
                

            FileInsertaPagina(Hoja);
            Pagina Raiz = new Pagina(Int32.Parse(tb_e.Text), 'R');
            FileInsertaPagina(Raiz); //Insertar Pagina a archivo, sera de tipo raiz
            FileModificaRaiz(Raiz); //Los primeros 4 bytes del archivo apuntan a la nueva raiz insertada

            //Independientemente del caso, estos se ejecutan antes
            Raiz.Apuntadores[0] = p.Direccion;
            Raiz.Apuntadores[1] = Hoja.Direccion;

            int i; //Verificar donde se tiene que poner el nuevo registro
            for (i = 0; i < 4; i++)
            {
                if (reg.Clave < p.Claves[i])
                    break;
            }

            if(Hoja.Tipo == 'H')
            {
                p.Apuntadores[4] = Hoja.Direccion; //La pagina donde se realizo la insercion es de tipo hoja, entonces apunta a la nueva hoja creada
                switch (i)
                {
                    case 0:
                        Raiz.Claves[0] = p.Claves[1];

                        Hoja.Claves[0] = p.Claves[1];
                        Hoja.Claves[1] = p.Claves[2];
                        Hoja.Claves[2] = p.Claves[3];
                        Hoja.Apuntadores[0] = p.Apuntadores[1];
                        Hoja.Apuntadores[1] = p.Apuntadores[2];
                        Hoja.Apuntadores[2] = p.Apuntadores[3];

                        p.Claves[1] = p.Claves[0];
                        p.Apuntadores[1] = p.Apuntadores[0];
                        p.Claves[0] = reg.Clave;
                        p.Apuntadores[0] = reg.Direccion;
                        break;
                    case 1:
                        Raiz.Claves[0] = p.Claves[1];

                        Hoja.Claves[0] = p.Claves[1];
                        Hoja.Claves[1] = p.Claves[2];
                        Hoja.Claves[2] = p.Claves[3];
                        Hoja.Apuntadores[0] = p.Apuntadores[1];
                        Hoja.Apuntadores[1] = p.Apuntadores[2];
                        Hoja.Apuntadores[2] = p.Apuntadores[3];

                        p.Claves[1] = reg.Clave;
                        p.Apuntadores[1] = reg.Direccion;
                        break;
                    case 2:
                        Raiz.Claves[0] = reg.Clave;

                        Hoja.Claves[0] = reg.Clave;
                        Hoja.Claves[1] = p.Claves[2];
                        Hoja.Claves[2] = p.Claves[3];

                        Hoja.Apuntadores[0] = reg.Direccion;
                        Hoja.Apuntadores[1] = p.Apuntadores[2];
                        Hoja.Apuntadores[2] = p.Apuntadores[3];
                        break;
                    case 3:
                        Raiz.Claves[0] = p.Claves[2];

                        Hoja.Claves[0] = p.Claves[2];
                        Hoja.Claves[1] = reg.Clave;
                        Hoja.Claves[2] = p.Claves[3];

                        Hoja.Apuntadores[0] = p.Apuntadores[2];
                        Hoja.Apuntadores[1] = reg.Direccion;
                        Hoja.Apuntadores[2] = p.Apuntadores[3];
                        break;
                    case 4:
                        Raiz.Claves[0] = p.Claves[2];

                        Hoja.Claves[0] = p.Claves[2];
                        Hoja.Claves[1] = p.Claves[3];
                        Hoja.Claves[2] = reg.Clave;

                        Hoja.Apuntadores[0] = p.Apuntadores[2];
                        Hoja.Apuntadores[1] = p.Apuntadores[3];
                        Hoja.Apuntadores[2] = reg.Direccion;
                        break;
                }
                p.Apuntadores[2] = 0;
                p.Apuntadores[3] = 0;
            }
            else
            {
                //En caso de que sea intermedio o primer raiz
                switch (i)
                {
                    case 0:
                        Raiz.Claves[0] = p.Claves[1];

                        Hoja.Claves[0] = p.Claves[2];
                        Hoja.Claves[1] = p.Claves[3];
                        Hoja.Apuntadores[0] = p.Apuntadores[2];
                        Hoja.Apuntadores[1] = p.Apuntadores[3];
                        Hoja.Apuntadores[2] = p.Apuntadores[4];

                        p.Claves[1] = p.Claves[0];
                        p.Claves[0] = reg.Clave;

                        p.Apuntadores[2] = p.Apuntadores[1];
                        p.Apuntadores[1] = dirA;

                        break;
                    case 1:
                        Raiz.Claves[0] = p.Claves[1];

                        Hoja.Claves[0] = p.Claves[2];
                        Hoja.Claves[1] = p.Claves[3];
                        Hoja.Apuntadores[0] = p.Apuntadores[2];
                        Hoja.Apuntadores[1] = p.Apuntadores[3];
                        Hoja.Apuntadores[2] = p.Apuntadores[4];

                        p.Claves[1] = reg.Clave;

                        p.Apuntadores[2] = dirA;

                        break;
                    case 2:
                        Raiz.Claves[0] = reg.Clave;

                        Hoja.Claves[0] = p.Claves[2];
                        Hoja.Claves[1] = p.Claves[3];

                        Hoja.Apuntadores[0] = dirA;
                        Hoja.Apuntadores[1] = p.Apuntadores[3];
                        Hoja.Apuntadores[2] = p.Apuntadores[4];

                        break;
                    case 3:
                        Raiz.Claves[0] = p.Claves[2];

                        Hoja.Claves[0] = reg.Clave;
                        Hoja.Claves[1] = p.Claves[3];

                        Hoja.Apuntadores[0] = p.Apuntadores[3];
                        Hoja.Apuntadores[1] = dirA;
                        Hoja.Apuntadores[2] = p.Apuntadores[4];


                        break;
                    case 4:
                        Raiz.Claves[0] = p.Claves[2];

                        Hoja.Claves[0] = p.Claves[3];
                        Hoja.Claves[1] = reg.Clave;

                        Hoja.Apuntadores[0] = p.Apuntadores[3];
                        Hoja.Apuntadores[1] = p.Apuntadores[4];
                        Hoja.Apuntadores[2] = dirA;

                        break;
                }
                p.Apuntadores[3] = 0;
                p.Apuntadores[4] = 0;
            }
            

            //Independientemente del caso, estos se ejecutan despues
            p.Claves[2] = 0;
            p.Claves[3] = 0;

            FileModificaPagina(p.Direccion, p);
            FileModificaPagina(Hoja.Direccion, Hoja);
            FileModificaPagina(Raiz.Direccion, Raiz);
        }


        //Para nivel de intermedio modificar ajuste de nuevo intermedio. Es lo unico que falta PENDIENTE
        public void LlenoTambienPadre(Registro reg, Pagina p)
        {
            //MessageBox.Show("LLtep: " + p.Direccion.ToString());

            Pagina Hoja,Padre;
            Registro SubeR;

            if (p.Tipo != 'H')
            {
                Hoja = new Pagina(Int32.Parse(tb_e.Text), 'I');
                p.Tipo = 'I';
            }
            else
            {
                //Insercion del registro y pagina hoja a archivo
                FileInsertaRegistro(reg);
                Hoja = new Pagina(Int32.Parse(tb_e.Text));
                //Independientemente del caso, esto se ejecuta antes
                //La pagina apunta a la nueva hoja
                Hoja.Apuntadores[4] = p.Apuntadores[4];
                p.Apuntadores[4] = Hoja.Direccion;
            }
            FileInsertaPagina(Hoja);

            int i;
            for (i = 0; i < 4; i++)
            {
                if (reg.Clave < p.Claves[i])
                    break;
            }

            if (p.Tipo == 'H')
            {
                switch (i)
                {
                    case 0:

                        Hoja.Claves[0] = p.Claves[1];
                        Hoja.Claves[1] = p.Claves[2];
                        Hoja.Claves[2] = p.Claves[3];
                        Hoja.Apuntadores[0] = p.Apuntadores[1];
                        Hoja.Apuntadores[1] = p.Apuntadores[2];
                        Hoja.Apuntadores[2] = p.Apuntadores[3];

                        p.Claves[1] = p.Claves[0];
                        p.Apuntadores[1] = p.Apuntadores[0];
                        p.Claves[0] = reg.Clave;
                        p.Apuntadores[0] = reg.Direccion;
                        break;
                    case 1:

                        Hoja.Claves[0] = p.Claves[1];
                        Hoja.Claves[1] = p.Claves[2];
                        Hoja.Claves[2] = p.Claves[3];
                        Hoja.Apuntadores[0] = p.Apuntadores[1];
                        Hoja.Apuntadores[1] = p.Apuntadores[2];
                        Hoja.Apuntadores[2] = p.Apuntadores[3];

                        p.Claves[1] = reg.Clave;
                        p.Apuntadores[1] = reg.Direccion;
                        break;
                    case 2:

                        Hoja.Claves[0] = reg.Clave;
                        Hoja.Claves[1] = p.Claves[2];
                        Hoja.Claves[2] = p.Claves[3];

                        Hoja.Apuntadores[0] = reg.Direccion;
                        Hoja.Apuntadores[1] = p.Apuntadores[2];
                        Hoja.Apuntadores[2] = p.Apuntadores[3];
                        break;
                    case 3:

                        Hoja.Claves[0] = p.Claves[2];
                        Hoja.Claves[1] = reg.Clave;
                        Hoja.Claves[2] = p.Claves[3];

                        Hoja.Apuntadores[0] = p.Apuntadores[2];
                        Hoja.Apuntadores[1] = reg.Direccion;
                        Hoja.Apuntadores[2] = p.Apuntadores[3];
                        break;
                    case 4:

                        Hoja.Claves[0] = p.Claves[2];
                        Hoja.Claves[1] = p.Claves[3];
                        Hoja.Claves[2] = reg.Clave;

                        Hoja.Apuntadores[0] = p.Apuntadores[2];
                        Hoja.Apuntadores[1] = p.Apuntadores[3];
                        Hoja.Apuntadores[2] = reg.Direccion;
                        break;
                }

                //Independientemente del caso, estos se ejecutan despues
                p.Claves[2] = 0;
                p.Claves[3] = 0;
                p.Apuntadores[2] = 0;
                p.Apuntadores[3] = 0;

                FileModificaPagina(p.Direccion, p);
                FileModificaPagina(Hoja.Direccion, Hoja);

                Padre = HistorialPaginas.Pop(); //Recuperar ultimo padre
                SubeR = LeerRegistroArchivo(Hoja.Apuntadores[0]); //Registro a subir al padre

            }
            else
            {
                //REVISAR
                //En caso de que sea intermedio o primer raiz
                int NumeroMEDIO = -111111;
                switch (i)
                {
                    case 0:
                        NumeroMEDIO = p.Claves[1];

                        Hoja.Claves[0] = p.Claves[2];
                        Hoja.Claves[1] = p.Claves[3];

                        Hoja.Apuntadores[0] = p.Apuntadores[2];
                        Hoja.Apuntadores[1] = p.Apuntadores[3];
                        Hoja.Apuntadores[2] = p.Apuntadores[4];

                        p.Claves[1] = p.Claves[0];
                        p.Claves[0] = reg.Clave;

                        p.Apuntadores[2] = p.Apuntadores[1];
                        p.Apuntadores[1] = dirA;

                        break;
                    case 1:
                        NumeroMEDIO = p.Claves[1];

                        Hoja.Claves[0] = p.Claves[2];
                        Hoja.Claves[1] = p.Claves[3];

                        Hoja.Apuntadores[0] = p.Apuntadores[2];
                        Hoja.Apuntadores[1] = p.Apuntadores[3];
                        Hoja.Apuntadores[2] = p.Apuntadores[4];

                        p.Claves[1] = reg.Clave;

                        p.Apuntadores[2] = dirA;

                        break;
                    case 2:
                        NumeroMEDIO = reg.Clave;

                        Hoja.Claves[0] = p.Claves[2];
                        Hoja.Claves[1] = p.Claves[3];

                        Hoja.Apuntadores[0] = dirA;
                        Hoja.Apuntadores[1] = p.Apuntadores[3];
                        Hoja.Apuntadores[2] = p.Apuntadores[4];

                        break;
                    case 3:
                        NumeroMEDIO = p.Claves[2];

                        Hoja.Claves[0] = reg.Clave;
                        Hoja.Claves[1] = p.Claves[3];

                        Hoja.Apuntadores[0] = p.Apuntadores[3];
                        Hoja.Apuntadores[1] = dirA;
                        Hoja.Apuntadores[2] = p.Apuntadores[4];

                        break;
                    case 4:
                        NumeroMEDIO = p.Claves[2];

                        Hoja.Claves[0] = p.Claves[3];
                        Hoja.Claves[1] = reg.Clave;

                        Hoja.Apuntadores[0] = p.Apuntadores[3];
                        Hoja.Apuntadores[1] = p.Apuntadores[4];
                        Hoja.Apuntadores[2] = dirA;

                        break;
                }

                //Esto se ejecuta despues
                p.Claves[2] = 0;
                p.Claves[3] = 0;
                p.Apuntadores[3] = 0;
                p.Apuntadores[4] = 0;

                FileModificaPagina(p.Direccion, p);
                FileModificaPagina(Hoja.Direccion, Hoja);

                Padre = HistorialPaginas.Pop(); //Recuperar ultimo padre
                SubeR = new Registro(); //Registro a subir al padre
                SubeR.Clave = NumeroMEDIO;
            }

            dirA = Hoja.Direccion;

            //MessageBox.Show("Subiendo: " + SubeR.Clave.ToString());

            Insertar_en_Pagina(Padre, SubeR); //Algo de recursividad
        }


        //Listo
        public int CalcularM(Pagina p)
        {
            int m = 0;
            for (int i = 0; i < 4; i++)
            {
                if (p.Claves[i] > 0)
                    m++;
            }
            return m;
        }

        //Listo
        public int BuscarClave(Pagina p, int x)
        {
            int pos = 0;
            while (pos < CalcularM(p) && p.Claves[pos] < x)
                pos++;
            return pos;
        }

        #endregion

        //Listo, no tocar
        #region LeerArchivo
        //Listo
        public Pagina LeerArbolArchivo()
        {
            if (System.IO.File.Exists(Archivo))
            {
                FileStream fs = File.Open(Archivo, FileMode.Open);
                fs.Position = 0; //Al inicio de archivo un int indica la posicion de la raiz
                byte[] bytes = new byte[4];
                fs.Read(bytes,0,4);
                int dr = ConvertByteArrayToInt32(bytes);
                TB_Raiz.Text = dr.ToString();    //Imprimir direccion de raiz
                fs.Close();
                Pagina raiz = LeerPaginaArchivo(dr);
                return raiz;
            }
            else
            {
                return null;
            }
        }

        //Listo
        public Pagina LeerPaginaArchivo(int Direccion)
        {
            if (System.IO.File.Exists(Archivo))
            {
                FileStream fs = File.Open(Archivo, FileMode.Open);
                fs.Position = Direccion;
                byte[] bytes = new byte[42];
                fs.Read(bytes,0,42);
                Pagina p = ConvertByteArrayToPagina(bytes);
                fs.Close();
                return p;
            }
            else
            {
                return null;
            }
        }

        //Listo
        public Registro LeerRegistroArchivo(int Direccion)
        {
            if (System.IO.File.Exists(Archivo))
            {
                FileStream fs = File.Open(Archivo, FileMode.Open);
                fs.Position = Direccion;
                byte[] bytes = new byte[12];
                fs.Read(bytes, 0, 12);
                Registro reg = ConvertByteArrayToRegistro(bytes);
                fs.Close();
                return reg;
            }
            else
            {
                return null;
            }
        }

        //Listo
        private void ImprimirArchivoCargado()
        {
            if (System.IO.File.Exists(Archivo))
            {
                int pos = 4;
                while(pos < Int32.Parse(tb_e.Text))
                {
                    //MessageBox.Show(pos.ToString());
                    if (pos + 42 <= Int32.Parse(tb_e.Text))
                    {
                        Pagina p = LeerPaginaArchivo(pos);
                        if(p.Tipo != 'H' && p.Tipo != 'R' && p.Tipo != 'I')
                        {
                            Registro r = LeerRegistroArchivo(pos);
                            //MessageBox.Show(r.Clave.ToString() + " " + r.Direccion.ToString() + " " + r.sigRegistro.ToString());
                            GridInsertaRegistro(r);
                            tb_u.Text = pos.ToString(); //Ultimo registro leido
                            pos += 12;
                        }
                        else
                        {
                            GridInsertaPagina(p);
                            pos += 42;
                        }
                    }
                    else
                    {
                        Registro r = LeerRegistroArchivo(pos);
                        GridInsertaRegistro(r);
                        pos += 12;
                        tb_u.Text = pos.ToString(); //Ultimo registro leido
                    }
                }
                //MARCAR EN EL TEXT BOX DIRECCION DE RAIZ
                FileStream fs = File.Open(Archivo, FileMode.Open);
                fs.Position = 0; //Al inicio de archivo un int indica la posicion de la raiz
                byte[] bytes = new byte[4];
                fs.Read(bytes, 0, 4);
                int dr = ConvertByteArrayToInt32(bytes);
                TB_Raiz.Text = dr.ToString();    //Imprimir direccion de raiz
                fs.Close();
            }
            else
            {
                MessageBox.Show("No se puede mostrar el archivo");
            }
        }

        //Listo
        private void CargarArchivoTexto(String path)
        {
            String Contenido = File.ReadAllText(path);
            String lectura = "";
            for(int i = 0; i<=Contenido.Length; i++)
            {
                if(i == Contenido.Length)
                {
                    if (lectura != "" && Int32.Parse(lectura) > 0)
                    {
                        if (File.Exists(Archivo))
                        {
                            Registro reg = new Registro(Int32.Parse(tb_e.Text), Int32.Parse(lectura));
                            Insertar_en_arbol(reg);
                        }
                        else
                            MessageBox.Show("No existe el archivo");
                    }
                    else
                        MessageBox.Show("Vacio o valor de 0 siendo introducido, ignorando insercion actual");
                }
                else if (Contenido[i] == ',')
                {
                    if (lectura != "" && Int32.Parse(lectura) > 0)
                    {
                        if (File.Exists(Archivo))
                        {
                            Registro reg = new Registro(Int32.Parse(tb_e.Text), Int32.Parse(lectura));
                            Insertar_en_arbol(reg);
                        }
                        else
                            MessageBox.Show("No existe el archivo");
                    }
                    else
                        MessageBox.Show("Vacio o valor de 0 siendo introducido, ignorando insercion actual");

                    lectura = "";
                }

                else
                    lectura += Contenido[i];
            }
        }

        #endregion

        //Listo...
        #region Grid
        private void GridInsertaPagina(Pagina p)
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dGV);
            r.Cells[0].Value = p.Direccion;
            r.Cells[1].Value = p.Tipo;
            r.Cells[2].Value = p.Apuntadores[0];
            r.Cells[3].Value = p.Claves[0];
            r.Cells[4].Value = p.Apuntadores[1];
            r.Cells[5].Value = p.Claves[1];
            r.Cells[6].Value = p.Apuntadores[2];
            r.Cells[7].Value = p.Claves[2];
            r.Cells[8].Value = p.Apuntadores[3];
            r.Cells[9].Value = p.Claves[3];
            r.Cells[10].Value = p.Apuntadores[4];
            dGV.Rows.Add(r);
        }

        private void GridActualizaPagina(Pagina p)
        {
            for(int i=0; i<dGV.RowCount;i++)
            {
                if (Int32.Parse(dGV.Rows[i].Cells[0].Value.ToString()) == p.Direccion)
                {
                    dGV.Rows[i].Cells[0].Value = p.Direccion;
                    dGV.Rows[i].Cells[1].Value = p.Tipo;
                    dGV.Rows[i].Cells[2].Value = p.Apuntadores[0];
                    dGV.Rows[i].Cells[3].Value = p.Claves[0];
                    dGV.Rows[i].Cells[4].Value = p.Apuntadores[1];
                    dGV.Rows[i].Cells[5].Value = p.Claves[1];
                    dGV.Rows[i].Cells[6].Value = p.Apuntadores[2];
                    dGV.Rows[i].Cells[7].Value = p.Claves[2];
                    dGV.Rows[i].Cells[8].Value = p.Apuntadores[3];
                    dGV.Rows[i].Cells[9].Value = p.Claves[3];
                    dGV.Rows[i].Cells[10].Value = p.Apuntadores[4];
                }
            }
        }

        private void GridInsertaRegistro(Registro reg)
        {
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dGV);
            r.Cells[0].Value = reg.Direccion;
            r.Cells[1].Value = reg.Clave;
            r.Cells[2].Value = reg.sigRegistro;
            r.Cells[3].Value = "...";
            r.Cells[4].Value = "...";
            r.Cells[5].Value = "...";
            r.Cells[6].Value = "...";
            r.Cells[7].Value = "...";
            r.Cells[8].Value = "...";
            r.Cells[9].Value = "...";
            r.Cells[10].Value = "...";
            dGV.Rows.Add(r);
        }

        private void GridActualizaRegistro(Registro reg)
        {
            for (int i = 0; i < dGV.RowCount; i++)
            {
                if (Int32.Parse(dGV.Rows[i].Cells[0].Value.ToString()) == reg.Direccion)
                {
                    dGV.Rows[i].Cells[0].Value = reg.Direccion;
                    dGV.Rows[i].Cells[1].Value = reg.Clave;
                    dGV.Rows[i].Cells[2].Value = reg.sigRegistro;
                }
            }
        }
        #endregion

    }
}
