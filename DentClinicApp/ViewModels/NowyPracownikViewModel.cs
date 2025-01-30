using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Linq;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class NowyPracownikViewModel : JedenViewModel<Pracownicy>
    {
        #region Constructor
        public NowyPracownikViewModel()
            : base("Pracownik")
        {
            item = new Pracownicy();
            DataZatrudnienia = DateTime.Now;
            CzyZwolniony = false;

            // Rejestracja Messengera do odbioru wybranego stanowiska
            Messenger.Default.Register<Stanowiska>(this, getWybraneStanowisko);
        }
        #endregion

        #region Properties
        public string Imie
        {
            get => item.Imie;
            set
            {
                item.Imie = value;
                OnPropertyChanged(() => Imie);
            }
        }

        public string Nazwisko
        {
            get => item.Nazwisko;
            set
            {
                item.Nazwisko = value;
                OnPropertyChanged(() => Nazwisko);
            }
        }

        public DateTime DataZatrudnienia
        {
            get => item.DataZatrudnienia;
            set
            {
                item.DataZatrudnienia = value;
                OnPropertyChanged(() => DataZatrudnienia);
            }
        }

        public string Telefon
        {
            get => item.Telefon;
            set
            {
                item.Telefon = value;
                OnPropertyChanged(() => Telefon);
            }
        }

        public string Email
        {
            get => item.Email;
            set
            {
                item.Email = value;
                OnPropertyChanged(() => Email);
            }
        }

        private string _stanowisko;
        public string WybraneStanowisko
        {
            get => _stanowisko;
            set
            {
                _stanowisko = value;
                OnPropertyChanged(() => WybraneStanowisko);
            }
        }

        public int? IdStanowiska
        {
            get => item.IdStanowiska;
            set
            {
                item.IdStanowiska = value;
                OnPropertyChanged(() => IdStanowiska);
            }
        }

        private string _zakresObowiazkow;
        public string ZakresObowiazkow
        {
            get => _zakresObowiazkow;
            set
            {
                _zakresObowiazkow = value;
                OnPropertyChanged(() => ZakresObowiazkow);
            }
        }

        private bool _czyZwolniony;
        public bool CzyZwolniony
        {
            get => _czyZwolniony;
            set
            {
                _czyZwolniony = value;
                if (!_czyZwolniony)
                {
                    item.DataZwolnienia = null;
                }
                OnPropertyChanged(() => CzyZwolniony);
            }
        }
        #endregion

        #region Commands
        public ICommand ShowStanowiskaWindowCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("StanowiskaWindowAll");
        });
        #endregion

        #region Helpers
        private void getWybraneStanowisko(Stanowiska stanowisko)
        {
            if (stanowisko != null)
            {
                item.IdStanowiska = stanowisko.IdStanowiska;
                WybraneStanowisko = stanowisko.Nazwa;
                ZakresObowiazkow = stanowisko.ZakresObowiazkow;
                OnPropertyChanged(() => IdStanowiska);
                OnPropertyChanged(() => WybraneStanowisko);
                OnPropertyChanged(() => ZakresObowiazkow);
            }
        }

        public override void Save()
        {
            if (string.IsNullOrWhiteSpace(Imie) || string.IsNullOrWhiteSpace(Nazwisko))
                throw new InvalidOperationException("Imię i nazwisko są wymagane.");
            if (string.IsNullOrWhiteSpace(WybraneStanowisko))
                throw new InvalidOperationException("Nie wybrano stanowiska.");

            item.Status = CzyZwolniony ? "Nieaktywny" : "Aktywny";

            dentCareEntities.Pracownicy.Add(item);
            dentCareEntities.SaveChanges();
        }
        #endregion
    }
}
