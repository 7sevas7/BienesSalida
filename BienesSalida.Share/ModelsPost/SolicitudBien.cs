using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BienesSalida.Share.Models;

namespace BienesSalida.Share.ModelsPost
{
    public class SolicitudBien
    {

        [Required(ErrorMessage = "Área requerida.")]
        public string? Area { get; set; }

        [Required(ErrorMessage = "Encargado de área requerido.")]
        public string? EncargadoArea { get; set; }

        public string? Numero { get; set; }

        [Required(ErrorMessage = "No. Inventario requerido, solo se aceptan valores numéricos.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números enteros.")]
        public string? NumeroInventario { get; set; }

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Motivo de salida requerido.")]
        public string? MotivoSalida { get; set; }

        [Required(ErrorMessage = "Observaciones requerido.")]
        public string? Observaciones { get; set; }

        public Producto producto { set; get; }
    }
}
