using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class WszyscyPracownicyViewModel : WszystkieViewModel<PracownikForAllView>
    {
        #region Constructor

        public WszyscyPracownicyViewModel()
            : base("Pracownicy")
        {
        }

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "nazwisko", "imię", "status" };

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "nazwisko")
                List = new ObservableCollection<PracownikForAllView>(List.OrderBy(item => item.Nazwisko));
            if (SortField == "imię")
                List = new ObservableCollection<PracownikForAllView>(List.OrderBy(item => item.Imie));
            if (SortField == "status")
                List = new ObservableCollection<PracownikForAllView>(List.OrderBy(item => item.IsPracownikActive));
        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> {"nazwisko", "imię", "stanowisko"};

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "nazwisko")
                List = new ObservableCollection<PracownikForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
            if (FindField == "imię")
                List = new ObservableCollection<PracownikForAllView>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox)));
            if (FindField == "stanowisko")
                List = new ObservableCollection<PracownikForAllView>(List.Where(item => item.Stanowiska != null && item.Stanowiska.StartsWith(FindTextBox)));
        
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PracownikForAllView>
                (
                    //zapytanie Linq
                    from pracownicy in dentCareEntities.Pracownicy
                    select new PracownikForAllView
                    {
                       Pracownik = pracownicy,
                       Stanowisko = pracownicy.Stanowiska
                    }
                );
        }
        #endregion
    }
}
