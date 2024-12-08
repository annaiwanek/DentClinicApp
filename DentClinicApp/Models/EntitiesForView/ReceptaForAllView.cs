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
        public Pracownicy Pracownik { get; set; } // Lekarz związany z pracownikiem 
      
    }
}
