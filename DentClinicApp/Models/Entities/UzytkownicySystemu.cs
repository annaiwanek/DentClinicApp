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
    
    public partial class UzytkownicySystemu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UzytkownicySystemu()
        {
            this.LogiAktywnosci = new HashSet<LogiAktywnosci>();
            this.Platnosci = new HashSet<Platnosci>();
        }
    
        public int IdUzytkownika { get; set; }
        public Nullable<int> IdPracownika { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }
        public string Rola { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogiAktywnosci> LogiAktywnosci { get; set; }
        public virtual Pracownicy Pracownicy { get; set; }
        public virtual Pracownicy Pracownicy1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Platnosci> Platnosci { get; set; }
    }
}
