using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditordeGrafos
{
    public partial class HashTable : Form
    {
        public String Archivo;
        public int NumZocalos;
        public int RegistrosPorCubeta;
        public int TamRegistro;

        public HashTable()
        {
            InitializeComponent();
        }

        //Conversiones correctas
        #region Conversion Int32 <-> bytes

        public byte[] ConvertInt32ToByteArray(Int32 I32)
        {
            return BitConverter.GetBytes(I32);
        }

        public Int32 ConvertByteArrayToInt32(byte[] b)
        {
            return BitConverter.ToInt32(b, 0);
        }

        #endregion

        #region Conversion TablaDirecciones <-> bytes

        public byte[] Convertir_TD_ToByteArray(TablaDireccion td)
        {
            int CalculoBytes = 20 + (td.NumZocalos * 4);
            byte[] bytes = new byte[CalculoBytes];
            byte[] auxBytes;

            auxBytes = ConvertInt32ToByteArray(td.NumZocalos);
            auxBytes.CopyTo(bytes, 0);
            auxBytes = ConvertInt32ToByteArray(td.RegistrosPorCubeta);
            auxBytes.CopyTo(bytes, 4);
            auxBytes = ConvertInt32ToByteArray(td.TamRegistro);
            auxBytes.CopyTo(bytes, 8);
            auxBytes = ConvertInt32ToByteArray(td.SigVacio);
            auxBytes.CopyTo(bytes, 12);
            auxBytes = ConvertInt32ToByteArray(td.EOFInicial);
            auxBytes.CopyTo(bytes, 16);

            int pos = 20;
            for (int i = 0; i < td.NumZocalos; i++)
            {
                auxBytes = ConvertInt32ToByteArray(td.Zocalos[i]);
                auxBytes.CopyTo(bytes, pos);
                pos += 4;
            }

            return bytes;
        }

        public TablaDireccion Convertir_ByteArrayTo_TD(byte[] b)
        {

            TablaDireccion td = new TablaDireccion(NumZocalos, RegistrosPorCubeta, TamRegistro, 0, 0);

            td.NumZocalos = BitConverter.ToInt32(b, 0);
            td.RegistrosPorCubeta = BitConverter.ToInt32(b, 4);
            td.TamRegistro = BitConverter.ToInt32(b, 8);
            td.SigVacio = BitConverter.ToInt32(b, 12);
            td.EOFInicial = BitConverter.ToInt32(b, 16);

            int pos = 20;
            for (int i = 0; i < td.NumZocalos; i++)
            {
                td.Zocalos[i] = BitConverter.ToInt32(b, pos);
                pos += 4;
            }

            return td;
        }

        #endregion

        #region Conversion Cubeta <-> bytes

        public byte[] Convertir_Cubeta_ToByteArray(Cubeta cu)
        {
            int CalculoBytes = 12 + (RegistrosPorCubeta * TamRegistro);
            byte[] bytes = new byte[CalculoBytes];
            byte[] auxBytes;

            auxBytes = ConvertInt32ToByteArray(cu.Direccion);
            auxBytes.CopyTo(bytes, 0);
            auxBytes = ConvertInt32ToByteArray(cu.NumOcupados);
            auxBytes.CopyTo(bytes, 4);
            auxBytes = ConvertInt32ToByteArray(cu.SigCubeta);
            auxBytes.CopyTo(bytes, 8);

            int pos = 12;
            for (int i = 0; i < RegistrosPorCubeta; i++)
            {
                auxBytes = ConvertInt32ToByteArray(cu.Valores[i]);
                auxBytes.CopyTo(bytes, pos);
                auxBytes = new byte[TamRegistro - 4];
                auxBytes.CopyTo(bytes, pos + 4);
                pos += TamRegistro;
            }
            return bytes;
        }

        public Cubeta Convertir_ByteArrayTo_Cubeta(byte[] b)
        {
            Cubeta cu = new Cubeta(this.RegistrosPorCubeta, this.TamRegistro);

            cu.Direccion = BitConverter.ToInt32(b, 0);
            cu.NumOcupados = BitConverter.ToInt32(b, 4);
            cu.SigCubeta = BitConverter.ToInt32(b, 8);

            int pos = 12;
            for (int i = 0; i < cu.NumOcupados; i++)
            {
                cu.Valores[i] = BitConverter.ToInt32(b, pos);
                pos += cu.TamRegistro;
            }

            return cu;
        }

        #endregion


        //Listo
        #region Botones
        //Abrir form para crear un archivo nuevo
        private void TSB_CrearArchivo_Click(object sender, EventArgs e)
        {
            NuevaTabla form = new NuevaTabla(this);
            form.ShowDialog();
        }

        //Cargar archivo dat conteniendo HashTable 
        private void TSB_Abrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "dat files (*.dat)|*.dat|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.Contains(".dat"))
                {
                    this.Archivo = ofd.FileName;
                    DGV_Direcciones.Rows.Clear();
                    DGV_Cubetas.Rows.Clear();
                    DGV_Cubetas.Columns.Clear();

                    Leer_EOF(Archivo);
                    FileStream fs = File.Open(Archivo, FileMode.Open);
                    byte[] bytes = new byte[4];

                    fs.Position = 0;
                    fs.Read(bytes, 0, bytes.Length);
                    NumZocalos = ConvertByteArrayToInt32(bytes);
                    TB_mod.Text = NumZocalos.ToString();


                    fs.Position = 4;
                    fs.Read(bytes, 0, bytes.Length);
                    RegistrosPorCubeta = ConvertByteArrayToInt32(bytes);
                    TB_rpc.Text = RegistrosPorCubeta.ToString();


                    fs.Position = 8;
                    fs.Read(bytes, 0, bytes.Length);
                    TamRegistro = ConvertByteArrayToInt32(bytes);
                    TB_rb.Text = TamRegistro.ToString();
                    fs.Close();

                    //Mostrar tamaño de Dispersion y cubeta
                    TB_dis.Text = (20 + (NumZocalos * 4)).ToString();
                    TB_cu.Text = (12 + (TamRegistro * RegistrosPorCubeta) ).ToString();

                    TablaDireccion td = Leer_TablaDireccion(Archivo);
                    Grid_TablaDirecciones(td);
                    Grid_Cubetas(Leer_Cubetas(Archivo));
                }
            }
        }

        //Importar un archivo de texto
        private void TSB_ImportarDispersion_Click(object sender, EventArgs e)
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
                    DGV_Direcciones.Rows.Clear();
                    DGV_Cubetas.Rows.Clear();
                    DGV_Cubetas.Columns.Clear();
                    FileStream fs = File.Create(Archivo); //Crear el archivo
                    fs.Close();

                    FileStream fileStream = File.Open(ArchivoTxt, FileMode.Open);
                    TablaDireccion td = null;
                    Cubeta UltCu = null;

                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        int Renglon = 0;
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (line.Length > 0)
                            {
                                if (line[0] >= 33 && line[0] <= 126)
                                {
                                    //MessageBox.Show("Usando: " + line);
                                    if (Renglon == 0)
                                    {
                                        String lectura = "";
                                        List<int> nums = new List<int>();
                                        for (int i = 0; i < line.Length; i++)
                                        {
                                            if (line[i] == ';')
                                            {
                                                if (lectura != "")
                                                    nums.Add(Int32.Parse(lectura));
                                                lectura = "";
                                                break;
                                            }
                                            else if (line[i] == ',')
                                            {
                                                if (lectura != "")
                                                    nums.Add(Int32.Parse(lectura));
                                                lectura = "";
                                            }
                                            else
                                                lectura += line[i];
                                        }
                                        this.NumZocalos = nums[0];
                                        this.RegistrosPorCubeta = nums[1];
                                        this.TamRegistro = nums[2];
                                        TB_mod.Text = NumZocalos.ToString();
                                        TB_rpc.Text = RegistrosPorCubeta.ToString();
                                        TB_rb.Text = TamRegistro.ToString();
                                        //Mostrar tamaño de Dispersion y cubeta
                                        TB_dis.Text = (20 + (NumZocalos * 4)).ToString();
                                        TB_cu.Text = (12 + (TamRegistro * RegistrosPorCubeta)).ToString();

                                        td = new TablaDireccion(NumZocalos, RegistrosPorCubeta, TamRegistro, 0, 0);
                                        Escribir_TablaDirecciones(Archivo, td);
                                        Renglon++;
                                    }
                                    else if (Renglon >= 1 && Renglon <= NumZocalos)
                                    {
                                        String lectura = "";
                                        List<int> nums = new List<int>();
                                        for (int i = 0; i < line.Length; i++)
                                        {
                                            if (line[i] == ';')
                                            {
                                                if (lectura != "")
                                                    nums.Add(Int32.Parse(lectura));
                                                lectura = "";
                                                break;
                                            }
                                            else if (line[i] == ',')
                                            {
                                                if (lectura != "")
                                                    nums.Add(Int32.Parse(lectura));
                                                lectura = "";
                                            }
                                            else
                                                lectura += line[i];
                                        }
                                        if (nums.Count > 1)
                                            td.Zocalos[nums[0]] = nums[1];
                                        else
                                            td.Zocalos[nums[0]] = -1;

                                        Renglon++;
                                    }
                                    else
                                    {
                                        String lectura = "";
                                        List<int> nums = new List<int>();
                                        for (int i = 0; i < line.Length; i++)
                                        {
                                            if (line[i] == ';')
                                            {
                                                if (lectura != "")
                                                    nums.Add(Int32.Parse(lectura));
                                                lectura = "";
                                                break;
                                            }
                                            else if (line[i] == ',')
                                            {
                                                if (lectura != "")
                                                    nums.Add(Int32.Parse(lectura));
                                                lectura = "";
                                            }
                                            else
                                                lectura += line[i];
                                        }
                                        if (nums.Count == 3)
                                        {
                                            UltCu = new Cubeta(nums[0], nums[1], nums[2], RegistrosPorCubeta, TamRegistro);
                                            Escribir_Cubeta(Archivo,UltCu);
                                        }
                                        else if (nums.Count == 1)
                                        {
                                            UltCu.Ingresar_Ordenar(nums[0]);
                                            Escribir_Cubeta(Archivo, UltCu);
                                        }  
                                    }
                                }
                            }
                        }
                        streamReader.Close();
                    }
                    fileStream.Close();
                    Escribir_TablaDirecciones(Archivo, td);
                    Grid_TablaDirecciones(td);
                    Grid_Cubetas(Leer_Cubetas(this.Archivo));
                }
            }
        }

        private void B_Insertar_Click(object sender, EventArgs e)
        {
            if (NUP_Clave.Value >= 0)
            {
                InsertarClave((int)NUP_Clave.Value);
            }
            else
                MessageBox.Show("Elige un numero 0 o mayor");
        }

        #endregion

        //Listo
        #region LeerArchivo

        public int Leer_EOF(String FileName)
        {
            if (File.Exists(FileName))
            {
                FileStream fs = File.Open(FileName, FileMode.Open);
                int EOF = (int)fs.Length;
                TB_EOF.Text = fs.Length.ToString(); //EOF en textbox
                fs.Close();
                return EOF;
            }
            else
            {
                MessageBox.Show("El archivo: " + FileName + " no existe");
                return -1;
            }
        }

        public TablaDireccion Leer_TablaDireccion(String FileName)
        {
            if (File.Exists(FileName))
            {
                FileStream fs = File.Open(FileName, FileMode.Open);
                fs.Position = 0;
                int Calculado = 20 + (NumZocalos * 4);
                byte[] bytes = new byte[Calculado];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                TablaDireccion td = Convertir_ByteArrayTo_TD(bytes);
                return td;
            }
            else
            {
                MessageBox.Show("El archivo: " + FileName + " no existe");
                return null;
            }
        }

        public Cubeta Leer_Cubeta(String FileName, int Direccion)
        {
            if (File.Exists(FileName))
            {
                FileStream fs = File.Open(FileName, FileMode.Open);
                fs.Position = Direccion;
                int Calculado = 12 + (RegistrosPorCubeta * TamRegistro);
                byte[] bytes = new byte[Calculado];
                fs.Read(bytes, 0, bytes.Length);
                Cubeta cu = Convertir_ByteArrayTo_Cubeta(bytes);
                fs.Close();
                return cu;
            }
            else
            {
                MessageBox.Show("El archivo: " + FileName + " no existe");
                return null;
            }
        }

        public List<Cubeta> Leer_Cubetas(String FileName)
        {
            List<Cubeta> cubetas = new List<Cubeta>();

            int CalcularTablaDireccion = 20 + (NumZocalos * 4);
            int recorrido = 0;
            try
            {
                while ((CalcularTablaDireccion + recorrido) < Leer_EOF(FileName))
                {
                    Cubeta cu = Leer_Cubeta(FileName, (CalcularTablaDireccion + recorrido));
                    cubetas.Add(cu);
                    recorrido += 12 + (RegistrosPorCubeta * TamRegistro);
                }
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }

            return cubetas;
        }

        #endregion

        //Listo
        #region EscribirArchivo

        public void Escribir_TablaDirecciones(String FileName, TablaDireccion td)
        {

            if (File.Exists(FileName))
            {
                byte[] bytes = Convertir_TD_ToByteArray(td);
                FileStream fs = File.Open(FileName, FileMode.Open);
                fs.Position = 0;
                fs.Write(bytes, 0, bytes.Length);
                TB_EOF.Text = fs.Length.ToString(); //EOF en textbox
                fs.Close();
            }
            else
            {
                MessageBox.Show("El archivo: " + FileName + " no existe");
            }

        }

        public void Escribir_Cubeta(String FileName, Cubeta cu)
        {
            if (File.Exists(FileName))
            {
                byte[] bytes = Convertir_Cubeta_ToByteArray(cu);
                FileStream fs = File.Open(FileName, FileMode.Open);
                fs.Position = cu.Direccion;
                fs.Write(bytes, 0, bytes.Length);
                TB_EOF.Text = fs.Length.ToString(); //EOF en textbox
                fs.Close();
            }
            else
            {
                MessageBox.Show("El archivo: " + FileName + " no existe");
            }
        }

        #endregion

        //Listo
        #region Logica

        public void InsertarClave(int Clave)
        {
            int mod = Clave % NumZocalos;
            TablaDireccion td = Leer_TablaDireccion(Archivo);

            //Caso: No existe un numero con ese mod aun
            if (td.Zocalos[mod] == -1)
            {
                int EOF = Leer_EOF(Archivo);
                Cubeta cu = new Cubeta(EOF, 1, 0, this.RegistrosPorCubeta, this.TamRegistro);
                cu.Valores[0] = Clave;
                td.Zocalos[mod] = EOF;
                Escribir_Cubeta(Archivo, cu);
                Escribir_TablaDirecciones(Archivo, td);
                Grid_Cubetas(Leer_Cubetas(Archivo));
                Grid_TablaDirecciones(td);
            }
            else
            {
                //Caso: Existe una cubeta con el mod
                Cubeta cu = Leer_Cubeta(Archivo, td.Zocalos[mod]);
                if (BuscarClave(cu, Clave))
                {
                    MessageBox.Show("La clave ya fue insertada");
                    return;
                }
                else
                {
                    Insercion(cu, Clave);
                }
            }

        }

        public void Insercion(Cubeta cu, int Clave)
        {
            for (int i = 0; i < RegistrosPorCubeta; i++)
            {
                if (cu.Valores[i] == -1 && cu.NumOcupados < RegistrosPorCubeta)
                {
                    //MessageBox.Show("Cond 1");
                    cu.Valores[i] = Clave;
                    cu.NumOcupados++;
                    Escribir_Cubeta(Archivo, cu); //Actualizar cubeta en archivo
                    Grid_Cubetas(Leer_Cubetas(Archivo));
                    return;
                }
                else if (cu.Valores[i] > Clave && cu.NumOcupados < RegistrosPorCubeta)
                {
                    //MessageBox.Show("Cond 2");
                    int Pasado = Clave;
                    for (int j = i; j < RegistrosPorCubeta; j++)
                    {
                        int aux = cu.Valores[j];
                        cu.Valores[j] = Pasado;
                        Pasado = aux;
                    }
                    cu.NumOcupados++;
                    Escribir_Cubeta(Archivo, cu); //Actualizar cubeta en archivo
                    Grid_Cubetas(Leer_Cubetas(Archivo));
                    return;
                }
                else if (cu.Valores[i] > Clave && cu.NumOcupados == RegistrosPorCubeta)
                {
                    //MessageBox.Show("Cond 3");
                    int Pasado = Clave;
                    for (int j = i; j < RegistrosPorCubeta; j++)
                    {
                        int aux = cu.Valores[j];
                        cu.Valores[j] = Pasado;
                        Pasado = aux;
                    }
                    //El Pasado tiene el ultimo mas grande, verificar si hay una siguiente cubeta, si no lo hay hacer la insercion de nueva cubeta
                    if (cu.SigCubeta == 0)
                    {
                        //MessageBox.Show("Cond 3-1");
                        int EOF = Leer_EOF(Archivo);
                        cu.SigCubeta = EOF;
                        Cubeta cu2 = new Cubeta(EOF, 1, 0, this.RegistrosPorCubeta, this.TamRegistro);
                        cu2.Valores[0] = Pasado;
                        Escribir_Cubeta(Archivo, cu); //Actualizar cubeta en archivo
                        Escribir_Cubeta(Archivo, cu2);//Insertar cubeta en archivo
                        Grid_Cubetas(Leer_Cubetas(Archivo));
                    }
                    else
                    {
                        //MessageBox.Show("Cond 3-2");
                        Escribir_Cubeta(Archivo, cu); //Actualizar cubeta en archivo
                        Cubeta cu2 = Leer_Cubeta(Archivo, cu.SigCubeta);
                        Insercion(cu2, Pasado);
                    }
                    return;
                }
                else if (cu.NumOcupados == RegistrosPorCubeta && i == RegistrosPorCubeta - 1)
                {
                    //MessageBox.Show("Cond 4");
                    if (cu.SigCubeta == 0)
                    {
                        //MessageBox.Show("Cond 4-1");
                        int EOF = Leer_EOF(Archivo);
                        cu.SigCubeta = EOF;
                        Cubeta cu2 = new Cubeta(EOF, 1, 0, this.RegistrosPorCubeta, this.TamRegistro);
                        cu2.Valores[0] = Clave;
                        Escribir_Cubeta(Archivo, cu); //Actualizar cubeta en archivo
                        Escribir_Cubeta(Archivo, cu2);//Insertar cubeta en archivo
                        Grid_Cubetas(Leer_Cubetas(Archivo));
                    }
                    else
                    {
                        //MessageBox.Show("Cond 4-2");
                        Cubeta cu2 = Leer_Cubeta(Archivo, cu.SigCubeta);
                        Insercion(cu2, Clave);
                    }
                    return;
                }
            }
        }

        public bool BuscarClave(Cubeta cu, int Clave)
        {
            //Retorno true si ya existe, falso si aun no se encuentra entre los insertados.
            if (cu != null)
            {
                for (int i = 0; i < cu.NumOcupados; i++)
                    if (cu.Valores[i] == Clave)
                        return true;

                if (cu.SigCubeta != 0)
                {
                    Cubeta Scu = Leer_Cubeta(Archivo, cu.SigCubeta);
                    return BuscarClave(Scu, Clave);
                }
                else
                    return false;
            }
            else
                return false;
        }

        #endregion

        //Listo
        #region Grid

        public void Grid_TablaDirecciones(TablaDireccion td)
        {
            DGV_Direcciones.Rows.Clear();
            for (int i = 0; i < td.NumZocalos; i++)
            {
                DataGridViewRow r = new DataGridViewRow();
                r.CreateCells(DGV_Direcciones);
                r.Cells[0].Value = i.ToString();
                r.Cells[1].Value = td.Zocalos[i].ToString();
                DGV_Direcciones.Rows.Add(r);
            }
        }

        public void Grid_Cubetas(List<Cubeta> cubetas)
        {
            DGV_Cubetas.Columns.Clear();
            DGV_Cubetas.Rows.Clear();
            DGV_Cubetas.Columns.Add("Direccion", "Dirección");
            DGV_Cubetas.Columns.Add("Ocupados", "Ocupados");
            DGV_Cubetas.Columns.Add("SigCubeta", "Sig. Cubeta");

            for (int i = 0; i < RegistrosPorCubeta; i++)
            {
                DGV_Cubetas.Columns.Add("R" + i.ToString(), "R" + i.ToString());
            }

            if (cubetas != null)
            {
                for (int i = 0; i < cubetas.Count; i++)
                {
                    DataGridViewRow r = new DataGridViewRow();
                    r.CreateCells(DGV_Cubetas);
                    r.Cells[0].Value = cubetas[i].Direccion.ToString();
                    r.Cells[1].Value = cubetas[i].NumOcupados.ToString();
                    r.Cells[2].Value = cubetas[i].SigCubeta.ToString();
                    for (int j = 0; j < RegistrosPorCubeta; j++)
                    {
                        r.Cells[j + 3].Value = cubetas[i].Valores[j].ToString();
                    }
                    DGV_Cubetas.Rows.Add(r);
                }
            }
        }


        #endregion

    }
}
