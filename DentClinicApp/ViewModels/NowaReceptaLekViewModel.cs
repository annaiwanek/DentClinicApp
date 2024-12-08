using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class NowaReceptaLekViewModel : WorkspaceViewModel // wszystkie zakładki dziedziczą po WVM
    {
        public NowaReceptaLekViewModel()
        {
            base.DisplayName = "ReceptaLek";
        }
    }
}
