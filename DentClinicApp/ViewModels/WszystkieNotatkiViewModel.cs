using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class WszystkieNotatkiViewModel : WszystkieViewModel<NotatkaForAllView>
    {
        #region Constructor

        public WszystkieNotatkiViewModel()
            : base("Notatki")
        {
        }

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "PESEL", "nazwisko", "data"};

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "PESEL")
                List = new ObservableCollection<NotatkaForAllView>(List.OrderBy(item => item.PESEL));
            if (SortField == "nazwisko")
                List = new ObservableCollection<NotatkaForAllView>(List.OrderBy(item => item.Nazwisko));

            if (SortField == "data")
            {
                List = new ObservableCollection<NotatkaForAllView>(
                    List.Where(item => item.Data != null) // Sprawdzenie, czy Data nie jest null
                        .OrderBy(item => item.Data)
                );
            }
        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "PESEL", "nazwisko", "imię", "data"};

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "PESEL")
                List = new ObservableCollection<NotatkaForAllView>(List.Where(item => item.PESEL != null && item.PESEL.StartsWith(FindTextBox)));
            if (FindField == "nazwisko")
                List = new ObservableCollection<NotatkaForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
            if (FindField == "imię")
                List = new ObservableCollection<NotatkaForAllView>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox)));
            if (FindField == "data")
            {
                List = new ObservableCollection<NotatkaForAllView>(
                    List.Where(item => item.Data.ToString("dd-MM-yyyy").StartsWith(FindTextBox))
                );
            }
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<NotatkaForAllView>
                (
                    //zapytanie Linq
                    from notatki in dentCareEntities.Notatki
                    select new NotatkaForAllView
                    {
                        IdNotatki = notatki.IdNotatki,
                        Pacjent = notatki.Pacjenci, 
                        Data = notatki.Data,
                        Tekst = notatki.Tekst,
                        Pracownik = notatki.Pracownicy,
                        PESEL = notatki.Pacjenci.PESEL,
                        Imie = notatki.Pacjenci.Imie,
                        Nazwisko = notatki.Pacjenci.Nazwisko,
                    }
                );
        }
        #endregion

    }
}
