using DentClinicApp.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DentClinicApp.ViewModels
{
    public class LekiWindowViewModel : WszystkieViewModel<Leki>
    {
        #region Constructor
        public LekiWindowViewModel()
            : base("Leki")
        {
        }
        #endregion

        #region Properties
        private Leki _wybranyLek;
        public Leki WybranyLek
        {
            get => _wybranyLek;
            set
            {
                if (_wybranyLek != value)
                {
                    _wybranyLek = value;

                    if (_wybranyLek != null)
                    {
                        Messenger.Default.Send(_wybranyLek);
                        OnRequestClose(); // Zamknięcie okna po wyborze leku
                    }

                    OnPropertyChanged(() => WybranyLek);
                }
            }
        }
        #endregion

        #region Sort And Find
        public override List<string> GetComboboxSortList()
        {
            return new List<string> { "nazwa", "substancja czynna", "postać", "dawka" };
        }

        public override void Sort()
        {
            if (SortField == "nazwa")
                List = new ObservableCollection<Leki>(List.OrderBy(item => item.Nazwa));
            if (SortField == "substancja czynna")
                List = new ObservableCollection<Leki>(List.OrderBy(item => item.SubstancjaCzynna));
            if (SortField == "postać")
                List = new ObservableCollection<Leki>(List.OrderBy(item => item.Postac));
            if (SortField == "dawka")
                List = new ObservableCollection<Leki>(List.OrderBy(item => item.Dawka));
        }

        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "nazwa", "substancja czynna", "postać", "dawka" };
        }

        public override void Find()
        {
            if (FindField == "nazwa")
                List = new ObservableCollection<Leki>(List.Where(item => item.Nazwa != null && item.Nazwa.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase)));

            if (FindField == "substancja czynna")
                List = new ObservableCollection<Leki>(List.Where(item => item.SubstancjaCzynna != null && item.SubstancjaCzynna.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase)));

            if (FindField == "postać")
                List = new ObservableCollection<Leki>(List.Where(item => item.Postac != null && item.Postac.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase)));

            if (FindField == "dawka")
                List = new ObservableCollection<Leki>(List.Where(item => item.Dawka != null && item.Dawka.StartsWith(FindTextBox, StringComparison.OrdinalIgnoreCase)));
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Leki>(dentCareEntities.Leki.ToList());
        }
        #endregion
    }
}
