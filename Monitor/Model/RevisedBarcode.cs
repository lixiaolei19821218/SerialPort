using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Model
{
    class RevisedBarcode
    {
        public BarCode Barcode { get; private set; }
        public bool Valid { get; set; }

        public RevisedBarcode(BarCode barcode)
        {
            Barcode = barcode;
        }
    }
}
