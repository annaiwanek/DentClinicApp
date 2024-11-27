using DentClinicApp.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        // Komendy do obsługi przycisków w widoku
        public ICommand ShowPacjenciCommand => new BaseCommand(() => ShowWorkspace<WszyscyPacjenciViewModel>());
        public ICommand ShowPracownicyCommand => new BaseCommand(() => ShowWorkspace<WszyscyPracownicyViewModel>());

        // Aktualny aktywny workspace (widok w centralnej części aplikacji)
        private WorkspaceViewModel _activeWorkspace;
        public WorkspaceViewModel ActiveWorkspace
        {
            get => _activeWorkspace;
            set
            {
                if (_activeWorkspace != value)
                {
                    _activeWorkspace = value;
                    OnPropertyChanged(() => ActiveWorkspace);
                }
            }
        }

        // Kolekcja wszystkich otwartych widoków
        public ObservableCollection<WorkspaceViewModel> Workspaces { get; }

        // Konstruktor
        public MainWindowViewModel()
        {
            Workspaces = new ObservableCollection<WorkspaceViewModel>();
        }

        // Generyczna metoda do wyświetlania widoku
        private void ShowWorkspace<T>() where T : WorkspaceViewModel, new()
        {
            // Sprawdzenie, czy workspace już istnieje
            var workspace = Workspaces.OfType<T>().FirstOrDefault();
            if (workspace == null)
            {
                // Jeśli nie istnieje, utwórz nowy
                workspace = new T();
                Workspaces.Add(workspace);
            }
            // Ustaw jako aktywny widok
            SetActiveWorkspace(workspace);
        }

        // Ustawienie aktywnego workspace
        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            ActiveWorkspace = workspace;
        }
    }
}
