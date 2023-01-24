using System.Net;
using Moq;
using UniSpyServer.Servers.QueryReport.V2.Entity.Structure;
using UniSpyServer.Servers.QueryReport.V2.Handler;
using UniSpyServer.UniSpyLib.Abstraction.Interface;
using Xunit;

namespace UniSpyServer.Servers.QueryReport.V2.Test
{
    public class GameTest
    {
        IClient _client;
        public GameTest()
        {
            _client = TestClasses.Client;
        }
        [Fact]
        public void FaltOut2()
        {
            var raw = new byte[]{0x03,0xc2,0x94,0x1c,0xe7,0x6c,0x6f,0x63,0x61,0x6c,0x69,0x70,0x30,0x00,0x31,0x39
                                    ,0x32,0x2e,0x31,0x36,0x38,0x2e,0x30,0x2e,0x35,0x30,0x00,0x6c,0x6f,0x63,0x61,0x6c
                                    ,0x69,0x70,0x31,0x00,0x31,0x37,0x32,0x2e,0x31,0x36,0x2e,0x37,0x34,0x2e,0x31,0x00
                                    ,0x6c,0x6f,0x63,0x61,0x6c,0x69,0x70,0x32,0x00,0x31,0x37,0x32,0x2e,0x31,0x37,0x2e
                                    ,0x30,0x2e,0x31,0x00,0x6c,0x6f,0x63,0x61,0x6c,0x69,0x70,0x33,0x00,0x31,0x39,0x32
                                    ,0x2e,0x31,0x36,0x38,0x2e,0x31,0x32,0x32,0x2e,0x31,0x00,0x6c,0x6f,0x63,0x61,0x6c
                                    ,0x69,0x70,0x34,0x00,0x31,0x37,0x32,0x2e,0x31,0x36,0x2e,0x36,0x35,0x2e,0x31,0x00
                                    ,0x6c,0x6f,0x63,0x61,0x6c,0x70,0x6f,0x72,0x74,0x00,0x32,0x33,0x37,0x35,0x36,0x00
                                    ,0x6e,0x61,0x74,0x6e,0x65,0x67,0x00,0x31,0x00,0x73,0x74,0x61,0x74,0x65,0x63,0x68
                                    ,0x61,0x6e,0x67,0x65,0x64,0x00,0x31,0x00,0x67,0x61,0x6d,0x65,0x6e,0x61,0x6d,0x65
                                    ,0x00,0x66,0x6c,0x61,0x74,0x6f,0x75,0x74,0x32,0x70,0x63,0x00,0x70,0x75,0x62,0x6c
                                    ,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6c,0x69,0x63,0x70,0x6f,0x72
                                    ,0x74,0x00,0x30,0x00,0x68,0x6f,0x73,0x74,0x6b,0x65,0x79,0x00,0x2d,0x38,0x32,0x30
                                    ,0x39,0x36,0x36,0x33,0x32,0x32,0x00,0x68,0x6f,0x73,0x74,0x6e,0x61,0x6d,0x65,0x00
                                    ,0x53,0x70,0x6f,0x72,0x65,0x00,0x67,0x61,0x6d,0x65,0x76,0x65,0x72,0x00,0x46,0x4f
                                    ,0x31,0x34,0x00,0x67,0x61,0x6d,0x65,0x74,0x79,0x70,0x65,0x00,0x72,0x61,0x63,0x65
                                    ,0x00,0x67,0x61,0x6d,0x65,0x76,0x61,0x72,0x69,0x61,0x6e,0x74,0x00,0x6e,0x6f,0x72
                                    ,0x6d,0x61,0x6c,0x5f,0x72,0x61,0x63,0x65,0x00,0x67,0x61,0x6d,0x65,0x6d,0x6f,0x64
                                    ,0x65,0x00,0x6f,0x70,0x65,0x6e,0x77,0x61,0x69,0x74,0x69,0x6e,0x67,0x00,0x6e,0x75
                                    ,0x6d,0x70,0x6c,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6d,0x61,0x78,0x70,0x6c
                                    ,0x61,0x79,0x65,0x72,0x73,0x00,0x38,0x00,0x6d,0x61,0x70,0x6e,0x61,0x6d,0x65,0x00
                                    ,0x54,0x69,0x6d,0x62,0x65,0x72,0x6c,0x61,0x6e,0x64,0x73,0x5f,0x31,0x00,0x74,0x69
                                    ,0x6d,0x65,0x6c,0x69,0x6d,0x69,0x74,0x00,0x30,0x00,0x70,0x61,0x73,0x73,0x77,0x6f
                                    ,0x72,0x64,0x00,0x30,0x00,0x63,0x61,0x72,0x5f,0x74,0x79,0x70,0x65,0x00,0x30,0x00
                                    ,0x63,0x61,0x72,0x5f,0x63,0x6c,0x61,0x73,0x73,0x00,0x30,0x00,0x72,0x61,0x63,0x65
                                    ,0x73,0x5f,0x70,0x00,0x31,0x30,0x30,0x00,0x64,0x65,0x72,0x62,0x69,0x65,0x73,0x5f
                                    ,0x70,0x00,0x30,0x00,0x73,0x74,0x75,0x6e,0x74,0x73,0x5f,0x70,0x00,0x30,0x00,0x6e
                                    ,0x6f,0x72,0x6d,0x61,0x6c,0x5f,0x72,0x61,0x63,0x65,0x5f,0x70,0x00,0x31,0x30,0x30
                                    ,0x00,0x70,0x6f,0x6e,0x67,0x5f,0x72,0x61,0x63,0x65,0x5f,0x70,0x00,0x30,0x00,0x77
                                    ,0x72,0x65,0x63,0x6b,0x5f,0x64,0x65,0x72,0x62,0x79,0x5f,0x70,0x00,0x30,0x00,0x73
                                    ,0x75,0x72,0x76,0x69,0x76,0x6f,0x72,0x5f,0x64,0x65,0x72,0x62,0x79,0x5f,0x70,0x00
                                    ,0x30,0x00,0x66,0x72,0x61,0x67,0x5f,0x64,0x65,0x72,0x62,0x79,0x5f,0x70,0x00,0x30
                                    ,0x00,0x74,0x61,0x67,0x5f,0x70,0x00,0x30,0x00,0x75,0x70,0x67,0x72,0x61,0x64,0x65
                                    ,0x73,0x00,0x32,0x00,0x6e,0x69,0x74,0x72,0x6f,0x5f,0x72,0x65,0x67,0x65,0x6e,0x65
                                    ,0x72,0x61,0x74,0x69,0x6f,0x6e,0x00,0x32,0x00,0x64,0x61,0x6d,0x61,0x67,0x65,0x5f
                                    ,0x6c,0x65,0x76,0x65,0x6c,0x00,0x32,0x00,0x64,0x65,0x72,0x62,0x79,0x5f,0x64,0x61
                                    ,0x6d,0x61,0x67,0x65,0x5f,0x6c,0x65,0x76,0x65,0x6c,0x00,0x31,0x00,0x6e,0x65,0x78
                                    ,0x74,0x5f,0x72,0x61,0x63,0x65,0x5f,0x74,0x79,0x70,0x65,0x00,0x6e,0x6f,0x72,0x6d
                                    ,0x61,0x6c,0x5f,0x72,0x61,0x63,0x65,0x00,0x6c,0x61,0x70,0x73,0x5f,0x6f,0x72,0x5f
                                    ,0x74,0x69,0x6d,0x65,0x6c,0x69,0x6d,0x69,0x74,0x00,0x34,0x00,0x6e,0x75,0x6d,0x5f
                                    ,0x72,0x61,0x63,0x65,0x73,0x00,0x31,0x00,0x6e,0x75,0x6d,0x5f,0x64,0x65,0x72,0x62
                                    ,0x69,0x65,0x73,0x00,0x30,0x00,0x6e,0x75,0x6d,0x5f,0x73,0x74,0x75,0x6e,0x74,0x73
                                    ,0x00,0x30,0x00,0x64,0x61,0x74,0x61,0x63,0x68,0x65,0x63,0x6b,0x73,0x75,0x6d,0x00
                                    ,0x33,0x35,0x34,0x36,0x64,0x35,0x38,0x30,0x39,0x33,0x32,0x33,0x37,0x65,0x62,0x33
                                    ,0x33,0x62,0x32,0x61,0x39,0x36,0x62,0x62,0x38,0x31,0x33,0x33,0x37,0x30,0x64,0x38
                                    ,0x34,0x36,0x66,0x66,0x63,0x65,0x63,0x38,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00};
            var switcher = new CmdSwitcher(_client, raw);
            switcher.Switch();
        }
        [Fact]
        public void Worms3d()
        {
            var raw = new byte[] {
                0x03,0x51,0x5d,0xa0
                ,0xe8,0x6c,0x6f,0x63,0x61,0x6c,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2e,0x31,0x36
                ,0x38,0x2e,0x30,0x2e,0x36,0x30,0x00,0x6c,0x6f,0x63,0x61,0x6c,0x70,0x6f,0x72,0x74
                ,0x00,0x36,0x35,0x30,0x30,0x00,0x6e,0x61,0x74,0x6e,0x65,0x67,0x00,0x31,0x00,0x73
                ,0x74,0x61,0x74,0x65,0x63,0x68,0x61,0x6e,0x67,0x65,0x64,0x00,0x33,0x00,0x67,0x61
                ,0x6d,0x65,0x6e,0x61,0x6d,0x65,0x00,0x77,0x6f,0x72,0x6d,0x73,0x33,0x00,0x68,0x6f
                ,0x73,0x74,0x6e,0x61,0x6d,0x65,0x00,0x74,0x65,0x73,0x74,0x00,0x67,0x61,0x6d,0x65
                ,0x6d,0x6f,0x64,0x65,0x00,0x6f,0x70,0x65,0x6e,0x73,0x74,0x61,0x67,0x69,0x6e,0x67
                ,0x00,0x67,0x72,0x6f,0x75,0x70,0x69,0x64,0x00,0x36,0x32,0x32,0x00,0x6e,0x75,0x6d
                ,0x70,0x6c,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6d,0x61,0x78,0x70,0x6c,0x61
                ,0x79,0x65,0x72,0x73,0x00,0x32,0x00,0x68,0x6f,0x73,0x74,0x6e,0x61,0x6d,0x65,0x00
                ,0x74,0x65,0x73,0x74,0x00,0x68,0x6f,0x73,0x74,0x70,0x6f,0x72,0x74,0x00,0x00,0x6d
                ,0x61,0x78,0x70,0x6c,0x61,0x79,0x65,0x72,0x73,0x00,0x32,0x00,0x6e,0x75,0x6d,0x70
                ,0x6c,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x53,0x63,0x68,0x65,0x6d,0x65,0x43
                ,0x68,0x61,0x6e,0x67,0x69,0x6e,0x67,0x00,0x30,0x00,0x67,0x61,0x6d,0x65,0x76,0x65
                ,0x72,0x00,0x31,0x30,0x37,0x33,0x00,0x67,0x61,0x6d,0x65,0x74,0x79,0x70,0x65,0x00
                ,0x00,0x6d,0x61,0x70,0x6e,0x61,0x6d,0x65,0x00,0x00,0x66,0x69,0x72,0x65,0x77,0x61
                ,0x6c,0x6c,0x00,0x30,0x00,0x70,0x75,0x62,0x6c,0x69,0x63,0x69,0x70,0x00,0x32,0x35
                ,0x35,0x2e,0x32,0x35,0x35,0x2e,0x32,0x35,0x35,0x2e,0x32,0x35,0x35,0x00,0x70,0x72
                ,0x69,0x76,0x61,0x74,0x65,0x69,0x70,0x00,0x31,0x39,0x32,0x2e,0x31,0x36,0x38,0x2e
                ,0x30,0x2e,0x36,0x30,0x00,0x67,0x61,0x6d,0x65,0x6d,0x6f,0x64,0x65,0x00,0x6f,0x70
                ,0x65,0x6e,0x73,0x74,0x61,0x67,0x69,0x6e,0x67,0x00,0x76,0x61,0x6c,0x00,0x30,0x00
                ,0x70,0x61,0x73,0x73,0x77,0x6f,0x72,0x64,0x00,0x30,0x00,0x00,0x00,0x01,0x70,0x6c
                ,0x61,0x79,0x65,0x72,0x5f,0x00,0x70,0x69,0x6e,0x67,0x5f,0x00,0x68,0x6f,0x73,0x74
                ,0x6e,0x61,0x6d,0x65,0x00,0x68,0x6f,0x73,0x74,0x70,0x6f,0x72,0x74,0x00,0x6d,0x61
                ,0x78,0x70,0x6c,0x61,0x79,0x65,0x72,0x73,0x00,0x6e,0x75,0x6d,0x70,0x6c,0x61,0x79
                ,0x65,0x72,0x73,0x00,0x53,0x63,0x68,0x65,0x6d,0x65,0x43,0x68,0x61,0x6e,0x67,0x69
                ,0x6e,0x67,0x00,0x67,0x61,0x6d,0x65,0x76,0x65,0x72,0x00,0x67,0x61,0x6d,0x65,0x74
                ,0x79,0x70,0x65,0x00,0x6d,0x61,0x70,0x6e,0x61,0x6d,0x65,0x00,0x66,0x69,0x72,0x65
                ,0x77,0x61,0x6c,0x6c,0x00,0x70,0x75,0x62,0x6c,0x69,0x63,0x69,0x70,0x00,0x70,0x72
                ,0x69,0x76,0x61,0x74,0x65,0x69,0x70,0x00,0x67,0x61,0x6d,0x65,0x6d,0x6f,0x64,0x65
                ,0x00,0x76,0x61,0x6c,0x00,0x70,0x61,0x73,0x73,0x77,0x6f,0x72,0x64,0x00,0x00,0x77
                ,0x6f,0x72,0x6d,0x73,0x31,0x30,0x00,0x30,0x00,0x00,0x00,0x00,0x00,0x00,0x31,0x30
                ,0x37,0x33,0x00,0x00,0x00,0x31,0x00,0x32,0x35,0x35,0x2e,0x32,0x35,0x35,0x2e,0x32
                ,0x35,0x35,0x2e,0x32,0x35,0x35,0x00,0x31,0x39,0x32,0x2e,0x31,0x36,0x38,0x2e,0x30
                ,0x2e,0x36,0x30,0x00,0x00,0x30,0x00,0x00,0x00,0x00,0x68,0x6f,0x73,0x74,0x6e,0x61
                ,0x6d,0x65,0x00,0x68,0x6f,0x73,0x74,0x70,0x6f,0x72,0x74,0x00,0x6d,0x61,0x78,0x70
                ,0x6c,0x61,0x79,0x65,0x72,0x73,0x00,0x6e,0x75,0x6d,0x70,0x6c,0x61,0x79,0x65,0x72
                ,0x73,0x00,0x53,0x63,0x68,0x65,0x6d,0x65,0x43,0x68,0x61,0x6e,0x67,0x69,0x6e,0x67
                ,0x00,0x67,0x61,0x6d,0x65,0x76,0x65,0x72,0x00,0x67,0x61,0x6d,0x65,0x74,0x79,0x70
                ,0x65,0x00,0x6d,0x61,0x70,0x6e,0x61,0x6d,0x65,0x00,0x66,0x69,0x72,0x65,0x77,0x61
                ,0x6c,0x6c,0x00,0x70,0x75,0x62,0x6c,0x69,0x63,0x69,0x70,0x00,0x70,0x72,0x69,0x76
                ,0x61,0x74,0x65,0x69,0x70,0x00,0x67,0x61,0x6d,0x65,0x6d,0x6f,0x64,0x65,0x00,0x76
                ,0x61,0x6c,0x00,0x70,0x61,0x73,0x73,0x77,0x6f,0x72,0x64,0x00,0x00 };
            var switcher = new CmdSwitcher(_client, raw);
            switcher.Switch();
        }
    }
}