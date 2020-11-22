using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class DistribucionUbicacion:ICloneable   
    {
        public Distribucion Distribucion { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public decimal Precio { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
