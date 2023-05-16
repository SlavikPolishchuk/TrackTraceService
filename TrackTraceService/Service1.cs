using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Track_Trace_JsonApi;

namespace TrackTraceService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            MainTask();
        }

        protected override void OnStop()
        {
        }

        static async Task MainTask()
        {

            string _urlOrders = @"http://51-tt-tst-a.ukrposhta.loc/ords/oms_ukrposhta/up/orders";
            string _urlOrdersStatus = @"http://51-tt-tst-a.ukrposhta.loc/ords/oms_ukrposhta/up/orders/status";

            DALTrackTraceJsonApi dALTrackTraceJsonApi = new DALTrackTraceJsonApi();
            string _jsonPostOrdersText = string.Empty;
            string _jsonPostOrdersStatusText = string.Empty;
            int stateRecordFileBody = 0;
            bool isError;

            foreach (AsrkFile _asrkF in dALTrackTraceJsonApi.GetListAsrkFile())
            {
                stateRecordFileBody = 0;
                isError = false;
                foreach (FileBody fileBody in _asrkF.fileBody)
                {
                    _jsonPostOrdersText = dALTrackTraceJsonApi.GetApiOmsPostOrdersJson(fileBody);
                    _jsonPostOrdersStatusText = dALTrackTraceJsonApi.GetApiOmsPostOrdersStatusJson(fileBody);

                    if (await dALTrackTraceJsonApi.JsonPostSend(_urlOrders, _jsonPostOrdersText))
                    {
                        if (await dALTrackTraceJsonApi.JsonPostSend(_urlOrdersStatus, _jsonPostOrdersStatusText))
                            stateRecordFileBody = 1;
                        else
                        { stateRecordFileBody = -2; isError = true; }
                    }
                    else
                    { stateRecordFileBody = -1; isError = true; }

                    dALTrackTraceJsonApi.InsertIntoAsrkFilesExportApi(Int32.Parse(_asrkF.FileId), fileBody.Id, fileBody.DataStr, stateRecordFileBody, dALTrackTraceJsonApi.MessageState);

                }
                if (isError) dALTrackTraceJsonApi.SetIsSended(_asrkF, -1); else dALTrackTraceJsonApi.SetIsSended(_asrkF, 2);
            }
        }
    }
}
