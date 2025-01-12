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
            : base("Recepty")
        {
        }

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return null;

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {

        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return null;

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
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
