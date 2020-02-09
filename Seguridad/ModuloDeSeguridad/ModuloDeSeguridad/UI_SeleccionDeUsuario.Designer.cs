namespace ModuloDeSeguridad
{
    partial class UI_SeleccionDeUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_SeleccionDeUsuario));
            this.pictureBoxUserPhoto = new System.Windows.Forms.PictureBox();
            this.comboBoxNickname = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonSiguiente = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUserPhoto)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxUserPhoto
            // 
            this.pictureBoxUserPhoto.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxUserPhoto.Location = new System.Drawing.Point(146, 65);
            this.pictureBoxUserPhoto.Name = "pictureBoxUserPhoto";
            this.pictureBoxUserPhoto.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxUserPhoto.TabIndex = 0;
            this.pictureBoxUserPhoto.TabStop = false;
            this.pictureBoxUserPhoto.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxUserPhoto_Paint);
            // 
            // comboBoxNickname
            // 
            this.comboBoxNickname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(66)))));
            this.comboBoxNickname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNickname.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxNickname.ForeColor = System.Drawing.Color.White;
            this.comboBoxNickname.FormattingEnabled = true;
            this.comboBoxNickname.Location = new System.Drawing.Point(46, 285);
            this.comboBoxNickname.Name = "comboBoxNickname";
            this.comboBoxNickname.Size = new System.Drawing.Size(400, 25);
            this.comboBoxNickname.TabIndex = 1;
            this.comboBoxNickname.SelectedIndexChanged += new System.EventHandler(this.comboBoxNickname_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 50);
            this.panel1.TabIndex = 2;
            // 
            // buttonBack
            // 
            this.buttonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBack.BackgroundImage")));
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Location = new System.Drawing.Point(0, 0);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(51, 50);
            this.buttonBack.TabIndex = 1;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonSiguiente
            // 
            this.buttonSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(156)))), ((int)(((byte)(202)))));
            this.buttonSiguiente.FlatAppearance.BorderSize = 0;
            this.buttonSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSiguiente.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSiguiente.ForeColor = System.Drawing.Color.White;
            this.buttonSiguiente.Location = new System.Drawing.Point(46, 333);
            this.buttonSiguiente.Name = "buttonSiguiente";
            this.buttonSiguiente.Size = new System.Drawing.Size(400, 38);
            this.buttonSiguiente.TabIndex = 3;
            this.buttonSiguiente.Text = "Siguiente";
            this.buttonSiguiente.UseVisualStyleBackColor = false;
            this.buttonSiguiente.Click += new System.EventHandler(this.buttonSiguiente_Click);
            // 
            // UI_SeleccionDeUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.buttonSiguiente);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboBoxNickname);
            this.Controls.Add(this.pictureBoxUserPhoto);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(57)))), ((int)(((byte)(66)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UI_SeleccionDeUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UI_SeleccionDeUsuario";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUserPhoto)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxUserPhoto;
        private System.Windows.Forms.ComboBox comboBoxNickname;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSiguiente;
    }
}