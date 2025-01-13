using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DentClinicApp.ViewModels
{
    // Klasa ViewModel dostarczająca dane do widoku wyświetlającego wszystkie stanowiska
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

        // Lista kryteriów sortowania do ComboBoxa
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "nazwa stanowiska", "wynagrodzenie minimalne", "wynagrodzenie maksymalne" };
        }

        // Logika sortowania
       
           public override void Sort()
           {

            if (SortField == "nazwa stanowiska")
            {
                // Sortowanie po nazwie stanowiska
                List = new ObservableCollection<Stanowiska>(
                    List.OrderBy(item => item.Nazwa ?? string.Empty)
                );
            }
            else if (SortField == "wynagrodzenie minimalne")
            {
                // Sortowanie po wynagrodzeniu minimalnym, traktując null jako najmniejszą wartość
                List = new ObservableCollection<Stanowiska>(
                    List.OrderBy(item => item.WynagrodzenieMin ?? decimal.MinValue)
                );
            }
            else if (SortField == "wynagrodzenie maksymalne")
            {
                // Sortowanie po wynagrodzeniu maksymalnym, traktując null jako największą wartość
                List = new ObservableCollection<Stanowiska>(
                    List.OrderByDescending(item => item.WynagrodzenieMax ?? decimal.MinValue)
                );
            }
           }

           
        

        // Lista kryteriów wyszukiwania do ComboBoxa
        public override List<string> GetComboboxFindList()
        {
            return new List<string> {"nazwa stanowiska"};
        }

        // Logika wyszukiwania
        public override void Find()
        {
           
            
                if (FindField == "nazwa stanowiska")
                {
                    List = new ObservableCollection<Stanowiska>(
                        List.Where(item => !string.IsNullOrEmpty(item.Nazwa) &&
                                           item.Nazwa.IndexOf(FindTextBox ?? string.Empty, StringComparison.OrdinalIgnoreCase) >= 0)
                    );
                }
            
        }
               
        #endregion

        #region Helpers

        // Metoda wczytująca dane z bazy danych
        public override void Load()
        {
            try
            {
                List = new ObservableCollection<Stanowiska>(
                    dentCareEntities.Stanowiska.ToList() // Pobieranie danych z bazy
                );
            }
            catch (Exception ex)
            {
                // Obsługa błędów podczas ładowania danych
                Console.WriteLine($"Błąd podczas ładowania danych: {ex.Message}");
            }
        }

        #endregion
    }
}
