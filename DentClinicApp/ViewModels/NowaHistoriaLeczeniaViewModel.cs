using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    
        public class NowaHistoriaLeczeniaViewModel : JedenViewModel<HistoriaLeczenia>
        {
        #region Constructor

        public NowaHistoriaLeczeniaViewModel()
            : base("Nowa Historia Leczenia")
        {
            // Inicjalizacja nowego obiektu HistoriaLeczenia
            item = new HistoriaLeczenia();

            // Inicjalizacja listy lekarzy z bazy danych
            try
            {
                Lekarze = new ObservableCollection<Pracownicy>(dentCareEntities.Pracownicy.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas ładowania listy lekarzy: {ex.Message}");
                Lekarze = new ObservableCollection<Pracownicy>();
            }

            Data = DateTime.Now;

            // Messenger oczekujący na wybranie pacjenta z okna modalnego
            Messenger.Default.Register<Pacjenci>(this, getWybranyPacjent);
        }

        #endregion

        #region Properties

        public ObservableCollection<Pracownicy> Lekarze { get; set; }

        public Pracownicy WybranyLekarz
        {
            get => item.Pracownicy;
            set
            {
                item.Pracownicy = value;
                OnPropertyChanged(() => WybranyLekarz);
            }
        }

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

        public DateTime Data
        {
            get => item.Data;
            set
            {
                item.Data = value;
                OnPropertyChanged(() => Data);
            }
        }

        public string OpisLeczenia
        {
            get => item.OpisLeczenia;
            set
            {
                item.OpisLeczenia = value;
                OnPropertyChanged(() => OpisLeczenia);
            }
        }

        public string Zalecenia
        {
            get => item.Zalecenia;
            set
            {
                item.Zalecenia = value;
                OnPropertyChanged(() => Zalecenia);
            }
        }

        #endregion

        #region Command

        private BaseCommand _ShowPacjenciWindow;
        public ICommand ShowPacjenciWindow
        {
            get
            {
                if (_ShowPacjenciWindow == null)
                    _ShowPacjenciWindow = new BaseCommand(showPacjenciWindow);
                return _ShowPacjenciWindow;
            }
        }

        private void showPacjenciWindow()
        {
            Messenger.Default.Send<string>("PacjenciWindowAll");
        }

        #endregion

        #region Helpers

        private void getWybranyPacjent(Pacjenci pacjent)
        {
            if (pacjent != null)
            {
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
            // Walidacja danych przed zapisem
            if (item.Pacjenci == null || item.IdPacjenta <= 0)
                throw new InvalidOperationException("Nie wybrano poprawnego pacjenta.");

            if (string.IsNullOrWhiteSpace(item.OpisLeczenia))
                throw new InvalidOperationException("Pole 'Opis leczenia' jest wymagane.");

            if (WybranyLekarz == null)
                throw new InvalidOperationException("Nie wybrano lekarza prowadzącego.");

            // Przypisanie IdPracownika
            item.IdPracownika = WybranyLekarz.IdPracownika;

            saveHistoriaLeczenia();
        }

        private void saveHistoriaLeczenia()
        {
            try
            {
                dentCareEntities.HistoriaLeczenia.Add(item);
                dentCareEntities.SaveChanges();
                Console.WriteLine("Historia leczenia zapisana pomyślnie.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wystąpił błąd podczas zapisywania historii leczenia: {e.Message}");
            }
        }

        #endregion
    }
}

