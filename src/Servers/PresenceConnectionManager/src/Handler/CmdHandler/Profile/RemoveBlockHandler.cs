﻿using PresenceConnectionManager.Abstraction.BaseClass;
using PresenceConnectionManager.Entity.Contract;
using System;
using UniSpyLib.Abstraction.BaseClass;
using UniSpyLib.Abstraction.Interface;

namespace PresenceConnectionManager.Handler.CmdHandler
{
    [HandlerContract("removeblk")]
    public sealed class RemoveBlockHandler : Abstraction.BaseClass.CmdHandlerBase
    {
        public RemoveBlockHandler(IUniSpySession session, IUniSpyRequest request) : base(session, request)
        {
            throw new NotImplementedException();
        }

        protected override void DataOperation()
        {
            throw new NotImplementedException();
        }
    }
}
