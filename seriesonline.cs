using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using Bot;
using HeatonResearch.Spider.HTML;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Threading;

namespace JohanBot
{
    class seriesonline
    {
        //Atributos
        static string ultima;
        public static string Servidor;

        //Métodos
        public class Win32
        {
            [DllImport("kernel32.dll")]
            public static extern Boolean AllocConsole();
            [DllImport("kernel32.dll")]
            public static extern Boolean FreeConsole();
        }

        static void Ultima()
        {
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=./Pelis.mdb";
            OleDbConnection conexion = new OleDbConnection(strConexion);
            string consulta = "SELECT ultima FROM SerieOnline";
            OleDbCommand orden = new OleDbCommand(consulta, conexion);
            conexion.Open();
            OleDbDataReader lector = orden.ExecuteReader();
            while (lector.Read())
                ultima = lector.GetString(0);
            lector.Close();
        }

        static string ObtenerUrl(string url)
        {
            string codigo = MiembrosEstaticos.DescargarCadena(new Uri(url), null);
            if (codigo.IndexOf("http://www.megavideo.com/?s=cinetube.es&amp;v=") > -1)
                return "http://www.megavideo.com/v/" + MiembrosEstaticos.Extraer(codigo, "http://www.megavideo.com/?s=cinetube.es&amp;v=", "&amp;k=peliculas-online", 0);
            //http://www.megavideo.com/?s=cinetube.es&v=FJOLBY0X&k=peliculas-online
            else if (codigo.IndexOf("http://www.megavideo.com/?s=cinetube.es&v=") > -1)
                return "http://www.megavideo.com/v/" + MiembrosEstaticos.Extraer(codigo, "http://www.megavideo.com/?s=cinetube.es&v=", "&k=peliculas-online", 0);
            else if (codigo.IndexOf("http://www.tusdescargasonline.com/freeflashplayer.php?code") > -1)
                return "http://www.tusdescargasonline.com/freeflashplayer.php?code" + MiembrosEstaticos.Extraer(codigo, "http://www.tusdescargasonline.com/freeflashplayer.php?code", ".flv", 0) + ".flv";
            //http://www.veoh.com/video/v14241999RhkKgBqH?source=cinetube&confirmed=1
            else if (codigo.IndexOf("http://www.veoh.com/video") > -1)
                return "http://www.veoh.com/video/" + MiembrosEstaticos.Extraer(codigo, "http://www.veoh.com/video/", "?source=", 0);
            
            Console.WriteLine("No se encontro el origen del embed :S");
            Console.ReadLine();
            return "0";
        }

        static void ObtenerIndice(string url, string titulo, string temporada, int idSerie)
        {
            Thread.Sleep(4000);
            string subEnlace;
            if (url.IndexOf("http://www.cinetube.es") > -1)
                subEnlace = url;
            else
                subEnlace = url.Replace("..", "http://www.cinetube.es").Replace("indices/", "subindices/i");
            string codigo = MiembrosEstaticos.DescargarCadena(new Uri(subEnlace), null);
            Analizador enlaces = new Analizador(subEnlace);
            string enlace;
            int i = 1;
            string[] espejos = new string[2];
            List<string> capitulos = new List<string>();
            List<string> nombreCapitulos = new List<string>();
            //Si son capitulos los vamos grabando en la BD, si es un espejo acumulamos el enlace para
            //rellamar a este procedimiento
            while ((enlace = enlaces.Leer("a","href")) != null)
            {
                string episodio = MiembrosEstaticos.Extraer(codigo, "Estilo17 Estilo16\">", "</a></td>", i++);
                if (episodio.IndexOf("irror") > -1)
                {
                    Console.WriteLine("Se encontro un espejo " + enlace);
                    espejos[i-2] = enlace.Replace("..","http://www.cinetube.es").Replace("indices/","subindices/i");
                }
                if (espejos[0] != null)
                {
                    continue;
                }
                else
                {
                    capitulos.Add(enlace.Replace("..", "http://www.cinetube.es"));
                    nombreCapitulos.Add(episodio);
                    Console.WriteLine("episodio: " + episodio);
                    Console.WriteLine("enlace: " + enlace);
                }
            }
            enlaces.Cerrar();
            if (espejos[0] != null)
            {
                ObtenerIndice(espejos[0], titulo, temporada, idSerie);
                ObtenerIndice(espejos[1], titulo, temporada, idSerie);
            }
            int o = 0;
            foreach (string x in capitulos)
            {
                string url2 = ObtenerUrl(x);
                if (!Clases.ComprobarCapitulo(url2) && url2 != "0")
                {
                    Console.WriteLine("obteniendo: " + x);
                    Console.WriteLine("urlSerie:" + url2);   
                    Clases.AñadirCapitulo(titulo + " " + temporada + " X " + nombreCapitulos[o++], idSerie, url2);
                }
                else
                    Console.WriteLine("El capitulo ya esta en la BD");   
            }
        }

        public static void Procesar()
        {
            Win32.AllocConsole();  // Abrir una consola
            //try
            //{
                Obtener();
            //}
            //catch
            //{
                //Console.WriteLine("Excepcion encontradaa");
            //}
            Console.WriteLine("FIN");
            Console.ReadLine();
            Win32.FreeConsole();   // Cerrar consola   
        }


        public static void Obtener()
        {
            Ultima();
            Console.WriteLine("Obteniendo Series");
            Analizador analizador = new Analizador("http://www.cinetube.es/subindices/iserienovedades.html");
            //Nos transladamos a la tabla con las series.
            MiembrosEstaticos.AvanzarA(analizador.html, "table", "class", "sample");
            int ch, i = 1, idSerie = 0;
            bool x = false;
            string codigo = MiembrosEstaticos.DescargarCadena(new Uri("http://www.cinetube.es/subindices/iserienovedades.html"), null);
            string imagen = null, enlace = null, titulo, descripcion, embed = null, temporada = null;
            while ((ch = analizador.html.Read()) != -1)
            {
                if (ch == 0)
                {
                    if (analizador.html.Tag.Name == "img")
                    {
                        imagen = analizador.html.Tag["src"];
                        Console.WriteLine("Imagen: " + imagen);
                    }
                    if (analizador.html.Tag.Name == "a" && enlace == null)
                    {
                        Servidor = "1";
                        enlace = analizador.html.Tag["href"];
                        //Si es la primera pelicula no registrada la registramos
                        if (enlace != ultima && !x)
                        {
                            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                "Data Source=./Pelis.mdb";
                            OleDbConnection conexion = new OleDbConnection(strConexion);
                            conexion.Open();
                            OleDbDataAdapter adaptador = new OleDbDataAdapter("UPDATE SerieOnline " +
                                "SET ultima = '" + enlace + "' WHERE id = 1", conexion);
                            DataSet conjunto = new DataSet();
                            adaptador.Fill(conjunto);
                            conexion.Close();
                            x = true;
                        }

                        if (enlace == ultima)
                            break;

                        Console.WriteLine("Enlace: " + enlace);
                        titulo = MiembrosEstaticos.Extraer(codigo, "more_link Estilo17 Estilo16\">", "</a>", i);
                        Console.WriteLine("titulo bruto: " + titulo);
                        //Averiguamos la temporada, puede ser por 01x12 o puede poner Temporada 0 tras los parentesis
                        temporada = MiembrosEstaticos.Extraer(titulo, "(", ")", 0);
                        Console.WriteLine("Temporada/episodio: " + temporada);
                        if (x != null)
                        {
                            if (temporada.IndexOf("x") > -1)
                                temporada = temporada.Substring(0, temporada.IndexOf("x"));
                        }
                        else
                            temporada = "Sin temporada";
                        Console.WriteLine("Temporada: " + temporada);

                        titulo = (titulo[0].ToString().ToUpper() + titulo.Substring(1)).Substring(0,titulo.IndexOf(" ("));
                        titulo = MiembrosEstaticos.tituloAmigable(titulo);
                        Console.WriteLine("Titulo: " + titulo);
                        //Buscamos a ver si la serie esta incluida en la BD
                        if ((idSerie = Clases.ComprobarSerie(titulo)) == 0)
                        {
                            //Agregamos la serie a la BD.
                            MiembrosEstaticos.DescargarBinario(new Uri(imagen), "./imagenes/" + MiembrosEstaticos.tituloAmigable(titulo) + ".jpg");
                            idSerie = Clases.AñadirSerie(titulo, MiembrosEstaticos.tituloAmigable(titulo) + ".jpg");
                            Console.WriteLine("La serie no esta en la BD, la hemos añadido");
                        }
                        else
                            Console.WriteLine("Encontramos la serie");
                        Console.WriteLine("Id Serie: " + idSerie);
                        Console.WriteLine("Titulo: " + titulo);
                        ObtenerIndice(enlace, titulo, temporada, idSerie);
                        imagen = enlace = titulo = descripcion = embed = null;
                        i++;
                        Console.WriteLine("Hecho");
                    }
                }
            }
        }
    }
}
