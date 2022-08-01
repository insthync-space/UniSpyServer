using UniSpyServer.Servers.PresenceSearchPlayer.Entity.Enumerate;

namespace UniSpyServer.Servers.PresenceSearchPlayer.Entity.Exception.NewUser
{
    public class GPNewUserUniquenickInvalidException : GPNewUserException
    {
        public GPNewUserUniquenickInvalidException() : base("Uniquenick is invalid!", GPErrorCode.NewUserUniquenickInvalid)
        {
        }

        public GPNewUserUniquenickInvalidException(string message) : base(message, GPErrorCode.NewUserUniquenickInvalid)
        {
        }

        public GPNewUserUniquenickInvalidException(string message, System.Exception innerException) : base(message, GPErrorCode.NewUserUniquenickInvalid, innerException)
        {
        }
    }
}