using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{
    public class LogForAllView
    {
        public int IdLog { get; set; }
        public string LoginUzytkownika  { get; set; }
        public string Akcja  { get; set; }
        public DateTime Data { get; set; }
        public string Opis { get; set; }


    }
}
