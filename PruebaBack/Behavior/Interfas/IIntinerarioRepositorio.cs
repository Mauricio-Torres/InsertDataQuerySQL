using Behavior.Model;
using System.Collections.Generic;

namespace Behavior.Interfas
{
    public interface IIntinerarioRepositorio
    {
        List<AvionComercial> GetIntinerarios();
        List<AvionComercial> GetIntinerario(int idPaciente);
        void Update(AvionComercial intienrario);
        void Insert(AvionComercial intienrario);
        void Delet(int idIntienrario);


        List<CiudadesVisitadas> ciudadesVisitadas(string tipoVisita);
        int HorasDeVuelo(int id);
        object MenorTiempoVuelo();


    }
}
