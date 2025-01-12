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
            : base("Historie leczenia")
        {
        }

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "PESEL", "nazwisko", "data", "lekarz prowadzący" };

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "PESEL")
                List = new ObservableCollection<HistoriaLeczeniaForAllView>(List.OrderBy(item => item.PESEL));
            if (SortField == "nazwisko")
                List = new ObservableCollection<HistoriaLeczeniaForAllView>(List.OrderBy(item => item.Nazwisko));

            if (SortField == "data")
            {
                List = new ObservableCollection<HistoriaLeczeniaForAllView>(
                    List.Where(item => item.Data != null) // Sprawdzenie, czy Data nie jest null
                        .OrderBy(item => item.Data)
                );
            }


            if (SortField == "lekarz prowadzący")
            {
                List = new ObservableCollection<HistoriaLeczeniaForAllView>(
                    List.OrderBy(item => item.LekarzImieNazwisko)
                );
            }


        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "PESEL", "nazwisko", "imię", "data", "lekarz prowadzący"};

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "PESEL")
                List = new ObservableCollection<HistoriaLeczeniaForAllView>(List.Where(item => item.PESEL != null && item.PESEL.StartsWith(FindTextBox)));
            if (FindField == "nazwisko")
                List = new ObservableCollection<HistoriaLeczeniaForAllView>(List.Where(item => item.Nazwisko != null && item.Nazwisko.StartsWith(FindTextBox)));
            if (FindField == "imię")
                List = new ObservableCollection<HistoriaLeczeniaForAllView>(List.Where(item => item.Imie != null && item.Imie.StartsWith(FindTextBox)));
            if (FindField == "data")
            {
                List = new ObservableCollection<HistoriaLeczeniaForAllView>(
                    List.Where(item => item.Data.ToString("dd-MM-yyyy").StartsWith(FindTextBox))
                );
            }

            if (FindField == "lekarz prowadzący")
            {
                List = new ObservableCollection<HistoriaLeczeniaForAllView>(
                    List.Where(item => item.LekarzImieNazwisko != null && item.LekarzImieNazwisko.StartsWith(FindTextBox))
                );
            }

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
                        PESEL = historiaLeczenia.Pacjenci.PESEL,
                        Imie = historiaLeczenia.Pacjenci.Imie,
                        Nazwisko = historiaLeczenia.Pacjenci.Nazwisko,
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
