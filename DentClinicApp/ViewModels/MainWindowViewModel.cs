using DentClinicApp.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        // Komendy do przełączania stref
        public ICommand SwitchToPacjentSectionCommand => new BaseCommand(() => SwitchToSection("Pacjent"));
        public ICommand SwitchToPracownikSectionCommand => new BaseCommand(() => SwitchToSection("Pracownik"));

        // Aktualna strefa w aplikacji
        private object _activeSection;
        public object ActiveSection
        {
            get => _activeSection;
            set
            {
                _activeSection = value;
                OnPropertyChanged(() => ActiveSection);
            }
        }

        // Konstruktor - ustaw domyślną sekcję
        public MainWindowViewModel()
        {
            SwitchToSection("Pacjent"); // Domyślnie pokaż strefę pacjenta
        }

        // Logika przełączania sekcji
        private void SwitchToSection(string section)
        {
            if (section == "Pacjent")
            {
                ActiveSection = new PacjentSectionViewModel();
            }
            else if (section == "Pracownik")
            {
                ActiveSection = new PracownikSectionViewModel();
            }
        }
    }
}
