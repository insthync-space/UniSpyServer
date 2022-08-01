using System;
using System.Net;
using UniSpyServer.Servers.PresenceSearchPlayer.Entity.Structure;
using UniSpyServer.UniSpyLib.Abstraction.Interface;

namespace UniSpyServer.Servers.PresenceSearchPlayer.Application
{
    class TcpServer : UniSpyServer.UniSpyLib.Abstraction.BaseClass.Network.Tcp.Server.TcpServer
    {
        public TcpServer(Guid serverID, string serverName, IPEndPoint endpoint) : base(serverID, serverName, endpoint)
        {
        }

        protected override IClient CreateClient(ISession session) => new Client(session);
    }
}