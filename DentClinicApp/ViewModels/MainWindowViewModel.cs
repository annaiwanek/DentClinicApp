using DentClinicApp.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace DentClinicApp.ViewModels
{ 
    // Zarządzanie interakcjami użytkownika - otwieranie i zamykanie zakładek, obsługa komend
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private ReadOnlyCollection<CommandViewModel> _Commands;
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        #endregion

        #region Commands
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }
        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    "Pacjenci",
                    new BaseCommand(() => this.ShowWorkspace<WszyscyPacjenciViewModel>())),

                new CommandViewModel(
                    "Pacjent",
                    new BaseCommand(() => this.CreateView(new NowyPacjentViewModel()))),

                 new CommandViewModel(
                    "Leki",
                    new BaseCommand(() => this.ShowWorkspace<WszystkieLekiViewModel>())),

                new CommandViewModel(
                    "Lek",
                    new BaseCommand(() => this.CreateView(new NowyLekViewModel()))),

                 new CommandViewModel(
                    "Stanowiska",
                    new BaseCommand(() => this.ShowWorkspace<WszystkieStanowiskaViewModel>())),

                new CommandViewModel(
                    "Stanowisko",
                    new BaseCommand(() => this.CreateView(new NoweStanowiskoViewModel()))),

                   new CommandViewModel(
                    "Uslugi",
                    new BaseCommand(() => this.ShowWorkspace<WszystkieUslugiViewModel>())),

                new CommandViewModel(
                    "Usluga",
                    new BaseCommand(() => this.CreateView(new NowaUslugaViewModel()))),

                  new CommandViewModel(
                    "Dokumenty",
                    new BaseCommand(() => this.ShowWorkspace<WszystkieDokumentyViewModel>())),

                new CommandViewModel(
                    "Dokument",
                    new BaseCommand(() => this.CreateView(new NowyDokumentViewModel()))),

                 new CommandViewModel(
                    "Grafiki",
                    new BaseCommand(() => this.ShowWorkspace<WszystkieGrafikiViewModel>())),

                new CommandViewModel(
                    "Grafik",
                    new BaseCommand(() => this.CreateView(new NowyGrafikViewModel()))),

                   new CommandViewModel(
                    "Historie leczenia",
                    new BaseCommand(() => this.ShowWorkspace<WszystkieHistorieLeczeniaViewModel>())),

                new CommandViewModel(
                    "Historia leczenia",
                    new BaseCommand(() => this.CreateView(new NowaHistoriaLeczeniaViewModel()))),

                new CommandViewModel(
                    "Logi aktywności",
                    new BaseCommand(() => this.ShowWorkspace<WszystkieLogiViewModel>())),

                new CommandViewModel(
                    "Log aktywności",
                    new BaseCommand(() => this.CreateView(new NowyLogViewModel()))),

                 new CommandViewModel(
                    "Notatki",
                    new BaseCommand(() => this.ShowWorkspace<WszystkieNotatkiViewModel>())),

                new CommandViewModel(
                    "Notatka",
                    new BaseCommand(() => this.CreateView(new NowaNotatkaViewModel()))),

                new CommandViewModel(
                    "Płatnosci",
                    new BaseCommand(() => this.ShowWorkspace<WszystkiePlatnosciViewModel>())),

                new CommandViewModel(
                    "Płatnosc",
                    new BaseCommand(() => this.CreateView(new NowaPlatnoscViewModel())))


            };
        }
        #endregion

        #region Workspaces
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers
        private void CreateView(WorkspaceViewModel nowy)
        {
            this.Workspaces.Add(nowy);
            this.SetActiveWorkspace(nowy);
        }
        private void ShowWorkspace<T>() where T : WorkspaceViewModel,  new()
        {
            T workspace = this.Workspaces.OfType<T>().FirstOrDefault();
            if (workspace == null)
            {
                workspace = new T();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }
        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        #endregion
    }
}
