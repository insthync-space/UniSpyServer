using UniSpyServer.Servers.Chat.Abstraction.BaseClass;
using UniSpyServer.Servers.Chat.Entity.Contract;

namespace UniSpyServer.Servers.Chat.Entity.Structure.Request.General
{
    [RequestContract("CRYPT")]
    public sealed class CryptRequest : RequestBase
    {

        public string VersionID { get; private set; }
        public string GameName { get; private set; }
        public CryptRequest(string rawRequest) : base(rawRequest) { }
        public override void Parse()
        {
            base.Parse();

            if (_cmdParams.Count < 3)
            {
                throw new Exception.ChatException("The number of IRC params in CRYPT request is incorrect.");
            }

            VersionID = _cmdParams[1];
            GameName = _cmdParams[2];
        }
    }
}
