using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class Horario:ICloneable
    {
        public int HorarioId { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public Evento Evento { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
