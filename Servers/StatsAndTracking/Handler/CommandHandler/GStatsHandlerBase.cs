﻿using GameSpyLib.Common.BaseClass;
using GameSpyLib.Common.Entity.Interface;
using GameSpyLib.Extensions;
using GameSpyLib.Logging;
using Serilog.Events;
using StatsAndTracking.Entity.Enumerator;
using StatsAndTracking.Entity.Structure.Request;
using StatsAndTracking.Handler.SystemHandler;
using System.Collections.Generic;

namespace StatsAndTracking.Handler.CommandHandler
{
    /// <summary>
    /// we only use selfdefine error code here
    /// so we do not need to send it to client
    /// </summary>
    public abstract class GStatsCommandHandlerBase : CommandHandlerBase
    {
        protected string _sendingBuffer;
        protected GStatsErrorCode _errorCode;
        protected GStatsRequestBase _request;
        protected new GStatsSession _session;
        protected GStatsCommandHandlerBase(ISession session, Dictionary<string, string> recv) : base(session)
        {
            _errorCode = GStatsErrorCode.NoError;
            _session = (GStatsSession)session.GetInstance();
        }

        public override void Handle()
        {
            LogWriter.LogCurrentClass(this);

            CheckRequest();
            if (_errorCode != GStatsErrorCode.NoError)
            {
                LogWriter.ToLog(LogEventLevel.Error, ErrorMessage.ToMsg(_errorCode));
                return;
            }

            DataOperation();

            if (_errorCode == GStatsErrorCode.Database)
            {
                LogWriter.ToLog(LogEventLevel.Error, ErrorMessage.ToMsg(_errorCode));
                return;
            }

            ConstructResponse();

            if (_errorCode != GStatsErrorCode.NoError)
            {
                LogWriter.ToLog(LogEventLevel.Error, ErrorMessage.ToMsg(_errorCode));
                return;
            }

            Response();
        }

        protected abstract void CheckRequest();

        protected abstract void DataOperation();

        protected virtual void ConstructResponse()
        {
            if (!StringExtensions.CheckResponseValidation(_sendingBuffer))
            {
                return;
            }

            _sendingBuffer += @$"\lid\{_request.OperationID}";
        }

        protected virtual void Response()
        {
            if (!StringExtensions.CheckResponseValidation(_sendingBuffer))
            {
                return;
            }

            _session.SendAsync(_sendingBuffer);
        }
    }
}
