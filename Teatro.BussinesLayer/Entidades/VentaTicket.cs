using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class VentaTicket:ICloneable
    {
        public Ticket Ticket { get; set; }
        public Venta Venta { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
