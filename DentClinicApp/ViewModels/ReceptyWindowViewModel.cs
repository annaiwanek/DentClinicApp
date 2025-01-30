using DentClinicApp.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using DentClinicApp.Models.EntitiesForView;


namespace DentClinicApp.ViewModels
{
    public class ReceptyWindowViewModel : WszystkieViewModel<Recepty>
    {
        #region Constructor
        public ReceptyWindowViewModel()
            : base("Recepty")
        {
        }
        #endregion

        #region Properties
        private Recepty _wybranaRecepta;
        public Recepty WybranaRecepta
        {
            get => _wybranaRecepta;
            set
            {
                if (_wybranaRecepta != value)
                {
                    _wybranaRecepta = value;

                    if (_wybranaRecepta != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Wybrana recepta w ReceptyWindowViewModel: {_wybranaRecepta.IdRecepty}");
                        Messenger.Default.Send(_wybranaRecepta);
                        OnRequestClose();
                    }

                    OnPropertyChanged(() => WybranaRecepta);
                }
            }
        }

        //public Recepty WybranaRecepta
        //{
        //    get => _wybranaRecepta;
        //    set
        //    {
        //        if (_wybranaRecepta != value)
        //        {
        //            _wybranaRecepta = value;

        //            if (_wybranaRecepta != null)
        //            {
        //                Messenger.Default.Send(_wybranaRecepta);
        //                Load(); // Odśwież listę recept
        //                OnRequestClose();
        //            }

        //            OnPropertyChanged(() => WybranaRecepta);
        //        }
        //    }
        //}

        #endregion

        #region Sort And Find
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "id recepty", "data wystawienia", "nazwisko pacjenta" };
        }

        public override void Sort()
        {
            if (SortField == "id recepty")
                List = new ObservableCollection<Recepty>(List.OrderBy(item => item.IdRecepty));
            if (SortField == "data wystawienia")
                List = new ObservableCollection<Recepty>(List.OrderBy(item => item.DataWystawienia));
            if (SortField == "nazwisko pacjenta")
                List = new ObservableCollection<Recepty>(List.OrderBy(item => item.Pacjenci.Nazwisko));
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "id recepty", "PESEL", "nazwisko pacjenta", "data wystawienia" };
        }

        public override void Find()
        {
            if (FindField == "id recepty")
                List = new ObservableCollection<Recepty>(List.Where(item => item.IdRecepty.ToString().Contains(FindTextBox)));

            if (FindField == "PESEL")
                List = new ObservableCollection<Recepty>(List.Where(item => item.Pacjenci?.PESEL != null && item.Pacjenci.PESEL.Contains(FindTextBox)));

            if (FindField == "nazwisko pacjenta")
                List = new ObservableCollection<Recepty>(List.Where(item => item.Pacjenci?.Nazwisko != null && item.Pacjenci.Nazwisko.Contains(FindTextBox)));

            if (FindField == "data wystawienia")
                List = new ObservableCollection<Recepty>(List.Where(item => item.DataWystawienia.ToString("yyyy-MM-dd").Contains(FindTextBox)));
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Recepty>(
                dentCareEntities.Recepty
                .Include(r => r.Pacjenci)  // Załaduj relację z pacjentami
                .Include(r => r.Pracownicy) // Załaduj relację z lekarzami
                .ToList()
            );
        }

        #endregion
    }
}
