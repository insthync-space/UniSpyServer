﻿using Chat.Abstraction.BaseClass;
using Chat.Entity.Structure;
using Chat.Entity.Structure.Misc;
using Chat.Entity.Structure.Request.General;
using Chat.Entity.Structure.Response.General;
using UniSpyLib.Abstraction.Interface;

namespace Chat.Handler.CmdHandler.General
{
    public class NICKHandler : ChatCmdHandlerBase
    {
        new NICKRequest _request;
        public NICKHandler(IUniSpySession session, IUniSpyRequest request) : base(session, request)
        {
            _request = (NICKRequest)request;
        }

        protected override void RequestCheck()
        {
            base.RequestCheck();

            if (ChatSessionManager.IsNickNameExisted(_request.NickName))
            {
                _errorCode = ChatErrorCode.NickNameExisted;
                return;
            }
        }

        protected override void DataOperation()
        {
            _session.UserInfo.NickName = _request.NickName;
        }

        protected override void BuildErrorResponse()
        {
            _sendingBuffer = ChatIRCErrorCode.BuildNoSuchNickError();
        }

        protected override void BuildNormalResponse()
        {
            base.BuildNormalResponse();
            _sendingBuffer = NICKReply.BuildWelcomeReply(_session.UserInfo);
        }
    }
}
