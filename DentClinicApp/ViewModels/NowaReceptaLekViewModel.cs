using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class NowaReceptaLekViewModel : JedenViewModel<ReceptaLekForAllView>
    {
        public NowaReceptaLekViewModel()
            : base("Nowa Recepta - Lek")
        {
            item = new ReceptaLekForAllView();
            DataDodania = DateTime.Now;

            // Rejestracja Messenger dla wyboru recepty i leku
            Messenger.Default.Register<Recepty>(this, getWybranaRecepta);
            Messenger.Default.Register<Leki>(this, getWybranyLek);
        }

        #region Properties

        private int _wybranaReceptaId;
        public int WybranaReceptaId
        {
            get => _wybranaReceptaId;
            set
            {
                _wybranaReceptaId = value;
                OnPropertyChanged(() => WybranaReceptaId);
            }
        }

        private string _peselPacjenta;
        public string PeselPacjenta
        {
            get => _peselPacjenta;
            set
            {
                _peselPacjenta = value;
                OnPropertyChanged(() => PeselPacjenta);
            }
        }

        private string _wybranyLekNazwa;
        public string WybranyLekNazwa
        {
            get => _wybranyLekNazwa;
            set
            {
                _wybranyLekNazwa = value;
                OnPropertyChanged(() => WybranyLekNazwa);
            }
        }

        private string _dawkaPostac;
        public string DawkaPostac
        {
            get => _dawkaPostac;
            set
            {
                _dawkaPostac = value;
                OnPropertyChanged(() => DawkaPostac);
            }
        }

        //private Leki _wybranyLek;
        //public Leki WybranyLek
        //{
        //    get => _wybranyLek;
        //    set
        //    {
        //        _wybranyLek = value;
        //        OnPropertyChanged(() => WybranyLek);
        //    }
        //}

       

        private Leki _wybranyLek;
        public Leki WybranyLek
        {
            get => _wybranyLek;
            set
            {
                _wybranyLek = value;
                OnPropertyChanged(() => WybranyLek);
                OnPropertyChanged(() => WybranyLekNazwa); // Odświeżenie nazwy w interfejsie
            }
        }

        //private int _wybranaReceptaId;
        //public int WybranaReceptaId
        //{
        //    get => _wybranaReceptaId;
        //    set
        //    {
        //        _wybranaReceptaId = value;
        //        OnPropertyChanged(() => WybranaReceptaId);
        //    }
        //}


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
            get => item.DataWystawienia;
            set
            {
                item.DataWystawienia = value;
                OnPropertyChanged(() => DataDodania);
            }
        }

        #endregion

        #region Commands

        public ICommand ShowReceptyWindowCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("ReceptyWindowAll");
        });

        public ICommand ShowLekiWindowCommand => new BaseCommand(() =>
        {
            Messenger.Default.Send("LekiWindowAll");
        });

        #endregion

        #region Messenger Handlers

        private void getWybranaRecepta(Recepty recepta)
        {
            if (recepta != null)
            {
                WybranaReceptaId = recepta.IdRecepty;
                System.Diagnostics.Debug.WriteLine($"Wybrana recepta: {WybranaReceptaId}"); // Debug
                OnPropertyChanged(() => WybranaReceptaId);
            }
        }




        //private void getWybranyLek(Leki lek)
        //{
        //    if (lek != null)
        //    {
        //        WybranyLek = lek;
        //        OnPropertyChanged(() => WybranyLek);
        //    }
        //}
        private void getWybranyLek(Leki lek)
        {
            if (lek != null)
            {
                WybranyLek = lek; // Przypisanie obiektu leku do właściwości
                WybranyLekNazwa = lek.Nazwa; // Przypisanie nazwy leku do pola tekstowego
                OnPropertyChanged(() => WybranyLek);
                OnPropertyChanged(() => WybranyLekNazwa);
            }
        }



        #endregion

        #region Helpers

        public override void Save()
        {
            if (WybranaReceptaId == 0)
                throw new InvalidOperationException("Nie wybrano recepty.");
            if (WybranyLek == null || WybranyLek.IdLeku == 0)
                throw new InvalidOperationException("Nie wybrano leku.");
            if (Ilosc <= 0)
                throw new InvalidOperationException("Ilość musi być większa od zera.");

            var receptaLek = new ReceptyLeki
            {
                IdRecepty = WybranaReceptaId,
                IdLeku = WybranyLek.IdLeku,  // Poprawne odwołanie
                Ilosc = Ilosc,
                DataDodania = DateTime.Now   // Poprawione zgodnie z bazą danych
            };

            dentCareEntities.ReceptyLeki.Add(receptaLek);
            dentCareEntities.SaveChanges();
        }


        #endregion
    }
}
