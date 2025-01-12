using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class WszyscyUzytkownicyViewModel : WszystkieViewModel<UzytkownikForAllView>
    {
        #region Constructor

        public WszyscyUzytkownicyViewModel()
            : base("Uzytkownicy")
        {
        }

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "nazwisko", "imię", "rola"};

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "nazwisko")
                List = new ObservableCollection<UzytkownikForAllView>(List.OrderBy(item => item.Nazwisko));
            if (SortField == "imię")
                List = new ObservableCollection<UzytkownikForAllView>(List.OrderBy(item => item.Imie));
            if (SortField == "rola")
                List = new ObservableCollection<UzytkownikForAllView>(List.OrderBy(item => item.Rola));


        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "nazwisko", "imię", "login", "rola"};

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "nazwisko")
                List = new ObservableCollection<UzytkownikForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
            if (FindField == "imie")
                List = new ObservableCollection<UzytkownikForAllView>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox)));
            if (FindField == "login")
                List = new ObservableCollection<UzytkownikForAllView>(List.Where(item => item.Login != null && item.Login.StartsWith(FindTextBox)));
            if (FindField == "rola")
                List = new ObservableCollection<UzytkownikForAllView>(List.Where(item => item.Rola != null && item.Rola.StartsWith(FindTextBox)));
            

        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<UzytkownikForAllView>
                (
                    //zapytanie Linq
                    from uzytkownicySystemu in dentCareEntities.UzytkownicySystemu
                    select new UzytkownikForAllView
                    {
                        Pracownik = uzytkownicySystemu.Pracownicy,
                        UzytkownikSystemu = uzytkownicySystemu,
                        Stanowisko = uzytkownicySystemu.Pracownicy.Stanowiska
                    }
                );
        }
        #endregion
    }
}
