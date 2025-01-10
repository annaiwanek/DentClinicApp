using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class NowaNotatkaViewModel : JedenViewModel<Notatki>
    {
        public NowaNotatkaViewModel()
             : base("Nowa Notatka")
        {
            item = new Notatki();
            Data = DateTime.Now;

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

        public string Tekst
        {
            get => item.Tekst;
            set
            {
                item.Tekst = value;
                OnPropertyChanged(() => Tekst);
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
            if (item.Pacjenci == null || item.IdPacjenta <= 0)
                throw new InvalidOperationException("Nie wybrano poprawnego pacjenta.");

            if (string.IsNullOrWhiteSpace(item.Tekst))
                throw new InvalidOperationException("Treść notatki jest wymagana.");

            saveNotatka();
        }

        private void saveNotatka()
        {
            try
            {
                dentCareEntities.Notatki.Add(item);
                dentCareEntities.SaveChanges();
                Console.WriteLine("Notatka zapisana pomyślnie.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wystąpił błąd: {e.Message}");
            }
        }

        #endregion
    }
}
