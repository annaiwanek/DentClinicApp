using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{// Ta klasa wzorowana jest na klasie Grafik, tylko zamiast kluczy obcych ma czytelne dla klienta pola (klucze obce będą zastąpione czytelnymi dla zwykłego użytkownika danymi)

    public class GrafikForAllView
    {
        public int IdGrafiku { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan GodzinaRozpoczecia { get; set; }
        public TimeSpan GodzinaZakonczenia { get; set; }

    }
}
