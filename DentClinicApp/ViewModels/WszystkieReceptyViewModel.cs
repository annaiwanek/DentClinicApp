using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{ // Klasa, która dostarcza danych do widoku wyświetlającego wszystkie recepty
    public class WszystkieReceptyViewModel : WszystkieViewModel<ReceptaForAllView>
    {
        #region Constructor

        public WszystkieReceptyViewModel()
            : base("Recepty")
        {
        }

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "data wystawienia", "nazwisko"};

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "data wystawienia")
            {
                List = new ObservableCollection<ReceptaForAllView>(
                    List.Where(item => item.DataWystawienia != null) // Sprawdzenie, czy Data nie jest null
                        .OrderBy(item => item.DataWystawienia)
                );
            }

            if (SortField == "nazwisko")
            {
                List = new ObservableCollection<ReceptaForAllView>(
                    List.OrderBy(item => item.Nazwisko ?? string.Empty) // Zastąp null pustym stringiem
                );
            }


        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "kod recepty", "PESEL", "nazwisko", "data wystawienia", "wystawił" };

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "kod recepty")
            {
                List = new ObservableCollection<ReceptaForAllView>(
                    List.Where(item => item.IdRecepty.ToString().Contains(FindTextBox))
                );
            }

            if (FindField == "PESEL")
            {
                List = new ObservableCollection<ReceptaForAllView>(
                    List.Where(item => !string.IsNullOrEmpty(item.PESEL) && item.PESEL.Contains(FindTextBox))
                );
            }

            if (FindField == "nazwisko")
            {
                List = new ObservableCollection<ReceptaForAllView>(
                    List.Where(item => !string.IsNullOrEmpty(item.Nazwisko) && item.Nazwisko.Contains(FindTextBox))
                );
            }


            if (FindField == "data wystawienia")
            {
                List = new ObservableCollection<ReceptaForAllView>(
                    List.Where(item => item.DataWystawienia.ToString("dd-MM-yyyy").Contains(FindTextBox))
                );
            }

            if (FindField == "wystawił")
            {
                List = new ObservableCollection<ReceptaForAllView>(
                    List.Where(item => !string.IsNullOrEmpty(item.LekarzImieNazwisko) && item.LekarzImieNazwisko.Contains(FindTextBox))
                );
            }

        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ReceptaForAllView>
                (
                    //zapytanie Linq
                    from recepty in dentCareEntities.Recepty
                    select new ReceptaForAllView
                    {
                        Recepta = recepty,
                        Pacjent = recepty.Pacjenci,
                        Pracownik = recepty.Pracownicy,
                        IdRecepty = recepty.IdRecepty,
                        DataWystawienia = recepty.DataWystawienia,
                        Nazwisko = recepty.Pacjenci.Nazwisko,
                        Imie = recepty.Pacjenci.Imie,
                        PESEL = recepty.Pacjenci.PESEL

                    }
                );
        }
        #endregion
    }
}
