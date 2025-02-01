using DentClinicApp.Models.Entities;
using DentClinicApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DentClinicApp.Models.BusinessLogic
{
    public class PrzychodB : DatabaseClass
    {
        public PrzychodB(DentCareEntities db) : base(db) { }

        // Stara metoda - do pojedynczej usługi
        public decimal PrzychodOkresUsluga(int IdUslugi, DateTime dataOd, DateTime dataDo)
        {
            return (
                from wizyta in db.Wizyty
                join usluga in db.Uslugi on wizyta.IdUslugi equals usluga.IdUslugi
                where wizyta.IdUslugi == IdUslugi
                      && wizyta.Data >= dataOd
                      && wizyta.Data <= dataDo
                      && wizyta.Status == "Zakończona"
                select (decimal?)usluga.Cena
            ).Sum() ?? 0;
        }

        // NOWA metoda: zwraca listę [usługa, kwota]
        public List<PrzychodDto> GetPrzychodyOkresUslug(int idUslugi, DateTime dataOd, DateTime dataDo)
        {
            // wizyta z uslugą, zakończona, w okresie
            var query = db.Wizyty
                .Include(w => w.Uslugi)
                .Where(w => w.Data >= dataOd
                            && w.Data <= dataDo
                            && w.Status == "Zakończona");

            if (idUslugi != 0)
            {
                query = query.Where(w => w.IdUslugi == idUslugi);
            }

            // grupujemy, bo może być wiele wizyt jednej usługi
            var list = query
                .GroupBy(w => w.Uslugi.Nazwa)
                .Select(g => new PrzychodDto
                {
                    UslugaNazwa = g.Key,
                    Kwota = g.Sum(x => x.Uslugi.Cena)
                })
                .ToList();

            return list;
        }
    }
}
