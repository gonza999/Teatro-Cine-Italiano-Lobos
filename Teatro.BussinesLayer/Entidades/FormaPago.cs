using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class FormaPago:ICloneable
    {
        public int FormaPagoId { get; set; }

        public string NombreFormaPago { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
