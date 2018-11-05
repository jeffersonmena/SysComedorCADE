using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysComedorCADE.Models
{
    public class VentaModel
    {

        public int CodVenta { get; set; }
        public int CodPersona { get; set; }
        public int CodTipoPago { get; set; }
        public int Cantidad { get; set; }
        public string Detalle { get; set; }
        public decimal Costo { get; set; }
        public decimal Total { get; set; }
        public DateTime FVenta { get; set; }
        public int anio { get; set; }


    }
}