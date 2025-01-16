using DentClinicApp.Models.Entities;
using DentClinicApp.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class NowyLekViewModel : JedenViewModel<Leki>, IDataErrorInfo
    {
        #region Constructor

        public NowyLekViewModel()
            : base("Lek")
        {
            item = new Leki();

        }

        #endregion

        #region Properties
        //dla każdego pola na interface tworzymy properties
        public string Nazwa
        {
            get
            {
                return item.Nazwa;
            }

            set
            {
                item.Nazwa= value;
                OnPropertyChanged(() => Nazwa);
            }
        }

        public string SubstancjaCzynna
        {
            get
            {
                return item.SubstancjaCzynna;
            }

            set
            {
                item.SubstancjaCzynna = value;
                OnPropertyChanged(() => SubstancjaCzynna);
            }
        }

        public string Dawka
        {
            get
            {
                return item.Dawka;
            }

            set
            {
                item.Dawka = value;
                OnPropertyChanged(() => Dawka);
            }
        }

        public string Postac
        {
            get
            {
                return item.Postac;
                    
            }
            set
            {
                item.Postac = value;
                OnPropertyChanged(() => Postac);
            }
        }

        #endregion


        #region Validation

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(Nazwa):
                        error = StringValidator.SprawdzCzyZaczynaSieOdDuzej(Nazwa);
                        break;
                    case nameof(SubstancjaCzynna):
                        if (string.IsNullOrEmpty(SubstancjaCzynna))
                        {
                            error = "Substancja czynna jest wymagana.";
                        }
                        break;
                    case nameof(Dawka):
                        error = LekValidator.SprawdzCzyPoprawnaDawka(Dawka);
                        break;
                    case nameof(Postac):
                        error = LekValidator.SprawdzPostac(Postac);
                        break;
                }

                return error;
            }
        }

        public string Error => null;

        public override bool IsValid()
        {
            return string.IsNullOrEmpty(this[nameof(Nazwa)])
                && string.IsNullOrEmpty(this[nameof(SubstancjaCzynna)])
                && string.IsNullOrEmpty(this[nameof(Dawka)])
                && string.IsNullOrEmpty(this[nameof(Postac)]);
        }

        #endregion



        #region Helpers
        public override void Save()
        {
            if (string.IsNullOrWhiteSpace(item.Nazwa))
                throw new InvalidOperationException("Pole 'Nazwa' jest wymagane.");

            if (string.IsNullOrWhiteSpace(item.SubstancjaCzynna))
                throw new InvalidOperationException("Pole 'Substancja czyna' jest wymagane.");

            if (string.IsNullOrWhiteSpace(item.Dawka))
                throw new InvalidOperationException("Pole 'Dawka' jest wymagane.");

            saveDrug();
        }
        private void saveDrug()
        {
            try
            {
                Console.WriteLine("Adding drug");
                dentCareEntities.Leki.Add(item); // dodawanie rekordu najpierw do lokalnej kolekcji
                dentCareEntities.SaveChanges(); // zapisywanie do bazy danych 

                // Dodawanie logów aktywności
                LogiAktywnosci logi = new LogiAktywnosci();
                logi.IdUzytkownika = 3; // Aktualnie brak systemu zalogowanego użytkownika
                logi.Akcja = $"Dodano nowy lek o ID: {item.IdLeku}";
                logi.Data = DateTime.Now;
                logi.Godzina = logi.Data.TimeOfDay;
                logi.Opis = $"Dodano lek '{item.Nazwa}' z substancją czynną '{item.SubstancjaCzynna}'";
                dentCareEntities.LogiAktywnosci.Add(logi);
                dentCareEntities.SaveChanges(); // Zapisywanie logów do bazy

                Console.WriteLine("Added drug successfully");
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine("Errors");
                foreach (DbEntityValidationResult entityError in e.EntityValidationErrors)
                {
                    foreach (DbValidationError validationError in entityError.ValidationErrors)
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

