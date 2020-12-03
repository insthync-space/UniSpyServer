﻿using UniSpyLib.Logging;
using UniSpyLib.MiscMethod;
using Serilog.Events;
using GameStatus.Entity.Structure;
using UniSpyLib.Abstraction.Interface;
using UniSpyLib.Abstraction.BaseClass;
using GameStatus.Abstraction.BaseClass;
using GameStatus.Handler.CommandHandler;

namespace GameStatus.Handler.CommandSwitcher
{
    public class GSCommandHandlerSerializer : CommandHandlerSerializerBase
    {
        protected new GSRequestBase _request;
        public GSCommandHandlerSerializer(ISession session, IRequest request) : base(session, request)
        {
            _request = (GSRequestBase)request;
        }

        public override IHandler Serialize()
        {
            switch (_request.CommandName)
            {
                case GStatsRequestName.AuthenticateUser:
                    return new AuthHandler(_session, _request);
                case GStatsRequestName.AuthenticatePlayer:
                    return new AuthPHandler(_session, _request);
                case GStatsRequestName.GetProfileID:
                    return new GetPIDHandler(_session, _request);
                case GStatsRequestName.GetPlayerData:
                    return new GetPDHandler(_session, _request);
                case GStatsRequestName.SetPlayerData:
                    return new SetPDHandler(_session, _request);
                case GStatsRequestName.UpdateGameData:
                    return new UpdGameHandler(_session, _request);
                case GStatsRequestName.CreateNewGamePlayerData:
                    return new NewGameHandler(_session, _request);
                default:
                    LogWriter.UnknownDataRecieved(_request.RawRequest);
                    return null;
            }
        }
    }
}
