using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{ // Klasa, która dostarcza danych do widoku wyświetlającego wszystkie grafiki
    public class WszystkieGrafikiViewModel : WszystkieViewModel<GrafikForAllView>
    {
        #region Constructor

        public WszystkieGrafikiViewModel()
            : base("Grafiki")
        {
        }

        #endregion

        #region Helpers

        // metoda load pobiera wszystkich pacjentów z bazy danych 

        public override void Load()
        {
            List = new ObservableCollection<GrafikForAllView>
                (
                    //zapytanie Linq
                    from grafikPracownikow in dentCareEntities.GrafikPracownikow // dla każdego grafiku z bazy danych 
                    select new GrafikForAllView // tworzymy nowy GrafikForAllView i uzupełniamy jego dane
                    {
                        IdGrafiku = grafikPracownikow.IdGrafiku,
                        Imie = grafikPracownikow.Pracownicy.Imie,
                        Nazwisko = grafikPracownikow.Pracownicy.Nazwisko,
                        Data = grafikPracownikow.Data,
                        GodzinaRozpoczecia = grafikPracownikow.GodzinaRozpoczęcia,
                        GodzinaZakonczenia = grafikPracownikow.GodzinaZakończenia
                        

                    }
                );
        }
        #endregion
    }
}

