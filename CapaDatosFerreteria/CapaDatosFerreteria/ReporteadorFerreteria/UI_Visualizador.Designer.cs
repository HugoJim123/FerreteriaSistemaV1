namespace ReporteadorFerreteria
{
    partial class UI_Visualizador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_Visualizador));
            this.panelBack = new System.Windows.Forms.Panel();
            this.buttonMaximized = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.crystalReportViewerGlobal = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panelBack.SuspendLayout();
            this.SuspendLayout();
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
            this.panelBack.Size = new System.Drawing.Size(1000, 30);
            this.panelBack.TabIndex = 50;
            // 
            // buttonMaximized
            // 
            this.buttonMaximized.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMaximized.BackgroundImage")));
            this.buttonMaximized.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMaximized.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonMaximized.FlatAppearance.BorderSize = 0;
            this.buttonMaximized.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMaximized.Location = new System.Drawing.Point(940, 0);
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
            this.buttonClose.Location = new System.Drawing.Point(970, 0);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(30, 30);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // crystalReportViewerGlobal
            // 
            this.crystalReportViewerGlobal.ActiveViewIndex = -1;
            this.crystalReportViewerGlobal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerGlobal.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerGlobal.DisplayStatusBar = false;
            this.crystalReportViewerGlobal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerGlobal.Location = new System.Drawing.Point(0, 30);
            this.crystalReportViewerGlobal.Name = "crystalReportViewerGlobal";
            this.crystalReportViewerGlobal.ShowCloseButton = false;
            this.crystalReportViewerGlobal.ShowGroupTreeButton = false;
            this.crystalReportViewerGlobal.ShowLogo = false;
            this.crystalReportViewerGlobal.ShowParameterPanelButton = false;
            this.crystalReportViewerGlobal.Size = new System.Drawing.Size(1000, 570);
            this.crystalReportViewerGlobal.TabIndex = 51;
            this.crystalReportViewerGlobal.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // UI_Visualizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.crystalReportViewerGlobal);
            this.Controls.Add(this.panelBack);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UI_Visualizador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panelBack.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBack;
        private System.Windows.Forms.Button buttonMaximized;
        private System.Windows.Forms.Button buttonClose;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerGlobal;
    }
}

