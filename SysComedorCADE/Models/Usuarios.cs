//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SysComedorCADE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuarios
    {
        public int CodUsuario { get; set; }
        public int CodPersona { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public System.DateTime FRegistro { get; set; }
        public bool Estado { get; set; }
        public Nullable<int> politica { get; set; }
    
        public virtual Persona Persona { get; set; }
    }
}
