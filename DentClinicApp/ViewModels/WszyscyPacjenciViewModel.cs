using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{// To jest klasa, która dostarcza danych do widoku wyświetlającego wszystkich pacjentów
    public class WszyscyPacjenciViewModel : WszystkieViewModel<Pacjenci>
    {
        #region Constructor

        public WszyscyPacjenciViewModel() 
            :base("Pacjenci")
        {
         
        }

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "nazwisko", "imię" };

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "nazwisko")
                List = new ObservableCollection<Pacjenci>(List.OrderBy(item => item.Nazwisko));
            if (SortField == "imię")
                List = new ObservableCollection<Pacjenci>(List.OrderBy(item => item.Imie));

        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "PESEL", "nazwisko" };

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            Console.WriteLine("FindField: " + FindField);
            if (FindField == "nazwisko")
            {
                Console.WriteLine("Find by Subname: " + FindTextBox);
                List = new ObservableCollection<Pacjenci>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
            }

            if (FindField == "PESEL")
                List = new ObservableCollection<Pacjenci>(List.Where(item => item.PESEL != null && item.PESEL.StartsWith(FindTextBox)));

        }

        #endregion

        #region Helpers

        // metoda load pobiera wszystkich pacjentów z bazy danych 

        public override void Load()
        {
            List = new ObservableCollection<Pacjenci>
                (
                dentCareEntities.Pacjenci.ToList() // z bazy danych pobieram tabelę Pacjenci i wszystkie rekordy zamieniam na listę, która jest ObservableCollection
                );
        }
        #endregion
    }
}
