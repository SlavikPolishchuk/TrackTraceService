using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTraceService
{
    internal class DALTrackTraceJsonApi
    {
        public string ConnectionString { get; set; }
        private string sqlText = string.Empty;

        public DALTrackTraceJsonApi()
        {
            ConnectionString =
                " DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=50-oracle-b.ukrposhta.loc)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=kyasomp_dg)));User Id=API;Password=API123";
        }

        public DALTrackTraceJsonApi(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<AsrkFile> GetListAsrkFile()
        {
            List<AsrkFile> _listAsrkFile = new List<AsrkFile>();
            string str = string.Empty;


            sqlText = @"SELECT FILE_ID, FILE_NAME, FILE_BODY, CREATE_DATE, IS_SENDED, SEND_DATE
            FROM BOU.ASRK_FILES_EXPORT
            WHERE IS_SENDED =1 AND rownum<=3";

            using (OracleConnection oracleConnection = new OracleConnection(ConnectionString))
            {
                oracleConnection.Open();

                OracleCommand command = new OracleCommand(sqlText, oracleConnection);
                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    _listAsrkFile.Add(new AsrkFile()
                    {
                        FileId = reader["FILE_ID"].ToString(),
                        FileName = reader["FILE_NAME"].ToString(),
                        fileBody = GetAllFileBodies(reader["FILE_BODY"].ToString()),
                        CreateDate = (DateTime)reader["CREATE_DATE"],
                        IsSended = (Int16)reader["IS_SENDED"],
                        SendDate = (DateTime)reader["SEND_DATE"]

                    });
                }

                oracleConnection.Close();
            }
            return _listAsrkFile;
        }

        public bool SetIsSended(AsrkFile _asrkFile)
        {
            try
            {
                sqlText = string.Format($"UPDATE BOU.ASRK_FILES_EXPORT SET IS_SENDED=2 WHERE FILE_ID={_asrkFile.FileId} AND FILE_NAME='{_asrkFile.FileName}'");

                using (OracleConnection oracleConnection = new OracleConnection(ConnectionString))
                {
                    oracleConnection.Open();

                    OracleCommand command = new OracleCommand(sqlText, oracleConnection);
                    command.ExecuteNonQuery();

                    oracleConnection.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private List<string> ParseStringFileBodyToRow(string _stringFileBody)
        {
            _stringFileBody = _stringFileBody.Replace("\n", "");
            List<string> result = new List<string>();
            int step = 0;
            string rowString = string.Empty;


            for (int i = 0; i < _stringFileBody.Length; i++)
            {
                if (step == 999)
                {
                    result.Add(CutSpaceInString(rowString));

                    rowString = string.Empty;
                    step = 0;
                    i = i - 1;

                }
                rowString = rowString + _stringFileBody[i].ToString();

                step++;
            }

            return result;
        }

        private string CutSpaceInString(string s)
        {
            string newString = string.Empty;
            string p = string.Empty;
            int spaceCount = 0;
            for (int i = 0; i < s.Length; i++)
            {
                p = s[i].ToString();
                if (p == " " || p == "\n" || p == "0 ") spaceCount++;
                else break;
            }
            newString = s.Substring(spaceCount, s.Length - spaceCount);

            for (int i = 0; i < spaceCount; i++)
            {
                newString = newString + " ";
            }

            return newString;
        }

        private List<FileBody> GetAllFileBodies(string _stringFileBody)
        {
            List<FileBody> fileBodies = new List<FileBody>();

            List<string> rows = ParseStringFileBodyToRow(_stringFileBody);

            foreach (string row in rows)
                fileBodies.Add(new FileBody(row));

            return fileBodies;
        }

        public string GetApiOmsPostOrdersJson(AsrkFile _asrkFile)
        {
            List<PostApiOmsJsonOrders> _postApiOmsJsons = new List<PostApiOmsJsonOrders>();
            List<Seat> _seat = new List<Seat>();

            foreach (FileBody body in _asrkFile.fileBody)
            {
                _seat.Clear();
                _seat.Add(new Seat(body.Id.Trim(), body.Weigth.Trim()));
                _postApiOmsJsons.Add(new PostApiOmsJsonOrders()
                {
                    recip_post_code = " ",
                    extr_guid_order = body.Id.Trim(),
                    ext_doc_numb = body.Id.Trim(),
                    ext_doc_date = Convert.ToDateTime(body.RegDate).ToShortDateString(),
                    status_date = Convert.ToDateTime(body.RegDate).ToShortDateString(),
                    send_name = (string.IsNullOrEmpty(body.SenderName.Trim())) ? " " : body.SenderName.Trim(),
                    send_contact_tel = "",
                    send_address = body.AddressTo.Trim(),
                    recip_name = (string.IsNullOrEmpty(body.RecipientName.Trim())) ? " " : body.RecipientName.Trim(),
                    recip_contact_tel = body.AddresseePhoneNum.Trim(),
                    recip_address = body.AddressTo.Trim(),
                    mail_type = body.MailType.Trim(),
                    seats = _seat
                });

            }

            return JsonConvert.SerializeObject(_postApiOmsJsons);
            //return JsonSerializer.Serialize(_postApiOmsJsons);
        }


        public string GetApiOmsPostOrdersStatusJson(AsrkFile _asrkFile)
        {
            List<PostApiOmsJsonOrderStatus> _postApiOmsJsons = new List<PostApiOmsJsonOrderStatus>();

            foreach (FileBody body in _asrkFile.fileBody)
            {
                _postApiOmsJsons.Add(new PostApiOmsJsonOrderStatus()
                {
                    extr_guid_order = body.Id.Trim(),
                    status_code = "90801",
                    status_date = Convert.ToDateTime(body.RegDate).ToShortDateString(),
                    reason_ext_id = "",
                    location = body.AddressTo.Trim(),
                    exec_subdiv_id = "",
                    barcode_gu = body.Id.Trim()
                });

            }

            return JsonConvert.SerializeObject(_postApiOmsJsons);
        }

        public async Task<bool> JsonPostSend(string _requestUrl, string _jsonPostText)
        {
            string _apiKey = "801067c6-afc3-332e-eae7-bc6f5f910b89";

            using (HttpClient _httpClient = new HttpClient())
            {
                HttpContent _httpContent = new StringContent(_jsonPostText, Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(HttpMethod.Post, _requestUrl);
                request.Content = _httpContent;
                request.Headers.Add("api_key", _apiKey);
                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }

    }
}
}
