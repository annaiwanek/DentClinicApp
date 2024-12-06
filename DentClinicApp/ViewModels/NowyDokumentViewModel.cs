using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.ViewModels
{
    public class NowyDokumentViewModel : WorkspaceViewModel // wszystkie zakładki dziedziczą po WorkspaceViewModel
    {
        public NowyDokumentViewModel() 
        {
            base.DisplayName = "Dokument";
        }
    }
}
