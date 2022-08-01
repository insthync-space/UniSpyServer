using UniSpyServer.UniSpyLib.Abstraction.BaseClass;

namespace UniSpyServer.UniSpyLib.Abstraction.Interface
{
    public interface IClient
    {
        // we store client info here
        ISession Session { get; }
        ICryptography Crypto { get; }
        ClientInfoBase Info { get; }
        public void Send(IResponse response);
    }
}