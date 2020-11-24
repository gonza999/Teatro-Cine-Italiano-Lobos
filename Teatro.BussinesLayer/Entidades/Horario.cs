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
        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha.Date+Hora.TimeOfDay; }
            set { fecha = value.Date + Hora.TimeOfDay; }
        }

        private DateTime hora;

        public DateTime Hora
        {
            get { return hora; }
            set { hora = value; }
        }
  


        public Evento Evento { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{Fecha.Date} {Hora}";
        }
    }
}
