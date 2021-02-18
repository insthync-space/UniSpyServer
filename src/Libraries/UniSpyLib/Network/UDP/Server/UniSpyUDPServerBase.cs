﻿using NetCoreServer;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UniSpyLib.Abstraction.Interface;
using UniSpyLib.Extensions;
using UniSpyLib.Logging;

namespace UniSpyLib.Network
{
    /// <summary>
    /// This is a template class that helps creating a UDP Server with
    /// logging functionality and ServerName, as required in the old network stack.
    /// </summary>
    public abstract class UniSpyUDPServerBase : UdpServer, IUniSpyServer
    {
        public Guid ServerID { get; private set; }
        /// <summary>
        /// currently, we do not to care how to delete elements in dictionary
        /// </summary>
        public static Dictionary<EndPoint, UniSpyUDPSessionBase> Sessions = new Dictionary<EndPoint, UniSpyUDPSessionBase>();

        public UniSpyUDPServerBase(Guid serverID, IPEndPoint endpoint) : base(endpoint)
        {
            ServerID = serverID;
        }

        protected virtual UniSpyUDPSessionBase CreateSession(EndPoint endPoint)
        {
            return new UniSpyUDPSessionBase(this, endPoint);
        }

        protected override void OnStarted()
        {
            // Start receive datagrams
            ReceiveAsync();
        }



        /// <summary>
        /// Handle error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected override void OnError(SocketError error)
        {
            LogWriter.ToLog(LogEventLevel.Error, error.ToString());
        }

        public override long Send(EndPoint endpoint, byte[] buffer, long offset, long size)
        {
            LogWriter.ToLog(LogEventLevel.Debug,
                $"[Send] {StringExtensions.ReplaceUnreadableCharToHex(buffer, 0, (int)size)}");
            return base.Send(endpoint, buffer, offset, size);
        }

        public override bool SendAsync(EndPoint endpoint, byte[] buffer, long offset, long size)
        {
            LogWriter.ToLog(LogEventLevel.Debug,
                $"[Send] {StringExtensions.ReplaceUnreadableCharToHex(buffer, 0, (int)size)}");
            return base.SendAsync(endpoint, buffer, offset, size);
        }

        public bool BaseSendAsync(EndPoint endPoint, string buffer)
        {
            return BaseSendAsync(endPoint, Encoding.ASCII.GetBytes(buffer));
        }

        public bool BaseSendAsync(EndPoint endPoint, byte[] buffer)
        {
            return BaseSendAsync(endPoint, buffer, 0, buffer.Length);
        }

        public bool BaseSendAsync(EndPoint endpoint, byte[] buffer, long offset, long size)
        {
            return base.SendAsync(endpoint, buffer, offset, size);
        }

        protected override void OnSent(EndPoint endpoint, long sent)
        {
            base.OnSent(endpoint, sent);
            // Continue receive datagrams
            ReceiveAsync();
        }

        protected virtual void OnReceived(UniSpyUDPSessionBase session, string message) { }

        protected virtual void OnReceived(UniSpyUDPSessionBase session, byte[] message)
        {
            OnReceived(session, Encoding.ASCII.GetString(message));
        }

        protected virtual void OnReceived(EndPoint endPoint, string message) { }

        protected virtual void OnReceived(EndPoint endPoint, byte[] message)
        {
            OnReceived(endPoint, Encoding.ASCII.GetString(message));
        }

        protected override void OnReceived(EndPoint endPoint, byte[] buffer, long offset, long size)
        {
            // Need at least 2 bytes
            if (size < 2 && size > 2048)
            {
                return;
            }
            byte[] message = new byte[(int)size];
            Array.Copy(buffer, 0, message, 0, (int)size);

            LogWriter.ToLog(LogEventLevel.Debug,
                $"[Recv] {StringExtensions.ReplaceUnreadableCharToHex(buffer, 0, (int)size)}");
            //even if we did not response we keep receive message
            ReceiveAsync();

            UniSpyUDPSessionBase session;
            if (!Sessions.TryGetValue(endPoint, out session))
            {
                session = CreateSession(endPoint);
            }
            OnReceived(session, message);
        }
    }
}
