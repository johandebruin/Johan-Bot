using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Bot;

namespace JohanBot
{
    public class Clases
    {
        /// <summary>
        /// Añadir nuevas peliculas en la BD
        /// </summary>
        /// <param name="titulo">Título de la película</param>
        /// <param name="categoria">Nombre de la categoría</param>
        /// <param name="imagen">Nombre de la imagen</param>
        /// <param name="codigo1"></param>
        /// <param name="servidor">Nombre del servidor</param>
        /// <param name="descripcion">Descripción de la película</param>
        public static void Añadir(string titulo, string categoria, string imagen, string codigo1, string servidor,string descripcion)
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            diccionario.Add("1", "Comedia");
            diccionario.Add("2", "Terror");
            diccionario.Add("3", "Ciencia Ficcion");
            diccionario.Add("17", "Varios");
            diccionario.Add("5", "Animacion");
            diccionario.Add("7", "Aventura");
            diccionario.Add("8", "Accion");
            diccionario.Add("9", "Documental");
            diccionario.Add("14", "Drama");
            diccionario.Add("16", "Suspense");
            diccionario.Add("18", "Estrenos");
            diccionario.Add("19", "Series");
            diccionario.Add("20", "Belicas");
            diccionario.Add("21", "Deportes");
            diccionario.Add("22", "Fantasia y Ficcion");

            StringBuilder sb1 = new StringBuilder();
            if (codigo1.IndexOf("egavideo") > -1)
                sb1.Append("Megavideo,");
            if (codigo1.IndexOf("veoh") > -1)
                sb1.Append("Veoh,");
            if (codigo1.IndexOf("wuapi") > -1)
                sb1.Append("Wuapi,");
            sb1.Append(diccionario[categoria] + "," + titulo);

            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=./BD.mdb";
            MiembrosEstaticos.UploadFTP("./imagenes/" + imagen, "ftp://ftp.ocioseries.com/www.ocioseries.com/imagenes/caratulas/", "1967127@aruba.it", "6qyknogp");
            string codigo = "[center][img]http://www.ocioseries.com/imagenes/" + imagen + "[/img][/center] , " + descripcion + " , " + codigo1;
            OleDbConnection conexion = new OleDbConnection(strConexion);
            conexion.Open();
            OleDbCommand comando = new OleDbCommand();
            OleDbDataAdapter adaptador = new OleDbDataAdapter("INSERT INTO Orden " +
                "(titulo,codigo,tags,categoria) "+
                "VALUES ('" + titulo + "','" + codigo + "','" + sb1.ToString() + "','" + diccionario[categoria] + "')", conexion);
            DataSet conjunto = new DataSet();
            adaptador.Fill(conjunto);
            conexion.Close();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="imagen"> Es el nombre del titulo pero amigable + .jpg</param>
        public static int AñadirSerie(string titulo, string imagen)
        {
            //Obtenemos el ultimo ID
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=./Pelis.mdb";
            OleDbConnection conexion = new OleDbConnection(strConexion);
            string consulta = "SELECT id FROM series ORDER BY id DESC";
            OleDbCommand orden = new OleDbCommand(consulta, conexion);
            conexion.Open();
            OleDbDataReader lector = orden.ExecuteReader();
            lector.Read();
            int id = lector.GetInt32(0) + 1;
            lector.Close();

            //Lo insertamos
            OleDbConnection conexion2 = new OleDbConnection(strConexion);
            conexion2.Open();
            OleDbCommand comando2 = new OleDbCommand();
            OleDbDataAdapter adaptador = new OleDbDataAdapter("INSERT INTO series " +
                "(id,titulo,imagen) " +
                "VALUES ('" + id + "','" + titulo + "','" + imagen + "')", conexion2);
            DataSet conjunto = new DataSet();
            adaptador.Fill(conjunto);
            conexion2.Close();
            return id;
        }

        public static void AñadirCapitulo(string titulo, int serie, string url)
        {
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=./Pelis.mdb";
            OleDbConnection conexion2 = new OleDbConnection(strConexion);
            conexion2.Open();
            OleDbCommand comando2 = new OleDbCommand();
            OleDbDataAdapter adaptador = new OleDbDataAdapter("INSERT INTO capitulos " +
                "(Titulo, idSerie, url) " +
                "VALUES ('" + titulo + "','" + serie + "','" + url + "')", conexion2);
            DataSet conjunto = new DataSet();
            adaptador.Fill(conjunto);
            conexion2.Close();
        }

        public static bool ComprobarCapitulo(string url)
        {
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=./Pelis.mdb";
            OleDbConnection conexion = new OleDbConnection(strConexion);
            string consulta = "SELECT url FROM Capitulos";
            OleDbCommand orden = new OleDbCommand(consulta, conexion);
            conexion.Open();
            OleDbDataReader lector = orden.ExecuteReader();
            string dato;
            while (lector.Read())
            {
                dato = lector.GetString(0);
                if (dato == url)
                    return true;
            }
            lector.Close();
            return false;
        }

        public static int ComprobarSerie(string serie)
        {
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=./Pelis.mdb";
            OleDbConnection conexion = new OleDbConnection(strConexion);
            string consulta = "SELECT id,titulo FROM series";
            OleDbCommand orden = new OleDbCommand(consulta, conexion);
            conexion.Open();
            OleDbDataReader lector = orden.ExecuteReader();
            string dato;
            while (lector.Read())
            {
                dato = lector.GetString(1);
                if (dato == serie)
                    return (int)lector.GetInt32(0);
            }
            lector.Close();
            return 0;
        }
    }
}
