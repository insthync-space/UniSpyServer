using System;
using System.Net;
using Newtonsoft.Json;
using UniSpyServer.Servers.QueryReport.Abstraction.BaseClass;
using UniSpyServer.UniSpyLib.MiscMethod;
using System.Linq;

namespace UniSpyServer.Servers.QueryReport.Entity.Structure.Request
{
    public sealed class ClientMessageRequest : RequestBase
    {
        public Guid ServerBrowserSenderId { get; init; }
        public new uint? InstantKey { get => base.InstantKey; set => base.InstantKey = value; }
        public byte[] NatNegMessage { get; init; }
        [JsonConverter(typeof(IPEndPointConverter))]
        public IPEndPoint TargetIPEndPoint { get; init; }
        public readonly int? MessageKey = 0;
        public uint Coookie => BitConverter.ToUInt32(NatNegMessage.Skip(6).Take(4).ToArray());
        public ClientMessageRequest() : base(null)
        {
        }
    }
}
