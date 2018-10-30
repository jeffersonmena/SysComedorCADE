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
    
    public partial class Venta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venta()
        {
            this.EstadoCuentaPersona = new HashSet<EstadoCuentaPersona>();
        }
    
        public int CodVenta { get; set; }
        public int CodPersona { get; set; }
        public int CodTipoPago { get; set; }
        public int Cantidad { get; set; }
        public string Detalle { get; set; }
        public decimal Costo { get; set; }
        public decimal Total { get; set; }
        public System.DateTime FVenta { get; set; }
        public int anio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadoCuentaPersona> EstadoCuentaPersona { get; set; }
        public virtual Persona Persona { get; set; }
        public virtual TipoPago TipoPago { get; set; }
    }
}
