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
    public class NowaWizytaViewModel : JedenViewModel<Wizyty>
    {
        public NowaWizytaViewModel()
            : base("Nowa Wizyta")
        {
            item = new Wizyty();
            DataWizyty = DateTime.Now;

            // Załaduj listy do comboboxów
            Uslugi = new ObservableCollection<Uslugi>(dentCareEntities.Uslugi.ToList());
            //Lekarze = new ObservableCollection<Pracownicy>(dentCareEntities.Pracownicy.ToList());

            Lekarze = new ObservableCollection<Pracownicy>(
               dentCareEntities.Pracownicy
                   .Where(p => p.Status == "Aktywny") // Filtrowanie tylko aktywnych pracowników
                   .ToList()
           );
            StatusyWizyt = new ObservableCollection<string> { "Zaplanowana", "Zakończona", "Anulowana" };
            Godzina = GenerateTimeSlots();
            CzasTrwaniaWMinutach = new ObservableCollection<int> { 15, 30, 45, 60 };

            // Messenger do wyboru pacjenta
            Messenger.Default.Register<Pacjenci>(this, getWybranyPacjent);
        }

        #region Properties

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

        public ObservableCollection<Uslugi> Uslugi { get; set; }
        public Uslugi WybranaUsluga
        {
            get => item.Uslugi;
            set
            {
                item.Uslugi = value;
                OnPropertyChanged(() => WybranaUsluga);
            }
        }

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

        public DateTime DataWizyty
        {
            get => item.Data;
            set
            {
                item.Data = value;
                OnPropertyChanged(() => DataWizyty);
            }
        }

        public ObservableCollection<string> Godzina { get; set; }
        public TimeSpan WybranaGodzina
        {
            get => item.Godzina;
            set
            {
                item.Godzina = value;
                OnPropertyChanged(() => WybranaGodzina);
            }
        }

        public ObservableCollection<string> StatusyWizyt { get; set; }
        public string WybranyStatus
        {
            get => item.Status;
            set
            {
                item.Status = value;
                OnPropertyChanged(() => WybranyStatus);
            }
        }

        public ObservableCollection<int> CzasTrwaniaWMinutach { get; set; }
        public int WybranyCzasTrwania
        {
            get => item.CzasTrwaniaWMinutach;
            set
            {
                item.CzasTrwaniaWMinutach = value;
                OnPropertyChanged(() => WybranyCzasTrwania);
            }
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
                }
            }
        }

        private ObservableCollection<string> GenerateTimeSlots()
        {
            var slots = new ObservableCollection<string>();
            for (var time = new TimeSpan(8, 0, 0); time <= new TimeSpan(20, 0, 0); time = time.Add(TimeSpan.FromMinutes(15)))
            {
                slots.Add(time.ToString(@"hh\:mm"));
            }
            return slots;
        }

        public ICommand ShowPacjenciWindowCommand
        {
            get
            {
                return new BaseCommand(() =>
                {
                    Messenger.Default.Send("PacjenciWindowAll"); // Wyślij komunikat, aby otworzyć okno modalne
                });
            }
        }


        public override void Save()
        {
            if (item.Pacjenci == null || item.IdPacjenta <= 0)
                throw new InvalidOperationException("Nie wybrano poprawnego pacjenta.");

            if (WybranaUsluga == null)
                throw new InvalidOperationException("Nie wybrano usługi.");

            if (WybranyLekarz == null)
                throw new InvalidOperationException("Nie wybrano lekarza.");

            if (string.IsNullOrEmpty(WybranyStatus))
                throw new InvalidOperationException("Nie wybrano statusu.");

            saveWizyta();
        }

        private void saveWizyta()
        {
            try
            {
                dentCareEntities.Wizyty.Add(item);
                dentCareEntities.SaveChanges();
                Console.WriteLine("Wizyta zapisana pomyślnie.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wystąpił błąd: {e.Message}");
            }
        }

        #endregion
    }
}

