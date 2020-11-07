using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class Ubicacion:ICloneable
    {
        public int UbicacionId { get; set; }
        public string NombreUbicacion { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
