using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{
    public class HistoriaLeczeniaForAllView
    {
        public int IdHistorii { get; set; }

        public Pacjenci Pacjent { get; set; }
        public string PESEL { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime Data { get; set; }
        public string OpisLeczenia { get; set; }
        public string Zalecenia { get; set; }

        public Pracownicy Pracownik { get; set; } // Lekarz związany z pracownikiem 
        public string LekarzImieNazwisko //potrzebne do sortowania filtrowania
        {
            get => $"{Pracownik?.Imie} {Pracownik?.Nazwisko}";
        }


    }
}
