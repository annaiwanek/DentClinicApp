using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{ // Klasa, która dostarcza danych do widoku wyświetlającego wszystkie HistorieLeczenia
    public class WszystkieHistorieLeczeniaViewModel : WszystkieViewModel<HistoriaLeczeniaForAllView>
    {
        #region Constructor

        public WszystkieHistorieLeczeniaViewModel()
            : base("Historia Leczenia")
        {
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<HistoriaLeczeniaForAllView>
                (
                    //zapytanie Linq
                    from historiaLeczenia in dentCareEntities.HistoriaLeczenia
                    select new HistoriaLeczeniaForAllView
                    {
                        IdHistorii = historiaLeczenia.IdHistorii,
                        Pacjent = historiaLeczenia.Pacjenci,
                        Pracownik = historiaLeczenia.Pracownicy,
                        Data = historiaLeczenia.Data,
                        OpisLeczenia = historiaLeczenia.OpisLeczenia,
                        Zalecenia = historiaLeczenia.Zalecenia



                    }
                );
        }
        #endregion

        // metoda load pobiera wszystkie historie leczenia z bazy danych 


    }
}
