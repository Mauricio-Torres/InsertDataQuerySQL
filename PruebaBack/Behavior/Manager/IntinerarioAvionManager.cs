using Behavior.Interfas;
using Behavior.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Behavior.Manager
{
    public class IntinerarioAvionManager
    {

        IIntinerarioRepositorio _intinerarioRepositorio;
        public IntinerarioAvionManager(IIntinerarioRepositorio intinerarioRepositorio)
        {
            _intinerarioRepositorio = intinerarioRepositorio;
        }


        public List<IntinerarioOutput> GetIntinerario(int idIntienrario) 
        {
            try
            {
                List<IntinerarioOutput> intinerarioOutputs = new List<IntinerarioOutput>();

                var intinerarios = _intinerarioRepositorio.GetIntinerario(idIntienrario);
                if (intinerarios != null) 
                {
                    foreach (var intinerario in intinerarios)
                    {
                        intinerarioOutputs.Add(new IntinerarioOutput()
                        {
                            Id = intinerario.Id,
                            IdComercial = intinerario.IdAvion,
                            LugarVuelo = intinerario.LugarVuelo,
                            TiempoLLegada = intinerario.TiempoLLegada,
                            TiempoSalida = intinerario.TiempoSalida
                        });
                    }
                    return intinerarioOutputs;
                }
              
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<IntinerarioOutput> GetIntinerarios()
        {
            try
            {

                var intinerarios = _intinerarioRepositorio.GetIntinerarios();
                var listPacientes = new List<IntinerarioOutput>();

                foreach (var intinerario in intinerarios) {
                  
                    listPacientes.Add(new IntinerarioOutput()
                    {
                        Id = intinerario.Id,
                        IdComercial = intinerario.IdAvion,
                        LugarVuelo = intinerario.LugarVuelo,
                        TiempoLLegada = intinerario.TiempoLLegada,
                        TiempoSalida = intinerario.TiempoSalida
                    });
                };

                return listPacientes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(IntinerarioInput intinerario)
        {
            try
            {
                var output = new AvionComercial()
                {
                    Id = intinerario.Id,
                    IdAvion = intinerario.IdComercial,
                    LugarVuelo = intinerario.LugarVuelo,
                    TiempoLLegada = intinerario.TiempoLLegada,
                    TiempoSalida = intinerario.TiempoSalida
                };

                 _intinerarioRepositorio.Update(output);
              
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insert(IntinerarioInput intinerario)
        {
            try
            {
                var pacienteOutput = new AvionComercial()
                {
                    IdAvion = intinerario.IdComercial,
                    LugarVuelo = intinerario.LugarVuelo,
                    TiempoLLegada = intinerario.TiempoLLegada,
                    TiempoSalida = intinerario.TiempoSalida
                };

                _intinerarioRepositorio.Insert(pacienteOutput);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(int IdPaciente)
        {
            try
            {
                _intinerarioRepositorio.Delet(IdPaciente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CiudadesVisitadas> ciudadesVisitadas(string tipoVisita)
        {
            return _intinerarioRepositorio.ciudadesVisitadas(tipoVisita);
        }
        public int HorasDeVuelo(int id)
        {
            return _intinerarioRepositorio.HorasDeVuelo(id);
        }
        public object MenorTiempoVuelo()
        {
            return _intinerarioRepositorio.MenorTiempoVuelo();
        }
    }
}
