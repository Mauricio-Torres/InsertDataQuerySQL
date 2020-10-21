using Behavior.Manager;
using Behavior.Model;
using DataConect.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntinerarioAvionController : ControllerBase
    {
        private readonly IntinerarioAvionManager _context;
        private readonly ILogger<IntinerarioAvionController> logger;

        public IntinerarioAvionController(IntinerarioAvionManager context, ILogger<IntinerarioAvionController> _logger)
        {
            _context = context;
            logger = _logger;
        }

        /// <summary>
        /// Retorna los intinerarios de un avion 
        /// </summary>
        /// <param name="idAvion"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/intinerarioAvion")]
        public List<IntinerarioOutput> GetIntinerarios(int idAvion)
        {
            List<IntinerarioOutput> paciente = new List<IntinerarioOutput>();
            try
            {
                paciente = _context.GetIntinerario(idAvion);


            }
            catch (Exception ex)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exception ", ex.Message);
            }

            return paciente;
        }

        /// <summary>
        /// retorna todos los intinirarios registrados 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/intinerarios")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "<pendiente>")]
        public List<IntinerarioOutput> GetIntinerarios()
        {
            try
            {
                var paciente = _context.GetIntinerarios();

                return paciente;
            }
            catch (Exception ex)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exception ", ex.Message);
            }
            return null;
        }

        /// <summary>
        /// actualiza un intinirario de un avion
        /// </summary>
        /// <param name="paciente"></param>
        [HttpPost]
        [Route("/cambiarIntinerario")]
        public void UpdateIntinerarios(IntinerarioInput paciente)
        {
            try
            {
                _context.Update(paciente);
            }
            catch (Exception ex)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exception ", ex.Message);
            }
        }


        /// <summary>
        /// inserta un intinerario de algun avion
        /// </summary>
        /// <param name="paciente"></param>
        [HttpPost]
        [Route("/ingresarIntinerario")]
        public void InsertIntinerario(IntinerarioInput paciente)
        {
            try
            {
                _context.Insert(paciente);
            }
            catch (Exception ex)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exception ", ex.Message);
            }
        }
        /// <summary>
        /// borra un intenirario de un avion 
        /// </summary>
        /// <param name="idPaciente"></param>
        [HttpDelete]
        [Route("/borrarIntinerario")]
        public void DeleteIntinerario(int idIntinerario)
        {
            try
            {
                _context.Delete(idIntinerario);
            }
            catch (Exception ex)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exception ", ex.Message);
            }
        }


        /// <summary>
        /// retorna las ciudades con menor y mayor numero de visitas realizado
        /// </summary>
        /// <param name="tipoVisita">si ingresa mayor obtendra la ciudad con mayor num de visitas y menor o vacio obtendra la ciudad con el menor num de visitas</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/ciudadesVisitadas")]
        public List<CiudadesVisitadas> ciudadesVisitadas(string tipoVisita)
        {
            try
            {
                return _context.ciudadesVisitadas(tipoVisita);
            }
            catch (Exception ex)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exception ", ex.Message);
            }
            return null;
        }

        /// <summary>
        /// obtiene el numero de horas que ha tenido un avion 
        /// </summary>
        /// <param name="id">id del avion  </param>
        /// <returns></returns>
        [HttpGet]
        [Route("/HorasDeVuelo")]
        public int HorasDeVuelo(int id)
        {
            try
            {
                return _context.HorasDeVuelo(id);
            }
            catch (Exception ex)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exception ", ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// Retorna el avion con el menor tiempo de vuelo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/MenorTiempoVuelo")]
        public object MenorTiempoVuelo()
        {
            try
            {
                return _context.MenorTiempoVuelo();
            }
            catch (Exception ex)
            {
                Logger.Logguer(System.Reflection.MethodBase.GetCurrentMethod().Name, "Exception ", ex.Message);
            }
            return 0;
        }
    }


}
