﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teatro.Windows
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void tiposDeEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTipoEventos frmTipoEventos = new frmTipoEventos();
            frmTipoEventos.Text = "TipoEventos";
            frmTipoEventos.ShowDialog(this);
        }

        private void clasificacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClasificaciones frmClasificaciones = new frmClasificaciones();
            frmClasificaciones.Text = "Clasificaciones";
            frmClasificaciones.ShowDialog(this);
        }

        private void ubicacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUbicaciones frmUbicaciones = new frmUbicaciones();
            frmUbicaciones.Text = "Ubicaciones";
            frmUbicaciones.ShowDialog(this);
        }

        private void formasDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFormasVentas frm = new frmFormasVentas();
            frm.Text = "Formas de Ventas";
            frm.ShowDialog(this);
        }

        private void formasDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFormasPagos frm = new frmFormasPagos();
            frm.Text = "Formas de Pagos";
            frm.ShowDialog(this);
        }

        private void plantasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPlantas frm = new frmPlantas();
            frm.Text = "Plantas";
            frm.ShowDialog(this);
        }

        private void eventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEventos frm = new frmEventos();
            frm.Text = "Eventos";
            frm.ShowDialog(this);
        }

        private void tiposDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTiposDocumentos frm = new frmTiposDocumentos();
            frm.Text = "Tipos de Documentos";
            frm.ShowDialog(this);
        }

        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpleados frm = new frmEmpleados();
            frm.Text = "Empleados";
            frm.ShowDialog(this);
        }

        private void distribucionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDistribuciones frm = new frmDistribuciones();
            frm.Text = "Distribuciones";
            frm.ShowDialog(this);
        }

        private void localidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalidades frm = new frmLocalidades();
            frm.Text = "Localidades";
            frm.ShowDialog(this);
        }
    }
}
