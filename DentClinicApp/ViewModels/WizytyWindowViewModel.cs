using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class WizytyWindowViewModel : WszystkieViewModel<WizytaForAllView>
    {
        #region Constructor
        public WizytyWindowViewModel()
            : base("Wizyty")
        {
        }
        #endregion
        private WizytaForAllView _WybranaWizyta;
        public WizytaForAllView WybranaWizyta
        {
            get => _WybranaWizyta;
            set
            {
                if (_WybranaWizyta != value)
                {
                    _WybranaWizyta = value;

                    if (_WybranaWizyta != null)
                    {
                        Console.WriteLine($"Wysłano wizytę: IdWizyty = {_WybranaWizyta.IdWizyty}");
                        Messenger.Default.Send(_WybranaWizyta);
                        OnRequestClose();
                    }

                    OnPropertyChanged(() => WybranaWizyta);
                }
            }
        }



        public ICommand WybierzWizyteCommand => new BaseCommand(() =>
        {
            if (WybranaWizyta != null)
            {
                Messenger.Default.Send(WybranaWizyta); // Wysyłamy wybraną wizytę
                OnRequestClose(); // Zamykamy okno modalne
            }
        });


        #region Sort and Find
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "data", "pacjent", "pracownik" };
        }

        public override void Sort()
        {
            if (SortField == "data")
                List = new ObservableCollection<WizytaForAllView>(List.OrderBy(item => item.Data));
            if (SortField == "pacjent")
                List = new ObservableCollection<WizytaForAllView>(List.OrderBy(item => item.NazwiskoPacjenta));
            if (SortField == "pracownik")
                List = new ObservableCollection<WizytaForAllView>(List.OrderBy(item => item.Pracownik.Nazwisko));
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "pacjent", "pracownik", "data" };
        }

        public override void Find()
        {
            if (FindField == "pacjent")
                List = new ObservableCollection<WizytaForAllView>(List.Where(item => item.NazwiskoPacjenta.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase)));
            if (FindField == "pracownik")
                List = new ObservableCollection<WizytaForAllView>(List.Where(item => item.Pracownik.Nazwisko.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase)));
            if (FindField == "data")
                List = new ObservableCollection<WizytaForAllView>(List.Where(item => item.Data.ToString("yyyy-MM-dd").Contains(FindTextBox)));
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<WizytaForAllView>
            (
                from wizyty in dentCareEntities.Wizyty
                where wizyty.Status == "Zakończona" // Pobieramy tylko zakończone wizyty
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
