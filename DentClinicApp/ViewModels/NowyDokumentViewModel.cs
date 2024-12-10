using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            item = new Dokumenty
            {
                DataDodania = DateTime.Now // Domyślna data dodania
            };
        }

        #endregion

        #region Properties

        // Właściwość dla ComboBox z pacjentami
        public IQueryable<KeyAndValue> PacjenciItems
        {
            get
            {
                PacjentB pacjentB = new PacjentB(dentCareEntities);
                return pacjentB.GetPacjentKeyAndValueItems();
            }
        }

        // Właściwość dla wybranego pacjenta
        private int? _wybranyPacjentId;
        public int? WybranyPacjentId
        {
            get => _wybranyPacjentId;
            set
            {
                _wybranyPacjentId = value;

                if (_wybranyPacjentId.HasValue)
                {
                    var pacjent = dentCareEntities.Pacjenci.FirstOrDefault(p => p.IdPacjenta == _wybranyPacjentId.Value);
                    if (pacjent != null)
                    {
                        item.Pacjenci = pacjent;
                        Pesel = pacjent.PESEL;
                        Imie = pacjent.Imie;
                        Nazwisko = pacjent.Nazwisko;
                    }
                }

                OnPropertyChanged(() => WybranyPacjentId);
            }
        }

        public string Pesel
        {
            get => item.Pacjenci?.PESEL;
            set
            {
                if (item.Pacjenci == null)
                    item.Pacjenci = new Pacjenci();

                item.Pacjenci.PESEL = value;
                OnPropertyChanged(() => Pesel);
            }
        }

        public string Nazwisko
        {
            get => item.Pacjenci?.Nazwisko;
            set
            {
                if (item.Pacjenci == null)
                    item.Pacjenci = new Pacjenci();

                item.Pacjenci.Nazwisko = value;
                OnPropertyChanged(() => Nazwisko);
            }
        }

        public string Imie
        {
            get => item.Pacjenci?.Imie;
            set
            {
                if (item.Pacjenci == null)
                    item.Pacjenci = new Pacjenci();

                item.Pacjenci.Imie = value;
                OnPropertyChanged(() => Imie);
            }
        }

        public string NazwaPliku
        {
            get => item.NazwaDokumentu;
            set
            {
                item.NazwaDokumentu = value;
                OnPropertyChanged(() => NazwaPliku);
            }
        }

        public string TypDokumentu
        {
            get => item.TypDokumentu;
            set
            {
                item.TypDokumentu = value;
                OnPropertyChanged(() => TypDokumentu);
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

        public string SciezkaDoPliku
        {
            get => item.SciezkaDoPliku;
            set
            {
                item.SciezkaDoPliku = value;
                OnPropertyChanged(() => SciezkaDoPliku);
            }
        }

        #endregion

        #region Helpers

        public override void Save()
        {
            if (string.IsNullOrWhiteSpace(item.NazwaDokumentu))
                throw new InvalidOperationException("Pole 'Nazwa pliku' jest wymagane.");

            if (string.IsNullOrWhiteSpace(item.TypDokumentu))
                throw new InvalidOperationException("Pole 'Typ dokumentu' jest wymagane.");

            if (item.DataDodania == null || item.DataDodania == DateTime.MinValue)
                throw new InvalidOperationException("Pole 'Data dodania' jest wymagane.");

            saveDokument();
        }

        private void saveDokument()
        {
            try
            {
                dentCareEntities.Dokumenty.Add(item); // Dodanie dokumentu do kolekcji lokalnej
                dentCareEntities.SaveChanges(); // Zapisanie do bazy danych
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

