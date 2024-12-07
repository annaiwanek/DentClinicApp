using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class NowaHistoriaLeczeniaViewModel : WorkspaceViewModel // wszystkie zakładki dziedziczą po WVM
    {
        public NowaHistoriaLeczeniaViewModel() 
        {
            base.DisplayName = "Historia leczenia";
        }
    }
}
