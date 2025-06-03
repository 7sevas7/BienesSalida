using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BienesSalida.Share.Models
{
    public class Producto
    {
        public int? bienID { get; set; }
        public int? numeroInventario_SAI { get; set; }
        public string? tipoBien { get; set; }
        public string? descripcion { get; set; }
        public string? marca { get; set; }
        public string? modelo { get; set; }
        public string? numeroDeSerie { get; set; }
        public string? unidadMedida { get; set; }
        public string? descripcionArticulo { get; set; }
        public string? status { get; set; }
        public string? persona { get; set; }
        public int? numeroEmpleado { get; set; }
        public string? rfc { get; set; }
        public string? unidadPresupuestal_tipoArea { get; set; }
        public string? dependencia { get; set; }
        public string? subdependencia { get; set; }
        public string? area { get; set; }
    }
}
