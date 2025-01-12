using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{// To jest klasa, która dostarcza danych do widoku wyświetlającego wszystkie leki 
    public class WszystkieLekiViewModel : WszystkieViewModel<Leki>
    {
        #region Constructor

        public WszystkieLekiViewModel()
            : base("Leki")
        {

        }

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> {"nazwa", "substancja czynna", "postać"};

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "nazwa")
                List = new ObservableCollection<Leki>(List.OrderBy(item => item.Nazwa));

            if (SortField == "substancja czynna")
                List = new ObservableCollection<Leki>(List.OrderBy(item => item.SubstancjaCzynna));


            if (SortField == "postać")
                List = new ObservableCollection<Leki>(List.OrderBy(item => item.Postac));
        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> {"nazwa", "substancja czynna", "postać", "dawka" };


        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "nazwa")
                List = new ObservableCollection<Leki>(List.Where(item => item.Nazwa != null && item.Nazwa.StartsWith(FindTextBox)));

            if (FindField == "substancja czynna")
                List = new ObservableCollection<Leki>(List.Where(item => item.SubstancjaCzynna != null && item.SubstancjaCzynna.StartsWith(FindTextBox)));

            if (FindField == "postać")
                List = new ObservableCollection<Leki>(List.Where(item => item.Postac != null && item.Postac.StartsWith(FindTextBox)));

            if (FindField == "dawka")
                List = new ObservableCollection<Leki>(List.Where(item => item.Dawka != null && item.Dawka.StartsWith(FindTextBox)));

        }

        #endregion

        #region Helpers

        // metoda load pobiera wszystkie leki z bazy danych 

        public override void Load()
        {
            List = new ObservableCollection<Leki>
                (
                dentCareEntities.Leki.ToList() // z bazy danych pobieram tabelę Leki i wszystkie rekordy zamieniam na listę, która jest ObservableCollection
                );
        }
        #endregion
    }
}
