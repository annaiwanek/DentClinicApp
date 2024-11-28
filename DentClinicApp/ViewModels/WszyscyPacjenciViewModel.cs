using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{// To jest klasa, która dostarcza danych do widoku wyświetlającego wszystkich pacjentów
    public class WszyscyPacjenciViewModel : WorkspaceViewModel
    {
        #region DB

        private readonly DentCareEntities dentCareEntities; // pole, które reprezentuje bazę danych
       
        
        #endregion

        #region LoadCommand

        private BaseCommand _LoadComand; // to jest komenda, która będzie wywoływała funkcję Load pobierającą z bazy danych
        public ICommand LoadComand
        {
            get 
            {
                if (_LoadComand == null)
                    _LoadComand = new BaseCommand(() => load()); // jeśli komenda nie została skojarzona, to pod LoadCommand podstawiamy funkcję load
                return _LoadComand;
            }
        }
        #endregion

        //properties - zbiór dwóch metod do pobierania i ustawiania pola 
        #region List
        //tu będą przechowywani pacjenci pobrani z BD (w liście)
        private ObservableCollection<Pacjenci> _List; //_ - oznacze, że to jest pole, do którego zostanie zbudowany properties
        public ObservableCollection<Pacjenci> List // powyżej List ma podkreślnik, więc musimy stworzyć propertis ObservableCollection
        {
            get
            {if (_List == null)
                    load();
            return _List;

            }

            set
            {
                _List = value;
                OnPropertyChanged(() => List); // to jest zlecenie odświeżenia listy na interfejsie 

            }
        }


        #endregion
        #region Constructor
        public WszyscyPacjenciViewModel()
        {
            base.DisplayName = "Pacjenci";
            dentCareEntities = new DentCareEntities();
        }
        #endregion

        #region Helpers

        // metoda load pobiera wszystkich pacjentów z bazy danych 

        private void load()
        {
            List = new ObservableCollection<Pacjenci>
                (
                dentCareEntities.Pacjenci.ToList() // z bazy danych pobieram tabelę Pacjenci i wszystkie rekordy zamieniam na listę, która jest ObservableCollection
                );
        }
        #endregion
    }
}
