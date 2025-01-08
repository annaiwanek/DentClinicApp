using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.BusinessLogic
{
    public class StanowiskoB : DatabaseClass
    {
        public StanowiskoB(DentCareEntities db) : base(db) { }

        // Funkcja do dostarczania danych dla ComboBox
        public IQueryable<KeyAndValue> GetStanowiskoKeyAndValueItems()
        {
            return
             (
                from stanowisko in db.Stanowiska
                select new KeyAndValue
                {
                    Key = stanowisko.IdStanowiska,
                    Value = stanowisko.Nazwa
                }

             ).ToList().AsQueryable();
        }
    }
}
