using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{// To jest klasa, która dostarcza danych do widoku wyświetlającego wszystkie stanowiska
    public class WszystkieStanowiskaViewModel : WszystkieViewModel<Stanowiska>
    {
        #region Constructor

        public WszystkieStanowiskaViewModel()
            : base("Stanowiska")
        {

        }

        #endregion
        public string Nazwa { get; set; }
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

        // metoda load pobiera wszystkie leki z bazy danych 

        public override void Load()
        {
            List = new ObservableCollection<Stanowiska>
                (
                dentCareEntities.Stanowiska.ToList() // z bazy danych pobieram tabelę Leki i wszystkie rekordy zamieniam na listę, która jest ObservableCollection
                );
        }
        #endregion
    }
}
