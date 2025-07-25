﻿using System;
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
        public string? Nombre { get; set; }
        public string? Fecha { get; set; }
        public int? Numero { get; set; } 

        [Required(ErrorMessage = "Área requerida.")]
        public string? Area { get; set; }
        [Required(ErrorMessage = "Encargado de área requerido.")]
        public string? EncargadoArea { get; set; }
        
        public long? NumeroInventario { get; set; }
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "Motivo de salida requerido.")]
        public string? MotivoSalida { get; set; }
        public string? Observaciones { get; set; }
        //public Producto? producto { get; set; } = new Producto();
        public string? estatus { get; set; } = "Salida";
    }
}
