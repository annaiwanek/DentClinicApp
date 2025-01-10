using DentClinicApp.Helper;
using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class NowyDokumentViewModel : JedenViewModel<Dokumenty>
    {
        #region Constructor

        public NowyDokumentViewModel()
            : base("Dokument")
        {
            item = new Dokumenty();

            DataDodania = DateTime.Now; // Domyślna data dodania

            //to jest Messenger, który oczekuje na pacjenta z widoku PacienciWindow
            //jak go złapiemy to wywoływana jest metoda getWybranyPacjent
            Messenger.Default.Register<Pacjenci>(this, getWybranyPacjent);
            
        }

        #endregion

        #region Properties

    

        #region Command

        private BaseCommand _ShowPacjenciWindow; 
        public ICommand ShowPacjenciWindow
        {
            get
            {
                if (_ShowPacjenciWindow == null)
                    _ShowPacjenciWindow = new BaseCommand(() => showPacjenciWindow());
                return _ShowPacjenciWindow;
            }
        }
        public void showPacjenciWindow()
        {
            Messenger.Default.Send<string>("PacjenciWindowAll");

        }
        #endregion

        public string WybranyPacjent
        {
            get
            {
                if (item.Pacjenci != null)
                    return $"{item.Pacjenci.Imie} {item.Pacjenci.Nazwisko}";
                return string.Empty;
            }
            set
            {
                // Nie zmieniamy tego pola bezpośrednio
                OnPropertyChanged(() => WybranyPacjent);
            }
        }




        public string WybranyPacjentPESEL
        {
            get => item.Pacjenci?.PESEL;
            set
            {
                if (item.Pacjenci == null)
                    item.Pacjenci = new Pacjenci();

                item.Pacjenci.PESEL = value;
                OnPropertyChanged(() => WybranyPacjentPESEL);
            }
        }


     

        public string NazwaDokumentu
        {
            get => item.NazwaDokumentu;
            set
            {
                item.NazwaDokumentu = value;
                OnPropertyChanged(() => NazwaDokumentu);
            }
        }

      public string TypDokumentu
{
    get => item.TypDokumentu;
    set
    {
        if (item.TypDokumentu != value)
        {
            item.TypDokumentu = value;
            OnPropertyChanged(() => TypDokumentu);
        }
    }
}


        public DateTime DataDodania
        {
            get => item.DataDodania;
            set
            {
                item.DataDodania = value;
                OnPropertyChanged(() => DataDodania);
            }
        }

        public string Opis
        {
            get => item.Opis;
            set
            {
                if (item.Opis != value)
                {
                    item.Opis = value;
                    OnPropertyChanged(() => Opis);
                }
            }
        }



        //public string SciezkaDoPliku
        //{
        //    get => item.SciezkaDoPliku;
        //    set
        //    {
        //        item.SciezkaDoPliku = value;
        //        OnPropertyChanged(() => SciezkaDoPliku);
        //    }
        //}

        #endregion

        #region Helpers

        private void getWybranyPacjent(Pacjenci pacjent)
        {
            if (pacjent != null)
            {
                // Pobierz obiekt pacjenta z aktualnego kontekstu
                var pacjentFromDb = dentCareEntities.Pacjenci.SingleOrDefault(p => p.IdPacjenta == pacjent.IdPacjenta);

                if (pacjentFromDb != null)
                {
                    item.Pacjenci = pacjentFromDb;
                    item.IdPacjenta = pacjentFromDb.IdPacjenta;
                    OnPropertyChanged(() => WybranyPacjent);
                    OnPropertyChanged(() => WybranyPacjentPESEL);
                }
            }
        }




        public override void Save()
        {
            if (item.Pacjenci == null || item.IdPacjenta <= 0)
                throw new InvalidOperationException("Nie wybrano poprawnego pacjenta.");

            if (string.IsNullOrWhiteSpace(item.NazwaDokumentu))
                throw new InvalidOperationException("Pole 'Nazwa pliku' jest wymagane.");

            if (string.IsNullOrWhiteSpace(item.TypDokumentu))
                throw new InvalidOperationException("Pole 'Typ dokumentu' jest wymagane.");

            if (string.IsNullOrWhiteSpace(item.Opis))
                throw new InvalidOperationException("Pole 'Opis' jest wymagane."); // Opcjonalnie

            Console.WriteLine($"Opis: {item.Opis}"); // Debugowanie

            saveDokument();
        }



        private void saveDokument()
        {
            try
            {
                // Debugowanie danych przed zapisem
                Console.WriteLine($"IdPacjenta: {item.IdPacjenta}, TypDokumentu: {item.TypDokumentu}, NazwaDokumentu: {item.NazwaDokumentu}");
                if (item.Pacjenci == null)
                {
                    Console.WriteLine("Pacjenci jest null.");
                    throw new InvalidOperationException("Nie przypisano pacjenta do dokumentu.");
                }

                dentCareEntities.Dokumenty.Add(item);
                dentCareEntities.SaveChanges();
                Console.WriteLine("Dokument zapisany pomyślnie.");
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
                Console.WriteLine($"Wystąpił błąd: {e.Message}");
            }
        }



        #endregion
    }

} 

