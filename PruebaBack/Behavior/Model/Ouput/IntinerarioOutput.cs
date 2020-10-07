using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Behavior.Model
{
    public class IntinerarioOutput
    {
        public int Id { set; get; }
        public int IdComercial { set; get; }
        public string LugarVuelo { set; get; }
        public DateTime TiempoSalida { set; get; }
        public DateTime TiempoLLegada { set; get; }
    }
}
