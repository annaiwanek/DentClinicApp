//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DentClinicApp.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recepty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Recepty()
        {
            this.ReceptyLeki = new HashSet<ReceptyLeki>();
        }
    
        public int IdRecepty { get; set; }
        public int IdPacjenta { get; set; }
        public int IdPracownika { get; set; }
        public System.DateTime DataWystawienia { get; set; }
        public string Uwagi { get; set; }
    
        public virtual Pacjenci Pacjenci { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceptyLeki> ReceptyLeki { get; set; }
        public virtual Pracownicy Pracownicy { get; set; }
    }
}
