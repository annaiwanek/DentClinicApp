using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{ // Klasa, która dostarcza danych do widoku wyświetlającego wszystkie recepty
    public class WszystkieReceptyViewModel : WszystkieViewModel<ReceptaForAllView>
    {
        #region Constructor

        public WszystkieReceptyViewModel()
            : base("Recepta")
        {
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ReceptaForAllView>
                (
                    //zapytanie Linq
                    from recepty in dentCareEntities.Recepty
                    select new ReceptaForAllView
                    {
                        Recepta = recepty,
                        Pacjent = recepty.Pacjenci,
                        Pracownik = recepty.Pracownicy
                      
                    }
                );
        }
        #endregion
    }
}
