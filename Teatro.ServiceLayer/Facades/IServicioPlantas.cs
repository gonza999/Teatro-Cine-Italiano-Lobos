﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioPlantas
    {
        Planta GetPlantaPorId(int id);
        List<Planta> GetLista();
        void Guardar(Planta planta);
        void Borrar(int id);
        bool Existe(Planta planta);
        bool EstaRelacionado(Planta planta);
        Planta GetPlanta(string nombrePlanta);
        List<Planta> BuscarPlanta(string text);
    }
}
