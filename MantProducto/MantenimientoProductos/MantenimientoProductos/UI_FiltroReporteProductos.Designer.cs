namespace MantenimientoProductos
{
    partial class UI_FiltroReporteProductos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_FiltroReporteProductos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxMaterial = new System.Windows.Forms.TextBox();
            this.textBoxBodega = new System.Windows.Forms.TextBox();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxCodigo = new System.Windows.Forms.TextBox();
            this.buttonGenerarReporte = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownVentaMaximo = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCompraMaximo = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownVentaMinimo = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPrecioCompraMinimo = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonMaximized = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panelBack = new System.Windows.Forms.Panel();
            this.dtg_Productos = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVentaMaximo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCompraMaximo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVentaMinimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrecioCompraMinimo)).BeginInit();
            this.panel2.SuspendLayout();
            this.panelBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_Productos)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dtg_Productos);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.panelBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 600);
            this.panel1.TabIndex = 0;
            // 
            // textBoxMaterial
            // 
            this.textBoxMaterial.Location = new System.Drawing.Point(140, 205);
            this.textBoxMaterial.Name = "textBoxMaterial";
            this.textBoxMaterial.Size = new System.Drawing.Size(758, 25);
            this.textBoxMaterial.TabIndex = 28;
            this.textBoxMaterial.TextChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // textBoxBodega
            // 
            this.textBoxBodega.Location = new System.Drawing.Point(140, 174);
            this.textBoxBodega.Name = "textBoxBodega";
            this.textBoxBodega.Size = new System.Drawing.Size(758, 25);
            this.textBoxBodega.TabIndex = 27;
            this.textBoxBodega.TextChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(140, 80);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(758, 25);
            this.textBoxNombre.TabIndex = 26;
            this.textBoxNombre.TextChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // textBoxCodigo
            // 
            this.textBoxCodigo.Location = new System.Drawing.Point(140, 49);
            this.textBoxCodigo.Name = "textBoxCodigo";
            this.textBoxCodigo.Size = new System.Drawing.Size(758, 25);
            this.textBoxCodigo.TabIndex = 25;
            this.textBoxCodigo.TextChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // buttonGenerarReporte
            // 
            this.buttonGenerarReporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(77)))), ((int)(((byte)(117)))));
            this.buttonGenerarReporte.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonGenerarReporte.FlatAppearance.BorderSize = 0;
            this.buttonGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGenerarReporte.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGenerarReporte.ForeColor = System.Drawing.Color.White;
            this.buttonGenerarReporte.Location = new System.Drawing.Point(140, 233);
            this.buttonGenerarReporte.Margin = new System.Windows.Forms.Padding(0);
            this.buttonGenerarReporte.Name = "buttonGenerarReporte";
            this.buttonGenerarReporte.Size = new System.Drawing.Size(758, 35);
            this.buttonGenerarReporte.TabIndex = 24;
            this.buttonGenerarReporte.Text = "Generar Reporte";
            this.buttonGenerarReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonGenerarReporte.UseVisualStyleBackColor = false;
            this.buttonGenerarReporte.Click += new System.EventHandler(this.buttonGenerarReporte_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(509, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "Y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(509, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "Y";
            // 
            // numericUpDownVentaMaximo
            // 
            this.numericUpDownVentaMaximo.DecimalPlaces = 2;
            this.numericUpDownVentaMaximo.Location = new System.Drawing.Point(530, 143);
            this.numericUpDownVentaMaximo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownVentaMaximo.Name = "numericUpDownVentaMaximo";
            this.numericUpDownVentaMaximo.Size = new System.Drawing.Size(368, 25);
            this.numericUpDownVentaMaximo.TabIndex = 14;
            this.numericUpDownVentaMaximo.ValueChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // numericUpDownCompraMaximo
            // 
            this.numericUpDownCompraMaximo.DecimalPlaces = 2;
            this.numericUpDownCompraMaximo.Location = new System.Drawing.Point(531, 112);
            this.numericUpDownCompraMaximo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownCompraMaximo.Name = "numericUpDownCompraMaximo";
            this.numericUpDownCompraMaximo.Size = new System.Drawing.Size(368, 25);
            this.numericUpDownCompraMaximo.TabIndex = 13;
            this.numericUpDownCompraMaximo.ValueChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // numericUpDownVentaMinimo
            // 
            this.numericUpDownVentaMinimo.DecimalPlaces = 2;
            this.numericUpDownVentaMinimo.Location = new System.Drawing.Point(140, 143);
            this.numericUpDownVentaMinimo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownVentaMinimo.Name = "numericUpDownVentaMinimo";
            this.numericUpDownVentaMinimo.Size = new System.Drawing.Size(363, 25);
            this.numericUpDownVentaMinimo.TabIndex = 12;
            this.numericUpDownVentaMinimo.ValueChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // numericUpDownPrecioCompraMinimo
            // 
            this.numericUpDownPrecioCompraMinimo.DecimalPlaces = 2;
            this.numericUpDownPrecioCompraMinimo.Location = new System.Drawing.Point(141, 112);
            this.numericUpDownPrecioCompraMinimo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownPrecioCompraMinimo.Name = "numericUpDownPrecioCompraMinimo";
            this.numericUpDownPrecioCompraMinimo.Size = new System.Drawing.Size(363, 25);
            this.numericUpDownPrecioCompraMinimo.TabIndex = 11;
            this.numericUpDownPrecioCompraMinimo.ValueChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(79, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Material:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(55, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Precio Venta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Precio Compra:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(84, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Código:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(78, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filtrar por:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxMaterial);
            this.panel2.Controls.Add(this.textBoxBodega);
            this.panel2.Controls.Add(this.textBoxNombre);
            this.panel2.Controls.Add(this.textBoxCodigo);
            this.panel2.Controls.Add(this.buttonGenerarReporte);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.numericUpDownVentaMaximo);
            this.panel2.Controls.Add(this.numericUpDownCompraMaximo);
            this.panel2.Controls.Add(this.numericUpDownVentaMinimo);
            this.panel2.Controls.Add(this.numericUpDownPrecioCompraMinimo);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 277);
            this.panel2.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(82, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Bodega:";
            // 
            // buttonMaximized
            // 
            this.buttonMaximized.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMaximized.BackgroundImage")));
            this.buttonMaximized.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMaximized.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonMaximized.FlatAppearance.BorderSize = 0;
            this.buttonMaximized.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMaximized.Location = new System.Drawing.Point(938, 0);
            this.buttonMaximized.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMaximized.Name = "buttonMaximized";
            this.buttonMaximized.Size = new System.Drawing.Size(30, 30);
            this.buttonMaximized.TabIndex = 1;
            this.buttonMaximized.UseVisualStyleBackColor = true;
            this.buttonMaximized.Click += new System.EventHandler(this.buttonMaximized_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonClose.BackgroundImage")));
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(968, 0);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(30, 30);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // panelBack
            // 
            this.panelBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(81)))), ((int)(((byte)(77)))), ((int)(((byte)(117)))));
            this.panelBack.Controls.Add(this.buttonMaximized);
            this.panelBack.Controls.Add(this.buttonClose);
            this.panelBack.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBack.Location = new System.Drawing.Point(0, 0);
            this.panelBack.Margin = new System.Windows.Forms.Padding(0);
            this.panelBack.Name = "panelBack";
            this.panelBack.Size = new System.Drawing.Size(998, 30);
            this.panelBack.TabIndex = 54;
            // 
            // dtg_Productos
            // 
            this.dtg_Productos.AllowUserToAddRows = false;
            this.dtg_Productos.AllowUserToDeleteRows = false;
            this.dtg_Productos.AllowUserToOrderColumns = true;
            this.dtg_Productos.AllowUserToResizeRows = false;
            this.dtg_Productos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtg_Productos.BackgroundColor = System.Drawing.Color.White;
            this.dtg_Productos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(156)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(203)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtg_Productos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtg_Productos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(156)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(203)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtg_Productos.DefaultCellStyle = dataGridViewCellStyle5;
            this.dtg_Productos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtg_Productos.EnableHeadersVisualStyles = false;
            this.dtg_Productos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(254)))));
            this.dtg_Productos.Location = new System.Drawing.Point(0, 307);
            this.dtg_Productos.Margin = new System.Windows.Forms.Padding(0);
            this.dtg_Productos.Name = "dtg_Productos";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(156)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(203)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtg_Productos.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dtg_Productos.RowHeadersVisible = false;
            this.dtg_Productos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtg_Productos.Size = new System.Drawing.Size(998, 291);
            this.dtg_Productos.TabIndex = 56;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 998F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(998, 277);
            this.tableLayoutPanel1.TabIndex = 57;
            // 
            // UI_FiltroReporteProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UI_FiltroReporteProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UI_FiltroReporteProductos";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVentaMaximo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCompraMaximo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVentaMinimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrecioCompraMinimo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtg_Productos)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxMaterial;
        private System.Windows.Forms.TextBox textBoxBodega;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.TextBox textBoxCodigo;
        private System.Windows.Forms.Button buttonGenerarReporte;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownVentaMaximo;
        private System.Windows.Forms.NumericUpDown numericUpDownCompraMaximo;
        private System.Windows.Forms.NumericUpDown numericUpDownVentaMinimo;
        private System.Windows.Forms.NumericUpDown numericUpDownPrecioCompraMinimo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelBack;
        private System.Windows.Forms.Button buttonMaximized;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView dtg_Productos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}