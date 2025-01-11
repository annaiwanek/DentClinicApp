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
    public class NowaReceptaLekViewModel : JedenViewModel<ReceptyLeki>
  
        {
            public NowaReceptaLekViewModel()
                 : base("Nowa Recepta - Lek")
            {
                item = new ReceptyLeki();
                DataDodania = DateTime.Now;

                // Załaduj listy
                Recepty = new ObservableCollection<Recepty>(dentCareEntities.Recepty.ToList());
                Leki = new ObservableCollection<Leki>(dentCareEntities.Leki.ToList());
            }

            #region Properties

            public ObservableCollection<Recepty> Recepty { get; set; }
            public ObservableCollection<Leki> Leki { get; set; }

            public Recepty WybranaRecepta
            {
                get => item.Recepty;
                set
                {
                    item.Recepty = value;
                    if (value != null)
                    {
                        DataWystawienia = value.DataWystawienia;
                        PeselPacjenta = value.Pacjenci.PESEL;
                    }
                    OnPropertyChanged(() => WybranaRecepta);
                }
            }

            public Leki WybranyLek
            {
                get => item.Leki;
                set
                {
                    item.Leki = value;
                    if (value != null)
                    {
                        DawkaPostac = $"{value.Dawka} {value.Postac}";
                    }
                    OnPropertyChanged(() => WybranyLek);
                }
            }

            public string DawkaPostac { get; set; }
            public string PeselPacjenta { get; set; }
            public DateTime DataWystawienia { get; set; }

            public int Ilosc
            {
                get => item.Ilosc;
                set
                {
                    item.Ilosc = value;
                    OnPropertyChanged(() => Ilosc);
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

        #endregion

        public override void Save()
        {
            // Walidacja danych wejściowych
            if (WybranaRecepta == null)
                throw new InvalidOperationException("Nie wybrano recepty.");
            if (WybranyLek == null)
                throw new InvalidOperationException("Nie wybrano leku.");
            if (Ilosc <= 0)
                throw new InvalidOperationException("Ilość musi być większa od zera.");

            try
            {
                // Przypisz wartości do elementu ReceptyLeki
                item.IdRecepty = WybranaRecepta.IdRecepty;
                item.IdLeku = WybranyLek.IdLeku;
                item.Ilosc = Ilosc;
                item.DataDodania = DateTime.Now; // Automatyczne przypisanie daty

                // Sprawdzenie, czy recepta istnieje w bazie danych
                var receptaFromDb = dentCareEntities.Recepty
                    .SingleOrDefault(r => r.IdRecepty == item.IdRecepty);
                if (receptaFromDb == null)
                    throw new InvalidOperationException("Wybrana recepta nie istnieje w bazie danych.");

                // Sprawdzenie, czy lek istnieje w bazie danych
                var lekFromDb = dentCareEntities.Leki
                    .SingleOrDefault(l => l.IdLeku == item.IdLeku);
                if (lekFromDb == null)
                    throw new InvalidOperationException("Wybrany lek nie istnieje w bazie danych.");

                // Dodanie nowego wpisu do tabeli ReceptyLeki
                dentCareEntities.ReceptyLeki.Add(item);
                dentCareEntities.SaveChanges();

                Console.WriteLine("Lek został pomyślnie przypisany do recepty.");
            }
            catch (InvalidOperationException ex)
            {
                // Obsługa błędów walidacji
                Console.WriteLine($"Błąd walidacji: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Obsługa błędów przy zapisie do bazy danych
                Console.WriteLine($"Błąd podczas zapisu do bazy danych: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }



    }
}
