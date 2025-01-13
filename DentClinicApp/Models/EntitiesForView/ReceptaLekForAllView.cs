using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{
    public class ReceptaLekForAllView
    {
        public Pacjenci Pacjent { get; set; }
        public string PESEL { get; set; }
        public Recepty Recepta { get; set; }
        public ReceptyLeki ReceptaLek { get; set; }
        public Leki Lek { get; set; }
        public string Nazwa { get; set; }
        public DateTime DataWystawienia { get; set; }
        public int Ilosc { get; set; }

    }
}
