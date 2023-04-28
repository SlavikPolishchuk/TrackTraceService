using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTraceService
{
    internal class PostApiOmsJsonOrderStatus
    {
        public string extr_guid_order { get; set; }
        public string status_code { get; set; }
        public string status_date { get; set; }
        public string reason_ext_id { get; set; }
        public string location { get; set; }
        public string exec_subdiv_id { get; set; }
        public string barcode_gu { get; set; }
    }
}
