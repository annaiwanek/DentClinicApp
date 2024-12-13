using DentClinicApp.Helper;
using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class RaportLogiViewModel : WorkspaceViewModel
    {
        
        
        #region DB

        DentCareEntities db;

        #endregion

        #region Constructor 

        public RaportLogiViewModel()
        {
            base.DisplayName = "Raport logi aktywności";
            _DataOd = DateTime.Now;
            _DataDo = DateTime.Now;

            db = new DentCareEntities();
        

        }
        #endregion
        #region Fields 
        // dla każdego pola istotnego dla obliczeń tworzymy pole i właściwość

        private DateTime _DataOd; //_ oznacza, że będzie properties

        public DateTime DataOd
        {
            get { return _DataOd; }

            set
            {
                if (_DataOd != value)
                {
                    _DataOd = value;
                    OnPropertyChanged(() => DataOd);
                }
            }
        }

        private DateTime _DataDo; //_ oznacza, że będzie properties

        public DateTime DataDo
        {
            get { return _DataDo; }

            set
            {
                if (_DataDo != value)
                {
                    _DataDo = value;
                    OnPropertyChanged(() => DataDo);
                }
            }
        }

        private int _IdUzytkownika; //_ oznacza, że będzie properties

        public int IdUzytkownika
        {
            get { return _IdUzytkownika; }

            set
            {
                if (_IdUzytkownika != value)
                {
                    _IdUzytkownika = value;
                    OnPropertyChanged(() => IdUzytkownika);
                }
            }
        }

        private IEnumerable<LogForAllView> _Logi; //_ oznacza, że będzie properties

        public IEnumerable<LogForAllView> Logi
        {
            get { return _Logi; }

            set
            {
                if (_Logi != value)
                {
                    _Logi = value;
                    OnPropertyChanged(() => Logi);
                }
            }
        }

        public IQueryable<KeyAndValue> UzytkownicyItems
        {
            get // pobieranie wszystkich użytkowników do comboboxa poprzez key and value
            {
                return new UzytkownikB(db).GetUzytkownicyKeyAndValueItems();
            }
        }

        #endregion

        #region Commands
        // Komenda dla przycisku wygeneruj
        private BaseCommand _WygenerujCommand;

        public ICommand WygenerujCommand
        {
            get
            {
                if (_WygenerujCommand == null)
                    _WygenerujCommand = new BaseCommand(() => WygenerujLogiClick());
                return _WygenerujCommand;
            }
        }

        private void WygenerujLogiClick()
        {
            Logi = new LogiB(db).GetLogiForUserInPeriod(IdUzytkownika, _DataOd, _DataDo);
        }

        #endregion
    }
}
