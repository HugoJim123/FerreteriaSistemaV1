namespace VentasCompras.REPORTES
{
    partial class UI_FiltroReporteCVCD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_FiltroReporteCVCD));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxEstado = new System.Windows.Forms.ComboBox();
            this.dateTimePickerFCancelacionMinimo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFCancelacionMaximo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFCreacionMaximo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFCreacionMinimo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxCodigo = new System.Windows.Forms.TextBox();
            this.buttonGenerarReporte = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelObjetivo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBack = new System.Windows.Forms.Panel();
            this.buttonMaximized = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.DataGridViewDetalles = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetalles)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 254F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(998, 254);
            this.tableLayoutPanel1.TabIndex = 57;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboBoxEstado);
            this.panel2.Controls.Add(this.dateTimePickerFCancelacionMinimo);
            this.panel2.Controls.Add(this.dateTimePickerFCancelacionMaximo);
            this.panel2.Controls.Add(this.dateTimePickerFCreacionMaximo);
            this.panel2.Controls.Add(this.dateTimePickerFCreacionMinimo);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBoxNombre);
            this.panel2.Controls.Add(this.textBoxCodigo);
            this.panel2.Controls.Add(this.buttonGenerarReporte);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.labelObjetivo);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 254);
            this.panel2.TabIndex = 55;
            // 
            // comboBoxEstado
            // 
            this.comboBoxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEstado.FormattingEnabled = true;
            this.comboBoxEstado.Items.AddRange(new object[] {
            "--",
            "Cancelado",
            "Pendiente de Pago"});
            this.comboBoxEstado.Location = new System.Drawing.Point(177, 173);
            this.comboBoxEstado.Name = "comboBoxEstado";
            this.comboBoxEstado.Size = new System.Drawing.Size(758, 25);
            this.comboBoxEstado.TabIndex = 34;
            this.comboBoxEstado.SelectedIndexChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // dateTimePickerFCancelacionMinimo
            // 
            this.dateTimePickerFCancelacionMinimo.Location = new System.Drawing.Point(177, 142);
            this.dateTimePickerFCancelacionMinimo.Name = "dateTimePickerFCancelacionMinimo";
            this.dateTimePickerFCancelacionMinimo.Size = new System.Drawing.Size(363, 25);
            this.dateTimePickerFCancelacionMinimo.TabIndex = 33;
            this.dateTimePickerFCancelacionMinimo.ValueChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // dateTimePickerFCancelacionMaximo
            // 
            this.dateTimePickerFCancelacionMaximo.Location = new System.Drawing.Point(567, 142);
            this.dateTimePickerFCancelacionMaximo.Name = "dateTimePickerFCancelacionMaximo";
            this.dateTimePickerFCancelacionMaximo.Size = new System.Drawing.Size(368, 25);
            this.dateTimePickerFCancelacionMaximo.TabIndex = 32;
            this.dateTimePickerFCancelacionMaximo.ValueChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // dateTimePickerFCreacionMaximo
            // 
            this.dateTimePickerFCreacionMaximo.Location = new System.Drawing.Point(567, 111);
            this.dateTimePickerFCreacionMaximo.Name = "dateTimePickerFCreacionMaximo";
            this.dateTimePickerFCreacionMaximo.Size = new System.Drawing.Size(368, 25);
            this.dateTimePickerFCreacionMaximo.TabIndex = 31;
            this.dateTimePickerFCreacionMaximo.ValueChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // dateTimePickerFCreacionMinimo
            // 
            this.dateTimePickerFCreacionMinimo.Location = new System.Drawing.Point(177, 111);
            this.dateTimePickerFCreacionMinimo.Name = "dateTimePickerFCreacionMinimo";
            this.dateTimePickerFCreacionMinimo.Size = new System.Drawing.Size(363, 25);
            this.dateTimePickerFCreacionMinimo.TabIndex = 30;
            this.dateTimePickerFCreacionMinimo.ValueChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(55, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 17);
            this.label2.TabIndex = 29;
            this.label2.Text = "Fecha Cancelación:";
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(177, 80);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(758, 25);
            this.textBoxNombre.TabIndex = 26;
            this.textBoxNombre.TextChanged += new System.EventHandler(this.textBoxCodigo_TextChanged);
            // 
            // textBoxCodigo
            // 
            this.textBoxCodigo.Location = new System.Drawing.Point(177, 49);
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
            this.buttonGenerarReporte.Location = new System.Drawing.Point(177, 202);
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
            this.label9.Location = new System.Drawing.Point(546, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "Y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(546, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(121, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Estado:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(73, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fecha Creación:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(118, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Código:";
            // 
            // labelObjetivo
            // 
            this.labelObjetivo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelObjetivo.Location = new System.Drawing.Point(18, 83);
            this.labelObjetivo.Name = "labelObjetivo";
            this.labelObjetivo.Size = new System.Drawing.Size(153, 17);
            this.labelObjetivo.TabIndex = 1;
            this.labelObjetivo.Text = "Objetivo:";
            this.labelObjetivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // DataGridViewDetalles
            // 
            this.DataGridViewDetalles.AllowUserToAddRows = false;
            this.DataGridViewDetalles.AllowUserToDeleteRows = false;
            this.DataGridViewDetalles.AllowUserToOrderColumns = true;
            this.DataGridViewDetalles.AllowUserToResizeRows = false;
            this.DataGridViewDetalles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridViewDetalles.BackgroundColor = System.Drawing.Color.White;
            this.DataGridViewDetalles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(156)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(203)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewDetalles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(156)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(203)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewDetalles.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridViewDetalles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridViewDetalles.EnableHeadersVisualStyles = false;
            this.DataGridViewDetalles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(254)))));
            this.DataGridViewDetalles.Location = new System.Drawing.Point(0, 284);
            this.DataGridViewDetalles.Margin = new System.Windows.Forms.Padding(0);
            this.DataGridViewDetalles.Name = "DataGridViewDetalles";
            this.DataGridViewDetalles.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(156)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(203)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewDetalles.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewDetalles.RowHeadersVisible = false;
            this.DataGridViewDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewDetalles.Size = new System.Drawing.Size(998, 314);
            this.DataGridViewDetalles.TabIndex = 56;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DataGridViewDetalles);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.panelBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 600);
            this.panel1.TabIndex = 1;
            // 
            // UI_FiltroReporteCVCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UI_FiltroReporteCVCD";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UI_FiltroReporteCVCD";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetalles)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.TextBox textBoxCodigo;
        private System.Windows.Forms.Button buttonGenerarReporte;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelObjetivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelBack;
        private System.Windows.Forms.Button buttonMaximized;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView DataGridViewDetalles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxEstado;
        private System.Windows.Forms.DateTimePicker dateTimePickerFCancelacionMinimo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFCancelacionMaximo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFCreacionMaximo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFCreacionMinimo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
    }
}