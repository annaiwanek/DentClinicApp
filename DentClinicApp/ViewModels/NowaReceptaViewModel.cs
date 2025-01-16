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
    public class NowaReceptaViewModel : JedenViewModel<Recepty>
    {
        public NowaReceptaViewModel()
             : base("Nowa Recepta")
        {
            item = new Recepty();
            DataWystawienia = DateTime.Now;

            // Załaduj listy
            Lekarze = new ObservableCollection<Pracownicy>(
                dentCareEntities.Pracownicy
                    .Where(p => p.Status == "Aktywny") // Filtrowanie tylko aktywnych pracowników
                    .ToList()
            );




            // Rejestracja komunikatora
            Messenger.Default.Register<Pacjenci>(this, getWybranyPacjent);
        }

        #region Properties

        // Wybrany pacjent
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

        // Uwagi
        private string _uwagi;
        public string Uwagi
        {
            get => _uwagi;
            set
            {
                _uwagi = value;
                item.Uwagi = value;
                OnPropertyChanged(() => Uwagi);
            }
        }

        // Data wystawienia
        public DateTime DataWystawienia
        {
            get => item.DataWystawienia;
            set
            {
                item.DataWystawienia = value;
                OnPropertyChanged(() => DataWystawienia);
            }
        }

        // Lista lekarzy
        public ObservableCollection<Pracownicy> Lekarze { get; set; }

        // Wybrany lekarz
        private Pracownicy _wybranyLekarz;
        public Pracownicy WybranyLekarz
        {
            get => _wybranyLekarz;
            set
            {
                _wybranyLekarz = value;
                if (value != null)
                {
                    item.IdPracownika = value.IdPracownika;
                }
                OnPropertyChanged(() => WybranyLekarz);
            }
        }

        #endregion

        #region Commands

        // Komenda otwierająca okno wyboru pacjenta
        public ICommand ShowPacjenciWindowCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("PacjenciWindowAll"); // Wyślij komunikat do otwarcia okna
        });

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
                }
            }
        }

        public override void Save()
        {
            if (item.Pacjenci == null)
                throw new InvalidOperationException("Nie wybrano pacjenta.");
            if (WybranyLekarz == null)
                throw new InvalidOperationException("Nie wybrano lekarza.");
            if (string.IsNullOrWhiteSpace(Uwagi))
                throw new InvalidOperationException("Uwagi nie mogą być puste.");

            try
            {
                item.Pacjenci = dentCareEntities.Pacjenci.Single(p => p.IdPacjenta == item.IdPacjenta);
                item.Pracownicy = dentCareEntities.Pracownicy.Single(p => p.IdPracownika == item.IdPracownika);

                dentCareEntities.Recepty.Add(item);
                dentCareEntities.SaveChanges();
                Console.WriteLine("Recepta została zapisana.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas zapisywania recepty: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        #endregion
    }
}
