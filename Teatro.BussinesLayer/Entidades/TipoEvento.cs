using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class TipoEvento:ICloneable
    {
        public int TipoEventoId { get; set; }
        public string NombreTipoEvento { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
