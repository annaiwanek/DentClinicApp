using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{   
    // To jest klasa, która dostarcza danych do widoku wyświetlającego wszystkie usługi
    public class WszystkieUslugiViewModel : WszystkieViewModel<Uslugi>
    {
        #region Constructor

        public WszystkieUslugiViewModel()
            : base("Usługi")
        {

        }

        #endregion

        #region Properties

        public ObservableCollection<int> DostepneCzasy { get; } = new ObservableCollection<int> { 15, 30, 45, 60 };   // Dostępne czasy trwania usługi w minutach

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> {"cena min", "cena max", "czas trwania min", "czas trwania max"};

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "cena min")
            {
                // Sortowanie według ceny w porządku rosnącym
                List = new ObservableCollection<Uslugi>(
                    List.OrderBy(item => item.Cena)
                );
            }
            else if (SortField == "cena max")
            {
                // Sortowanie według ceny w porządku malejącym
                List = new ObservableCollection<Uslugi>(
                    List.OrderByDescending(item => item.Cena)
                );
            }
            else if (SortField == "czas trwania min")
            {
                // Sortowanie według czasu trwania w porządku rosnącym
                List = new ObservableCollection<Uslugi>(
                    List.OrderBy(item => item.CzasTrwania)
                );
            }
            else if (SortField == "czas trwania max")
            {
                // Sortowanie według czasu trwania w porządku malejącym
                List = new ObservableCollection<Uslugi>(
                    List.OrderByDescending(item => item.CzasTrwania)
                );
            }
        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "nazwa", "cena", "czas trwania" };

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "nazwa")
            {
                List = new ObservableCollection<Uslugi>(
                    List.Where(item => item.Nazwa != null && item.Nazwa.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase))
                );
            }

            if (FindField == "cena")
            {
                if (decimal.TryParse(FindTextBox, out var cena))
                {
                    List = new ObservableCollection<Uslugi>(
                        List.Where(item => item.Cena.ToString().StartsWith(FindTextBox))
                    );
                }
            }

            if (FindField == "czas trwania")
            {
                if (int.TryParse(FindTextBox, out var czas))
                {
                    List = new ObservableCollection<Uslugi>(
                        List.Where(item => item.CzasTrwania.ToString().StartsWith(FindTextBox))
                    );
                }
            }
        }

        #endregion

        #region Helpers

        // metoda load pobiera wszystkich pacjentów z bazy danych 

        public override void Load()
        {
            List = new ObservableCollection<Uslugi>
                (
                dentCareEntities.Uslugi.ToList() // z bazy danych pobieram tabelę Pacjenci i wszystkie rekordy zamieniam na listę, która jest ObservableCollection
                );
        }
        #endregion
    }
}
