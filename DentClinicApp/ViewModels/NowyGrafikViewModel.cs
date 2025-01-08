using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class NowyGrafikViewModel : JedenViewModel<GrafikPracownikow>
    {
        #region Constructor
        public NowyGrafikViewModel()
          : base("GrafikPracownikow")
        {
            item = new GrafikPracownikow();
            Data = DateTime.Now;

        }

        #endregion

        #region Fields
        //Dla każdego pola na interface dodajemy properties

        public int IdPracownika
        {
            get
            {
                return item.IdPracownika;
            }

            set
            {
                item.IdPracownika = value;
                OnPropertyChanged(() => IdPracownika);
            }
        }

        public DateTime Data
        {
            get => item.Data;
            set
            {
                item.Data = value;
                OnPropertyChanged(() => Data);
            }
        }

        public string GodzinaRozpoczeciaHours
        {
            get => GodzinaRozpoczecia.Hours.ToString();
            set
            {
                GodzinaRozpoczecia = new TimeSpan(Int32.Parse(value), GodzinaRozpoczecia.Minutes, 0);
                OnPropertyChanged(() => GodzinaRozpoczeciaHours);
            }
        }

        public string GodzinaRozpoczeciaMinutes
        {
            get => GodzinaRozpoczecia.Minutes.ToString();
            set
            {
                GodzinaRozpoczecia = new TimeSpan(GodzinaRozpoczecia.Hours, Int32.Parse(value), 0);
                OnPropertyChanged(() => GodzinaRozpoczeciaMinutes);
            }
        }

        public string GodzinaZakonczeniaHours
        {
            get => GodzinaZakonczenia.Hours.ToString();
            set
            {
                GodzinaZakonczenia = new TimeSpan(Int32.Parse(value), GodzinaZakonczenia.Minutes, 0);
                OnPropertyChanged(() => GodzinaZakonczeniaHours);
            }
        }

        public string GodzinaZakonczeniaMinutes
        {
            get => GodzinaZakonczenia.Minutes.ToString();
            set
            {
                GodzinaZakonczenia = new TimeSpan(GodzinaZakonczenia.Hours, Int32.Parse(value), 0);
                OnPropertyChanged(() => GodzinaZakonczeniaMinutes);
            }
        }

        public TimeSpan GodzinaRozpoczecia
        {
            get => item.GodzinaRozpoczęcia;
            set
            {
                item.GodzinaRozpoczęcia = value;
                OnPropertyChanged(() => GodzinaRozpoczecia);
            }
        }

        public TimeSpan GodzinaZakonczenia
        {
            get => item.GodzinaZakończenia;
            set
            {
                item.GodzinaZakończenia = value;
                OnPropertyChanged(() => GodzinaZakonczenia);
            }
        }

        #endregion

    
        #region Properties
        // Właściwości dla combobox

        public IQueryable<KeyAndValue> PracownicyItems
        {
            get // pobieranie wszystkich uslug do comboboxa poprzez key and value pobrać za pomocą klasy PracownikB
            {
                return new PracownikB(dentCareEntities).GetPracownikKeyAndValueItems();
            }
        }


        #endregion

        #region Helpers

        public override void Save()
        {
            dentCareEntities.GrafikPracownikow.Add(item);
            dentCareEntities.SaveChanges();


        }

        #endregion

    }
}
