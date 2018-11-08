using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//para validar
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysComedorCADE.Models
{
    public class CostosModel
    {
        
        public int CodCosto { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public string Detalle { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public decimal Valor { get; set; }
        public DateTime? FRegistro { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        public bool Estado { get; set; }       
        public int? anio { get; set; }
    }
}