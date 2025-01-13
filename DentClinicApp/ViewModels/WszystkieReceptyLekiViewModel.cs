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

        #region Sort And Find 
        // tu decydujemy po czym sortować do combobox
        public override List<string> GetComboboxSortList()
        {
            return new List<string> {"data wystawienia", "nazwa leku", "ilość"};

        }

        // tu decydujemy jak sortować
        public override void Sort()
        {
            if (SortField == "data wystawienia")
            {
                List = new ObservableCollection<ReceptaLekForAllView>(
                    List.Where(item => item.DataWystawienia != null) // Sprawdzenie, czy Data nie jest null
                        .OrderBy(item => item.DataWystawienia)
                );
            }

         
            if (SortField == "nazwa leku")
                List = new ObservableCollection<ReceptaLekForAllView>(List.OrderBy(item => item.Nazwa));

            if (SortField == "ilość")
                List = new ObservableCollection<ReceptaLekForAllView>(List.OrderBy(item => item.Ilosc));

        }

        // tu decydujemy po czym wyszukiwać do combobox 
        public override List<string> GetComboboxFindList()
        {
            return new List<string> { "data wystawienia", "PESEL", "nazwa leku", "ilość","kod recepty", "dawka", "postać" };

        }

        // tu decydujemy jak wyszukiwać 
        public override void Find()
        {
            if (FindField == "data wystawienia")
            {
                List = new ObservableCollection<ReceptaLekForAllView>(
                    List.Where(item => item.DataWystawienia.ToString("dd-MM-yyyy").Contains(FindTextBox))
                );
            }


            if (FindField == "PESEL")
            {
                List = new ObservableCollection<ReceptaLekForAllView>(
                    List.Where(item => item.Pacjent?.PESEL != null &&
                                       item.Pacjent.PESEL.Contains(FindTextBox))
                );
            }

            if (FindField == "nazwa leku")
            {
                List = new ObservableCollection<ReceptaLekForAllView>(
                    List.Where(item => item.Nazwa != null &&
                                       item.Nazwa.Contains(FindTextBox))
                );
            }

            if (FindField == "ilość")
            {
                if (int.TryParse(FindTextBox, out int ilosc))
                {
                    List = new ObservableCollection<ReceptaLekForAllView>(
                        List.Where(item => item.Ilosc == ilosc)
                    );
                }
            }

            if (FindField == "kod recepty")
            {
                List = new ObservableCollection<ReceptaLekForAllView>(
                    List.Where(item => item.Recepta?.IdRecepty.ToString().Contains(FindTextBox) == true)
                );
            }

            if (FindField == "dawka")
            {
                List = new ObservableCollection<ReceptaLekForAllView>(
                    List.Where(item => item.Lek?.Dawka != null &&
                                       item.Lek.Dawka.Contains(FindTextBox))
                );
            }

            if (FindField == "postać")
            {
                List = new ObservableCollection<ReceptaLekForAllView>(
                    List.Where(item => item.Lek?.Postac != null &&
                                       item.Lek.Postac.Contains(FindTextBox))
                );
            }

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
                        Ilosc = lek.Ilosc,
                        Nazwa = lek.Leki.Nazwa,
                        DataWystawienia = recepta.DataWystawienia



                    }
                );
        }
        #endregion
    }
}
