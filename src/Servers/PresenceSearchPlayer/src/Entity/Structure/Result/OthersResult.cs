using UniSpyServer.Servers.PresenceSearchPlayer.Abstraction.BaseClass;
using System.Collections.Generic;

namespace UniSpyServer.Servers.PresenceSearchPlayer.Entity.Structure.Result
{
    public sealed class OthersDatabaseModel
    {
        public int ProfileId;
        public string Nick;
        public string Uniquenick;
        public string Lastname;
        public string Firstname;
        public int Userid;
        public string Email;
    }

    public sealed class OthersResult : ResultBase
    {
        public List<OthersDatabaseModel> DatabaseResults { get; set; }
        public OthersResult()
        {
            DatabaseResults = new List<OthersDatabaseModel>();
        }
    }
}
