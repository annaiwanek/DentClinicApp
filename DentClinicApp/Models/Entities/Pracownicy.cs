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
    
    public partial class Pracownicy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pracownicy()
        {
            this.GrafikPracownikow = new HashSet<GrafikPracownikow>();
            this.HistoriaLeczenia = new HashSet<HistoriaLeczenia>();
            this.UzytkownicySystemu = new HashSet<UzytkownicySystemu>();
            this.UzytkownicySystemu1 = new HashSet<UzytkownicySystemu>();
            this.Wizyty = new HashSet<Wizyty>();
        }
    
        public int IdPracownika { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public Nullable<int> IdStanowiska { get; set; }
        public System.DateTime DataZatrudnienia { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GrafikPracownikow> GrafikPracownikow { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriaLeczenia> HistoriaLeczenia { get; set; }
        public virtual Stanowiska Stanowiska { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UzytkownicySystemu> UzytkownicySystemu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UzytkownicySystemu> UzytkownicySystemu1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wizyty> Wizyty { get; set; }
    }
}
