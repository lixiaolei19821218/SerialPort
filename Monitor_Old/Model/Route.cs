using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Model
{
    public class Route
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
