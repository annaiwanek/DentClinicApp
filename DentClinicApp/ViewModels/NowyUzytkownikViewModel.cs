using DentClinicApp.Helper;
using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class NowyUzytkownikViewModel : JedenViewModel<UzytkownicySystemu>
    {
        #region Constructor
        public NowyUzytkownikViewModel()
            :base("Uzytkownik")
        {
            item = new UzytkownicySystemu();
            Messenger.Default.Register<PracownikForAllView>(this, getWybranyPracownik);
        }


        #endregion

        #region Command
        private BaseCommand _ShowPracownicyWindow;
        public ICommand ShowPracownicyWindow
        {
            get
            {
                if (_ShowPracownicyWindow == null)
                    _ShowPracownicyWindow = new BaseCommand(() => showPracownicyWindow());
                return _ShowPracownicyWindow;
            }
        }

        private void showPracownicyWindow()
        {
            Messenger.Default.Send("PracownicyWindowAll"); // Wyślij komunikat, aby otworzyć okno modalne
        }
        #endregion



        #region Fields

        public string WybranyPracownik
        {
            get
            {
                if (item.Pracownicy != null)
                    return $"{item.Pracownicy.Imie} {item.Pracownicy.Nazwisko}";
                return string.Empty;
            }
            set
            {
                // Pole nie może być zmieniane bezpośrednio
                OnPropertyChanged(() => WybranyPracownik);
            }
        }
        public int? IdPracownika
        {
            get
            {
                return item.IdPracownika;
            }

            set
            {
                item.IdPracownika = value;
                OnPropertyChanged(() => IdPracownika);
            }
        }

        public string Login
        {
            get
            {
                return item.Login;
            }

            set
            {
                item.Login = value;
                OnPropertyChanged(() => Login);
            }
        }

        public string Haslo
        {
            get
            {
                return item.Haslo;
            }

            set
            {
                item.Haslo = value;
                OnPropertyChanged(() => Haslo);
            }
        }

        public string Rola
        {
            get
            {
                return item.Rola ;
            }

            set
            {
                item.Rola = value;
                OnPropertyChanged(() => Rola);
            }
        }



        public IQueryable<KeyAndValue> PracownicyItems
        {
            get
            {
                return new PracownikB(dentCareEntities).GetPracownikKeyAndValueItems();
            }
        }

        #endregion

        #region Helpers

        private void getWybranyPracownik(PracownikForAllView pracownik)
        {
            if (pracownik != null)
            {
                // Pobierz obiekt pracownika z bazy danych
                var pracownikFromDb = dentCareEntities.Pracownicy.SingleOrDefault(p => p.IdPracownika == pracownik.IdPracownika);

                if (pracownikFromDb != null)
                {
                    item.Pracownicy = pracownikFromDb;
                    item.IdPracownika = pracownikFromDb.IdPracownika;
                    OnPropertyChanged(() => WybranyPracownik);
                }
            }
        }

        public override void Save()
        {
            dentCareEntities.UzytkownicySystemu.Add(item);

            // dodawanie logów aktywności 
            LogiAktywnosci logi = new LogiAktywnosci();
            logi.IdUzytkownika = 3; //Aktualnie nie ma dostępu do zalogowanego użytkownika
            logi.Akcja = "Dodanie nowego użytkownika o ID: " + item.IdPracownika;
            logi.Data = DateTime.Now;
            logi.Godzina = logi.Data.TimeOfDay;
            logi.Opis = "Dodanie nowego nowego użytkownika o ID: " + item.IdPracownika;
            dentCareEntities.LogiAktywnosci.Add(logi);
            
            dentCareEntities.SaveChanges();
        }

        #endregion
    }
}
