﻿using NatNegotiation.Entity.Structure;
using NatNegotiation.Entity.Structure.Packet;
using NATNegotiation.Handler.SystemHandler;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NATNegotiation.Entity.Structure
{
    public class ClientInfo
    {
        public byte Version;
        public byte[] Cookie = new byte[4];
        public byte ClientIndex;
        public bool IsConnected;
        public bool IsGotInit;
        public bool IsGotConnectAck;
        public bool IsGotErtAck;
        public GameInfo Game;
        public EndPoint EndPoint;

       
        public byte[] ExternalIP = new byte[4];
        public byte[] ExternalPort = new byte[2];
        public byte[] InternalIP = new byte[4];
        public byte[] InternalPort = new byte[2];


        public DateTime ConnectTime;
        public DateTime LastPacketTime;
        public DateTime SentConnectPacketTime;


        public void Parse(byte[] recv)
        {
            Version = recv[BasePacket.MagicData.Length];
            ExternalIP = NNFormat.IPToByte(this.EndPoint);
            ExternalPort = NNFormat.PortToByte(this.EndPoint);
        }
    }
}
