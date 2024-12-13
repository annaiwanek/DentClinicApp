using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.BusinessLogic
{
    public class PrzychodB : DatabaseClass
    {
        #region Constructor

        public PrzychodB(DentCareEntities db) // konstruktor "wypełnia" dostęp do bazy danych
        :base(db) { }

        #endregion

        #region Business function
        // Ta funkcja oblicza przychód danej usługi w danym okresie od do 

        public decimal PrzychodOkresUsluga(int IdUslugi, DateTime dataOd, DateTime dataDo)
        {
            return
                (
                    from wizyta in db.Wizyty
                    join usluga in db.Uslugi on wizyta.IdUslugi equals usluga.IdUslugi
                    where wizyta.IdUslugi == IdUslugi &&
                      wizyta.Data >= dataOd &&
                      wizyta.Data <= dataDo
                       && wizyta.Status == "Zakończona"
                    select (decimal?)usluga.Cena  // Obsługuje wartości null

                    //select usluga.Cena  // Cena usługi

                ).Sum() ?? 0;
        }
        #endregion
    }
}
