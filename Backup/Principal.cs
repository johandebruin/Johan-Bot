using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Bot;

namespace JohanBot
{
    public partial class Principal : Form
    {
        public static int tiempo;

        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            botList.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewCine.Obtener();
        }

        private void peliculonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DNewCine formulario = new DNewCine();
            formulario.Show();
        }

        private void pendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DPendientes formulario = new DPendientes();
            formulario.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessStartInfo ejecutar = new ProcessStartInfo("./actualizar.exe");
            ejecutar.Verb = "open";
            Process.Start(ejecutar);
        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuracion form = new Configuracion();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CineTube.Procesar();
        }
    }
}