using System;
using System.Collections.Generic;
using System.Text;
using Bot;
using HeatonResearch.Spider.HTML;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.OleDb;

namespace JohanBot
{
    class NewCine
    {
        static string ultima;

        public class Win32
        {
            [DllImport("kernel32.dll")]
            public static extern Boolean AllocConsole();
            [DllImport("kernel32.dll")]
            public static extern Boolean FreeConsole();
        }

        static string ProcesarCategoria(string categoria)
        {
            categoria = categoria.ToLower();
            if (categoria.IndexOf("acci") > -1)
                return "8";
            if (categoria.IndexOf("series") > -1)
                return null;
            if (categoria.IndexOf("anima") > -1)
                return "5";
            if (categoria.IndexOf("avent") > -1)
                return "7";
            if (categoria.IndexOf("ciencia") > -1)
                return "3";
            if (categoria.IndexOf("comedi") > -1)
                return "1";
            if (categoria.IndexOf("drama") > -1)
                return "14";
            if (categoria.IndexOf("infan") > -1)
                return "5";
            if (categoria.IndexOf("susp") > -1)
                return "16";
            if (categoria.IndexOf("intri") > -1)
                return "16";
            if (categoria.IndexOf("extre") > -1)
                return "8";
            if (categoria.IndexOf("terror") > -1 || categoria.IndexOf("miedo") > -1 || categoria.IndexOf("thri") > -1)
                return "2";
            else
                return "17";
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
            return null;
        }

        static string Titulo(ParseHTML analizador)
        {
            int ch;
            StringBuilder sb1 = new StringBuilder();
            while ((ch = analizador.Read()) != -1)
                if (ch > 0)
                    sb1.Append((char)ch);
                else
                    return sb1.ToString();
            return null;
        }

        static string Imagen(ParseHTML analizador)
        {
            int ch, x = 0;
            while ((ch = analizador.Read()) != -1)
            {
                if (ch == 0)
                    if (analizador.Tag.Name == "img")
                        if (x == 3)
                        {
                            Console.WriteLine(analizador.Tag["src"]);
                            return analizador.Tag["src"];
                        }
                        else
                            x++;
            }
            return null;
        }

        static string Categoria(ParseHTML analizador)
        {
            int ch;
            bool x = false;
            while ((ch = analizador.Read()) != -1)
            {
                if (ch == 0)
                {
                    if (analizador.Tag.Name == "a")
                    {
                        if (x)
                            return analizador.Tag["href"];
                        else
                            x = true;
                    }
                }
            }
            return null;
        }

        static string Descripcion(ParseHTML analizador)
        {
            int ch;
            StringBuilder sb1 = new StringBuilder();
            while ((ch = analizador.Read()) != -1)
            {
                if (ch > 0)
                    sb1.Append((char)ch);
                else
                    return sb1.ToString().Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n").Replace("É", "E").Replace("Á", "A").Replace("Í", "Í").Replace("Ó", "O").Replace("Ú", "Ú").Replace("'","");
            }
            return null;
        }

        static string[] Embed(string url)
        {
            string[] x = new String[2];
            string codigo = MiembrosEstaticos.DescargarCadena(new Uri(url), null);
            if ((codigo.ToLower().IndexOf("megavdeo") > -1) || (codigo.ToLower().IndexOf("megavideo-logo") > -1) || (codigo.ToLower().IndexOf("http://www.megavideo.com") > -1))
            {
                string cod = MiembrosEstaticos.Extraer(codigo, "src=\"http://www.megavideo.com/v/", "\" type=", 0);
                x[0] = "<object width=\"425\" height=\"350\"><param name=\"movie\" value=\"http://www.megavideo.com/v/" + cod + "\"></param><param name=\"allowFullScreen\" value=\"true\"></param><embed src=\"http://www.megavideo.com/v/" + cod + "\" type=\"application/x-shockwave-flash\" allowfullscreen=\"true\" width=\"425\" height=\"350\"></embed></object>";
                x[1] = "1";
                return x;
            }
            else if (codigo.ToLower().IndexOf("<br /><br /><embed src=\"http://www.veoh.com/veohplayer.swf?permalinkid=") > -1)
            {
                string cod = MiembrosEstaticos.Extraer(codigo, "<br /><br /><embed src=\"http://www.veoh.com/veohplayer.swf?permalinkid=", "&id=anonymous", 0);
                x[0] = "<embed src=\"http://www.veoh.com/veohplayer.swf?permalinkId=" + cod + "&id=anonymous&player=videodetailsembedded&videoAutoPlay=0\" allowFullScreen=\"true\" width=\"425\" height=\"350\" bgcolor=\"#FFFFFF\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>";
                x[1] = "2";
                return x;
            }
            else if (codigo.ToLower().IndexOf("http://www.newcineonline.com/veoh-logo.jpg") > -1)
            {
                if (codigo.IndexOf("PARTE 1<br />") > -1)
                {
                    string cod = MiembrosEstaticos.Extraer(codigo, "PARTE 1<br /><embed src=\"http://www.veoh.com/veohplayer.swf?player=videodetailsembedded&videoAutoPlay=0&type=v&permalinkId=", "&id=", 0);
                    x[0] = "<embed src=\"http://www.veoh.com/veohplayer.swf?permalinkId=" + cod + "&id=anonymous&player=videodetailsembedded&videoAutoPlay=0\" allowFullScreen=\"true\" width=\"425\" height=\"350\" bgcolor=\"#FFFFFF\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>";
                    x[1] = "2";
                }
                if (codigo.IndexOf("PARTE 2<br />") > -1)
                {
                    string cod = MiembrosEstaticos.Extraer(codigo, "PARTE 2<br /><embed src=\"http://www.veoh.com/veohplayer.swf?player=videodetailsembedded&videoAutoPlay=0&type=v&permalinkId=", "&id=", 0);
                    x[0] += "<br><br><embed src=\"http://www.veoh.com/veohplayer.swf?permalinkId=" + cod + "&id=anonymous&player=videodetailsembedded&videoAutoPlay=0\" allowFullScreen=\"true\" width=\"425\" height=\"350\" bgcolor=\"#FFFFFF\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>";
                }
                if (codigo.IndexOf("PARTE 3<br />") > -1)
                {
                    string cod = MiembrosEstaticos.Extraer(codigo, "PARTE 3<br /><embed src=\"http://www.veoh.com/veohplayer.swf?player=videodetailsembedded&videoAutoPlay=0&type=v&permalinkId=", "&id=", 0);
                    x[0] += "<br><br><embed src=\"http://www.veoh.com/veohplayer.swf?permalinkId=" + cod + "&id=anonymous&player=videodetailsembedded&videoAutoPlay=0\" allowFullScreen=\"true\" width=\"425\" height=\"350\" bgcolor=\"#FFFFFF\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>";
                }
                if (codigo.IndexOf("PARTE 4<br />") > -1)
                {
                    string cod = MiembrosEstaticos.Extraer(codigo, "PARTE 4<br /><embed src=\"http://www.veoh.com/veohplayer.swf?player=videodetailsembedded&videoAutoPlay=0&type=v&permalinkId=", "&id=", 0);
                    x[0] += "<br><br><embed src=\"http://www.veoh.com/veohplayer.swf?permalinkId=" + cod + "&id=anonymous&player=videodetailsembedded&videoAutoPlay=0\" allowFullScreen=\"true\" width=\"425\" height=\"350\" bgcolor=\"#FFFFFF\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>";
                }
                if (codigo.IndexOf("PARTE 5<br />") > -1)
                {
                    string cod = MiembrosEstaticos.Extraer(codigo, "PARTE 53<br /><embed src=\"http://www.veoh.com/veohplayer.swf?player=videodetailsembedded&videoAutoPlay=0&type=v&permalinkId=", "&id=", 0);
                    x[0] += "<br><br><embed src=\"http://www.veoh.com/veohplayer.swf?permalinkId=" + cod + "&id=anonymous&player=videodetailsembedded&videoAutoPlay=0\" allowFullScreen=\"true\" width=\"425\" height=\"350\" bgcolor=\"#FFFFFF\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>";
                }
                if (x[0] != null)
                    return x;
            }
            else if (codigo.IndexOf("wuapi-logo.jpg") > -1)
            {
                x[0] = "false";
                x[1] = "";
                return x;
            }
            else if (codigo.IndexOf("http://stagevu.com/video") > -1)
            {
                //http://stagevu.com/video/fjdqvridcume
                string enlace = codigo.Substring(codigo.IndexOf("http://stagevu.com/video/")+"http://stagevu.com/video/".Length, 12);
                x[0] = "<a href=http://stagevu.com/video/" + enlace + ">Ver online</a>";
                x[1] = "2";
                return x;
            }
            else if (codigo.IndexOf("http://www.zshare.net/video/") > -1)
            {
                //http://www.zshare.net/video/5637331209c2cf02/
                string enlace = codigo.Substring(codigo.IndexOf("http://www.zshare.net/video/") + "http://www.zshare.net/video/".Length, 16);
                x[0] = "<a href=http://www.zshare.net/video/" + enlace + ">Ver online</a>";
                x[1] = "2";
                return x;
            }
            //http://www.tu.tv/
            else if (codigo.IndexOf("http://www.tu.tv/tutvweb.swf") > -1)
            {
                //value="http://www.tu.tv/tutvweb.swf?kpt=aHR0cDovL3d3dy50dS50di92aWRlb3Njb2RpL24vYS9uYXppcy11bi1hdmlzby1kZS1sYS1oaXN0b3JpYS0xLTYtbGEtbC5mbHY=&amp
                string enlace = codigo.Substring(codigo.IndexOf("http://www.tu.tv/tutvweb.swf?kpt=") + "http://www.tu.tv/tutvweb.swf?kpt=".Length, "aHR0cDovL3d3dy50dS50di92aWRlb3Njb2RpL24vYS9uYXppcy11bi1hdmlzby1kZS1sYS1oaXN0b3JpYS0xLTYtbGEtbC5mbHY".Length);
                x[0] = "<a href=http://www.tu.tv/tutvweb.swf?kpt=" + enlace + ">Ver online</a>";
                x[1] = "2";
                return x;
            }
            else if (codigo.IndexOf("http://beta.vreel.net/") > -1)
            {
                x[0] = "false";
                x[1] = "";
                return x;
            }
            else
            {
                Console.WriteLine("Se encontro un codigo que no es de Megavideo");
                Console.ReadLine();
            }
            return null;
        }

        static void Ultima()
        {
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=./Pelis.mdb";
            OleDbConnection conexion = new OleDbConnection(strConexion);
            string consulta = "SELECT ultima FROM Peliculon";
            OleDbCommand orden = new OleDbCommand(consulta, conexion);
            conexion.Open();
            OleDbDataReader lector = orden.ExecuteReader();
            while (lector.Read())
                ultima = lector.GetString(0);
            lector.Close();
        }

        public static void Obtener()
        {

            Win32.AllocConsole();  // Abrir una consola
            int estado = 0;
            Ultima();
            for (int i = 1; i < 221; i++)
            {
                if (estado == 2)
                    break;
                Analizador analizador = new Analizador("http://www.newcineonline.com/page/" + i + "/");
                MiembrosEstaticos.AvanzarA(analizador.html, "div", "class", "post-title");

                while (true)
                {
                    try
                    {
                        MiembrosEstaticos.AvanzarA(analizador.html, "div", "class", "post-title");
                        string enlace, titulo, categoria, imagen, descripcion;
                        string[] emb = new string[2];
                        enlace = Enlace(analizador.html);
                        if (enlace == ultima)
                        {
                            Console.WriteLine("SE encontro coincidencia");
                            estado = 2;
                            break;
                        }
                        if (estado == 0)
                        {
                            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                "Data Source=./Pelis.mdb";
                            OleDbConnection conexion = new OleDbConnection(strConexion);
                            conexion.Open();
                            OleDbDataAdapter adaptador = new OleDbDataAdapter("UPDATE Peliculon " +
                                "SET ultima = '" + enlace + "' WHERE id = 1", conexion);
                            DataSet conjunto = new DataSet();
                            adaptador.Fill(conjunto);
                            conexion.Close();
                            estado = 1;
                        }
                        //Si es una serie nos la saltamos
                        if (enlace.IndexOf("series") > -1)
                            continue;
                        //Console.WriteLine(enlace);
                        titulo = MiembrosEstaticos.tituloAmigable(Titulo(analizador.html));
                        Console.WriteLine("titulo: " + titulo);
                        MiembrosEstaticos.Avanzar(analizador.html, "a", 0);
                        categoria = ProcesarCategoria(Categoria(analizador.html));
                        imagen = Imagen(analizador.html);
                        MiembrosEstaticos.DescargarBinario(new Uri(imagen), "./imagenes/" + titulo + ".jpg");
                        imagen = titulo + ".jpg";
                        MiembrosEstaticos.Avanzar(analizador.html, "/b", 4);
                        descripcion = Descripcion(analizador.html);
                        if (enlace.IndexOf("anime") > -1)
                        {
                            Console.WriteLine("Encontrado un anime");
                            continue;
                        }
                        emb = Embed(enlace);
                        Console.WriteLine("Embed: "+emb[0]);
                        //Console.WriteLine(emb[1]);
                        //Ahora grabamos todoooo
                        if (emb[0] != "false")
                            Clases.Añadir(titulo, categoria, imagen, emb[0], emb[1], descripcion);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        break;
                    }
                }
                Console.WriteLine("FIN de pagina " + i);
                analizador.Cerrar();
            }
            Console.WriteLine("FIN");

            Win32.FreeConsole();   // Cerrar consola            
        }
    }
}
