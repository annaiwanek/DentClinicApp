using DentClinicApp.Models.Entities;
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
