﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioFormasPagos
    {
        FormaPago GetFormaPagoPorId(int id);
        List<FormaPago> GetLista();
        void Guardar(FormaPago formaPago);
        void Borrar(int id);
        bool Existe(FormaPago formaPago);
        bool EstaRelacionado(FormaPago formaPago);
        FormaPago GetFormaPago(string nombreFormaPago);
        List<FormaPago> BuscarFormaPago(string text);
    }
}
