using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{
    public class NotatkaForAllView
    {
        public int IdNotatki { get; set; }
        public Pacjenci Pacjent { get; set; }
        public Pracownicy Pracownik { get; set; }
        public DateTime Data { get; set; }
        public string Tekst { get; set; }

    }
}
