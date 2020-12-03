﻿using UniSpyLib.Abstraction.Interface;
using QueryReport.Entity.Structure;
using ServerBrowser.Abstraction.BaseClass;
using ServerBrowser.Entity.Structure;
using ServerBrowser.Entity.Structure.Packet.Request;

namespace ServerBrowser.Handler.CommandHandler
{
    public class GeneralRequestHandler : UpdateOptionHandlerBase
    {
        public GeneralRequestHandler(ServerListRequest request, ISession session, byte[] recv) : base(request, session, recv)
        {
        }
        /// <summary>
        /// we need to send empty server list response to game,
        /// even if there are no severs online
        /// </summary>
        protected override void DataOperation()
        {
            base.DataOperation();
            _gameServers =
                        GameServer.GetServers(_request.GameName);

        }

        protected override void ConstructResponse()
        {

            base.ConstructResponse();

            GenerateServerKeys();
            //we use NTS string so total unique value list is 0
            GenerateUniqueValue();
            //add server infomation such as public ip etc.
            GenerateServersInfo();
            //after all server information is added we add the end flag
            _dataList.AddRange(SBStringFlag.AllServerEndFlag);
        }
    }
}
