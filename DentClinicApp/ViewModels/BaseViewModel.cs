using DentClinicApp.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace DentClinicApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region DisplayName
        public virtual string DisplayName { get; protected set; }
        #endregion

        #region WindowProperties (Close, Minimice, Maximice, etc.)

        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public ICommand Close
        {
            get { return new BaseCommand(CloseApplication); }
        }

        public ICommand Maximice
        {
            get { return new BaseCommand(MaximiceApplication); }
        }

        public ICommand Minimice
        {
            get { return new BaseCommand(MinimiceApplication); }
        }

        public ICommand DragMove
        {
            get { return new BaseCommand(DragMoveCommand); }
        }

        public ICommand Restart
        {
            get { return new BaseCommand(RestartCommand); }
        }

        private static void RestartCommand()
        {
            Application.Current.Shutdown();
        }

        private static void DragMoveCommand()
        {
            Application.Current.MainWindow.DragMove();
        }

        private static void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private static void MaximiceApplication()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            else
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

        private static void MinimiceApplication()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.Opacity = 1;
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                Application.Current.MainWindow.Opacity = 0;
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
        }

        #endregion

        #region MessageBoxMethods

        protected void ShowMessageBoxError(string message)
        {
            MessageBox.Show(message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected void ShowMessageBoxInformation(string message)
        {
            MessageBox.Show(message, "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #region PropertyChanged

        // 1) Stara metoda: OnPropertyChanged(() => property)
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }
        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            return expression.Member.Name;
        }

        // 2) Nowsza metoda: OnPropertyChanged([CallerMemberName])
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
