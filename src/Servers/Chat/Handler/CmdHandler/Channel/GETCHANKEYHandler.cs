﻿using Chat.Abstraction.BaseClass;
using Chat.Entity.Structure.ChatCommand.Channel;
using Chat.Entity.Structure.Response.Channel;
using Chat.Entity.Structure.Result;
using UniSpyLib.Abstraction.Interface;

namespace Chat.Handler.CmdHandler.Channel
{
    // Sets channel key/values.
    // If user is NULL or "", the keys will be set on the channel.
    // Otherwise, they will be set on the user,
    // Only ops can set channel keys on other users.
    // Set a value to NULL or "" to clear that key.
    public class GETCHANKEYHandler : ChatChannelHandlerBase
    {
        protected new GETCHANKEYRequest _request
        {
            get { return (GETCHANKEYRequest)base._request; }
        }
        protected new GETCHANKEYResult _result
        {
            get { return (GETCHANKEYResult)base._result; }
            set { base._result = value; }
        }

        public GETCHANKEYHandler(IUniSpySession session, IUniSpyRequest request) : base(session, request)
        {
        }

        protected override void DataOperation()
        {
            base.DataOperation();
            var values = _channel.Property.GetChannelValueString(_request.Keys);
            _result = new GETCHANKEYResult(_user, _channel.Property.ChannelName, _request.Cookie, values);
        }

        protected override void BuildNormalResponse()
        {
            base.BuildNormalResponse();
            _response = new GETCHANKEYResponse(_result);
        }
    }
}
