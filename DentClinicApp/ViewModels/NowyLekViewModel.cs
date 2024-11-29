﻿using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class NowyLekViewModel : JedenViewModel<Leki>
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
                Console.WriteLine("Added drug successfuly");
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine("Errors");
                //Console.WriteLine(e.EntityValidationErrors.ToList);
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

