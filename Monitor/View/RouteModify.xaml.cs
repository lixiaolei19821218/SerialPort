using Monitor.Model;
using Monitor.ViewModel;
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
using System.Windows.Shapes;

namespace Monitor.View
{
    /// <summary>
    /// RouteModify.xaml 的交互逻辑
    /// </summary>
    public partial class RouteModify : Window
    {
        public RouteModify(ObservableCollection<Route> routes, List<Order> orders)
        {            
            InitializeComponent();
            DataContext = new RouteModifyVM(routes, orders);
        }
    }
}
