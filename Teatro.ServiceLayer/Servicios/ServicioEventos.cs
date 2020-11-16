﻿using System;
using System.Collections.Generic;
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
    public class ServicioEventos:IServicioEventos
    {
        private IRepositorioEventos repositorio;
        private ConexionBD conexion;
        private IRepositorioClasificaciones repositorioClasificaciones;
        private IRepositorioTipoEventos repositorioTipoEventos;
        public ServicioEventos()
        {

        }
        public void Borrar(int id)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioClasificaciones = new RepositorioClasificaciones(conexion.AbrirConexion());
                repositorioTipoEventos = new RepositorioTipoEvento(conexion.AbrirConexion());
                repositorio = new RepositorioEventos(conexion.AbrirConexion(), repositorioTipoEventos, repositorioClasificaciones);
                repositorio.Borrar(id);
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Evento evento)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioClasificaciones = new RepositorioClasificaciones(conexion.AbrirConexion());
                repositorioTipoEventos = new RepositorioTipoEvento(conexion.AbrirConexion());
                repositorio = new RepositorioEventos(conexion.AbrirConexion(), repositorioTipoEventos, repositorioClasificaciones);
                var relacionado = repositorio.EstaRelacionado(evento);
                conexion.CerrarConexion();
                return relacionado;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool Existe(Evento evento)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioClasificaciones = new RepositorioClasificaciones(conexion.AbrirConexion());
                repositorioTipoEventos = new RepositorioTipoEvento(conexion.AbrirConexion());
                repositorio = new RepositorioEventos(conexion.AbrirConexion(), repositorioTipoEventos, repositorioClasificaciones);
                var existe = repositorio.Existe(evento);
                conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Evento> GetLista()
        {
            try
            {
                conexion = new ConexionBD();
                repositorioClasificaciones = new RepositorioClasificaciones(conexion.AbrirConexion());
                repositorioTipoEventos = new RepositorioTipoEvento(conexion.AbrirConexion());
                repositorio = new RepositorioEventos(conexion.AbrirConexion(), repositorioTipoEventos, repositorioClasificaciones);
                var lista = repositorio.GetLista();
                conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        //public List<Evento> GetLista(int clasificacionId)
        //{
        //    try
        //    {
        //        conexion = new ConexionBD();
        //        repositorioClasificaciones = new RepositorioClasificaciones(conexion.AbrirConexion());
        //        repositorioTipoEventos = new RepositorioTipoEvento(conexion.AbrirConexion());
        //        repositorio = new RepositorioEventos(conexion.AbrirConexion(),repositorioTipoEventos,repositorioClasificaciones);
        //        var lista = repositorio.GetLista(clasificacionId);
        //        conexion.CerrarConexion();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public List<Evento> GetLista(TipoEvento tipoEvento)
        //{
        //    try
        //    {
        //        conexion = new ConexionBd();
        //        repositorioClasificaciones = new RepositorioClasificaciones(conexion.AbrirConexion());
        //        repositorioTipoEventos = new RepositorioTipoEventos(conexion.AbrirConexion());
        //        repositorioMedidas = new RepositorioMedidas(conexion.AbrirConexion());
        //        repositorio = new RepositorioEventos(conexion.AbrirConexion(), repositorioClasificaciones, repositorioTipoEventos, repositorioMedidas);
        //        var lista = repositorio.GetLista(tipoEvento);
        //        conexion.CerrarConexion();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public List<Evento> GetLista(string descripcion)
        //{
        //    try
        //    {
        //        conexion = new ConexionBd();
        //        repositorioClasificaciones = new RepositorioClasificaciones(conexion.AbrirConexion());
        //        repositorioTipoEventos = new RepositorioTipoEventos(conexion.AbrirConexion());
        //        repositorioMedidas = new RepositorioMedidas(conexion.AbrirConexion());
        //        repositorio = new RepositorioEventos(conexion.AbrirConexion(), repositorioClasificaciones, repositorioTipoEventos, repositorioMedidas);
        //        var lista = repositorio.GetLista(descripcion);
        //        conexion.CerrarConexion();
        //        return lista;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        //public Evento GetEventoPorCodigoDeBarras(string codigo)
        //{
        //    try
        //    {
        //        conexion = new ConexionBd();
        //        repositorioClasificaciones = new RepositorioClasificaciones(conexion.AbrirConexion());
        //        repositorioTipoEventos = new RepositorioTipoEventos(conexion.AbrirConexion());
        //        repositorioMedidas = new RepositorioMedidas(conexion.AbrirConexion());
        //        repositorio = new RepositorioEventos(conexion.AbrirConexion(), repositorioClasificaciones, repositorioTipoEventos, repositorioMedidas);
        //        var evento = repositorio.GetEventoPorCodigoDeBarras(codigo);
        //        conexion.CerrarConexion();
        //        return evento;
        //    }
        //    catch (Exception e)
        //    {

        //        throw new Exception(e.Message);
        //    }
        //}

        public Evento GetEventoPorId(int id)
        {
            try
            {
                conexion = new ConexionBD();
                repositorioClasificaciones = new RepositorioClasificaciones(conexion.AbrirConexion());
                repositorioTipoEventos = new RepositorioTipoEvento(conexion.AbrirConexion());
                repositorio = new RepositorioEventos(conexion.AbrirConexion(),repositorioTipoEventos ,repositorioClasificaciones );
                var evento = repositorio.GetEventoPorId(id);
                conexion.CerrarConexion();
                return evento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Guardar(Evento evento)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioEventos(conexion.AbrirConexion());
                repositorio.Guardar(evento);
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public List<Evento> BuscarEvento(string text)
        {
            try
            {
                List<Evento> lista = new List<Evento>();
                conexion = new ConexionBD();
                repositorio = new RepositorioEventos(conexion.AbrirConexion(),repositorioTipoEventos,repositorioClasificaciones);
                lista=repositorio.BuscarEvento(text);
                conexion.CerrarConexion();
                return lista;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}