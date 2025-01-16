using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Validators
{
    public static class LekValidator
    {
        private static readonly HashSet<string> DozwolonePostaci = new HashSet<string>
        {
            "tabletki",
            "syrop",
            "kapsułki"
        };

        public static string SprawdzPostac(string wartosc)
        {
            if (string.IsNullOrEmpty(wartosc) || !DozwolonePostaci.Contains(wartosc.ToLower()))
            {
                return $"Postać musi być jedną z następujących: {string.Join(", ", DozwolonePostaci)}.";
            }
            return null;
        }


        public static string SprawdzCzyPoprawnaDawka(string dawka)
    {
        if (string.IsNullOrEmpty(dawka))
        {
            return "Dawka jest wymagana.";
        }

        // Wyrażenie regularne sprawdzające format: liczba + spacja + jednostka (mg, ml, g, etc.)
        var regex = new System.Text.RegularExpressions.Regex(@"^\d+\s?(mg|ml|g|mcg)$");
        if (!regex.IsMatch(dawka))
        {
            return "Dawka musi być liczbą z jednostką (np. 500 mg).";
        }

        return null;
    }
    }
}
