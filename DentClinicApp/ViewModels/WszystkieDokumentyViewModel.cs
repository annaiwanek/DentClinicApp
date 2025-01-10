using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{ // Klasa, która dostarcza danych do widoku wyświetlającego wszystkie dokumenty
    public class WszystkieDokumentyViewModel : WszystkieViewModel<DokumentForAllView> // tym razem nie wyświetlamy Dokumenty tylko DokumentForAllView
    {
        
       #region Constructor

        public WszystkieDokumentyViewModel()
            : base("Dokumenty")
        {
            // Rejestracja wiadomości do odświeżania danych
            Messenger.Default.Register<string>(this, message =>
            {
                if (message == "RefreshDocuments")
                    Load();
            });
        }

        #endregion

 

        #region Helpers

        // metoda load pobiera wszystkich pacjentów z bazy danych 

        public override void Load()
        {
            List = new ObservableCollection<DokumentForAllView>
                (
                    //zapytanie Linq
                    from dokumenty in dentCareEntities.Dokumenty // dla każdego dokumentu z bazy danych 
                    select new DokumentForAllView // tworzymy nowy DokumentForAllView i uzupełniamy jego dane
                    {
                        IdDokumentu = dokumenty.IdDokumentu,
                        PESEL = dokumenty.Pacjenci.PESEL,
                        Imie = dokumenty.Pacjenci.Imie,
                        Nazwisko = dokumenty.Pacjenci.Nazwisko,
                        NazwaDokumentu = dokumenty.NazwaDokumentu,
                        TypDokumentu = dokumenty.TypDokumentu,
                        Opis = dokumenty.Opis,
                        DataDodania = dokumenty.DataDodania

                    }
                );
        }
        #endregion
    }
}


    

