using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace JohanBot
{
    public class Agregar
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
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=./Pelis.mdb";
            OleDbConnection conexion = new OleDbConnection(strConexion);
            conexion.Open();
            OleDbCommand comando = new OleDbCommand();
            OleDbDataAdapter adaptador = new OleDbDataAdapter("INSERT INTO Peliculas " +
                "(titulo,categoria,descripcion,imagen,codigo1,servidor) "+
                "VALUES ('" + titulo + "','" + categoria + "','" + descripcion + "','" + imagen + "','" + codigo1 + "','" + servidor + "')", conexion);
            DataSet conjunto = new DataSet();
            adaptador.Fill(conjunto);
            conexion.Close();
        }
    }
}
