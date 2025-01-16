using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentClinicApp.Validators
{
    // klasa do walidacji stringów
    public static class StringValidator 
    {
        // Walidacja: czy string zaczyna się od dużej litery
        public static string SprawdzCzyZaczynaSieOdDuzej(string wartosc)
        {
            try
            {
                if (string.IsNullOrEmpty(wartosc))
                {
                    return "Wartość nie może być pusta.";
                }

                if (!char.IsUpper(wartosc[0]))
                {
                    return "Rozpocznij dużą literą.";
                }
            }
            catch (Exception ex)
            {
                return $"Błąd podczas walidacji: {ex.Message}";
            }

            return null;
        }

        // Walidacja: poprawność formatu email
        public static string SprawdzEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return "Email nie może być pusty.";
                }

                if (!email.Contains("@") || !email.Contains("."))
                {
                    return "Nieprawidłowy format email.";
                }
            }
            catch (Exception ex)
            {
                return $"Błąd podczas walidacji: {ex.Message}";
            }

            return null;
        }

        // Walidacja: poprawność formatu adresu
        public static string SprawdzAdres(string adres)
        {
            try
            {
                if (string.IsNullOrEmpty(adres))
                {
                    return "Adres nie może być pusty.";
                }

                if (!adres.Contains(","))
                {
                    return "Adres musi zawierać miasto, ulicę i numer (oddzielone przecinkiem).";
                }
            }
            catch (Exception ex)
            {
                return $"Błąd podczas walidacji: {ex.Message}";
            }

            return null;
        }
    }
}

