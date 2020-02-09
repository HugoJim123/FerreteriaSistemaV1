namespace MantenimientoProductos
{
    partial class ReportViewProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportViewProductos));
            this.panelBack = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.crystalReportViewerProductos = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panelBack.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBack
            // 
            this.panelBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(156)))), ((int)(((byte)(202)))));
            this.panelBack.Controls.Add(this.buttonBack);
            this.panelBack.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBack.Location = new System.Drawing.Point(0, 0);
            this.panelBack.Margin = new System.Windows.Forms.Padding(0);
            this.panelBack.Name = "panelBack";
            this.panelBack.Size = new System.Drawing.Size(1383, 50);
            this.panelBack.TabIndex = 49;
            // 
            // buttonBack
            // 
            this.buttonBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
            this.buttonBack.Location = new System.Drawing.Point(0, 0);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(0);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(50, 50);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // crystalReportViewerProductos
            // 
            this.crystalReportViewerProductos.ActiveViewIndex = -1;
            this.crystalReportViewerProductos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerProductos.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerProductos.Location = new System.Drawing.Point(0, 50);
            this.crystalReportViewerProductos.Margin = new System.Windows.Forms.Padding(10);
            this.crystalReportViewerProductos.Name = "crystalReportViewerProductos";
            this.crystalReportViewerProductos.ShowCloseButton = false;
            this.crystalReportViewerProductos.ShowLogo = false;
            this.crystalReportViewerProductos.Size = new System.Drawing.Size(1383, 600);
            this.crystalReportViewerProductos.TabIndex = 51;
            this.crystalReportViewerProductos.ToolPanelWidth = 100;
            // 
            // ReportViewProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1383, 650);
            this.Controls.Add(this.crystalReportViewerProductos);
            this.Controls.Add(this.panelBack);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(107)))), ((int)(((byte)(110)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ReportViewProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportViewProductos";
            this.panelBack.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBack;
        private System.Windows.Forms.Button buttonBack;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerProductos;
    }
}