using DentClinicApp.Helper;
using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    // Klasa bazowa do zarządzania pojedynczym elementem danych typu T
    // Mechanizmy zarządzania bazą danych, obsługa komendy do zapisywania danych
    public abstract class JedenViewModel<T> : WorkspaceViewModel
    {
        #region DB

        protected DentCareEntities dentCareEntities;

        #endregion

        #region Item

        protected T item;

        #endregion

        #region Command
        // to jest komenda, która zostanie podpięta pod przycisk zapisz i zamknij i wywoła funkcję SaveAndClose 

        private BaseCommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new BaseCommand(() => SaveAndClose());
                return _SaveCommand;
            }
        }

        #endregion

        #region Constructor

        public JedenViewModel(string displayName)
        {
            base.DisplayName = displayName;
            dentCareEntities = new DentCareEntities();
    
        }

        #endregion

        #region Helpers
        public abstract void Save();
        public void SaveAndClose()
        {
            Save();
            base.OnRequestClose(); // Zamknięcie zakładki 
        }
        #endregion

    }
}
