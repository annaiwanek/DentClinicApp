using DentClinicApp.Models.Entities;
using DentClinicApp.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{ // Klasa, która dostarcza danych do widoku wyświetlającego wszystkie receptyLeki
    public class WszystkieReceptyLekiViewModel : WszystkieViewModel<ReceptaLekForAllView>
    {
        #region Constructor

        public WszystkieReceptyLekiViewModel()
            : base("ReceptyLeki")
        {
        }

        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ReceptaLekForAllView>
                (
                    //zapytanie Linq
                    from recepta in dentCareEntities.Recepty
                    from lek in recepta.ReceptyLeki
                    select new ReceptaLekForAllView
                    {
                        Pacjent = recepta.Pacjenci,
                        Recepta = recepta,
                        Lek = lek.Leki,
                        ReceptaLek = lek,
                        Ilosc = lek.Ilosc



                    }
                );
        }
        #endregion
    }
}
