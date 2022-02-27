﻿using UniSpyServer.Servers.Chat.Abstraction.BaseClass;
using UniSpyServer.Servers.Chat.Entity.Contract;
using UniSpyServer.Servers.Chat.Entity.Exception;
using UniSpyServer.Servers.Chat.Entity.Structure.Request.Channel;
using UniSpyServer.Servers.Chat.Entity.Structure.Response.Channel;
using UniSpyServer.Servers.Chat.Entity.Structure.Result.Channel;
using UniSpyServer.UniSpyLib.Abstraction.Interface;
using UniSpyServer.UniSpyLib.Encryption;

namespace UniSpyServer.Servers.Chat.Handler.CmdHandler.Channel
{
    [HandlerContract("TOPIC")]
    public sealed class TopicHandler : ChannelHandlerBase
    {
        private new TopicRequest _request => (TopicRequest)base._request;
        private new TopicResult _result { get => (TopicResult)base._result; set => base._result = value; }
        public TopicHandler(IClient client, IRequest request) : base(client, request)
        {
            _result = new TopicResult();
        }

        protected override void DataOperation()
        {
            if (!_user.IsChannelOperator)
            {
                throw new Exception("Edit topic failed because you are not channel operator.");
            }
            switch (_request.RequestType)
            {
                case TopicRequestType.GetChannelTopic:
                    break;
                case TopicRequestType.SetChannelTopic:
                    _channel.Topic = _request.ChannelTopic;
                    break;
            }
            _result.ChannelName = _channel.Name;
            _result.ChannelTopic = _channel.Topic;
        }
        protected override void ResponseConstruct()
        {
            _response = new TopicResponse(_request, _result);
        }
        protected override void Response()
        {
            switch (_request.RequestType)
            {
                case TopicRequestType.GetChannelTopic:
                    _response.Build();
                    if (_client.Crypto is not null)
                    {
                        _client.Session.Send(_client.Crypto.Encrypt(UniSpyEncoding.GetBytes(_response.SendingBuffer)));
                    }
                    else
                    {
                        _client.Session.Send(_response.SendingBuffer);
                    }
                    break;
                case TopicRequestType.SetChannelTopic:
                    _channel.MultiCast(_response);
                    break;
            }
        }
    }
}
