namespace JohanBot
{
    partial class DNewCine
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DNewCine));
            System.Windows.Forms.Label idLabel;
            System.Windows.Forms.Label ultimaLabel;
            this.pelisDataSet = new JohanBot.PelisDataSet();
            this.peliculonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.peliculonTableAdapter = new JohanBot.PelisDataSetTableAdapters.PeliculonTableAdapter();
            this.peliculonBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.peliculonBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.ultimaTextBox = new System.Windows.Forms.TextBox();
            idLabel = new System.Windows.Forms.Label();
            ultimaLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pelisDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peliculonBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peliculonBindingNavigator)).BeginInit();
            this.peliculonBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // pelisDataSet
            // 
            this.pelisDataSet.DataSetName = "PelisDataSet";
            this.pelisDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // peliculonBindingSource
            // 
            this.peliculonBindingSource.DataMember = "Peliculon";
            this.peliculonBindingSource.DataSource = this.pelisDataSet;
            // 
            // peliculonTableAdapter
            // 
            this.peliculonTableAdapter.ClearBeforeFill = true;
            // 
            // peliculonBindingNavigator
            // 
            this.peliculonBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.peliculonBindingNavigator.BindingSource = this.peliculonBindingSource;
            this.peliculonBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.peliculonBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.peliculonBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.peliculonBindingNavigatorSaveItem});
            this.peliculonBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.peliculonBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.peliculonBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.peliculonBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.peliculonBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.peliculonBindingNavigator.Name = "peliculonBindingNavigator";
            this.peliculonBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.peliculonBindingNavigator.Size = new System.Drawing.Size(512, 25);
            this.peliculonBindingNavigator.TabIndex = 0;
            this.peliculonBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Mover primero";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Mover anterior";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Posición";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Posición actual";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(38, 13);
            this.bindingNavigatorCountItem.Text = "de {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Número total de elementos";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveNextItem.Text = "Mover siguiente";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveLastItem.Text = "Mover último";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Agregar nuevo";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorDeleteItem.Text = "Eliminar";
            // 
            // peliculonBindingNavigatorSaveItem
            // 
            this.peliculonBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.peliculonBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("peliculonBindingNavigatorSaveItem.Image")));
            this.peliculonBindingNavigatorSaveItem.Name = "peliculonBindingNavigatorSaveItem";
            this.peliculonBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 23);
            this.peliculonBindingNavigatorSaveItem.Text = "Guardar datos";
            this.peliculonBindingNavigatorSaveItem.Click += new System.EventHandler(this.peliculonBindingNavigatorSaveItem_Click);
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new System.Drawing.Point(11, 36);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(19, 13);
            idLabel.TabIndex = 1;
            idLabel.Text = "Id:";
            // 
            // idTextBox
            // 
            this.idTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.peliculonBindingSource, "Id", true));
            this.idTextBox.Location = new System.Drawing.Point(54, 33);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(63, 20);
            this.idTextBox.TabIndex = 2;
            // 
            // ultimaLabel
            // 
            ultimaLabel.AutoSize = true;
            ultimaLabel.Location = new System.Drawing.Point(11, 62);
            ultimaLabel.Name = "ultimaLabel";
            ultimaLabel.Size = new System.Drawing.Size(37, 13);
            ultimaLabel.TabIndex = 3;
            ultimaLabel.Text = "ultima:";
            // 
            // ultimaTextBox
            // 
            this.ultimaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.peliculonBindingSource, "ultima", true));
            this.ultimaTextBox.Location = new System.Drawing.Point(54, 59);
            this.ultimaTextBox.Name = "ultimaTextBox";
            this.ultimaTextBox.Size = new System.Drawing.Size(446, 20);
            this.ultimaTextBox.TabIndex = 4;
            // 
            // DNewCine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 85);
            this.Controls.Add(idLabel);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(ultimaLabel);
            this.Controls.Add(this.ultimaTextBox);
            this.Controls.Add(this.peliculonBindingNavigator);
            this.Name = "DNewCine";
            this.Text = "DNewCine";
            this.Load += new System.EventHandler(this.DNewCine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pelisDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peliculonBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peliculonBindingNavigator)).EndInit();
            this.peliculonBindingNavigator.ResumeLayout(false);
            this.peliculonBindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PelisDataSet pelisDataSet;
        private System.Windows.Forms.BindingSource peliculonBindingSource;
        private JohanBot.PelisDataSetTableAdapters.PeliculonTableAdapter peliculonTableAdapter;
        private System.Windows.Forms.BindingNavigator peliculonBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton peliculonBindingNavigatorSaveItem;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox ultimaTextBox;
    }
}