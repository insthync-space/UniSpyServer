using System.Linq;
using UniSpyServer.Servers.PresenceConnectionManager.Abstraction.BaseClass;
using UniSpyServer.Servers.PresenceConnectionManager.Application;
using UniSpyServer.Servers.PresenceConnectionManager.Entity.Structure.Response;
using UniSpyServer.Servers.PresenceConnectionManager.Entity.Structure.Result;
using UniSpyServer.UniSpyLib.Abstraction.Interface;

namespace UniSpyServer.Servers.PresenceConnectionManager.Handler.CmdHandler.Buddy
{
    public sealed class BlockListHandler : LoggedInCmdHandlerBase
    {
        private new BlockListResult _result { get => (BlockListResult)base._result; set => base._result = value; }

        public BlockListHandler(IClient client) : base(client, null)
        {
            _result = new BlockListResult();
        }
        protected override void RequestCheck() { }
        protected override void DataOperation()
        {

            _result.ProfileIdList = StorageOperation.Persistance.GetBlockedProfileIds(_client.Info.ProfileInfo.ProfileId,
                                                                                      _client.Info.SubProfileInfo.NamespaceId);
        }

        protected override void ResponseConstruct()
        {
            _response = new BlockListResponse(null, _result);
        }
    }
}
