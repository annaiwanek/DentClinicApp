using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class NowaUslugaViewModel : JedenViewModel<Uslugi>
    {
        #region Constructor

        public NowaUslugaViewModel()
            : base("Usluga")
        {
            item = new Uslugi();
            DostepneCzasy = new ObservableCollection<int> { 15, 30, 45, 60, 90, 120 }; // Dostępne czasy trwania
        }

        #endregion

        #region Properties
        //dla każdego pola na interface tworzymy properties

        // Lista dostępnych czasów dla ComboBox
        public ObservableCollection<int> DostepneCzasy { get; }

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

        public string Opis
        {
            get
            {
                return item.Opis;
            }

            set
            {
                item.Opis = value;
                OnPropertyChanged(() => Opis);
            }
        }

        public decimal Cena
        {
            get
            {
                return item.Cena;
            }

            set
            {
                item.Cena = value;
                OnPropertyChanged(() => Cena);
            }
        }

        public int CzasTrwania
        {
            get
            {
                return item.CzasTrwania;
            }

            set
            {
                item.CzasTrwania = value;
                OnPropertyChanged(() => CzasTrwania);
            }
        }                                                                                                                                                                                                     

        #endregion

        #region Helpers
        public override void Save()
        {
            if (string.IsNullOrWhiteSpace(item.Nazwa))
                throw new InvalidOperationException("Pole 'Nazwa' jest wymagane.");

            if (item.Cena <= 0)
                throw new InvalidOperationException("Pole 'Cena' musi być większe od zera.");

            if (string.IsNullOrWhiteSpace(item.Nazwa))
                throw new InvalidOperationException("Pole 'Nazwa' jest wymagane.");

            if (!DostepneCzasy.Contains(item.CzasTrwania))
                throw new InvalidOperationException("Pole 'Czas Trwania' musi być jedną z dozwolonych wartości.");


            saveService();

        }

        private void saveService()
        {
            try
            {
                Console.WriteLine("Adding service");
                dentCareEntities.Uslugi.Add(item); // dodawanie rekordu najpierw do lokalnej kolekcji
                dentCareEntities.SaveChanges(); // zapisywanie do bazy danych 
                Console.WriteLine("Added service successfuly");
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
