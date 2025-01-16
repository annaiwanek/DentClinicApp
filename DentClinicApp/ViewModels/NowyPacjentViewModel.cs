using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using DentClinicApp.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class NowyPacjentViewModel : JedenViewModel<Pacjenci>, IDataErrorInfo
    {
        
        #region Constructor

        public NowyPacjentViewModel()
            :base("Pacjent")
        {
            item = new Pacjenci();
          
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

        public string Pesel
        {
            get
            {
                return item.PESEL;
            }

            set
            {
                item.PESEL = value;
                OnPropertyChanged(() => Pesel);
            }
        }

        public DateTime? DataUrodzenia
        {
            get
            {
                return item.DataUrodzenia;
            }
            set
            {
                item.DataUrodzenia = value;
                OnPropertyChanged(() => DataUrodzenia);
            }
        }

        public string Adres
        {
            get
            {
                return item.Adres;
            }

            set
            {
                item.Adres = value;
                OnPropertyChanged(() => Adres);
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

        #endregion

        #region Validation (IDataErrorInfo)

        // Implementacja interfejsu IDataErrorInfo
        public string Error => null;

        public string this[string name]
        {
            get
            {
                string komunikat = null;

                if (name == nameof(Imie))
                {
                    komunikat = StringValidator.SprawdzCzyZaczynaSieOdDuzej(Imie);
                }
                else if (name == nameof(Nazwisko))
                {
                    komunikat = StringValidator.SprawdzCzyZaczynaSieOdDuzej(Nazwisko);
                }
                else if (name == nameof(Pesel))
                {
                    if (string.IsNullOrEmpty(Pesel) || Pesel.Length != 11 || !Pesel.All(char.IsDigit))
                    {
                        komunikat = "PESEL musi składać się z 11 cyfr.";
                    }
                }
                else if (name == nameof(DataUrodzenia))
                {
                    if (DataUrodzenia == null || DataUrodzenia == DateTime.MinValue)
                    {
                        komunikat = "Data urodzenia jest wymagana.";
                    }
                }
                else if (name == nameof(Adres))
                {
                    komunikat = StringValidator.SprawdzAdres(Adres);
                }
                else if (name == nameof(Telefon))
                {
                    if (string.IsNullOrEmpty(Telefon) || Telefon.Length != 9 || !Telefon.All(char.IsDigit))
                    {
                        komunikat = "Telefon musi składać się z 9 cyfr.";
                    }
                }
                else if (name == nameof(Email))
                {
                    komunikat = StringValidator.SprawdzEmail(Email);
                }

                return komunikat;
            }
        }

        #endregion

        #region Helpers

        public override void Save()
        {
            if (!IsValid())
            {
                MessageBox.Show("Popraw błędy przed zapisaniem.", "Błąd walidacji", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            savePatient();
        }

        // Sprawdzenie, czy wszystkie dane są poprawne
        public override bool IsValid()
        {
            foreach (var property in new[] { nameof(Imie), nameof(Nazwisko), nameof(Pesel), nameof(DataUrodzenia), nameof(Adres), nameof(Telefon), nameof(Email) })
            {
                if (this[property] != null)
                {
                    return false; // Jeśli jakiekolwiek pole ma błąd, zwracamy false
                }
            }

            return true;
        }

        private void savePatient()
        {
            try
            {
                dentCareEntities.Pacjenci.Add(item); // dodawanie rekordu najpierw do lokalnej kolekcji
                dentCareEntities.SaveChanges(); // zapisywanie do bazy danych 

                // Dodawanie logów aktywności
                LogiAktywnosci logi = new LogiAktywnosci
                {
                    IdUzytkownika = 3, // Ustawienie identyfikatora zalogowanego użytkownika
                    Akcja = "Dodano pacjenta o ID: " + item.IdPacjenta,
                    Data = DateTime.Now,
                    Godzina = DateTime.Now.TimeOfDay,
                    Opis = "Dodano pacjenta: " + item.Imie + " " + item.Nazwisko
                };

                dentCareEntities.LogiAktywnosci.Add(logi);
                dentCareEntities.SaveChanges(); // Zapisywanie logów
            }
            catch (DbEntityValidationException e)
            {
                foreach (var entityError in e.EntityValidationErrors)
                {
                    foreach (var validationError in entityError.ValidationErrors)
                    {
                        Console.WriteLine(validationError.PropertyName + ": " + validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        #endregion
    }
}
