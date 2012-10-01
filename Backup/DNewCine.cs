using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JohanBot
{
    public partial class DNewCine : Form
    {
        public DNewCine()
        {
            InitializeComponent();
        }

        private void peliculonBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.peliculonBindingSource.EndEdit();
            this.peliculonTableAdapter.Update(this.pelisDataSet.Peliculon);

        }

        private void DNewCine_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'pelisDataSet.Peliculon' Puede moverla o quitarla según sea necesario.
            this.peliculonTableAdapter.Fill(this.pelisDataSet.Peliculon);

        }
    }
}