using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{
    public class WizytaForAllView
    {
        public Wizyty Wizyta { get; set; }
        public Pacjenci Pacjent { get; set; }
        public Pracownicy Pracownik { get; set; }
        public Uslugi Usluga { get; set; }

        // Właściwości wyliczane na podstawie powiązanych danych
        public DateTime Data => Wizyta?.Data ?? DateTime.MinValue; // Data wizyty
        public TimeSpan Godzina => Wizyta?.Godzina ?? TimeSpan.Zero; // Godzina wizyty

        // Zmieniona właściwość: Czas trwania wizyty w minutach
        public int CzasTrwaniaWMinutach => Wizyta?.CzasTrwaniaWMinutach ?? 0;

        // Nazwisko pacjenta
        public string NazwiskoPacjenta => Pacjent?.Nazwisko ?? string.Empty;
    }

}