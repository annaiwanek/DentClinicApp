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
            : base("Uslugi")
        {

        }

        #endregion

        #region Properties

        public ObservableCollection<int> DostepneCzasy { get; } = new ObservableCollection<int> { 15, 30, 45, 60 };   // Dostępne czasy trwania usługi w minutach

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
