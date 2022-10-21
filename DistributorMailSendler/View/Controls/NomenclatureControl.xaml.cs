using DistributorMailSendler.Model;
using DistributorMailSendler.ViewModel;
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

namespace DistributorMailSendler.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для NomenclatureControl.xaml
    /// </summary>
    public partial class NomenclatureControl : UserControl
    {
        internal NomenclatureControl(MainViewModel mvm)
        {
            InitializeComponent();
            DataContext = mvm;
        }

    }
}
