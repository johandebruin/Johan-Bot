using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JohanBot
{
    public partial class DPendientes : Form
    {
        public DPendientes()
        {
            InitializeComponent();
        }

        private void peliculasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.peliculasBindingSource.EndEdit();
            this.peliculasTableAdapter.Update(this.pelisDataSet.Peliculas);

        }

        private void DPendientes_Load(object sender, EventArgs e)
        {
            // TODO: esta l�nea de c�digo carga datos en la tabla 'pelisDataSet.Peliculas' Puede moverla o quitarla seg�n sea necesario.
            this.peliculasTableAdapter.Fill(this.pelisDataSet.Peliculas);

        }
    }
}