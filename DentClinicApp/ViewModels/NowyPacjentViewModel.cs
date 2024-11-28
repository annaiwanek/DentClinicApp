using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class NowyPacjentViewModel : WorkspaceViewModel
    {
        #region DB

        private DentCareEntities dentCareEntities;

        #endregion

        #region Item

        private Pacjenci pacjenci;

        #endregion

        #region Command
        // to jest komenda, która zostanie podpięta pod przycisk zapisz i zamknij i wywoła funkcję SaveAndClose 

        private BaseCommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new BaseCommand(() => SaveAndClose());
                return _SaveCommand;
            }
        }

        #endregion

        #region Constructor

        public NowyPacjentViewModel()
        {
            base.DisplayName = "Pacjent";
            dentCareEntities = new DentCareEntities();
            pacjenci = new Pacjenci();
        }

        #endregion

        #region Properties
        //dla każdego pola na interface tworzymy properties
        public string Imie
        {
            get
            {
                return pacjenci.Imie;
            }

            set
            {
                pacjenci.Imie = value;
                OnPropertyChanged(() => Imie);
            }
        }

        public string Nazwisko
        {
            get
            {
                return pacjenci.Nazwisko;
            }

            set
            {
                pacjenci.Nazwisko = value;
                OnPropertyChanged(() => Nazwisko);
            }
        }

        public string Pesel
        {
            get
            {
                return pacjenci.PESEL;
            }

            set
            {
                pacjenci.PESEL = value;
                OnPropertyChanged(() => Pesel);
            }
        }

        public DateTime? DataUrodzenia
        {
            get
            {
                return pacjenci.DataUrodzenia;
            }
            set
            {
                pacjenci.DataUrodzenia = value;
                OnPropertyChanged(() => DataUrodzenia);
            }
        }





        public string Adres
        {
            get
            {
                return pacjenci.Adres;
            }

            set
            {
                pacjenci.Adres = value;
                OnPropertyChanged(() => Adres);
            }
        }

        public string Telefon
        {
            get
            {
                return pacjenci.Telefon;
            }

            set
            {
                pacjenci.Telefon = value;
                OnPropertyChanged(() => Telefon);
            }
        }

        #endregion

        #region Helpers
        public void Save()
        {
            dentCareEntities.Pacjenci.Add(pacjenci); // dodawanie rekordu najpierw do lokalnej kolekcji
            dentCareEntities.SaveChanges(); // zapisywanie do bazy danych 
        }

        public void SaveAndClose()
        {
            Save();
            base.OnRequestClose(); // Zamknięcie zakładki 
        }
        #endregion
    }
}
