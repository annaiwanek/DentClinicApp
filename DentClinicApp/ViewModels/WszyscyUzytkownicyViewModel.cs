using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class WszyscyUzytkownicyViewModel : WszystkieViewModel<UzytkownikForAllView>
    {
        #region Constructor

        public WszyscyUzytkownicyViewModel()
            : base("Uzytkownik")
        {
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<UzytkownikForAllView>
                (
                    //zapytanie Linq
                    from uzytkownicySystemu in dentCareEntities.UzytkownicySystemu
                    select new UzytkownikForAllView
                    {
                        Pracownik = uzytkownicySystemu.Pracownicy,
                        UzytkownikSystemu = uzytkownicySystemu,
                        Stanowisko = uzytkownicySystemu.Pracownicy.Stanowiska
                    }
                );
        }
        #endregion
    }
}
