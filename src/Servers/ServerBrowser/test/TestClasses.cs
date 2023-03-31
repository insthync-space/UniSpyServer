using System.Net;
using Moq;
using UniSpy.Server.ServerBrowser.Application;
using UniSpy.Server.Core.Abstraction.Interface;

namespace UniSpy.Server.ServerBrowser.Test
{
    public static class TestClasses
    {
        public static IClient QRClient = QueryReport.Test.TestClasses.CreateClient();
        public static IClient SBClient = CreateClient();

        public static Client CreateClient(string ipAddress = "192.168.1.2", int port = 9999)
        {
            var managerMock = new Mock<IConnectionManager>();
            var connectionMock = new Mock<ITcpConnection>();
            connectionMock.Setup(s => s.RemoteIPEndPoint).Returns(new IPEndPoint(IPAddress.Parse(ipAddress), port));
            connectionMock.Setup(s => s.Manager).Returns(managerMock.Object);
            connectionMock.Setup(s => s.ConnectionType).Returns(NetworkConnectionType.Tcp);
            var serverMock = new ServerBrowser.Application.Server(managerMock.Object);

            return new Client(connectionMock.Object, serverMock);
        }
    }
}