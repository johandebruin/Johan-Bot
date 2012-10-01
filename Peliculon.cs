using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using HeatonResearch.Spider.HTML;
using Bot;

namespace JohanBot
{
    public class Peliculon
    {
        //Listas de atributos
        static List<string> enlace = new List<string>();
        static List<string> titulo = new List<string>();
        static List<string> descripcion = new List<string>();
        static List<string> categoria = new List<string>();
        static List<string> imagen = new List<string>();
        static List<string> embed = new List<string>();
        static List<string> servidor = new List<string>();
        static string ultima;

        public class Win32
        {
            [DllImport("kernel32.dll")]
            public static extern Boolean AllocConsole();
            [DllImport("kernel32.dll")]
            public static extern Boolean FreeConsole();
        }

        static string Enlace(ParseHTML analizador)
        {
            int ch;
            while ((ch = analizador.Read()) != -1)
            {
                if (ch == 0)
                    if (analizador.Tag.Name == "a")
                        return analizador.Tag["href"];
            }
            return "no se encontro enlaces :S";
        }

        static string Titulo(ParseHTML analizador)
        {
            int ch;
            StringBuilder buffer = new StringBuilder();
            while ((ch = analizador.Read()) != -1)
            {
                if (ch > 0)
                {
                    buffer.Append((char)ch);
                }
                else
                    return buffer.ToString();
            }
            return "No se encontro el titulo :S";
        }

        static string Categoria(ParseHTML analizador)
        {
            int ch;
            bool leer = false;
            StringBuilder buffer = new StringBuilder();
            while ((ch = analizador.Read()) != -1)
            {
                if (ch == 0)
                {
                    if (analizador.Tag.Name == "a")
                        leer = true;
                    else if (analizador.Tag.Name == "/a")
                        return buffer.ToString();
                }
                else if (leer)
                    buffer.Append((char)ch);
            }
            return "no se encontro la categoria :S";
        }

        static string Imagen(ParseHTML analizador)
        {
            int ch;
            while ((ch = analizador.Read()) != -1)
            {
                if (ch == 0)
                    if (analizador.Tag.Name == "img")
                        return analizador.Tag["src"];
            }
            return "no se encontro una imagen :S";
        }

        static string Descripcion(ParseHTML analizador)
        {
            int ch;
            StringBuilder buffer = new StringBuilder();
            while ((ch = analizador.Read()) > 0)
                buffer.Append((char)ch);
            return buffer.ToString();

        }

        static string ProcesarCategoria(string categoria)
        {
            switch (categoria)
            {
                case "Peliculas de Accion":
                    return "8";
                case "Peliculas de Animacion":
                    return "5";
                case "Peliculas de Aventura":
                    return "7";
                case "Peliculas de Ciencia Ficción":
                    return "3";
                case "Peliculas de Comedia":
                    return "1";
                case "Peliculas de Drama":
                    return "14";
                case "Peliculas de Estrenos":
                    return "18";
                case "Peliculas de Infantil":
                    return "5";
                case "Peliculas de Intriga":
                    return "16";
                case "Películas de Latino":
                    return "17";
                case "Peliculas de Suspense":
                    return "16";
                case "Peliculas de Terror":
                    return "2";
                default:
                    return "17";

            }
        }

        static void ProcesarVeoh(String url)
        {
            String codigo = url.Substring("javascript:veoh('".Length, 17);
            servidor.Add("2");
            embed.Add("<embed src=\"http://www.veoh.com/veohplayer.swf?permalinkId=" + codigo + "&id=anonymous&player=videodetailsembedded&videoAutoPlay=0\" allowFullScreen=\"true\" width=\"425\" height=\"350\" bgcolor=\"#FFFFFF\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>");
        }

        static void ProcesarMegavideo(String url)
        {
            String codigo = url.Substring("javascript:mvideo('".Length, 8);
            servidor.Add("1");
            embed.Add("<object width=\"425\" height=\"350\"><param name=\"movie\" value=\"http://www.megavideo.com/v/" + codigo + "\"></param><param name=\"allowFullScreen\" value=\"true\"></param><embed src=\"http://www.megavideo.com/v/" + codigo + "\" type=\"application/x-shockwave-flash\" allowfullscreen=\"true\" width=\"425\" height=\"350\"></embed></object>");
        }

        static void ProcesarStage(string url)
        {
            //javascript:link('http://stagevu.com/video/plzqyccwfpoz');
            String codigo = url.Substring("javascript:link('http://stagevu.com/video/".Length, 12);
            servidor.Add("4");
            embed.Add("<h2><a href=\"" + codigo + "\">Ver pelicula online</a></h2>");
        }

        static bool Embed(int i)
        {
            Analizador analizador = new Analizador(enlace[i]);
            if (MiembrosEstaticos.AvanzarA(analizador.html, "img", "src", "http://www.peliculon.net/ver-partes.jpg"))
            {
                int ch;
                while ((ch = analizador.html.Read()) != -1)
                {
                    if (ch == 0)
                    {
                        //si es un salto de linea se acabaron las partes
                        //if (analizador.Tag.Name == "br")
                        //break;
                        //Por cada ancla que se encuentre hasta entonces, una parte
                        if (analizador.html.Tag.Name == "a")
                        {
                            //Console.WriteLine(analizador.html.Tag["href"].ToString());
                            if (analizador.html.Tag["href"].StartsWith("javascript:veoh"))
                                ProcesarVeoh(analizador.html.Tag["href"]);
                            else if (analizador.html.Tag["href"].IndexOf("mvideo") > -1)
                                ProcesarMegavideo(analizador.html.Tag["href"]);
                            else if (analizador.html.Tag["href"].StartsWith("javascript:link('http://stagevu.com"))
                                ProcesarStage(analizador.html.Tag["href"]);
                            else
                            {
                                analizador.Cerrar();
                                break;
                            }
                        }
                    }
                }
                analizador.Cerrar();
            }
            return true;
        }

        public static void Obtener()
        {
            Analizador analizador = new Analizador("http://www.peliculon.net/");
            do
            {
                MiembrosEstaticos.AvanzarA(analizador.html, "div", "class", "post");
                enlace.Add(Enlace(analizador.html));
                titulo.Add(MiembrosEstaticos.tituloAmigable(Titulo(analizador.html).Trim()));
                Console.WriteLine("Obteniendo película: " + titulo[titulo.Count - 1]);
                categoria.Add(ProcesarCategoria(Categoria(analizador.html)));
                imagen.Add(Imagen(analizador.html));
                MiembrosEstaticos.DescargarBinario(new Uri(imagen[imagen.Count-1]), "C:/imagenes/" + titulo[titulo.Count-1] + ".jpg");
                imagen[imagen.Count - 1] = titulo[titulo.Count - 1] + ".jpg";
                descripcion.Add(Descripcion(analizador.html));
                //Console.ReadLine();
                Embed(titulo.Count - 1);
            }
            while (enlace[enlace.Count - 1] != ultima);
            analizador.Cerrar();
        }

        static void Ultima()
        {
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=c:/Pelis.mdb";
            OleDbConnection conexion = new OleDbConnection(strConexion);
            string consulta = "SELECT ultima FROM Peliculon";
            OleDbCommand orden = new OleDbCommand(consulta, conexion);
            conexion.Open();
            OleDbDataReader lector = orden.ExecuteReader();
            while (lector.Read())
                ultima = lector.GetString(0);
            lector.Close();
        }

        static void Añadir()
        {
            for (int i = 0; i < titulo.Count; i++)
            {
                if (enlace[i] != ultima)
                    Clases.Añadir(titulo[i], categoria[i], imagen[i], embed[i], servidor[i], descripcion[i]);
            }
        }

        static void Grabar()
        {
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=c:/Pelis.mdb";
            OleDbConnection conexion = new OleDbConnection(strConexion);
            conexion.Open();
            OleDbDataAdapter adaptador = new OleDbDataAdapter("UPDATE Peliculon " +
                "SET ultima = '" + enlace[0] + "' WHERE id = 1",conexion);
            DataSet conjunto = new DataSet();
            adaptador.Fill(conjunto);
            conexion.Close();
        }
        
        public static void procesar()
        {
            Win32.AllocConsole();  // Abrir una consola

            Ultima();
            Obtener();
            Console.WriteLine("Se encontro la última película grabada");
            if (titulo.Count > 1)
            {
                Añadir();
                Grabar();
            }

            Win32.FreeConsole();   // Cerrar consola
        }
    }
}