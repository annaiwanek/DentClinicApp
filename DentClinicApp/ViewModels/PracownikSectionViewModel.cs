using DentClinicApp.Helper;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class PracownikSectionViewModel : BaseViewModel
    {
        // Komenda do wyświetlania wszystkich pracowników
        public ICommand ShowPracownicyCommand => new BaseCommand(() => ShowAllPracownicy());

        // Komenda do dodawania nowego pracownika
        public ICommand AddPracownikCommand => new BaseCommand(() => AddPracownik());

        // Logika wyświetlania wszystkich pracowników
        private void ShowAllPracownicy()
        {
            // TODO: Logika otwierania widoku "Wszyscy Pracownicy"
        }

        // Logika dodawania nowego pracownika
        private void AddPracownik()
        {
            // TODO: Logika otwierania widoku "Dodaj Pracownika"
        }

        public PracownikSectionViewModel()
        {
            // Opcjonalnie: Wykonaj jakąś logikę przy inicjalizacji
        }
    }
}
