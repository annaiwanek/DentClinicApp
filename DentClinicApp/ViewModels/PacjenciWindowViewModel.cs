using DentClinicApp.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DentClinicApp.ViewModels
{
    public class PacjenciWindowViewModel : WszystkieViewModel<Pacjenci>
    {

        #region Constructor

        public PacjenciWindowViewModel()
            : base("Pacjenci")
        {

        }

        #endregion

        #region Properties
        // do tego propertiesa zostanie przypisany pacjent kliknięty na liście 
        private Pacjenci _WybranyPacjent;
        public Pacjenci WybranyPacjent
        {
            get => _WybranyPacjent;
            set
            {
                if (_WybranyPacjent != value)
                {
                    _WybranyPacjent = value;

                    // Wysłanie wybranego pacjenta za pomocą Messengera
                    if (_WybranyPacjent != null)
                    {
                        Messenger.Default.Send(_WybranyPacjent);
                        OnRequestClose(); // Zamknięcie okna po wyborze pacjenta
                    }

                    OnPropertyChanged(() => WybranyPacjent); // Powiadomienie widoku o zmianie
                }
            }
        }
        #endregion

            #region Helpers

            // metoda load pobiera wszystkich pacjentów z bazy danych 

        public override void Load()
        {
            List = new ObservableCollection<Pacjenci>
                (
                dentCareEntities.Pacjenci.ToList() // z bazy danych pobieram tabelę Pacjenci i wszystkie rekordy zamieniam na listę, która jest ObservableCollection
                );
        }
        #endregion
    }
}
