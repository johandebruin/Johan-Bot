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

namespace JohanBot
{
    class CineTube
    {
        public static string Servidor;
        static string ultima;

        public class Win32
        {
            [DllImport("kernel32.dll")]
            public static extern Boolean AllocConsole();
            [DllImport("kernel32.dll")]
            public static extern Boolean FreeConsole();
        }

        static string ObtenerEmbed(string url)
        {
            if (url.IndexOf("..") > -1)
                url = url.Replace("..", "http://www.cinetube.es");
            string codigo = MiembrosEstaticos.DescargarCadena(new Uri(url), null), referencia = null;
            if (codigo.IndexOf("http://www.megavideo.com/?v") > -1)
                referencia = MiembrosEstaticos.Extraer(codigo, "http://www.megavideo.com/?v=", "&", 0);
            else if (codigo.IndexOf("http://www.megavideo.com/?d") > -1)
                referencia = MiembrosEstaticos.Extraer(codigo, "http://www.megavideo.com/?d=", "&", 0);
            //src="http://www.megavideo.com/v/5OV0FH592bc4aa85291ad53c2f82b7f9e94b104c" type=
            else if (codigo.IndexOf("http://www.megavideo.com/v") > -1)
                referencia = MiembrosEstaticos.Extraer(codigo, "http://www.megavideo.com/v/", "\"", 0);
            //http://www.megavideo.com/?s=cinetube.es&v=5OV0FH59&k
            else if (codigo.IndexOf("megavideo.com/?s") > -1)
                referencia = MiembrosEstaticos.Extraer(codigo, "megavideo.com/?s=cinetube.es&v=", "&", 0);

            Console.WriteLine("referencia:" + referencia);
            return "http://www.megavideo.com/v/" + referencia;                
        }

        static string ObtenerWuapi(string url)
        {
            if (url.IndexOf("..") > -1)
                url = url.Replace("..", "http://www.cinetube.es");
            string codigo = MiembrosEstaticos.DescargarCadena(new Uri(url), null);
            string enl = MiembrosEstaticos.Extraer(codigo,"enlce\" --><a href=\"","\" target=\"_blank\" class=\"more_link\">pincha aqu", 0);
            Console.WriteLine("enlace Wuapi: " + enl);
            return enl;

        }

        static string ObtenerVeoh(string url)
        {
            if (url.IndexOf("..") > -1)
                url = url.Replace("..", "http://www.cinetube.es");
            string codigo = MiembrosEstaticos.DescargarCadena(new Uri(url), null);
            string embed2 = null;
            if (codigo.ToLower().IndexOf("parte 1") > -1)
            {
                string cod = MiembrosEstaticos.Extraer(codigo, "videodetailsembedded&amp;videoAutoPlay=0&amp;type=v&amp;permalinkId=", "&amp;id", 1);
                embed2 = "http://www.veoh.com/browse/videos/category/educational/watch/" + cod;
                Console.WriteLine("Parte 1: " + cod);
            }
            if (codigo.ToLower().IndexOf("parte 2") > -1)
            {
                string cod = MiembrosEstaticos.Extraer(codigo, "videodetailsembedded&amp;videoAutoPlay=0&amp;type=v&amp;permalinkId=", "&amp;id", 2);
                embed2 += "http://www.veoh.com/browse/videos/category/educational/watch/" + cod + ",";
                Console.WriteLine("parte 2: " + cod);
            }
            if (codigo.ToLower().IndexOf("parte 3") > -1)
            {
                string cod = MiembrosEstaticos.Extraer(codigo, "videodetailsembedded&amp;videoAutoPlay=0&amp;type=v&amp;permalinkId=", "&amp;id", 3);
                embed2 += "http://www.veoh.com/browse/videos/category/educational/watch/" + cod + ",";
                Console.WriteLine("parte 3: " + cod);
            }
            if (codigo.ToLower().IndexOf("parte 4") > -1)
            {
                string cod = MiembrosEstaticos.Extraer(codigo, "videodetailsembedded&amp;videoAutoPlay=0&amp;type=v&amp;permalinkId=", "&amp;id", 4);
                embed2 += "http://www.veoh.com/browse/videos/category/educational/watch/" + cod + ",";
                Console.WriteLine("parte 4: " + cod);
            }
            if (codigo.ToLower().IndexOf("parte 5") > -1)
            {
                string cod = MiembrosEstaticos.Extraer(codigo, "videodetailsembedded&amp;videoAutoPlay=0&amp;type=v&amp;permalinkId=", "&amp;id", 5);
                embed2 += "http://www.veoh.com/browse/videos/category/educational/watch/" + cod + ",";
                Console.WriteLine("parte 5: " + cod);
            }
            if (codigo.ToLower().IndexOf("parte 6") > -1)
            {
                string cod = MiembrosEstaticos.Extraer(codigo, "videodetailsembedded&amp;videoAutoPlay=0&amp;type=v&amp;permalinkId=", "&amp;id", 6);
                embed2 += "http://www.veoh.com/browse/videos/category/educational/watch/" + cod;
                Console.WriteLine("parte 6: " + cod);
            }
            return embed2;
        }

        static string ObtnerIndice(string url)
        {
            string subEmbed = null;
            string enlace = url.Substring(url.LastIndexOf("/")+1);
            //../subindices/i60segundos.html
            enlace = "http://www.cinetube.es/subindices/i" + enlace;
            string codigo = MiembrosEstaticos.DescargarCadena(new Uri(enlace), null);
            int i = 1;
            while (true)
            {
                enlace = MiembrosEstaticos.Extraer(codigo, "<a href=\"", "\" target=", i);
                string servidor = MiembrosEstaticos.Extraer(codigo, "Estilo17 Estilo16\">", "</a>", i++);
                if (servidor == null)
                    break;
                Console.WriteLine("Servidor: " + servidor);
                Console.WriteLine("Enlace: " + enlace);
                servidor = servidor.Substring(servidor.IndexOf("("));
                if (servidor.IndexOf("megavideo") > -1)
                {
                    subEmbed += ObtenerEmbed(enlace) + ",";
                    Servidor = "1";
                }
                else if (servidor.IndexOf("veoh") > -1)
                {
                    subEmbed += ObtenerVeoh(enlace) + ",";
                    Servidor = "2";
                }
                else if (servidor.ToLower().IndexOf("wuapi") > -1)
                {
                    subEmbed += ObtenerWuapi(enlace) + ",f";
                    Servidor = "2";
                }
            }

            return subEmbed;
        }

        static void Ultima()
        {
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=./Pelis.mdb";
            OleDbConnection conexion = new OleDbConnection(strConexion);
            string consulta = "SELECT ultima FROM CineTube";
            OleDbCommand orden = new OleDbCommand(consulta, conexion);
            conexion.Open();
            OleDbDataReader lector = orden.ExecuteReader();
            while (lector.Read())
                ultima = lector.GetString(0);
            lector.Close();
        }

        static void Obtener()
        {
            Ultima();
            bool x = false;
            string codigo = MiembrosEstaticos.DescargarCadena(new Uri("http://www.cinetube.es/subindices/inovedades.html"), null);
            string imagen = null,enlace = null, titulo, descripcion, embed = null;
            int i = 1;
            Analizador analizador = new Analizador("http://www.cinetube.es/subindices/inovedades.html");
            MiembrosEstaticos.AvanzarA(analizador.html, "table", "class", "sample");
            int ch;
            while ((ch = analizador.html.Read()) != -1)
            {
                if (ch == 0)
                {
                    if (analizador.html.Tag.Name == "img" && imagen == null)
                    {
                        imagen = analizador.html.Tag["src"];
                        Console.WriteLine("Imagen: "+imagen);
                    }
                    if (analizador.html.Tag.Name == "a" && enlace == null)
                    {
                        Servidor = "1";
                        enlace = analizador.html.Tag["href"];
                        if (enlace != ultima && !x)
                        {
                            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                "Data Source=./Pelis.mdb";
                            OleDbConnection conexion = new OleDbConnection(strConexion);
                            conexion.Open();
                            OleDbDataAdapter adaptador = new OleDbDataAdapter("UPDATE CineTube " +
                                "SET ultima = '" + enlace + "' WHERE id = 1", conexion);
                            DataSet conjunto = new DataSet();
                            adaptador.Fill(conjunto);
                            conexion.Close();
                            x = true;
                        }
                        if (enlace == ultima)
                            break;
                        Console.WriteLine("Enlace: " + enlace);
                        if (enlace.IndexOf("online") > -1)
                            embed = ObtenerEmbed(enlace);
                        else if (enlace.IndexOf("indices") > -1)
                            embed = ObtnerIndice(enlace);
                        Console.WriteLine("Embed: " + embed);
                        titulo = MiembrosEstaticos.Extraer(codigo, "more_link Estilo17 Estilo16\">", "</a>", i);
                        titulo = titulo[0].ToString().ToUpper() + titulo.Substring(1);
                        titulo = MiembrosEstaticos.tituloAmigable(titulo);
                        Console.WriteLine("Titulo: " + titulo);

                        // No olvidar el i++
                        descripcion = MiembrosEstaticos.Extraer(codigo, "class=\"cover\" align=\"left\">SINOPSIS: ", "</div>", i++).Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n").Replace("É", "E").Replace("Á", "A").Replace("Í", "Í").Replace("Ó", "O").Replace("Ú", "Ú").Replace("'", "").Replace(",","");
                        descripcion = descripcion[0].ToString().ToUpper() + descripcion.Substring(1);
                        MiembrosEstaticos.DescargarBinario(new Uri(imagen),"./imagenes/" + titulo + ".jpg");
                        if (embed != null)
                            Clases.Añadir(titulo, "17", titulo + ".jpg", embed, Servidor, descripcion);

                        imagen = enlace = titulo = descripcion = embed = null;
                        MiembrosEstaticos.Avanzar(analizador.html, "tr", 0);
                    }
                }
            }
        }

        public static void Procesar()
        {
            Win32.AllocConsole();  // Abrir una consola
            try
            {
                Obtener();
            }
            catch
            {
                Console.WriteLine("Excepcion encontradaa");
            }
            Console.WriteLine("FIN");

            Win32.FreeConsole();   // Cerrar consola   
        }
    }
}
