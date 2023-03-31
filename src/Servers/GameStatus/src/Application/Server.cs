using System.Net;
using UniSpy.Server.Core.Abstraction.BaseClass;
using UniSpy.Server.Core.Network.Tcp.Server;
using UniSpy.Server.Core.Abstraction.Interface;

namespace UniSpy.Server.GameStatus.Application
{
    public sealed class Server : ServerBase
    {
        static Server()
        {
            _name = "GameStatus";
        }

        public Server()
        {
        }

        public Server(IConnectionManager manager) : base(manager)
        {
        }

        protected override IClient CreateClient(IConnection connection) => new Client(connection, this);
        protected override IConnectionManager CreateConnectionManager(IPEndPoint endpoint) => new TcpConnectionManager(endpoint);
    }
}