using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTraceService
{
    internal class Seat
    {
        public string TaraNum { get; set; }
        public int length { get; set; }
        public string barcode { get; set; }
        public string weight { get; set; }

        public Seat()
        {
            TaraNum = string.Empty;
            barcode = string.Empty;
            weight = "1";
            length = 1;
        }

        public Seat(string _barcode, string _weight)
        {
            TaraNum = string.Empty;
            barcode = _barcode;
            weight = (string.IsNullOrEmpty(_weight)) ? "1" : _weight;
            length = 1;
        }
    }
}
