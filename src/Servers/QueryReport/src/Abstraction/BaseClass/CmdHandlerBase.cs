using UniSpyServer.Servers.QueryReport.Entity.Structure;
using UniSpyServer.Servers.QueryReport.Entity.Structure.Redis.GameServer;
using UniSpyServer.UniSpyLib.Abstraction.Interface;

namespace UniSpyServer.Servers.QueryReport.Abstraction.BaseClass
{
    public abstract class CmdHandlerBase : UniSpyServer.UniSpyLib.Abstraction.BaseClass.CmdHandlerBase
    {
        protected new Client _client => (Client)base._client;
        protected new RequestBase _request => (RequestBase)base._request;
        protected new ResultBase _result { get => (ResultBase)base._result; set => base._result = value; }
        protected RedisClient _redisClient { get; private set; }
        protected CmdHandlerBase(IClient client, IRequest request) : base(client, request)
        {
            _redisClient = new RedisClient();
        }
    }
}
