using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Bot;

namespace JohanBot
{
    public partial class AgregarSQL : Form
    {
        public AgregarSQL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=./Pelis.mdb";
            OleDbConnection conexion = new OleDbConnection(strConexion);
            conexion.Open();
            OleDbDataAdapter adaptador = new OleDbDataAdapter();
            adaptador.SelectCommand = new OleDbCommand(textBox1.Text, conexion);
            DataSet conjunto = new DataSet();
            adaptador.Fill(conjunto);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string texto = textBox1.Text.Replace("&ntilde;", "n").Replace("&acute;", "a").Replace("&iexcl;", "").Replace("&nbsp;", "").Replace("&#180;", "").Replace(";s","").Replace("&#161;","").Replace("&#189;","").Replace("&#191;","").Replace("&#8217;","").Replace(";&","").Replace("p;","").Replace("l;","").Replace("s);","s").Replace("):","");
            int i = 0;
            while (true)
            {
                try
                {
                    i++;
                    string s = MiembrosEstaticos.Extraer(texto, "INSERT", "');", i);
                    string x = "INSERT" + s + "')";
                    x = x.Replace(";", "");
                    string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                        "Data Source=./Pelis.mdb";
                    OleDbConnection conexion = new OleDbConnection(strConexion);
                    conexion.Open();
                    OleDbDataAdapter adaptador = new OleDbDataAdapter();
                    adaptador.SelectCommand = new OleDbCommand(x, conexion);
                    DataSet conjunto = new DataSet();
                    adaptador.Fill(conjunto);
                    conexion.Close();
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
