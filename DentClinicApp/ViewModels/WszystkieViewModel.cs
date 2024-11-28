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
{
    public abstract class WszystkieViewModel<T> : WorkspaceViewModel // pod T będą podstawiane konkretne typy elementów listy. Raz T będzie np.pacjentem, a raz pracownikiem
    {
        #region DB

        protected readonly DentCareEntities dentCareEntities; // pole, które reprezentuje bazę danych


        #endregion

        #region LoadCommand

        private BaseCommand _LoadComand; // to jest komenda, która będzie wywoływała funkcję Load pobierającą z bazy danych
        public ICommand LoadComand
        {
            get
            {
                if (_LoadComand == null)
                    _LoadComand = new BaseCommand(() => Load()); // jeśli komenda nie została skojarzona, to pod LoadCommand podstawiamy funkcję load
                return _LoadComand;
            }
        }
        #endregion

        //properties - zbiór dwóch metod do pobierania i ustawiania pola 
        #region List
        //tu będą przechowywane tabele pobrane z BD (w liście)
        private ObservableCollection<T> _List; //_ - oznacze, że to jest pole, do którego zostanie zbudowany properties
        public ObservableCollection<T> List // powyżej List ma podkreślnik, więc musimy stworzyć propertis ObservableCollection
        {
            get
            {
                if (_List == null)
                    Load();
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
        public WszystkieViewModel(String displayName)
        {
            dentCareEntities = new DentCareEntities();
            base.DisplayName = displayName;
        }
        #endregion

        #region Helpers

        public abstract void Load();
        #endregion

    }
}
