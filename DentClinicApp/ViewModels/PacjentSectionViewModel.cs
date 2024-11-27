using DentClinicApp.Helper;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class PacjentSectionViewModel : BaseViewModel
    {
        // Komenda do wyświetlania wszystkich pacjentów
        public ICommand ShowPacjenciCommand => new BaseCommand(() => ShowAllPacjenci());

        // Komenda do dodawania nowego pacjenta
        public ICommand AddPacjentCommand => new BaseCommand(() => AddPacjent());

        // Logika wyświetlania wszystkich pacjentów
        private void ShowAllPacjenci()
        {
            // TODO: Logika otwierania widoku "Wszyscy Pacjenci"
        }

        // Logika dodawania nowego pacjenta
        private void AddPacjent()
        {
            // TODO: Logika otwierania widoku "Dodaj Pacjenta"
        }

        public PacjentSectionViewModel()
        {
            // Opcjonalnie: Wykonaj jakąś logikę przy inicjalizacji
        }
    }
}
