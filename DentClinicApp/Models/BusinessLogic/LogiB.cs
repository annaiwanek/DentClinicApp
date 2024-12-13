using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.BusinessLogic
{
    public class LogiB : DatabaseClass
    {
        #region Constructor

        public LogiB(DentCareEntities db)
            : base(db) { }

        #endregion

        #region Business Functions

        // Funkcja zwraca logi dla danego użytkownika w określonym okresie
        public IEnumerable<LogForAllView> GetLogiForUserInPeriod(int idUzytkownika, DateTime dataOd, DateTime dataDo)
        {
            return
                from log in db.LogiAktywnosci
                where log.IdUzytkownika == idUzytkownika &&
                      log.Data >= dataOd &&
                      log.Data <= dataDo
                select new LogForAllView
                {
                    IdLog = log.LogID,
                    LoginUzytkownika = log.UzytkownicySystemu.Login,
                    Akcja = log.Akcja,
                    Data = log.Data,
                    Opis = log.Opis
                };
        }


        #endregion
    }
}

