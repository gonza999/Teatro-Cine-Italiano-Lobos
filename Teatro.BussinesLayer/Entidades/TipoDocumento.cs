﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teatro.BussinesLayer.Entidades
{
    public class TipoDocumento:ICloneable
    {
        public int TipoDocumentoId { get; set; }
        public string NombreTipoDocumento { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
