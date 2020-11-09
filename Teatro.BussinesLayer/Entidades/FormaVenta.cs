using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class FormaVenta:ICloneable
    {
        public int FormaVentaId { get; set; }
        public string NombreFormaVenta { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
