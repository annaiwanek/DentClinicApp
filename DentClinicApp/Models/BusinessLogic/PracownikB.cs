using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.BusinessLogic
{
    public class PracownikB : DatabaseClass
    {
        public PracownikB(DentCareEntities db) : base(db) { }

        // Funkcja do dostarczania danych dla ComboBox
        public IQueryable<KeyAndValue> GetPracownikKeyAndValueItems()
        {
            return
             (
                from pracownik in db.Pracownicy
                select new KeyAndValue
                {
                    Key = pracownik.IdPracownika,
                    Value = pracownik.Imie +" "+pracownik.Nazwisko    
                }

             ).ToList().AsQueryable();
        }
    }
}
