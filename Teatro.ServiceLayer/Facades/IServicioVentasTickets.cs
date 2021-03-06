﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;

namespace Teatro.ServiceLayer.Facades
{
    public interface IServicioVentasTickets
    {
        VentaTicket GetVentaTicketPorId(int ventaId, int ticketId);
        List<VentaTicket> GetLista();
        void Guardar(Venta venta, Ticket ticket);
        void Borrar(int id);
        bool Existe(Venta venta, Ticket ticket);
        bool EstaRelacionado(Venta venta, Ticket ticket);
        VentaTicket GetVentaTicket(Venta venta, Ticket ticket);
        List<int> GetListaVentas(List<Ticket> listaTickets);
        //List<VentaTicket> BuscarVentaTicket(string clasificacion);
    }
}
