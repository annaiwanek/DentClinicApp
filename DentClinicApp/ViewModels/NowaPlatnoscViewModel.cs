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
  
      
        public class NowaPlatnoscViewModel : JedenViewModel<Platnosci>
        {
        public NowaPlatnoscViewModel()
            : base("Nowa Płatność")
        {
            item = new Platnosci();
            DataPlatnosci = DateTime.Now;

            // Załaduj listy
            Uslugi = new ObservableCollection<Uslugi>(dentCareEntities.Uslugi.ToList());
            Wizyty = new ObservableCollection<Wizyty>(dentCareEntities.Wizyty.Where(w => w.Status == "Zakończona").ToList());
            MetodyPlatnosci = new ObservableCollection<string> { "Gotówka", "Karta", "Przelew" };

            // Rejestracja komunikatora
            Messenger.Default.Register<Pacjenci>(this, getWybranyPacjent);
        }

        #region Properties

        public ObservableCollection<Wizyty> Wizyty { get; set; }

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
                if (value != null)
                {
                    Kwota = value.Cena; // Przypisanie kwoty automatycznie
                    item.IdUslugi = value.IdUslugi;
                }
                OnPropertyChanged(() => WybranaUsluga);
            }
        }

        public decimal Kwota
        {
            get => item.Kwota;
            set
            {
                item.Kwota = value;
                OnPropertyChanged(() => Kwota);
            }
        }

        public ObservableCollection<string> MetodyPlatnosci { get; set; }

        public string WybranaMetodaPlatnosci
        {
            get => item.MetodaPlatnosci;
            set
            {
                item.MetodaPlatnosci = value;
                OnPropertyChanged(() => WybranaMetodaPlatnosci);
            }
        }

        public DateTime DataPlatnosci
        {
            get => item.DataPlatnosci ?? DateTime.Now;
            set
            {
                item.DataPlatnosci = value;
                OnPropertyChanged(() => DataPlatnosci);
            }
        }

        public bool CzyZaplacono
        {
            get => item.Status ?? false;
            set
            {
                item.Status = value;
                OnPropertyChanged(() => CzyZaplacono);
            }
        }

        private int? _idWizyty;
        public int? IdWizyty
        {
            get => _idWizyty;
            set
            {
                _idWizyty = value;
                item.IdWizyty = value ?? 0; // Przypisz ID wizyty do płatności
                OnPropertyChanged(() => IdWizyty);
            }
        }

        #endregion

        #region Commands

        public ICommand ShowPacjenciWindowCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("PacjenciWindowAll"); // Otwórz okno modalne
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
            if (WybranaUsluga == null)
                throw new InvalidOperationException("Nie wybrano usługi.");
            if (string.IsNullOrWhiteSpace(WybranaMetodaPlatnosci))
                throw new InvalidOperationException("Nie wybrano metody płatności.");
            if (item.IdWizyty <= 0)
                throw new InvalidOperationException("Nie wybrano wizyty.");

            try
            {
                item.Pacjenci = dentCareEntities.Pacjenci.Single(p => p.IdPacjenta == item.IdPacjenta);
                item.Uslugi = dentCareEntities.Uslugi.Single(u => u.IdUslugi == item.IdUslugi);
                item.Wizyty = dentCareEntities.Wizyty.Single(w => w.IdWizyty == item.IdWizyty);

                dentCareEntities.Platnosci.Add(item);
                dentCareEntities.SaveChanges();
                Console.WriteLine("Płatność została zapisana.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas zapisywania płatności: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }

        #endregion
    }

}





