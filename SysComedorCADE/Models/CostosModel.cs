using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SysComedorCADE.Models
{
    public class CostosModel
    {

        public int CodCosto { get; set; }
        public string Detalle { get; set; }
        public decimal Valor { get; set; }
        public DateTime FRegistro { get; set; }
        public bool Estado { get; set; }
        public int anio { get; set; }
    }
}