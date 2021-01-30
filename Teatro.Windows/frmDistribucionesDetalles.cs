using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.Windows
{
    public partial class frmDistribucionesDetalles : Form
    {
        public frmDistribucionesDetalles()
        {
            InitializeComponent();
        }
        private Distribucion distribucion;
        internal void SetDistribucion(Distribucion distribucion)
        {
            this.distribucion = distribucion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDistribucionesDetalles_Load(object sender, EventArgs e)
        {
            lblDistribucion.Text = distribucion.NombreDistribucion;
            foreach (var d in distribucion.DistribucionLocalidad)
            {
                foreach (var l in d.Localidades)
                {
                    if (l.Ubicacion.UbicacionId==2)
                    {
                        txtPalcos.Text = d.Precio.ToString();
                    }
                    else
                    {
                       
                        switch (l.Fila)
                        {
                            case 1:
                                fila1.Text = d.Precio.ToString();
                                break;
                            case 2:
                                fila2.Text = d.Precio.ToString();
                                break;
                            case 3:
                                fila3.Text = d.Precio.ToString();
                                break;
                            case 4:
                                fila4.Text = d.Precio.ToString();
                                break;
                            case 5:
                                fila5.Text = d.Precio.ToString();
                                break;
                            case 6:
                                fila6.Text = d.Precio.ToString();
                                break;
                            case 7:
                                fila7.Text = d.Precio.ToString();
                                break;
                            case 8:
                                fila8.Text = d.Precio.ToString();
                                break;
                            case 9:
                                fila9.Text = d.Precio.ToString();
                                break;
                            case 10:
                                fila10.Text = d.Precio.ToString();
                                break;
                            case 11:
                                fila11.Text = d.Precio.ToString();
                                break;
                            case 12:
                                fila12.Text = d.Precio.ToString();
                                break;
                            case 13:
                                fila13.Text = d.Precio.ToString();
                                break;
                            case 14:
                                fila14.Text = d.Precio.ToString();
                                break;
                            case 15:
                                fila15.Text = d.Precio.ToString();
                                break;
                            case 16:
                                fila16.Text = d.Precio.ToString();
                                break;
                            case 17:
                                fila17.Text = d.Precio.ToString();
                                break;
                            case 18:
                                fila18.Text = d.Precio.ToString();
                                break;
                            case 19:
                                fila19.Text = d.Precio.ToString();
                                break;
                            case 20:
                                fila20.Text = d.Precio.ToString();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
