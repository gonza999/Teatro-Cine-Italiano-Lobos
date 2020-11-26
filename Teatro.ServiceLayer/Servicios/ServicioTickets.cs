using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teatro.BussinesLayer.Entidades;
using Teatro.DataLayer;
using Teatro.DataLayer.Facades;
using Teatro.DataLayer.Repositorios;
using Teatro.ServiceLayer.Facades;

namespace Teatro.ServiceLayer.Servicios
{
    public class ServicioTickets : IServicioTickets
    {
        private IRepositorioTickets repositorio;
        private IRepositorioHorarios repositorioHorarios;
        private ConexionBD conexion;
        private IRepositorioLocalidades repositorioLocalidades;
        private IRepositorioFormasPagos repositorioFormasPagos;
        private IRepositorioFormasVentas repositorioFormasVentas;
        private SqlTransaction transaction;

        public void AnularTicket(int ticketId)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioTickets(conexion.AbrirConexion());
                repositorio.AnularTicket(ticketId);
                conexion.CerrarConexion();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Borrar(int id)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioTickets(conexion.AbrirConexion());
                repositorio.Borrar(id);
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Ticket ticket)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioTickets(conexion.AbrirConexion());
                var relacionado = repositorio.EstaRelacionado(ticket);
                conexion.CerrarConexion();
                return relacionado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Existe(Ticket ticket)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioTickets(conexion.AbrirConexion());
                var existe = repositorio.Existe(ticket);
                conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Existe(Localidad localidad,Horario horario)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioTickets(conexion.AbrirConexion());
                var existe = repositorio.Existe(localidad,horario);
                conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Ticket> GetLista()
        {
            try
            {
                conexion = new ConexionBD();
                SqlConnection cn = conexion.AbrirConexion();
                transaction = cn.BeginTransaction();
                repositorioLocalidades = new RepositorioLocalidades(cn, transaction);
                repositorioHorarios = new RepositorioHorarios(cn, transaction);
                repositorioFormasPagos = new RepositorioFormasPagos(cn, transaction);
                repositorioFormasVentas = new RepositorioFormasVentas(cn, transaction);
                repositorio = new RepositorioTickets(cn,repositorioLocalidades,repositorioHorarios,repositorioFormasPagos,repositorioFormasVentas, transaction);
                var lista = repositorio.GetLista();
                transaction.Commit();
                conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }

        public void Guardar(Ticket ticket)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioTickets(conexion.AbrirConexion());
                repositorio.Guardar(ticket);
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

     
    }
}
