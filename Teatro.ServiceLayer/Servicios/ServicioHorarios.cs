﻿using System;
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
    public class ServicioHorarios:IServicioHorarios
    {
        private IRepositorioHorarios repositorio;
        private ConexionBD conexion;
        private IRepositorioEventos repositorioEventos;
        private IRepositorioDistribuciones repositorioDistribuciones;
        private IRepositorioClasificaciones repositorioClasificaciones;
        private IRepositorioTipoEventos repositorioTipoEventos;
        private IServicioTickets servicioTickets;
        private SqlTransaction transaction;
        public ServicioHorarios()
        {

        }
        public void Borrar(Horario horario)
        {
            try
            {
                conexion = new ConexionBD();
                SqlConnection cn = conexion.AbrirConexion();
                transaction = cn.BeginTransaction();
                servicioTickets = new ServicioTickets(transaction);
                repositorio = new RepositorioHorarios(cn,transaction);
                servicioTickets.BorrarPorHorario(horario);
                repositorio.Borrar(horario.HorarioId);
                transaction.Commit();
                conexion.CerrarConexion();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }

        public bool EstaRelacionado(Horario horario)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioHorarios(conexion.AbrirConexion());
                var relacionado = repositorio.EstaRelacionado(horario);
                conexion.CerrarConexion();
                return relacionado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Existe(Horario horario)
        {
            try
            {
                conexion = new ConexionBD();
                //SqlConnection cn = conexion.AbrirConexion();
                //transaction = cn.BeginTransaction();
                //repositorioClasificaciones = new RepositorioClasificaciones(cn, transaction);
                //repositorioDistribuciones = new RepositorioDistribuciones(cn, transaction);
                //repositorioEventos = new RepositorioEventos(cn, repositorioTipoEventos, repositorioClasificaciones, repositorioDistribuciones, transaction);
                repositorio = new RepositorioHorarios(conexion.AbrirConexion());
                var existe = repositorio.Existe(horario);
                //transaction.Commit();
                conexion.CerrarConexion();
                return existe;
            }
            catch (Exception e)
            {
                //transaction.Rollback();
                throw new Exception(e.Message);
            }
        }

        public List<Horario> GetLista()
        {
            try
            {
                conexion = new ConexionBD();
                SqlConnection cn = conexion.AbrirConexion();
                transaction = cn.BeginTransaction();
                repositorioClasificaciones = new RepositorioClasificaciones(cn, transaction);
                repositorioDistribuciones = new RepositorioDistribuciones(cn, transaction);
                repositorioTipoEventos = new RepositorioTipoEvento(cn,transaction);
                repositorioEventos = new RepositorioEventos(cn, repositorioTipoEventos, repositorioClasificaciones, repositorioDistribuciones, transaction);
                repositorio = new RepositorioHorarios(cn, repositorioEventos, transaction);
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
        public List<Horario> GetLista(Evento evento)
        {
            try
            {
                conexion = new ConexionBD();
                SqlConnection cn = conexion.AbrirConexion();
                transaction = cn.BeginTransaction();
                repositorioClasificaciones = new RepositorioClasificaciones(cn);
                repositorioDistribuciones = new RepositorioDistribuciones(cn);
                repositorioTipoEventos = new RepositorioTipoEvento(cn, transaction);
                repositorioEventos = new RepositorioEventos(cn, repositorioTipoEventos, repositorioClasificaciones, repositorioDistribuciones, transaction);
                repositorio = new RepositorioHorarios(cn, repositorioEventos, transaction);
                var lista = repositorio.GetLista(evento);
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

        public void Guardar(Horario horario)
        {
            try
            {
                conexion = new ConexionBD();
                repositorio = new RepositorioHorarios(conexion.AbrirConexion()) ;
                repositorio.Guardar(horario);

                conexion.CerrarConexion();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
