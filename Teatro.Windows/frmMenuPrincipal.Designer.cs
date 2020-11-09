namespace Teatro.Windows
{
    partial class frmMenuPrincipal
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiposDeEventosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clasificacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ubicacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formasDeVentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(662, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiposDeEventosToolStripMenuItem,
            this.clasificacionesToolStripMenuItem,
            this.ubicacionesToolStripMenuItem,
            this.formasDeVentasToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(69, 21);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // tiposDeEventosToolStripMenuItem
            // 
            this.tiposDeEventosToolStripMenuItem.Name = "tiposDeEventosToolStripMenuItem";
            this.tiposDeEventosToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.tiposDeEventosToolStripMenuItem.Text = "Tipos de Eventos";
            this.tiposDeEventosToolStripMenuItem.Click += new System.EventHandler(this.tiposDeEventosToolStripMenuItem_Click);
            // 
            // clasificacionesToolStripMenuItem
            // 
            this.clasificacionesToolStripMenuItem.Name = "clasificacionesToolStripMenuItem";
            this.clasificacionesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.clasificacionesToolStripMenuItem.Text = "Clasificaciones";
            this.clasificacionesToolStripMenuItem.Click += new System.EventHandler(this.clasificacionesToolStripMenuItem_Click);
            // 
            // ubicacionesToolStripMenuItem
            // 
            this.ubicacionesToolStripMenuItem.Name = "ubicacionesToolStripMenuItem";
            this.ubicacionesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.ubicacionesToolStripMenuItem.Text = "Ubicaciones";
            this.ubicacionesToolStripMenuItem.Click += new System.EventHandler(this.ubicacionesToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(50, 21);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // formasDeVentasToolStripMenuItem
            // 
            this.formasDeVentasToolStripMenuItem.Name = "formasDeVentasToolStripMenuItem";
            this.formasDeVentasToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.formasDeVentasToolStripMenuItem.Text = "Formas de Ventas";
            this.formasDeVentasToolStripMenuItem.Click += new System.EventHandler(this.formasDeVentasToolStripMenuItem_Click);
            // 
            // frmMenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Teatro.Windows.Properties.Resources.descarga;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(662, 539);
            this.ControlBox = false;
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(678, 578);
            this.MinimumSize = new System.Drawing.Size(678, 578);
            this.Name = "frmMenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuPrincipal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiposDeEventosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clasificacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ubicacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formasDeVentasToolStripMenuItem;
    }
}