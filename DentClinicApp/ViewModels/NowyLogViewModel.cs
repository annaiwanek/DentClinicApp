using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class NowyLogViewModel : WorkspaceViewModel // wszystkie zakładki dziedziczą po WVM
    {
        public NowyLogViewModel() 
        {
            base.DisplayName = "Logi aktywności";
        }
    }
}
