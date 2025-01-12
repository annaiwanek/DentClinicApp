using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DentClinicApp.Models.EntitiesForView
{
    public class UzytkownikForAllView
    {
        public Pracownicy Pracownik { get; set; }

        public Stanowiska Stanowisko { get; set; }
     
        public UzytkownicySystemu UzytkownikSystemu { get; set; }

        public string Rola
        {
            get => UzytkownikSystemu?.Rola; 
        }


        public string Imie
        {
            get => Pracownik?.Imie;
        }
        public string Nazwisko
        {
            get => Pracownik?.Nazwisko;
        }

        public string Login
        {
            get => UzytkownikSystemu?.Login; 
        }


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
