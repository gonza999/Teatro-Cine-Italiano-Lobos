using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class Venta:ICloneable
    {
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public bool Estado { get; set; }
        public Empleado Empleado { get; set; }

        public List<Ticket> Tickets { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
