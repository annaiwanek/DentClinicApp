using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class WszystkieWizytyViewModel : WszystkieViewModel<WizytaForAllView>
    {
        #region Constructor

        public WszystkieWizytyViewModel()
            : base("Wizyty")
        {
        }

        #endregion

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "data", "godzina", "czas trwania min", "czas trwania max", "nazwisko pacjenta" };

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "data")
            {
                List = new ObservableCollection<WizytaForAllView>(
                    List.OrderBy(item => item.Wizyta.Data)
                );
            }

            if (SortField == "godzina")
            {
                List = new ObservableCollection<WizytaForAllView>(
                    List.OrderBy(item => item.Wizyta.Godzina)
                );
            }

            if (SortField == "czas trwania min")
            {
                List = new ObservableCollection<WizytaForAllView>(
                    List.OrderBy(item => item.Wizyta.CzasTrwaniaWMinutach)
                );
            }

            if (SortField == "czas trwania max")
            {
                List = new ObservableCollection<WizytaForAllView>(
                    List.OrderByDescending(item => item.Wizyta.CzasTrwaniaWMinutach)
                );
            }

            if (SortField == "nazwisko pacjenta")
            {
                List = new ObservableCollection<WizytaForAllView>(
                    List.OrderBy(item => item.Pacjent.Nazwisko)
                );
            }
        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "id", "pesel", "nazwisko", "usługa", "lekarz", "data", "godzina", "czas trwania", "status"};

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (string.IsNullOrWhiteSpace(FindTextBox))
            {
                // Jeśli pole wyszukiwania jest puste, pokazujemy pełną listę
                Load();
                return;
            }

            if (FindField == "id")
            {
                if (int.TryParse(FindTextBox, out var id))
                {
                    List = new ObservableCollection<WizytaForAllView>(
                        List.Where(item => item.Wizyta.IdWizyty.ToString().StartsWith(FindTextBox))
                         );
                }
            }

            if (FindField == "pesel")
            {
                List = new ObservableCollection<WizytaForAllView>(
                    List.Where(item => item.Pacjent.PESEL != null && item.Pacjent.PESEL.StartsWith(FindTextBox))
                );
            }

            if (FindField == "nazwisko")
            {
                List = new ObservableCollection<WizytaForAllView>(
                    List.Where(item => item.Pacjent.Nazwisko != null && item.Pacjent.Nazwisko.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase))
                );
            }

            if (FindField == "usługa")
            {
                List = new ObservableCollection<WizytaForAllView>(
                    List.Where(item => item.Usluga.Nazwa != null && item.Usluga.Nazwa.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase))
                );
            }

            if (FindField == "lekarz")
            {
                List = new ObservableCollection<WizytaForAllView>(
                    List.Where(item => item.Pracownik.Nazwisko != null && item.Pracownik.Nazwisko.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase))
                );
            }

            if (FindField == "data")
            {
                if (int.TryParse(FindTextBox, out var day))
                {
                    List = new ObservableCollection<WizytaForAllView>(
                        List.Where(item => item.Data.Day == day)
                    );
                }
                else if (DateTime.TryParse(FindTextBox, out var fullDate))
                {
                    List = new ObservableCollection<WizytaForAllView>(
                        List.Where(item => item.Data.Date == fullDate.Date)
                    );
                }
            }

            if (FindField == "godzina")
            {
               
                List = new ObservableCollection<WizytaForAllView>(
                    List.Where(item => item.Godzina.ToString(@"hh\:mm").StartsWith(FindTextBox))
                );
            }

            if (FindField == "czas trwania")
            {
               
                List = new ObservableCollection<WizytaForAllView>(
                    List.Where(item => item.CzasTrwaniaWMinutach.ToString().StartsWith(FindTextBox))
                );
            }

            if (FindField == "status")
            {
                List = new ObservableCollection<WizytaForAllView>(
                    List.Where(item => item.Wizyta.Status != null && item.Wizyta.Status.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase))
                );
            }
        }
        

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<WizytaForAllView>
                (
                    //zapytanie Linq
                    from wizyty in dentCareEntities.Wizyty
                    select new WizytaForAllView
                    {
                        Wizyta = wizyty,
                        Pacjent = wizyty.Pacjenci,
                        Pracownik = wizyty.Pracownicy,
                        Usluga = wizyty.Uslugi
                    }
                );
        }
        #endregion
    }
}
