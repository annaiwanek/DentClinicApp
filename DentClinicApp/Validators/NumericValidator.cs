using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Validators
{
    public static class NumericValidator
    {
        public static string SprawdzCzyJestLiczba(string wartosc)
        {
            if (!decimal.TryParse(wartosc, out _))
            {
                return "Wartość musi być liczbą.";
            }
            return null;
        }
    }
}
