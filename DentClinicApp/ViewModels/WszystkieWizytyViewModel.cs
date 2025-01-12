using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class WszystkieWizytyViewModel : WszystkieViewModel<WizytaForAllView>
    {
        #region Constructor

        public WszystkieWizytyViewModel()
            : base("Wizyty")
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
            List = new ObservableCollection<WizytaForAllView>
                (
                    //zapytanie Linq
                    from wizyty in dentCareEntities.Wizyty
                    select new WizytaForAllView
                    {
                        Wizyta = wizyty,
                        Pacjent = wizyty.Pacjenci,
                        Pracownik = wizyty.Pracownicy,
                        Usluga = wizyty.Uslugi
                    }
                );
        }
        #endregion
    }
}
