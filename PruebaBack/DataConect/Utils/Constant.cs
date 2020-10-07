using System;
using System.Collections.Generic;
using System.Text;

namespace DataConect.Utils
{
    public static class Constant
    {
        public static readonly string sqlIntinerarios = @"SELECT [IdAvion]
                                                      ,[LugarVuelo]
                                                      ,[TiempoSalida]
                                                      ,[TiempoLLegada]
                                                      ,[Id]
                                                  FROM [Aeropuerto].[dbo].[IntinerarioAvion]";

        public static readonly string sqlIntinerario = @"SELECT [IdAvion]
                                                      ,[LugarVuelo]
                                                      ,[TiempoSalida]
                                                      ,[TiempoLLegada]
                                                      ,[Id]
                                                  FROM [Aeropuerto].[dbo].[IntinerarioAvion]
                                                  where ID ={0}";

        public static readonly string sqlCiudadesMasVisitadas = @"execute CiudadesMasVisitadas"; 
        public static readonly string sqlHorasDeVuelo = @"execute HorasDeVuelo {0}";
        public static readonly string sqlMenorTiempoVuelo = @"execute MenorTiempoVuelo";
    }
}
