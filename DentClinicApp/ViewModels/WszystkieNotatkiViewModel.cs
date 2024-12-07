using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class WszystkieNotatkiViewModel : WszystkieViewModel<NotatkaForAllView>
    {
        #region Constructor

        public WszystkieNotatkiViewModel()
            : base("Notatki")
        {
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<NotatkaForAllView>
                (
                    //zapytanie Linq
                    from notatki in dentCareEntities.Notatki
                    select new NotatkaForAllView
                    {
                        IdNotatki = notatki.IdNotatki,
                        Pacjent = notatki.Pacjenci, 
                        Data = notatki.Data,
                        TrescNotatki = notatki.Tekst,
                        Pracownik = notatki.Pracownicy



                    }
                );
        }
        #endregion

    }
}
