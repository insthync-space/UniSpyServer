using System;
using UniSpyServer.Servers.QueryReport.Entity.Enumerate;
using UniSpyServer.Servers.QueryReport.Entity.Structure.Request;
using Xunit;
namespace UniSpyServer.Servers.UniSpyServer.Servers.QueryReport.Test
{
    public class RequestTest
    {
        [Fact]
        public void AvaliableRequestTest()
        {
            var rawRequest = new byte[]{
                0x09,//packet type
                0x00,0x00,0x00,0x00,//instant key
                0x09, 0x00, 0x00, 0x00, 0x00,//prefix
                0x67, 0x61, 0x6D, 0x65 ,0x73, 0x70, 0x79,//gamename
                0x00
            };
            var request = new AvaliableRequest(rawRequest);
            request.Parse();
            Assert.Equal(PacketType.AvaliableCheck, request.CommandName);
            Assert.Equal((uint)0, request.InstantKey);
        }
        [Fact]
        public void ChallengeRequestTest()
        {
            var rawRequest = new byte[]{
                0x01,//packet type
                0x00,0x00,0x00,0x00,//instant key
                0x67, 0x61, 0x6D, 0x65 ,0x73, 0x70, 0x79,//gamename
                0x00
            };
            var request = new ChallengeRequest(rawRequest);
            request.Parse();
            Assert.Equal(PacketType.Challenge, request.CommandName);
            Assert.Equal((uint)0, request.InstantKey);
        }
        [Fact]
        public void EchoRequest()
        {
            var rawRequest = new byte[]{
                0x02,//packet type
                0x00,0x00,0x00,0x00,//instant key
                0x67, 0x61, 0x6D, 0x65 ,0x73, 0x70, 0x79,//gamename
                0x00
            };
            var request = new EchoRequest(rawRequest);
            request.Parse();
            Assert.Equal(PacketType.Echo, request.CommandName);
            Assert.Equal((uint)0, request.InstantKey);
        }
        [Fact]
        public void HeartBeatRequestTest()
        {
            var rawRequest = new byte[] { 0x03, 0xae, 0x1f, 0x77, 0x64, 0x6c, 0x6f, 0x63, 0x61, 0x6c, 0x69, 0x70, 0x30, 0x00, 0x31, 0x39, 0x32, 0x2e, 0x31, 0x36, 0x38, 0x2e, 0x30, 0x2e, 0x31, 0x30, 0x39, 0x00, 0x6c, 0x6f, 0x63, 0x61, 0x6c, 0x70, 0x6f, 0x72, 0x74, 0x00, 0x31, 0x31, 0x31, 0x31, 0x31, 0x00, 0x6e, 0x61, 0x74, 0x6e, 0x65, 0x67, 0x00, 0x31, 0x00, 0x73, 0x74, 0x61, 0x74, 0x65, 0x63, 0x68, 0x61, 0x6e, 0x67, 0x65, 0x64, 0x00, 0x33, 0x00, 0x67, 0x61, 0x6d, 0x65, 0x6e, 0x61, 0x6d, 0x65, 0x00, 0x67, 0x6d, 0x74, 0x65, 0x73, 0x74, 0x00, 0x68, 0x6f, 0x73, 0x74, 0x6e, 0x61, 0x6d, 0x65, 0x00, 0x47, 0x61, 0x6d, 0x65, 0x53, 0x70, 0x79, 0x20, 0x51, 0x52, 0x32, 0x20, 0x53, 0x61, 0x6d, 0x70, 0x6c, 0x65, 0x00, 0x67, 0x61, 0x6d, 0x65, 0x76, 0x65, 0x72, 0x00, 0x32, 0x2e, 0x30, 0x30, 0x00, 0x68, 0x6f, 0x73, 0x74, 0x70, 0x6f, 0x72, 0x74, 0x00, 0x32, 0x35, 0x30, 0x30, 0x30, 0x00, 0x6d, 0x61, 0x70, 0x6e, 0x61, 0x6d, 0x65, 0x00, 0x67, 0x6d, 0x74, 0x6d, 0x61, 0x70, 0x31, 0x00, 0x67, 0x61, 0x6d, 0x65, 0x74, 0x79, 0x70, 0x65, 0x00, 0x61, 0x72, 0x65, 0x6e, 0x61, 0x00, 0x6e, 0x75, 0x6d, 0x70, 0x6c, 0x61, 0x79, 0x65, 0x72, 0x73, 0x00, 0x36, 0x00, 0x6e, 0x75, 0x6d, 0x74, 0x65, 0x61, 0x6d, 0x73, 0x00, 0x32, 0x00, 0x6d, 0x61, 0x78, 0x70, 0x6c, 0x61, 0x79, 0x65, 0x72, 0x73, 0x00, 0x33, 0x32, 0x00, 0x67, 0x61, 0x6d, 0x65, 0x6d, 0x6f, 0x64, 0x65, 0x00, 0x6f, 0x70, 0x65, 0x6e, 0x70, 0x6c, 0x61, 0x79, 0x69, 0x6e, 0x67, 0x00, 0x74, 0x65, 0x61, 0x6d, 0x70, 0x6c, 0x61, 0x79, 0x00, 0x31, 0x00, 0x66, 0x72, 0x61, 0x67, 0x6c, 0x69, 0x6d, 0x69, 0x74, 0x00, 0x30, 0x00, 0x74, 0x69, 0x6d, 0x65, 0x6c, 0x69, 0x6d, 0x69, 0x74, 0x00, 0x34, 0x30, 0x00, 0x67, 0x72, 0x61, 0x76, 0x69, 0x74, 0x79, 0x00, 0x38, 0x30, 0x30, 0x00, 0x72, 0x61, 0x6e, 0x6b, 0x69, 0x6e, 0x67, 0x6f, 0x6e, 0x00, 0x31, 0x00, 0x00, 0x00, 0x06, 0x70, 0x6c, 0x61, 0x79, 0x65, 0x72, 0x5f, 0x00, 0x73, 0x63, 0x6f, 0x72, 0x65, 0x5f, 0x00, 0x64, 0x65, 0x61, 0x74, 0x68, 0x73, 0x5f, 0x00, 0x70, 0x69, 0x6e, 0x67, 0x5f, 0x00, 0x74, 0x65, 0x61, 0x6d, 0x5f, 0x00, 0x74, 0x69, 0x6d, 0x65, 0x5f, 0x00, 0x00, 0x4a, 0x6f, 0x65, 0x20, 0x50, 0x6c, 0x61, 0x79, 0x65, 0x72, 0x00, 0x32, 0x37, 0x00, 0x36, 0x00, 0x31, 0x32, 0x33, 0x00, 0x31, 0x00, 0x34, 0x30, 0x39, 0x00, 0x4c, 0x33, 0x33, 0x74, 0x20, 0x30, 0x6e, 0x33, 0x00, 0x36, 0x00, 0x32, 0x33, 0x00, 0x32, 0x37, 0x37, 0x00, 0x30, 0x00, 0x36, 0x37, 0x33, 0x00, 0x52, 0x61, 0x70, 0x74, 0x6f, 0x72, 0x00, 0x33, 0x30, 0x00, 0x31, 0x00, 0x31, 0x34, 0x36, 0x00, 0x31, 0x00, 0x37, 0x30, 0x31, 0x00, 0x47, 0x72, 0x38, 0x31, 0x00, 0x32, 0x31, 0x00, 0x31, 0x36, 0x00, 0x31, 0x32, 0x35, 0x00, 0x31, 0x00, 0x35, 0x38, 0x32, 0x00, 0x46, 0x6c, 0x75, 0x62, 0x62, 0x65, 0x72, 0x00, 0x33, 0x00, 0x32, 0x31, 0x00, 0x31, 0x31, 0x30, 0x00, 0x30, 0x00, 0x32, 0x39, 0x38, 0x00, 0x53, 0x61, 0x72, 0x67, 0x65, 0x00, 0x33, 0x00, 0x32, 0x38, 0x00, 0x31, 0x32, 0x35, 0x00, 0x31, 0x00, 0x35, 0x39, 0x30, 0x00, 0x00, 0x02, 0x74, 0x65, 0x61, 0x6d, 0x5f, 0x74, 0x00, 0x73, 0x63, 0x6f, 0x72, 0x65, 0x5f, 0x74, 0x00, 0x61, 0x76, 0x67, 0x70, 0x69, 0x6e, 0x67, 0x5f, 0x74, 0x00, 0x00, 0x52, 0x65, 0x64, 0x00, 0x32, 0x39, 0x34, 0x00, 0x33, 0x37, 0x39, 0x00, 0x42, 0x6c, 0x75, 0x65, 0x00, 0x38, 0x39, 0x00, 0x33, 0x38, 0x33, 0x00 };
            var request = new HeartBeatRequest(rawRequest);
            request.Parse();
            Assert.Equal("gmtest", request.GameName);
        }
        [Fact]
        public void KeepAliveRequestTest()
        {
            var rawRequest = new byte[]{
                0x08,//packet type
                0x00,0x00,0x00,0x00,//instant key
            };
            var request = new EchoRequest(rawRequest);
            request.Parse();
            Assert.Equal(PacketType.KeepAlive, request.CommandName);
            Assert.Equal((uint)0, request.InstantKey);
        }
    }
}
