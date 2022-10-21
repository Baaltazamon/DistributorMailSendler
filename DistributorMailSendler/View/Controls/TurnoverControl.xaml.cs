using DistributorMailSendler.ViewModel;
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

namespace DistributorMailSendler.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для TurnoverControl.xaml
    /// </summary>
    public partial class TurnoverControl : UserControl
    {
        private MainViewModel _mvm;

        internal TurnoverControl(MainViewModel mvm)
        {
            InitializeComponent();
            _mvm = mvm;
            DataContext = mvm;
        }

        private void dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            _mvm.GetPositions(dg.SelectedItem);
            dgPos.ItemsSource = _mvm.Positions;
        }
    }
}
