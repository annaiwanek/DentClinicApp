using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class NoweStanowiskoViewModel : JedenViewModel<Stanowiska>
    {
        #region Constructor

        public NoweStanowiskoViewModel()
            : base("Stanowisko")
        {
            item = new Stanowiska();

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
                item.Nazwa = value;
                OnPropertyChanged(() => Nazwa);
            }
        }

        public string ZakresObowiazkow
        {
            get
            {
                return item.ZakresObowiazkow;
            }

            set
            {
                item.ZakresObowiazkow = value;
                OnPropertyChanged(() => ZakresObowiazkow);
            }
        }

        public bool CzyAktywne
        {
            get
            {
                return item.CzyAktywne;
            }

            set
            {
                item.CzyAktywne = value;
                OnPropertyChanged(() => CzyAktywne);
            }
        }

        public decimal? WynagrodzenieMin
        {
            get
            {
                return item.WynagrodzenieMin;
            }

            set
            {
                item.WynagrodzenieMin = value;
                OnPropertyChanged(() => WynagrodzenieMin);
            }
        }

        public decimal? WynagrodzenieMax
        {
            get
            {
                return item.WynagrodzenieMax;
            }

            set
            {
                item.WynagrodzenieMax = value;
                OnPropertyChanged(() => ZakresObowiazkow);
            }
        }

        #endregion

        #region Helpers

        public override void Save()
        {
            saveWorkStation();
        }
        private void saveWorkStation()
        {
            try
            {
                Console.WriteLine("Adding WorkStation");
                dentCareEntities.Stanowiska.Add(item); // dodawanie rekordu najpierw do lokalnej kolekcji
                dentCareEntities.SaveChanges(); // zapisywanie do bazy danych 
                Console.WriteLine("Added WorkStation successfuly");
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
