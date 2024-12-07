using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{ // Klasa, która dostarcza danych do widoku wyświetlającego wszystkie Logi
    public class WszystkieLogiViewModel : WszystkieViewModel<LogForAllView>
    {
        #region Constructor

        public WszystkieLogiViewModel()
            : base("Logi aktywności")
        {
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<LogForAllView>
                (
                    //zapytanie Linq
                    from logiAktywnosci in dentCareEntities.LogiAktywnosci
                    select new LogForAllView
                    {
                        IdLog = logiAktywnosci.LogID,
                        LoginUzytkownika = logiAktywnosci.UzytkownicySystemu.Login,
                        Akcja = logiAktywnosci.Akcja,
                        Data = logiAktywnosci.Data,
                        Opis = logiAktywnosci.Opis
                    }
                );
        }
        #endregion
    }
}
