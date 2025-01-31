using DentClinicApp.ViewModels;
using System;
using System.Collections.Generic;
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

namespace DentClinicApp.Views
{
    /// <summary>
    /// Interaction logic for RaportWizytView.xaml
    /// </summary>
    public partial class RaportWizytView : UserControl
    {
        private ViewModels.RaportWizytViewModel _vm;
        public RaportWizytView()
        {
            InitializeComponent();
            this.DataContext = new RaportWizytViewModel();

        }
    }
}
