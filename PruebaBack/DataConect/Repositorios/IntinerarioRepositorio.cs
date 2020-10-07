using Behavior.Interfas;
using Behavior.Model;
using DataConect.Utils;
using PruebaTecnica.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataConect.Repositorios
{
    public class IntinerarioRepositorio : IIntinerarioRepositorio
    {
        private readonly DataContext _context;
        public IntinerarioRepositorio(DataContext dataContext)
        {
            _context = dataContext;
        }

        public List<AvionComercial> GetIntinerario(int idIntinerario)
        {
            var sSQL = string.Format(Constant.sqlIntinerario, idIntinerario);

            var table = _context.GetEntity(sSQL).AsEnumerable().AsQueryable();

            var Intinerario = (from bp in table
                            select new AvionComercial()
                          {
                              Id = bp.Field<int>("Id"),
                                IdAvion = bp.Field<int>("IdAvion"),
                                LugarVuelo = bp.Field<string>("LugarVuelo"),
                                TiempoLLegada = bp.Field<DateTime>("TiempoLLegada"),
                                TiempoSalida = bp.Field<DateTime>("TiempoSalida"),
                            }).ToList();

            return Intinerario;
        }

        public List<AvionComercial> GetIntinerarios()
        {
            try
            {
                var tableIntinerarios = _context.GetEntity(Constant.sqlIntinerarios).AsEnumerable().AsQueryable();

                var Intinerarios = (from bp in tableIntinerarios
                                select new AvionComercial()
                                {
                                    Id = bp.Field<int>("Id"),
                                    IdAvion = bp.Field<int>("IdAvion"),
                                    LugarVuelo = bp.Field<string>("LugarVuelo"),
                                    TiempoLLegada = bp.Field<DateTime>("TiempoLLegada"),
                                    TiempoSalida = bp.Field<DateTime>("TiempoSalida"),
                                }).ToList();

                return Intinerarios;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Insert(AvionComercial Intinerario)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[IntinerarioAvion] ([IdAvion] ,[LugarVuelo] ,[TiempoSalida] ,[TiempoLLegada] )");
            sb.Append("VALUES ");
            sb.Append("(");
            sb.Append("@IdAvion, ");
            sb.Append("@LugarVuelo, ");
            sb.Append("@TiempoSalida, ");
            sb.Append("@TiempoLLegada");
            sb.Append(")");

            var listProperty = GenericPropertyEntity<AvionComercial>.PrintTModelProperty(Intinerario);

            _context.ExecuteQuery(listProperty, sb.ToString());
        }
        public void Update (AvionComercial Intinerario)
        {
            if (ExisteIntinerario(Intinerario.Id))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Update IntinerarioAvion SET ");
                sb.Append("[LugarVuelo]=@LugarVuelo, ");
                sb.Append("[TiempoSalida]=@TiempoSalida, ");
                sb.Append("[TiempoLLegada]=@TiempoLLegada, ");
                sb.Append("[IdAvion]=@IdAvion ");
                sb.Append("WHERE Id=@Id");

                var listProperty = GenericPropertyEntity<AvionComercial>.PrintTModelProperty(Intinerario);
                _context.ExecuteQuery(listProperty, sb.ToString());
            }        
        }
        public void Delet(int idIntinerario)
        {
            if (ExisteIntinerario(idIntinerario))
            {
                var sql = string.Format("DELETE FROM [dbo].[IntinerarioAvion] WHERE Id = {0}", idIntinerario);
                _context.ExecuteQuery(sql);
            }
        }

        private bool ExisteIntinerario(int idIntinerario) 
        {
            var sSQL = string.Format(Constant.sqlIntinerario, idIntinerario);
            var tableidIntinerario = _context.GetEntity(sSQL).AsEnumerable().AsQueryable().FirstOrDefault();

            return tableidIntinerario != null;
        }

        public List<CiudadesVisitadas> ciudadesVisitadas(string tipoVisita)
        {
            var sSQL = string.Format(Constant.sqlCiudadesMasVisitadas);

            var table = _context.GetEntity(sSQL).AsEnumerable().AsQueryable();

            var data = (from bp in table
                        select new CiudadesVisitadas()
                        {
                            NumVisitas = bp.Field<int>("NumVisitas"),
                            LugarVuelo = bp.Field<string>("LugarVuelo"),

                        }).ToList();

            if (tipoVisita == "mayor")
            {
                var retorno = data.Where (x =>  x.NumVisitas == data.Max(x => x.NumVisitas)).ToList();
                return retorno;
            } 
            else
            {
                var retorno = data.Where(x => x.NumVisitas == data.Min(x => x.NumVisitas)).ToList();
                return retorno;
            }
        }

        public int HorasDeVuelo(int idAvion)
        {

            var sSQL = string.Format(Constant.sqlHorasDeVuelo, idAvion);

            var table = _context.GetEntity(sSQL).AsEnumerable().AsQueryable();

            var data = (from bp in table
                        select new
                        {
                            data = bp.Field<int>("MinutosTranscurridos"),
                        }).FirstOrDefault();

            return data.data;
        }

        public object MenorTiempoVuelo()
        {
            var sSQL = string.Format(Constant.sqlMenorTiempoVuelo);

            var table = _context.GetEntity(sSQL).AsEnumerable().AsQueryable();

            var data = (from bp in table
                        select new
                        {
                            IdAvion = bp.Field<int>("IdAvion"),
                            marca = bp.Field<string>("marca"),
                            MinutosTranscurridos = bp.Field<int>("MinutosTranscurridos"),
                        }).ToList();

            var retorno = data.Where(x => x.MinutosTranscurridos == data.Min(x => x.MinutosTranscurridos)).FirstOrDefault();

            return retorno;
        }
    }
}
