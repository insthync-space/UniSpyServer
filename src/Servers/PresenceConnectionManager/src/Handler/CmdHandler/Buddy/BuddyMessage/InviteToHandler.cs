﻿using PresenceConnectionManager.Abstraction.BaseClass;
using PresenceConnectionManager.Application;
using PresenceConnectionManager.Entity.Structure.Request;
using PresenceConnectionManager.Network;
using System.Linq;
using UniSpyLib.Abstraction.Interface;

namespace PresenceConnectionManager.Handler.CmdHandler
{
    /// <summary>
    /// This function sets which games the local profile can be invited to.
    /// </summary>
    public sealed class InviteToHandler : CmdHandlerBase
    {
        //_session.SendAsync(@"\pinvite\\sesskey\223\profileid\13\productid\1038\final\");
        private new InviteToRequest _request => (InviteToRequest)base._request;
        public InviteToHandler(IUniSpySession session, IUniSpyRequest request) : base(session, request)
        {
        }

        protected override void DataOperation()
        {
            var session = ServerFactory.Server.SessionManager.SessionPool.Values.Where(
                u => ((Session)u).UserInfo.BasicInfo.ProductID == _request.ProductID
                && ((Session)u).UserInfo.BasicInfo.ProfileID == _request.ProfileID).FirstOrDefault();

            //user is offline
            if (session == null)
            {
                return;
            }
            else
            {

            }
            //TODO
            //parse user to buddy message system
        }
    }
}
