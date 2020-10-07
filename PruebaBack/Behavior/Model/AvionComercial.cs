using System;
using System.Collections.Generic;
using System.Text;

namespace Behavior.Model
{
    public class AvionComercial
    {
        public int Id { set; get; }
        public int IdAvion { set; get; }
        public string LugarVuelo { set; get; }
        public DateTime TiempoSalida { set; get; }
        public DateTime TiempoLLegada { set; get; }

    }
}
