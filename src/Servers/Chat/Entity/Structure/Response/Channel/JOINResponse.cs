﻿using Chat.Abstraction.BaseClass;
using Chat.Entity.Structure.Misc;
using Chat.Entity.Structure.Request;
using Chat.Entity.Structure.Response.Channel;
using Chat.Entity.Structure.Result.Channel;
using UniSpyLib.Abstraction.BaseClass;

namespace Chat.Entity.Structure.Response.General
{
    internal sealed class JOINResponse : ChatResponseBase
    {
        private new JOINResult _result => (JOINResult)base._result;
        private new JOINRequest _request => (JOINRequest)base._request;
        // public string SendingBufferOfChannelUsers { get; private set; }
        public JOINResponse(UniSpyRequestBase request, UniSpyResultBase result) : base(request, result)
        {
        }

        protected override void BuildNormalResponse()
        {
            // string modeReply = MODEResponse.BuildChannelModeReply(
            //           _request.ChannelName, _result.ChannelModes);

            // if (!_result.IsAlreadyJoinedChannel)
            // {
            //     SendingBufferOfChannelUsers = ChatIRCReplyBuilder.Build(
            //         _result.JoinerPrefix,
            //         ChatReplyName.JOIN,
            //         _request.ChannelName,
            //         null);
            //     SendingBufferOfChannelUsers += modeReply;
            // }

            SendingBuffer = ChatIRCReplyBuilder.Build(
                _result.JoinerPrefix,
                ChatReplyName.JOIN,
                _request.ChannelName,
                 null);
        }
    }
}
