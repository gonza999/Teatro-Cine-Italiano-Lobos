using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class Localidad:ICloneable
    {
        public int LocalidadId { get; set; }
        public Planta Planta { get; set; }

        public int Numero { get; set; }

        public Ubicacion Ubicacion { get; set; }

        public int Fila { get; set; }
        public object Clone()
        {
           return this.MemberwiseClone();
        }
    }
}
