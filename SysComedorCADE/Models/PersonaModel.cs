using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
namespace SysComedorCADE.Models
{
    public class PersonaModel
    {
        public int CodPersona { get; set; }
        public int? CodTipoPer { get; set; }
        public string NombresCompletos { get; set; }
        public string CiRuc { get; set; }
        public string Telf { get; set; }
        public string Cel { get; set; }
        public string Dir { get; set; }
        public int CodGenero { get; set; }
        public int CodEntidad { get; set; }
    }
}