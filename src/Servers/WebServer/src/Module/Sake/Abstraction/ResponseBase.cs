using System.Xml.Linq;
using UniSpyServer.Servers.WebServer.Abstraction;
using UniSpyServer.Servers.WebServer.Entity.Structure;
using UniSpyServer.Servers.WebServer.Module.Sake.Entity.Structure;

namespace UniSpyServer.Servers.WebServer.Module.Sake.Abstraction
{
    public abstract class ResponseBase : WebServer.Abstraction.ResponseBase
    {
        public ResponseBase(RequestBase request, ResultBase result) : base(request, result)
        {
            _content = new SakeSoapEnvelope();
        }
    }
}