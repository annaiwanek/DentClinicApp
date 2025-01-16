using DentClinicApp.Helper;
using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class NowyGrafikViewModel : JedenViewModel<GrafikPracownikow>
    {
        #region Constructor
        public NowyGrafikViewModel()
            : base("GrafikPracownikow")
        {
            item = new GrafikPracownikow();
            Data = DateTime.Now;

            // Rejestracja Messengera, który odbiera wybranego pracownika z widoku PracownicyWindow
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

        #region Properties

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

        public DateTime Data
        {
            get => item.Data;
            set
            {
                item.Data = value;
                OnPropertyChanged(() => Data);
            }
        }

        public TimeSpan GodzinaRozpoczecia
        {
            get => item.GodzinaRozpoczęcia;
            set
            {
                item.GodzinaRozpoczęcia = value;
                OnPropertyChanged(() => GodzinaRozpoczecia);
            }
        }

        public TimeSpan GodzinaZakonczenia
        {
            get => item.GodzinaZakończenia;
            set
            {
                item.GodzinaZakończenia = value;
                OnPropertyChanged(() => GodzinaZakonczenia);
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
            if (item.Pracownicy == null || item.IdPracownika <= 0)
                throw new InvalidOperationException("Nie wybrano poprawnego pracownika.");

            if (Data == DateTime.MinValue)
                throw new InvalidOperationException("Pole 'Data' jest wymagane.");

            if (GodzinaRozpoczecia >= GodzinaZakonczenia)
                throw new InvalidOperationException("Godzina rozpoczęcia musi być wcześniejsza niż godzina zakończenia.");

            saveGrafik();
        }

        private void saveGrafik()
        {
            try
            {
                dentCareEntities.GrafikPracownikow.Add(item);

                // Dodawanie logów aktywności
                LogiAktywnosci logi = new LogiAktywnosci
                {
                    IdUzytkownika = 3, // Aktualnie brak dostępu do zalogowanego użytkownika
                    Akcja = $"Dodano grafik dla pracownika ID: {item.IdPracownika}",
                    Data = DateTime.Now,
                    Godzina = DateTime.Now.TimeOfDay,
                    Opis = $"Dodano grafik dla pracownika o ID: {item.IdPracownika}"
                };

                dentCareEntities.LogiAktywnosci.Add(logi);
                dentCareEntities.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wystąpił błąd: {e.Message}");
            }
        }

        #endregion
    }

}

