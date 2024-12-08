using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class WszyscyPracownicyViewModel : WszystkieViewModel<PracownikForAllView>
    {
        #region Constructor

        public WszyscyPracownicyViewModel()
            : base("Pracownik")
        {
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PracownikForAllView>
                (
                    //zapytanie Linq
                    from pracownicy in dentCareEntities.Pracownicy
                    select new PracownikForAllView
                    {
                       Pracownik = pracownicy,
                       Stanowisko = pracownicy.Stanowiska
                    }
                );
        }
        #endregion
    }
}
