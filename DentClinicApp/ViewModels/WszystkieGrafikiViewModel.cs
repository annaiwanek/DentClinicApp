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

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "nazwisko", "imię", "data", "godzina rozpoczęcia", "godzina zakończenia" };

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "nazwisko")
                List = new ObservableCollection<GrafikForAllView>(List.OrderBy(item => item.Nazwisko));
            if (SortField == "imię")
                List = new ObservableCollection<GrafikForAllView>(List.OrderBy(item => item.Imie));
            if (SortField == "data")
                List = new ObservableCollection<GrafikForAllView>(List.OrderBy(item => item.Data));
            if (SortField == "godzina rozpoczęcia")
                List = new ObservableCollection<GrafikForAllView>(List.OrderBy(item => item.GodzinaRozpoczecia));
            if (SortField == "godzina zakończenia")
                List = new ObservableCollection<GrafikForAllView>(List.OrderBy(item => item.GodzinaZakonczenia));
        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "nazwisko", "imię", "data", "godzina rozpoczęcia", "godzina zakończenia"};

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "nazwisko")
                List = new ObservableCollection<GrafikForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
            if (FindField == "imię")
                List = new ObservableCollection<GrafikForAllView>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox)));
            if (FindField == "data")
            {
                List = new ObservableCollection<GrafikForAllView>(
                    List.Where(item => item.Data.ToString("dd-MM-yyyy").StartsWith(FindTextBox))
                );
            }
            if (FindField == "godzina rozpoczęcia")
            {
                List = new ObservableCollection<GrafikForAllView>(
                    List.Where(item => item.GodzinaRozpoczecia.ToString(@"hh\:mm\:ss").Contains(FindTextBox))
                );
            }

            if (FindField == "godzina zakończenia")
            {
                List = new ObservableCollection<GrafikForAllView>(
                    List.Where(item => item.GodzinaZakonczenia.ToString(@"hh\:mm\:ss").StartsWith(FindTextBox))
                );
            }

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

