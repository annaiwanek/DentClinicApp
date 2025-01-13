using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{ // Klasa, która dostarcza danych do widoku wyświetlającego wszystkie Platności
    public class WszystkiePlatnosciViewModel : WszystkieViewModel<PlatnoscForAllView>
    {
        #region Constructor

        public WszystkiePlatnosciViewModel()
            : base("Płatności")
        {
        }

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> {"nazwisko", "nr wizyty", "data wizyty", "kwota"};

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "nazwisko")
                List = new ObservableCollection<PlatnoscForAllView>(List.OrderBy(item => item.Nazwisko));
            if (SortField == "nr wizyty")
                List = new ObservableCollection<PlatnoscForAllView>(List.OrderBy(item => item.NumerWizyty));

            if (SortField == "data wizyty")
            {
                List = new ObservableCollection<PlatnoscForAllView>(
                    List.Where(item => item.DataWizyty != null) // Sprawdzenie, czy Data nie jest null
                        .OrderBy(item => item.DataWizyty)
                );
            }

            if (SortField == "kwota")
                List = new ObservableCollection<PlatnoscForAllView>(List.OrderBy(item => item.Kwota));

        }


        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "nazwisko", "nr wizyty", "data wizyty", "kwota", "metoda płatności" };

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "nazwisko")
                List = new ObservableCollection<PlatnoscForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));

            if (FindField == "nr wizyty")
            {
                // Sparsuj FindTextBox jako int
                if (int.TryParse(FindTextBox, out int numerWizyty))
                {
                    List = new ObservableCollection<PlatnoscForAllView>(
                        List.Where(item => item.NumerWizyty == numerWizyty)
                    );
                }
                else
                {
                    // Jeśli FindTextBox nie można sparsować do int, wyczyść listę
                    List = new ObservableCollection<PlatnoscForAllView>();
                }
            }

            if (FindField == "data wizyty")
            {
                List = new ObservableCollection<PlatnoscForAllView>(
                    List.Where(item => item.DataWizyty != null &&
                                       item.DataWizyty.ToString("dd-MM-yyyy").StartsWith(FindTextBox))
                );
            }

            if (FindField == "kwota")
            {
                // Użyj Decimal.TryParse, aby sprawdzić poprawność wprowadzonej wartości liczbowej
                if (decimal.TryParse(FindTextBox, out decimal kwota))
                {
                    List = new ObservableCollection<PlatnoscForAllView>(
                        List.Where(item => item.Kwota == kwota)
                    );
                }
                else
                {
                    // Jeśli FindTextBox nie jest liczbą, wyczyść listę
                    List = new ObservableCollection<PlatnoscForAllView>();
                }
            }

            if (FindField == "metoda płatności")
                List = new ObservableCollection<PlatnoscForAllView>(
                       List.Where(item => item.MetodaPlatnosci != null && item.MetodaPlatnosci.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase)));

        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PlatnoscForAllView>
                (
                    //zapytanie Linq
                    from platnosci in dentCareEntities.Platnosci
                    select new PlatnoscForAllView
                    {
                        IdPlatnosci = platnosci.IdPlatnosci,
                        NumerWizyty = platnosci.IdWizyty,
                        DataWizyty = platnosci.Wizyty.Data,
                        Pacjent = platnosci.Pacjenci,
                        DataPlatnosci = platnosci.DataPlatnosci,
                        Kwota = platnosci.Kwota,
                        MetodaPlatnosci = platnosci.MetodaPlatnosci,
                        Status = platnosci.Status,
                        Pracownik = platnosci.Pracownicy,
                        UzytkownikSystemu = platnosci.UzytkownicySystemu,
                        Nazwisko = platnosci.Pacjenci.Nazwisko



                    }
                );
        }
        #endregion
    }
}
