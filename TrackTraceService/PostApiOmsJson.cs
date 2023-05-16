using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackTraceService;

namespace Track_Trace_JsonApi
{
    internal class PostApiOmsJsonOrders
    {
        public int SendCtg { get; set; }
        public string ReturnPrice { get; set; }
        public string recip_contact_tel { get; set; }
        public int TransType { get; set; }
        public int MailCtg { get; set; }
        public string Payment { get; set; }
        public string send_address { get; set; }
        public int send_post_code { get; set; }
        public string ReturnType { get; set; }
        public string ParentID { get; set; }
        public int MailRank { get; set; }
        public string PostPaymentSendingSum { get; set; }
        public string send_name { get; set; }
        public string extr_guid_order { get; set; }
        public string recip_name { get; set; }
        public string InitialID { get; set; }
        public int CountryTo { get; set; }
        public string send_contact_tel { get; set; }
        public string Value { get; set; }
        public string ext_doc_numb { get; set; }
        public string ext_doc_date { get; set; }//string
        public string exec_post_code { get; set; }
        public string  CustomsDuty { get; set; }
        public string SendingPayerCode { get; set; }
        public string status_date { get; set; } //string
        public List<Seat> seats { get; set; }
        public string status_code { get; set; }
        public string recip_address { get; set; }
        public string SendingPaymentSum { get; set; }
        public string CustomsVAT { get; set; }
        public string recip_post_code { get; set; }
        public string  PostMark { get; set; }
        public string mail_type { get; set; }
        public string CountryFrom { get; set; }
    }
}
