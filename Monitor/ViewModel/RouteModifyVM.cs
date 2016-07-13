using Monitor.Helper;
using Monitor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.ViewModel
{
    class RouteModifyVM : ViewModelBase
    {
        public ObservableCollection<Route> Routes { get; set; }
        private List<Order> orders { get; set; }

        public DelegateCommand UpCommand { get; set; }
        public DelegateCommand DownCommand { get; set; }
        public DelegateCommand Confirm { get; set; }

        public RouteModifyVM(ObservableCollection<Route> routes, List<Order> orders)
        {
            this.Routes = routes == null ? new ObservableCollection<Route>() : routes;
            this.orders = orders;
            UpCommand = new DelegateCommand(Up, CanUp);
            DownCommand = new DelegateCommand(Down, CanDown);
        }

        private void Up(object o)
        {
            Route r = o as Route;
            int i = Routes.IndexOf(r);
            if (i > 0)
            {
                Route t = Routes[i - 1];
                Routes[i - 1] = r;
                Routes[i] = t;
            }
            orders.Clear();
            foreach (Route route in Routes)
            {
                orders.AddRange(route.Orders);
            }
        }

        private bool CanUp(object o)
        {
            Route r = o as Route;
            int i = Routes.IndexOf(r);
            return i != 0;
        }

        private void Down(object o)
        {
            Route r = o as Route;
            int i = Routes.IndexOf(r);
            if (i < Routes.Count - 1)
            {
                Route t = Routes[i + 1];
                Routes[i + 1] = r;
                Routes[i] = t;
            }
            orders.Clear();
            foreach (Route route in Routes)
            {
                orders.AddRange(route.Orders);
            }
        }

        private bool CanDown(object o)
        {
            Route r = o as Route;
            int i = Routes.IndexOf(r);
            return i != Routes.Count - 1;
        }
    }
}
