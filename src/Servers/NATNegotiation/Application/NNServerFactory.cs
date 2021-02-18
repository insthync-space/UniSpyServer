﻿using NATNegotiation.Network;
using System;
using System.Net;
using UniSpyLib.Abstraction.BaseClass;
using UniSpyLib.Extensions;
using UniSpyLib.UniSpyConfig;

namespace NATNegotiation.Application
{
    /// <summary>
    /// A factory that create the instance of servers
    /// </summary>
    internal sealed class NNServerFactory : UniSpyServerFactoryBase
    {
        public new static NNServer Server
        {
            get => (NNServer)UniSpyServerFactoryBase.Server;
            private set => UniSpyServerFactoryBase.Server = value;
        }

        public NNServerFactory(string serverName) : base(serverName)
        {
        }
        /// <summary>
        /// Starts a specific server, you can also start all server in once if you do not check the server name.
        /// </summary>
        /// <param name="cfg">The configuration of the specific server to run</param>
        protected override void StartServer(UniSpyServerConfig cfg)
        {
            if (cfg.Name == ServerName)
            {
                Server = new NNServer(cfg.ServerID, cfg.ListeningEndPoint);
                Server.Start();
                Console.WriteLine(
                    StringExtensions.FormatTableContext(cfg.Name, cfg.ListeningAddress, cfg.ListeningPort.ToString()));
            }
        }
    }
}
