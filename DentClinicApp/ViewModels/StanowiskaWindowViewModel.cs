using DentClinicApp.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DentClinicApp.ViewModels
{
    public class StanowiskaWindowViewModel : WszystkieViewModel<Stanowiska>
    {
        #region Constructor
        public StanowiskaWindowViewModel()
            : base("Stanowiska")
        {
        }
        #endregion

        #region Properties
        private Stanowiska _WybraneStanowisko;
        public Stanowiska WybraneStanowisko
        {
            get => _WybraneStanowisko;
            set
            {
                if (_WybraneStanowisko != value)
                {
                    _WybraneStanowisko = value;

                    if (_WybraneStanowisko != null)
                    {
                        Messenger.Default.Send(_WybraneStanowisko);
                        OnRequestClose(); // Zamknięcie okna po wyborze stanowiska
                    }

                    OnPropertyChanged(() => WybraneStanowisko);
                }
            }
        }
        #endregion

        #region Sort And Find 
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "nazwa", "zakres obowiązków" };
        }

        public override void Sort()
        {
            if (SortField == "nazwa")
                List = new ObservableCollection<Stanowiska>(List.OrderBy(item => item.Nazwa));

            if (SortField == "zakres obowiązków")
                List = new ObservableCollection<Stanowiska>(List.OrderBy(item => item.ZakresObowiazkow));
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "nazwa", "zakres obowiązków" };
        }

        public override void Find()
        {
            if (FindField == "nazwa")
                List = new ObservableCollection<Stanowiska>(
                    List.Where(item => item.Nazwa != null &&
                                      item.Nazwa.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase))
                );

            if (FindField == "zakres obowiązków")
                List = new ObservableCollection<Stanowiska>(
                    List.Where(item => item.ZakresObowiazkow != null &&
                                      item.ZakresObowiazkow.IndexOf(FindTextBox, StringComparison.OrdinalIgnoreCase) >= 0)
                );
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Stanowiska>(
                dentCareEntities.Stanowiska
                    .Where(s => s.CzyAktywne)  // CzyAktywne jako bool (bez == true)
                    .ToList()
            );
        }
        #endregion
    }
}
