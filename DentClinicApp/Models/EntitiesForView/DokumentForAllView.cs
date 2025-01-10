using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{   // Ta klasa wzorowana jest na klasie Dokument, tylko zamiast kluczy obcych ma czytelne dla klienta pola (klucze obce będą zastąpione czytelnymi dla zwykłego użytkownika danymi
    public class DokumentForAllView
    {
        // Zamiast IdPacjenta będzie wyświetlany nr PESEL 
        public int IdDokumentu { get; set; }
        public string PESEL { get; set; } // pole zamiast klucza obcego 
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NazwaDokumentu { get; set; }
        public string TypDokumentu { get; set; }
        public string Opis { get; set; }
        public  DateTime DataDodania { get; set; }
        //public string SciezkaDoPliku { get; set; }
    }
}
