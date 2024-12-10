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
    public abstract class WszystkieViewModel<T> : WorkspaceViewModel // pod T będą podstawiane konkretne typy elementów listy. Raz T będzie np.pacjentem, a raz pracownikiem
    {
        #region DB

        protected readonly DentCareEntities dentCareEntities; // pole, które reprezentuje bazę danych


        #endregion

        #region Command

        private BaseCommand _LoadCommand; // to jest komenda, która będzie wywoływała funkcję Load pobierającą z bazy danych
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                    _LoadCommand = new BaseCommand(() => Load()); // jeśli komenda nie została skojarzona, to pod LoadCommand podstawiamy funkcję load
                return _LoadCommand;
            }
        }

        private BaseCommand _AddCommand; // to jest komenda, która będzie wywoływała funkcję Add wywołującą okno do dodawania i zostanie podpięta pod przycisk dodaj
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                    _AddCommand = new BaseCommand(() => add()); 
                return _AddCommand;
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
        private void add()
        {
            // Dzięki messengerowi wysyłamy do innych obiektów komunikat DisplayName add, gdzie DisplayName jest nazwą widoku 
            // Ten komunikat odbierze MainWindowViewModel, który odpowiada za otwieranie zakładek (okien)
            Messenger.Default.Send(DisplayName + "Add"); //messenger z biblioteki MVVMLight
        }
        #endregion

    }
}
