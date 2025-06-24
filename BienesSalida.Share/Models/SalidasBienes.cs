using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BienesSalida.Share.Models
{
    public class SalidasBienes
    {
        public int? idSal { get; set; }
        public int? idUserEU { get; set; }
        public string? fechaHora { get; set; }
        public string? nombre { get; set; }
        public int? noSal { get; set; }
        public long? noInven { get; set; }
        public string? descrip { get; set; }
        public string? motivo { get; set; }
        public string? observa { get; set; }
        public string? area { get; set; }
        public string? eArea { get; set; }
        public string? estatus { get; set; }

    }
}
