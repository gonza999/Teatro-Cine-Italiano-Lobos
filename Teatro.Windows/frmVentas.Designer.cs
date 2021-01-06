namespace Teatro.Windows
{
    partial class frmVentas
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
            this.components = new System.ComponentModel.Container();
            this.btnVender = new System.Windows.Forms.Button();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.cmbLocalidades = new System.Windows.Forms.ComboBox();
            this.cmbUbicaciones = new System.Windows.Forms.ComboBox();
            this.cmbHorarios = new System.Windows.Forms.ComboBox();
            this.cmbEventos = new System.Windows.Forms.ComboBox();
            this.lblOcupado = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFormaVenta = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.cmnEvento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnHorario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnLocalidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnBorrar = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbEmpleado = new System.Windows.Forms.ComboBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVender
            // 
            this.btnVender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnVender.Enabled = false;
            this.btnVender.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVender.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVender.Location = new System.Drawing.Point(477, 421);
            this.btnVender.Name = "btnVender";
            this.btnVender.Size = new System.Drawing.Size(111, 42);
            this.btnVender.TabIndex = 24;
            this.btnVender.Text = "VENDER";
            this.btnVender.UseVisualStyleBackColor = false;
            this.btnVender.Click += new System.EventHandler(this.btnVender_Click);
            // 
            // txtImporte
            // 
            this.txtImporte.Enabled = false;
            this.txtImporte.Location = new System.Drawing.Point(615, 211);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.ReadOnly = true;
            this.txtImporte.Size = new System.Drawing.Size(100, 20);
            this.txtImporte.TabIndex = 23;
            this.txtImporte.Text = "0";
            this.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPago.Enabled = false;
            this.cmbFormaPago.FormattingEnabled = true;
            this.cmbFormaPago.Location = new System.Drawing.Point(600, 248);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Size = new System.Drawing.Size(146, 21);
            this.cmbFormaPago.TabIndex = 20;
            this.cmbFormaPago.SelectedValueChanged += new System.EventHandler(this.cmbFormaPago_SelectedValueChanged);
            // 
            // cmbLocalidades
            // 
            this.cmbLocalidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalidades.Enabled = false;
            this.cmbLocalidades.FormattingEnabled = true;
            this.cmbLocalidades.Location = new System.Drawing.Point(600, 141);
            this.cmbLocalidades.Name = "cmbLocalidades";
            this.cmbLocalidades.Size = new System.Drawing.Size(149, 21);
            this.cmbLocalidades.TabIndex = 19;
            this.cmbLocalidades.SelectedValueChanged += new System.EventHandler(this.cmbLocalidades_SelectedValueChanged);
            // 
            // cmbUbicaciones
            // 
            this.cmbUbicaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUbicaciones.Enabled = false;
            this.cmbUbicaciones.FormattingEnabled = true;
            this.cmbUbicaciones.Location = new System.Drawing.Point(600, 100);
            this.cmbUbicaciones.Name = "cmbUbicaciones";
            this.cmbUbicaciones.Size = new System.Drawing.Size(149, 21);
            this.cmbUbicaciones.TabIndex = 18;
            this.cmbUbicaciones.SelectedValueChanged += new System.EventHandler(this.cmbUbicaciones_SelectedValueChanged);
            // 
            // cmbHorarios
            // 
            this.cmbHorarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHorarios.Enabled = false;
            this.cmbHorarios.FormattingEnabled = true;
            this.cmbHorarios.Location = new System.Drawing.Point(600, 61);
            this.cmbHorarios.Name = "cmbHorarios";
            this.cmbHorarios.Size = new System.Drawing.Size(149, 21);
            this.cmbHorarios.TabIndex = 17;
            this.cmbHorarios.SelectedValueChanged += new System.EventHandler(this.cmbHorarios_SelectedValueChanged);
            // 
            // cmbEventos
            // 
            this.cmbEventos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventos.FormattingEnabled = true;
            this.cmbEventos.Location = new System.Drawing.Point(600, 23);
            this.cmbEventos.Name = "cmbEventos";
            this.cmbEventos.Size = new System.Drawing.Size(149, 21);
            this.cmbEventos.TabIndex = 16;
            this.cmbEventos.SelectedValueChanged += new System.EventHandler(this.cmbEventos_SelectedValueChanged);
            // 
            // lblOcupado
            // 
            this.lblOcupado.AutoSize = true;
            this.lblOcupado.BackColor = System.Drawing.Color.Aqua;
            this.lblOcupado.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOcupado.ForeColor = System.Drawing.Color.Black;
            this.lblOcupado.Location = new System.Drawing.Point(560, 176);
            this.lblOcupado.Name = "lblOcupado";
            this.lblOcupado.Size = new System.Drawing.Size(101, 20);
            this.lblOcupado.TabIndex = 14;
            this.lblOcupado.Text = "DESOCUPADO";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(473, 249);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 20);
            this.label10.TabIndex = 13;
            this.label10.Text = "Forma de Pago :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(520, 211);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "Importe: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(501, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Localidad :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(501, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ubicaciones :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(501, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Horarios :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(501, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Eventos :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(473, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Forma de Venta :";
            // 
            // cmbFormaVenta
            // 
            this.cmbFormaVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaVenta.Enabled = false;
            this.cmbFormaVenta.FormattingEnabled = true;
            this.cmbFormaVenta.Location = new System.Drawing.Point(600, 286);
            this.cmbFormaVenta.Name = "cmbFormaVenta";
            this.cmbFormaVenta.Size = new System.Drawing.Size(146, 21);
            this.cmbFormaVenta.TabIndex = 20;
            this.cmbFormaVenta.SelectedValueChanged += new System.EventHandler(this.cmbFormaVenta_SelectedValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvDatos);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 518);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(779, 208);
            this.panel1.TabIndex = 25;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cmnEvento,
            this.cmnHorario,
            this.cmnLocalidad,
            this.cmnImporte,
            this.cmnBorrar});
            this.dgvDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDatos.Location = new System.Drawing.Point(0, 0);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(779, 208);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellClick);
            // 
            // cmnEvento
            // 
            this.cmnEvento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmnEvento.HeaderText = "Evento";
            this.cmnEvento.Name = "cmnEvento";
            this.cmnEvento.ReadOnly = true;
            // 
            // cmnHorario
            // 
            this.cmnHorario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmnHorario.HeaderText = "Horario";
            this.cmnHorario.Name = "cmnHorario";
            this.cmnHorario.ReadOnly = true;
            // 
            // cmnLocalidad
            // 
            this.cmnLocalidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmnLocalidad.HeaderText = "Localidad";
            this.cmnLocalidad.Name = "cmnLocalidad";
            this.cmnLocalidad.ReadOnly = true;
            // 
            // cmnImporte
            // 
            this.cmnImporte.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmnImporte.HeaderText = "Importe";
            this.cmnImporte.Name = "cmnImporte";
            this.cmnImporte.ReadOnly = true;
            // 
            // cmnBorrar
            // 
            this.cmnBorrar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmnBorrar.HeaderText = "Borrar";
            this.cmnBorrar.Image = global::Teatro.Windows.Properties.Resources.delete_20px;
            this.cmnBorrar.Name = "cmnBorrar";
            this.cmnBorrar.ReadOnly = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelar.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(618, 421);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(131, 42);
            this.btnCancelar.TabIndex = 24;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAgregar.Enabled = false;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregar.Font = new System.Drawing.Font("Bookshelf Symbol 7", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(672, 322);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(74, 22);
            this.btnAgregar.TabIndex = 24;
            this.btnAgregar.Text = "Agregar -->";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(473, 381);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Empleado :";
            // 
            // cmbEmpleado
            // 
            this.cmbEmpleado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpleado.FormattingEnabled = true;
            this.cmbEmpleado.Location = new System.Drawing.Point(600, 380);
            this.cmbEmpleado.Name = "cmbEmpleado";
            this.cmbEmpleado.Size = new System.Drawing.Size(146, 21);
            this.cmbEmpleado.TabIndex = 20;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewImageColumn1.HeaderText = "Borrar";
            this.dataGridViewImageColumn1.Image = global::Teatro.Windows.Properties.Resources.delete_20px;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::Teatro.Windows.Properties.Resources._1;
            this.pictureBox1.Location = new System.Drawing.Point(14, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(452, 470);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LemonChiffon;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Bookshelf Symbol 7", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(667, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 37);
            this.button1.TabIndex = 24;
            this.button1.Text = "Lista de Localidades";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(779, 726);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnVender);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.cmbEmpleado);
            this.Controls.Add(this.cmbFormaVenta);
            this.Controls.Add(this.cmbFormaPago);
            this.Controls.Add(this.cmbLocalidades);
            this.Controls.Add(this.cmbUbicaciones);
            this.Controls.Add(this.cmbHorarios);
            this.Controls.Add(this.cmbEventos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblOcupado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(779, 741);
            this.MinimumSize = new System.Drawing.Size(779, 726);
            this.Name = "frmVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmVentas";
            this.Load += new System.EventHandler(this.frmVentas_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVender;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.ComboBox cmbLocalidades;
        private System.Windows.Forms.ComboBox cmbUbicaciones;
        private System.Windows.Forms.ComboBox cmbHorarios;
        private System.Windows.Forms.ComboBox cmbEventos;
        private System.Windows.Forms.Label lblOcupado;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFormaVenta;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnEvento;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnHorario;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnLocalidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmnImporte;
        private System.Windows.Forms.DataGridViewImageColumn cmnBorrar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbEmpleado;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button1;
    }
}