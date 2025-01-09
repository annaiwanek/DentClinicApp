using DentClinicApp.Models.BusinessLogic;
using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class NowyUzytkownikViewModel : JedenViewModel<UzytkownicySystemu>
    {
        #region Constructor
        public NowyUzytkownikViewModel()
            :base("Uzytkownik")
        {
            item = new UzytkownicySystemu();
            
        }

        #endregion

        #region Fields
        public int? IdPracownika
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

        public string Login
        {
            get
            {
                return item.Login;
            }

            set
            {
                item.Login = value;
                OnPropertyChanged(() => Login);
            }
        }

        public string Haslo
        {
            get
            {
                return item.Haslo;
            }

            set
            {
                item.Haslo = value;
                OnPropertyChanged(() => Haslo);
            }
        }

        public string Rola
        {
            get
            {
                return item.Rola ;
            }

            set
            {
                item.Rola = value;
                OnPropertyChanged(() => Rola);
            }
        }



        public IQueryable<KeyAndValue> PracownicyItems
        {
            get
            {
                return new PracownikB(dentCareEntities).GetPracownikKeyAndValueItems();
            }
        }

        #endregion

        #region Helpers

        public override void Save()
        {
            dentCareEntities.UzytkownicySystemu.Add(item);
            dentCareEntities.SaveChanges();


        }

        #endregion
    }
}
