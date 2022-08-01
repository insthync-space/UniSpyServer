using UniSpyServer.Servers.Chat.Abstraction.BaseClass;


namespace UniSpyServer.Servers.Chat.Entity.Structure.Request.General
{
    
    public sealed class SetGroupRequest : ChannelRequestBase
    {
        public SetGroupRequest(string rawRequest) : base(rawRequest){ }

        public string GroupName { get; private set; }
        public override void Parse()
        {
            base.Parse();

            if (_cmdParams.Count != 1)
            {
                throw new Exception.ChatException("The number of IRC cmd params in GETKEY request is incorrect.");
            }

            GroupName = _cmdParams[0];
        }
    }
}
