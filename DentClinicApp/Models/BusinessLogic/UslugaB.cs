using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.BusinessLogic
{
    public class UslugaB : DatabaseClass
    {
        #region Constructor 

        public UslugaB(DentCareEntities db)
            : base(db){ }

        #endregion

        #region Business Function
        // dodamy funkcje, która będzie zwracała id usług oraz ich nazwy i kod w KeyAndValue

        public IQueryable<KeyAndValue>GetUslugiKeyAndValueItems()
        {
            // powyższa funkcja ma zwrócić z bazy danych 
            return
                (
                    from usluga in db.Uslugi // dla każdej usługi z BD tworzymy key and value, gdzie Key= i Value= 
                    select new KeyAndValue
                    {
                        Key=usluga.IdUslugi,
                        Value=usluga.Nazwa+" "+usluga.Cena
                    }
                ).ToList().AsQueryable();
        }

        #endregion

    }
}
