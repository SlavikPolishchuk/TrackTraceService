using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTraceService
{
    internal class FileBody
    {
        public FileBody() { }
        public FileBody(string _stringFileBody)
        {
            ParseFileBody(_stringFileBody);
        }

        public string Id { get; set; }
        public string RegDate { get; set; }
        private string TareId { get; set; }
        private string CountryTo { get; set; }
        public string IndexTo { get; set; }
        public string AddressTo { get; set; }
        public string RecipientName { get; set; }
        private string CountryFrom { get; set; }
        private string IndexFrom { get; set; }
        public string MailType { get; set; }
        private string MailCtg { get; set; }
        private string SendCtg { get; set; }
        private string MailRank { get; set; }
        private string TransType { get; set; }
        private string PostMark { get; set; }
        public string Weigth { get; set; }
        private string Value { get; set; }
        private string Payment { get; set; }
        private string CustomsDuty { get; set; }
        private string CustomsVAT { get; set; }
        private string ContainerID { get; set; }
        private string InvoiceID { get; set; }
        public string AddresseePhoneNum { get; set; }
        private string TareType { get; set; }
        public string SenderName { get; set; }
        private string ReturnPrice { get; set; }
        private string OriginalID { get; set; }

        private void ParseFileBody(string _fileBodyString)
        {
            int l = _fileBodyString.Length;
            Id = _fileBodyString.Substring(0, 13).Trim();
            RegDate = _fileBodyString.Substring(13, 14).Trim().Substring(0, 8);
            TareId = _fileBodyString.Substring(27, 5).Trim();
            CountryTo = _fileBodyString.Substring(32, 3).Trim();
            IndexTo = _fileBodyString.Substring(35, 5).Trim();
            AddressTo = _fileBodyString.Substring(40, 255).Trim();
            RecipientName = _fileBodyString.Substring(295, 255).Trim();
            CountryFrom = _fileBodyString.Substring(550, 3).Trim();
            IndexFrom = _fileBodyString.Substring(553, 5).Trim();
            MailType = _fileBodyString.Substring(558, 10).Trim();
            MailCtg = _fileBodyString.Substring(568, 10).Trim();
            SendCtg = _fileBodyString.Substring(578, 10).Trim();
            MailRank = _fileBodyString.Substring(588, 10).Trim();
            TransType = _fileBodyString.Substring(598, 10).Trim();
            PostMark = _fileBodyString.Substring(608, 10).Trim();
            Weigth = _fileBodyString.Substring(618, 6).Trim();
            Value = _fileBodyString.Substring(624, 10).Trim();
            Payment = _fileBodyString.Substring(634, 10).Trim();
            CustomsDuty = _fileBodyString.Substring(644, 10).Trim();
            CustomsVAT = _fileBodyString.Substring(654, 10).Trim();
            ContainerID = _fileBodyString.Substring(664, 13).Trim();
            InvoiceID = _fileBodyString.Substring(677, 13).Trim();
            AddresseePhoneNum = _fileBodyString.Substring(690, 15).Trim();
            TareType = _fileBodyString.Substring(705, 10).Trim();
            SenderName = _fileBodyString.Substring(715, 255).Trim();
            ReturnPrice = _fileBodyString.Substring(970, 10).Trim();
            OriginalID = _fileBodyString.Substring(980, 13).Trim();
        }

        public string InfoFileBody()
        {
            return
                string.Format($"" +
        " Id: {0}, \n" +
        " RegDate: {1},\n" +
        " TareId: {2}\n" +
        " CountryTo: {3},\n" +
        " IndexTo: {4}, \n" +
        " AddressTo: {5},\n" +
        " RecipientName: {6},\n" +
        " CountryFrom: {7}, \n" +
        " IndexFrom: {8}, \n" +
        " MailType: {9}, \n" +
        " MailCtg: {10}, \n" +
        " SendCtg: {11}, \n" +
        " MailRank: {12}, \n" +
        " TransType: {13}, \n" +
        " PostMark: {14}, \n" +
        " Weigth: {15}, \n" +
        " Value: {16}, \n" +
        " Payment: {17},\n" +
        " CustomsDuty: {18},\n" +
        " CustomsVAT: {19}, \n" +
        " ContainerID: {20}, \n" +
        " InvoiceID: {21}, \n" +
        " AddresseePhoneNum: {22},\n" +
        " TareType: {23}, \n" +
        " SenderName: {24}, \n" +
        " ReturnPrice: {25}, \n" +
        " OriginalID: {26}",
            Id,
        RegDate,
        TareId,
        CountryTo,
        IndexTo,
        AddressTo,
        RecipientName,
        CountryFrom,
        IndexFrom,
        MailType,
        MailCtg,
        SendCtg,
        MailRank,
        TransType,
        PostMark,
        Weigth,
        Value,
        Payment,
        CustomsDuty,
        CustomsVAT,
        ContainerID,
        InvoiceID,
        AddresseePhoneNum,
        TareType,
        SenderName,
        ReturnPrice,
        OriginalID);
        }
    }
}
}
