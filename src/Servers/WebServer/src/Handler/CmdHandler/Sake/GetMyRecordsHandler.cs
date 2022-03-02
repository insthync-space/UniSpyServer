using System.Linq;
using UniSpyServer.Servers.WebServer.Entity.Contract;
using UniSpyServer.Servers.WebServer.Entity.Structure.Request.Sake;
using UniSpyServer.Servers.WebServer.Entity.Structure.Result.Sake;
using UniSpyServer.UniSpyLib.Abstraction.Interface;

namespace UniSpyServer.Servers.WebServer.Handler.CmdHandler.Sake
{
    [HandlerContract("GetMyRecords")]
    public class GetMyRecordsHandler : Abstraction.Sake.CmdHandlerBase
    {
        private new GetMyRecordsRequest _request => (GetMyRecordsRequest)base._request;
        private new GetMyRecordsResult _result { get => (GetMyRecordsResult)base._result; set => base._result = value; }
        public GetMyRecordsHandler(IClient client, IRequest request) : base(client, request)
        {
        }
        protected override void DataOperation()
        {
            base.DataOperation();
            _result = new GetMyRecordsResult();

            foreach (var field in _request.Fields)
            {
                var record = _sakeData.FirstOrDefault(x => x.FieldName == field.FieldName && x.FiledType == field.FiledType);
                if (record != null)
                {
                    _result.Records.Add(record);
                }
            }
        }
    }
}
