using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.BusinessLogic
{
    public class UzytkownikB : DatabaseClass
    {
        #region Constructor

        public UzytkownikB(DentCareEntities db)
            : base(db) { }

        #endregion

        #region Business Functions

        // Funkcja do dostarczania danych dla ComboBox
        public IQueryable<KeyAndValue> GetUzytkownicyKeyAndValueItems()
        {
            return
                (
                    from uzytkownik in db.UzytkownicySystemu
                    select new KeyAndValue
                    {
                        Key = uzytkownik.IdUzytkownika, // Identyfikator użytkownika
                        Value = uzytkownik.Login       // Login jako wartość do wyświetlenia
                    }
                ).ToList().AsQueryable();
        }

        #endregion
    }
}
