using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class Ticket:ICloneable
    {
        public int TicketId { get; set; }
        public Horario Horario { get; set; }
        public decimal Importe { get; set; }
        public Localidad Localidad { get; set; }
        public DateTime FechaVenta { get; set; }
        public FormaPago FormaPago { get; set; }
        public FormaVenta FormaVenta { get; set; }
        public bool Anulada { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
