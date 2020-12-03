﻿using System.Linq;
using PresenceSearchPlayer.Entity.Structure;
using PresenceSearchPlayer.Entity.Structure.Request;
using UniSpyLib.Abstraction.BaseClass;
using UniSpyLib.Abstraction.Interface;
using UniSpyLib.Logging;
using UniSpyLib.MiscMethod;

namespace PresenceSearchPlayer.Handler.CommandSwitcher
{
    public class PSPRequestSerializer : RequestSerializerBase
    {
        protected new string _rawRequest;
        public PSPRequestSerializer(string rawRequest) : base(rawRequest)
        {
            _rawRequest = rawRequest;
        }

        public override IRequest Serialize()
        {
            // Read client message, and parse it into key value pairs
            var keyValues = GameSpyUtils.ConvertToKeyValue(_rawRequest);
            switch (keyValues.Keys.First())
            {
                case PSPRequestName.Search:
                    return new SearchRequest(_rawRequest);
                case PSPRequestName.Valid:
                    return new SearchRequest(_rawRequest);
                case PSPRequestName.Nicks:
                    return new SearchRequest(_rawRequest);
                case PSPRequestName.PMatch:
                case PSPRequestName.Check:
                    return new SearchRequest(_rawRequest);
                case PSPRequestName.NewUser:
                    return new SearchRequest(_rawRequest);
                case PSPRequestName.SearchUnique:
                    return new SearchRequest(_rawRequest);
                case PSPRequestName.Others:
                    return new SearchRequest(_rawRequest);
                case PSPRequestName.OtherList:
                    return new SearchRequest(_rawRequest);
                case PSPRequestName.UniqueSearch:
                    return new SearchRequest(_rawRequest);
                default:
                    LogWriter.UnknownDataRecieved(_rawRequest);
                    return null;
            }
        }
    }
}
