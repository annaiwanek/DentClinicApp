using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class PracownicyWindowViewModel : WszystkieViewModel<PracownikForAllView>
    {
        #region Constructor

        public PracownicyWindowViewModel()
            : base("Pracownicy")
        {
        }

        #endregion

        private PracownikForAllView _WybranyPracownik;
        public PracownikForAllView WybranyPracownik
        {
            get => _WybranyPracownik;
            set
            {
                if (_WybranyPracownik != value)
                {
                    _WybranyPracownik = value;

                    // Wyślij wybranego pracownika i zamknij okno
                    if (_WybranyPracownik != null)
                    {
                        Messenger.Default.Send(_WybranyPracownik);
                        OnRequestClose();
                    }

                    OnPropertyChanged(() => WybranyPracownik);
                }
            }
        }


        #region Sort And Find 

        // Lista dla ComboBoxa sortowania
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "nazwisko", "imię"};
        }

        // Implementacja sortowania
        public override void Sort()
        {
            if (SortField == "nazwisko")
                List = new ObservableCollection<PracownikForAllView>(List.OrderBy(item => item.Nazwisko));
            if (SortField == "imię")
                List = new ObservableCollection<PracownikForAllView>(List.OrderBy(item => item.Imie));
           
         
        }

        // Lista dla ComboBoxa wyszukiwania
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "nazwisko", "imię", "stanowisko" };
        }

        // Implementacja wyszukiwania
        public override void Find()
        {
            if (FindField == "nazwisko")
                List = new ObservableCollection<PracownikForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase)));
            if (FindField == "imię")
                List = new ObservableCollection<PracownikForAllView>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase)));
            if (FindField == "stanowisko")
                List = new ObservableCollection<PracownikForAllView>(List.Where(item => item.Stanowiska != null && item.Stanowiska.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase)));
        }

        #endregion


        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PracownikForAllView>
                (
                    from pracownicy in dentCareEntities.Pracownicy
                    where pracownicy.Status == "Aktywny" // Filtrowanie tylko aktywnych pracowników
                    select new PracownikForAllView
                    {

                        //Nazwisko = pracownicy.Nazwisko,
                        //Imie = pracownicy.Imie,
                        IdPracownika = pracownicy.IdPracownika,
                        Pracownik = pracownicy,
                        Stanowisko = pracownicy.Stanowiska,
                        IsPracownikActive = pracownicy.Status == "Aktywny" // Przekazywanie logiki aktywności
                    }
                );
        }

        #endregion


    }
}

