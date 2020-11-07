using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class Clasificacion : ICloneable
    {
        public int ClasificacionId { get; set; }

        public string NombreClasificacion { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
