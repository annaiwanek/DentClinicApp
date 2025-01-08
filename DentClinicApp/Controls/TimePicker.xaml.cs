using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DentClinicApp.Controls
{
    public partial class TimePicker : UserControl
    {
        public ObservableCollection<int> ListaGodzin { get; } = new ObservableCollection<int>();
        public ObservableCollection<int> ListaMinut { get; } = new ObservableCollection<int>();

        public int Godzina
        {
            get { return (int)GetValue(GodzinaProperty); }
            set { SetValue(GodzinaProperty, value); }
        }

        public static readonly DependencyProperty GodzinaProperty =
            DependencyProperty.Register("Godzina", typeof(int), typeof(TimePicker), new PropertyMetadata(0));

        public int Minuta
        {
            get { return (int)GetValue(MinutaProperty); }
            set { SetValue(MinutaProperty, value); }
        }

        public static readonly DependencyProperty MinutaProperty =
            DependencyProperty.Register("Minuta", typeof(int), typeof(TimePicker), new PropertyMetadata(0));

        public TimePicker()
        {
            InitializeComponent();

            // Inicjalizacja list godzin i minut
            for (int i = 0; i < 24; i++)
                ListaGodzin.Add(i);

            for (int i = 0; i < 60; i += 5) // Minuty co 5
                ListaMinut.Add(i);

            DataContext = this;
        }
    }
}

