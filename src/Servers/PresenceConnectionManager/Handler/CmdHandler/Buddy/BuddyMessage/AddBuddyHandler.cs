﻿using PresenceConnectionManager.Abstraction.BaseClass;
using PresenceConnectionManager.Entity.Structure.Request;
using PresenceConnectionManager.Entity.Structure.Result;
using UniSpyLib.Abstraction.Interface;

namespace PresenceConnectionManager.Handler.CmdHandler
{
    //\addbuddy\\sesskey\<>\newprofileid\<>\reason\<>\final\
    [Command("addbuddy")]
    internal sealed class AddBuddyHandler : PCMCmdHandlerBase
    {
        private new AddBuddyRequest _request => (AddBuddyRequest)base._request;

        public AddBuddyHandler(IUniSpySession session, IUniSpyRequest request) : base(session, request)
        {
            _result = new AddBuddyResult();
        }

        protected override void DataOperation()
        {
            throw new System.NotImplementedException();
            //Check if the friend is online
            //if(online)
            //else
            //store add request to database
        }
    }
}
