using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class Distribucion:ICloneable
    {
        public int DistribucionId { get; set; }
        public string NombreDistribucion { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
