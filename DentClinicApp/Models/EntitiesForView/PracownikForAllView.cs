using DentClinicApp.Models.Entities;
using DentClinicApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{
    public class PracownikForAllView
    {
        public Pracownicy Pracownik { get; set; }
        public Stanowiska Stanowisko { get; set; }

        public string Nazwisko
        {
            get => Pracownik?.Nazwisko;
        }

        public string Imie
        {
            get => Pracownik?.Imie;
        }

        public string Stanowiska
        {
            get => Stanowisko?.Nazwa;
        }


        public bool IsPracownikActive {
            get {
                return Pracownik != null && Pracownik.Status == "Aktywny";
            }
            set {}
                
        }
       
    }
}
