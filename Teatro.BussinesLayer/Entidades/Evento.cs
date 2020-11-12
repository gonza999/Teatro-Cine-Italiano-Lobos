using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class Evento:ICloneable
    {
        public int EventoId { get; set; }
        public TipoEvento TipoEvento { get; set; }
        public Clasificacion Clasificacion { get; set; }
        public string NombreEvento { get; set; }
        public DateTime FechaEvento { get; set; }
        public string Descripcion { get; set; }
        public bool Suspendido { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
