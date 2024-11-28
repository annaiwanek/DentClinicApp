using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class NowyPacjentViewModel : JedenViewModel<Pacjenci>
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

        #region Helpers
        public override void Save()
        {
            if (string.IsNullOrWhiteSpace(item.Imie))
                throw new InvalidOperationException("Pole 'Imię' jest wymagane.");

            if (string.IsNullOrWhiteSpace(item.Nazwisko))
                throw new InvalidOperationException("Pole 'Nazwisko' jest wymagane.");

            if (item    .DataUrodzenia == null || item.DataUrodzenia == DateTime.MinValue)
                throw new InvalidOperationException("Pole 'Data Urodzenia' jest wymagane.");

            //savePatient();
           
        }

      

        ////private void savePatient() {
        ////    try {
        ////        Console.WriteLine("Adding patient");
        ////        dentCareEntities.Pacjenci.Add(item); // dodawanie rekordu najpierw do lokalnej kolekcji
        ////        dentCareEntities.SaveChanges(); // zapisywanie do bazy danych 
        ////        Console.WriteLine("Added patient successfuly");
        ////    }
        ////    catch (DbEntityValidationException e)
        ////    {
        ////        Console.WriteLine("Errors");
        ////        //Console.WriteLine(e.EntityValidationErrors.ToList);
        ////        foreach (DbEntityValidationResult entityError in e.EntityValidationErrors) {
                    
        ////            foreach (DbValidationError validationError in entityError.ValidationErrors)
        ////            {
        ////                Console.WriteLine(validationError.PropertyName + ": " + validationError.ErrorMessage);
        ////            }
        ////        }
        ////    }
        ////    catch (Exception e) {
        ////        Console.WriteLine(e);
        ////    }
        //}
        #endregion
    }
}
