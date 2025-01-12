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

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> {"login", "data"};


        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "login")
                List = new ObservableCollection<LogForAllView>(List.OrderBy(item => item.LoginUzytkownika));

            if (SortField == "data")
            {
                List = new ObservableCollection<LogForAllView>(
                    List.Where(item => item.Data != null) // Sprawdzenie, czy Data nie jest null
                        .OrderBy(item => item.Data)
                );
            }

        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> {"login", "akcja", "data" };

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "login")
                List = new ObservableCollection<LogForAllView>(List.Where(item => item.LoginUzytkownika != null && item.LoginUzytkownika.StartsWith(FindTextBox)));

            if (FindField == "akcja")
                List = new ObservableCollection<LogForAllView>(List.Where(item => item.Akcja!= null && item.Akcja.StartsWith(FindTextBox)));

            if (FindField == "data")
            {
                List = new ObservableCollection<LogForAllView>(
                    List.Where(item => item.Data.ToString("dd-MM-yyyy").StartsWith(FindTextBox))
                );
            }
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
