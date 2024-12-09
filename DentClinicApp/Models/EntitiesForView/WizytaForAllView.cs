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
    }
}
