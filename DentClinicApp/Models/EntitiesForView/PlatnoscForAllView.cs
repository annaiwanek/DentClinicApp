using DentClinicApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Models.EntitiesForView
{
    public class PlatnoscForAllView
    {
        public int IdPlatnosci { get; set; }

        public Pacjenci Pacjent { get; set; }
        public string Nazwisko { get; set; }
        public DateTime? DataPlatnosci { get; set; }
        public DateTime DataWizyty { get; set; }
        public decimal? Kwota { get; set; }
        public int NumerWizyty { get; set; }
        public string MetodaPlatnosci { get; set; }
        public bool? Status { get; set; }
        public Pracownicy Pracownik { get; set; }
        public UzytkownicySystemu UzytkownikSystemu { get; set; }

        // Właściwość do wyświetlania daty płatności
        // wyświetlanie daty płatności po zatwierdzeniu płatności 
        public string WidocznaDataPlatnosci
        {
            get
            {
                return Status.GetValueOrDefault() && DataPlatnosci.HasValue
                    ? DataPlatnosci.Value.ToString("yyyy-MM-dd")
                    : string.Empty;
            }
        }


    }
}
