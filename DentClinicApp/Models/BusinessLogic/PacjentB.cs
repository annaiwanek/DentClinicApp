using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.BusinessLogic
{
    public class PacjentB : DatabaseClass
    {
        public PacjentB(DentCareEntities db) : base(db) { }

        // Funkcja do dostarczania danych dla ComboBox
        public IQueryable<KeyAndValue> GetPacjentKeyAndValueItems()
        {
            return
             (from pacjent in db.Pacjenci
              select new KeyAndValue
              {
                  Key = pacjent.IdPacjenta, // Identyfikator pacjenta
                  Value = pacjent.PESEL    // PESEL jako wartość do wyświetlenia
              }).ToList().AsQueryable();
        }
    }
}

