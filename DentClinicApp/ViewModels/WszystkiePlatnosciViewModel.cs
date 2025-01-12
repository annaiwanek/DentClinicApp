using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{ // Klasa, która dostarcza danych do widoku wyświetlającego wszystkie Platności
    public class WszystkiePlatnosciViewModel : WszystkieViewModel<PlatnoscForAllView>
    {
        #region Constructor

        public WszystkiePlatnosciViewModel()
            : base("Płatności")
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
            List = new ObservableCollection<PlatnoscForAllView>
                (
                    //zapytanie Linq
                    from platnosci in dentCareEntities.Platnosci
                    select new PlatnoscForAllView
                    {
                        IdPlatnosci = platnosci.IdPlatnosci,
                        NumerWizyty = platnosci.IdWizyty,
                        DataWizyty = platnosci.Wizyty.Data,
                        Pacjent = platnosci.Pacjenci,
                        DataPlatnosci = platnosci.DataPlatnosci,
                        Kwota = platnosci.Kwota,
                        MetodaPlatnosci = platnosci.MetodaPlatnosci,
                        Status = platnosci.Status,
                        Pracownik = platnosci.Pracownicy,
                        UzytkownikSystemu = platnosci.UzytkownicySystemu


                    }
                );
        }
        #endregion
    }
}
