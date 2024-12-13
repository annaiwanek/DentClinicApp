using DentClinicApp.Helper;
using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class RaportPrzychodowViewModel : WorkspaceViewModel
    {
        #region DB

        DentCareEntities db;

        #endregion

        #region Constructor 

        public RaportPrzychodowViewModel()
        {
            base.DisplayName = "Raport przychodów";
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
                
               if( _DataOd != value)
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

        private int _IdUslugi; //_ oznacza, że będzie properties

        public int IdUslugi

        {
            get { return _IdUslugi; }

            set
            {

                if (_IdUslugi != value)
                {
                    _IdUslugi = value;
                    OnPropertyChanged(() => IdUslugi);
                }
            }
        }

        private decimal? _Przychod; //_ oznacza, że będzie properties

        public decimal? Przychod

        {
            get { return _Przychod; }

            set
            {

                if (_Przychod != value)
                {
                    _Przychod = value;
                    OnPropertyChanged(() => Przychod);
                }
            }
        }

        // to jest properties, który uzupełni wszystkie towary w comboxie, będzie miał tylko get, bo będziemy jedynie pobierać usługi z bazy danych 
        public IQueryable<KeyAndValue> UslugiItems
        {
            get // pobieranie wszystkich uslug do comboboxa poprzez key and value pobrać za pomocą klasy UslugaB
            {
                return new UslugaB(db).GetUslugiKeyAndValueItems();
            }
        }

        #endregion

        #region Commands
        // to jest komenda, która zostanie podpięta pod przycisk oblicz i która wywoła funkcję obliczPrzychodClick
        private BaseCommand _ObliczCommand;

        public ICommand ObliczCommand
        {
            get
            {
                if (_ObliczCommand == null)
                    _ObliczCommand = new BaseCommand(() => obliczPrzychodClick());
                return _ObliczCommand;

            }
        }

        private void obliczPrzychodClick()
        {
            // to jes użycie funkcji z klasy logiki biznesowej PrzychodB, która liczy przychód dla danej usługi w danym okresie 
            Przychod = new PrzychodB(db).PrzychodOkresUsluga(IdUslugi, DataOd, DataDo);
        }

        #endregion
    }
}
