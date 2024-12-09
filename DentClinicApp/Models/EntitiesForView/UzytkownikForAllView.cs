using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{
    public class UzytkownikForAllView
    {
        public Pracownicy Pracownik { get; set; }
        public Stanowiska Stanowisko { get; set; }
        public UzytkownicySystemu UzytkownikSystemu { get; set; }

        public bool IsPracownikActive
        {
            get
            {
                return Pracownik != null && Pracownik.Status == "Aktywny";
            }
            set { }

        }

    }
}
