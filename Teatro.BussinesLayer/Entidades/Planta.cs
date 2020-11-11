using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class Planta:ICloneable
    {
        public int PlantaId { get; set; }
        public string NombrePlanta { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
