using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace DentClinicApp.ViewModels
{
    public class NowyPracownikViewModel : JedenViewModel<Pracownicy>
    {
        #region Constructor
        public NowyPracownikViewModel()
            :base("Pracownik")
        {
            item = new Pracownicy();
            DataZatrudnienia = DateTime.Now;
            CzyZwolniony = false;

        }

        #endregion

        #region Properties
        //dla każdego pola na interface tworzymy properties
        public string Imie
        {
            get
            {
                return item.Imie;
            }

            set
            {
                item.Imie = value;
                OnPropertyChanged(() => Imie);
            }
        }

        public string Nazwisko
        {
            get
            {
                return item.Nazwisko;
            }

            set
            {
                item.Nazwisko = value;
                OnPropertyChanged(() => Nazwisko);
            }
        }

       

        public DateTime DataZatrudnienia
        {
            get
            {
                return item.DataZatrudnienia;
            }
            set
            {
                item.DataZatrudnienia = value;
                OnPropertyChanged(() => DataZatrudnienia);
            }
        }

        public DateTime? DataZwolnienia
        {
            get
            {
                return item.DataZwolnienia;
            }
            set
            {
                item.DataZwolnienia = value;
                OnPropertyChanged(() => DataZwolnienia);
            }
        }


        public string Telefon
        {
            get
            {
                return item.Telefon;
            }

            set
            {
                item.Telefon = value;
                OnPropertyChanged(() => Telefon);
            }
        }

        public string Email
        {
            get
            {
                return item.Email;
            }

            set
            {
                item.Email = value;
                OnPropertyChanged(() => Email);
            }
        }

        public int? IdStanowiska
        {
            get
            {
                return item.IdStanowiska;
            }

            set
            {
                item.IdStanowiska = value;
                OnPropertyChanged(() => IdStanowiska);
            }
        }
        public string Status
        {
            get
            {
                return CzyZwolniony ? "Nieaktywny" : "Aktywny";
            }
            set
            {
                CzyZwolniony = value == "Nieaktywny";
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
                    DataZwolnienia = null; // Resetuj datę zwolnienia, gdy pracownik nie jest zwolniony
                }
                OnPropertyChanged(() => CzyZwolniony);
            }
        }

        #endregion

        public IQueryable<KeyAndValue> StanowiskoItems
        {
            get 
            {
                return new StanowiskoB(dentCareEntities).GetStanowiskoKeyAndValueItems();
            }
        }

        #region Helpers

        public override void Save()
        {
            item.Status = CzyZwolniony ? "Nieaktywny" : "Aktywny";

            if (!CzyZwolniony)
            {
                DataZwolnienia = null; // Jeśli pracownik nie jest zwolniony, usuń datę zwolnienia
            }

            dentCareEntities.Pracownicy.Add(item);
            dentCareEntities.SaveChanges();

            // Dodawanie logów aktywności
            LogiAktywnosci logi = new LogiAktywnosci
            {
                IdUzytkownika = 3, 
                Akcja = $"Dodano pracownika o ID: {item.IdPracownika}",
                Data = DateTime.Now,
                Godzina = DateTime.Now.TimeOfDay,
                Opis = $"Dodano pracownika {item.Imie} {item.Nazwisko}"
            };
            dentCareEntities.LogiAktywnosci.Add(logi);
            dentCareEntities.SaveChanges();
        }


        #endregion

    }
}
