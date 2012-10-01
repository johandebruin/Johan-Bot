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
        NotifyIcon NotifyIcon1 = new NotifyIcon();
        ContextMenu ContextMenu1 = new ContextMenu();
        // la declaramos fuera de la funci�n, para que mantenga su valor
        Boolean PrimeraVez = true;

        public Principal()
        {
            InitializeComponent();
        }

        private void Salir_Click(object sender, System.EventArgs e)
        {
            //' Este procedimiento se usa para cerrar el formulario,
            //' se usar� como procedimiento de eventos, en principio usado por el bot�n Salir
            this.Close();
        }

        private void Restaurar_Click(object sender, System.EventArgs e)
        {
            //' Restaurar por si se minimiz�
            //' Este evento manejar� tanto los men�s Restaurar como el NotifyIcon.DoubleClick
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        private void AcercaDe_Click(object sender, System.EventArgs e)
        {
            //' Mostrar la informaci�n del autor, versi�n, etc.
            Configuracion form = new Configuracion();
            form.Show();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            botList.SelectedIndex = 0;
            //' Asignar los submen�s del ContextMenu
            //'
            //' A�adimos la opci�n Restaurar, que ser� el elemento predeterminado
            //          MenuItem tMenu = new MenuItem("&Restaurar", new EventHandler(this.Restaurar_Click));
            //          tMenu.DefaultItem = true;
            //          ContextMenu1.MenuItems.Add(tMenu);
            //
            //' Esto tambi�n se puede hacer as�:
            ContextMenu1.MenuItems.Add("&Restaurar", new EventHandler(this.Restaurar_Click));
            ContextMenu1.MenuItems[0].DefaultItem = true;
            //'
            //' A�adimos un separador
            ContextMenu1.MenuItems.Add("-");
            //' A�adimos el elemento Acerca de...
            ContextMenu1.MenuItems.Add("Configuraci�n", new EventHandler(this.AcercaDe_Click));
            //' A�adimos otro separador
            ContextMenu1.MenuItems.Add("-");
            //' A�adimos la opci�n de salir
            ContextMenu1.MenuItems.Add("&Salir", new EventHandler(this.Salir_Click));
            //'
            //' Asignar los valores para el NotifyIcon
            NotifyIcon1.Icon = this.Icon;
            NotifyIcon1.ContextMenu = this.ContextMenu1;
            NotifyIcon1.Text = Application.ProductName;
            NotifyIcon1.Visible = true;
            //
            // Asignamos los otros eventos al formulario
            this.Resize += new EventHandler(this.Principal_Resize);
            this.Activated += new EventHandler(this.Principal_Activated);
            //this.Closing += new System.ComponentModel.CancelEventHandler(this.Principal_FormClosing);
            // Asignamos el evento DoubleClick del NotifyIcon
            this.NotifyIcon1.DoubleClick += new EventHandler(this.Restaurar_Click);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            seriesonline.Procesar();
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
            ProcessStartInfo ejecutar = new ProcessStartInfo("./Infinito.exe");
            ejecutar.Verb = "open";
            Process.Start(ejecutar);
        }

        private void configuraci�nToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Configuracion form = new Configuracion();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CineTube.Procesar();
        }

        private void Principal_Resize(object sender, EventArgs e)
        {
            //' Cuando se minimice, ocultarla, se quedar� disponible en la barra de tareas
            if (this.WindowState == FormWindowState.Minimized)
                this.Visible = false;
        }

        private void Principal_Activated(object sender, EventArgs e)
        {
            // En C# no se puede usar static para hacer que una variable mantenga su valor
            // en C/C++ s� que se puede
            //static Boolean PrimeraVez = true;
            //
            //' La primera vez que se active, ocultar el form,
            //' es una chapuza, pero el formulario no permite que se oculte en el Form_Load
            if (PrimeraVez)
            {
                PrimeraVez = false;
                Visible = false;
            }

        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cuando se va a cerrar el formulario...
            // eliminar el objeto de la barra de tareas
            this.NotifyIcon1.Visible = false;
            // esto es necesario, para que no se quede el icono en la barra de tareas
            // (el cual se quita al pasar el rat�n por encima)
            this.NotifyIcon1 = null;
            // de paso eliminamos el men� contextual
            this.ContextMenu1 = null; 

        }

        private void insertarSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgregarSQL x = new AgregarSQL();
            x.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
                    }
    }
}