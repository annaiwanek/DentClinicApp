using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{
    public class ReceptaForAllView
    {
        public Pacjenci Pacjent { get; set; }
        public Recepty Recepta { get; set; }
        public int IdRecepty { get; set; }

        public string PESEL { get; set; }
        public string Nazwisko { get; set; }
        public string Imie { get; set; }

        public DateTime DataWystawienia { get; set; }
        public string Uwagi { get; set; }
        public Pracownicy Pracownik { get; set; } // Lekarz związany z pracownikiem 
        public string LekarzImieNazwisko //potrzebne do sortowania filtrowania
        {
            get => $"{Pracownik?.Imie} {Pracownik?.Nazwisko}";
        }

    }
}

