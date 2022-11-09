using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Redis;
using UniSpyServer.Servers.NatNegotiation.Handler;
using UniSpyServer.UniSpyLib.Abstraction.Interface;
using Xunit;

namespace UniSpyServer.Servers.NatNegotiation.Test
{
    public class GameTest
    {
        [Fact]
        public void NatFullConeTest()
        {
            // clean all stuff in database
            new RedisClient().Db.Execute("FLUSHALL");

            var client = TestClasses.CreateClient("192.168.1.2", 9999);
            var server = TestClasses.CreateClient("192.168.1.3", 9999);
            var clientInitGP = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x00, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x01, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x02, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xBB, 0x37, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN3 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x03, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xBB, 0x37, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };

            var serverInitGP = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x00, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x01, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x02, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xD2, 0xAE, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN3 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x03, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xD2, 0xAE, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientRequests = new List<byte[]> { clientInitGP, clientInitNN1, clientInitNN2, clientInitNN3 };
            var serverRequests = new List<byte[]> { serverInitGP, serverInitNN1, serverInitNN2, serverInitNN3 };


            foreach (var request in clientRequests)
            {
                ((ITestClient)client).TestReceived(request);
            }
            foreach (var request in serverRequests)
            {
                ((ITestClient)server).TestReceived(request);
            }
            // // because the process is running in background we need to wait it finish, so we can debug
            Thread.Sleep(10000);
            // Console.Read();
        }

        [Fact]
        public void NatConePortIncrement()
        {
            // clean all stuff in database
            new RedisClient().Db.Execute("FLUSHALL");

            var client = TestClasses.CreateClient("192.168.1.2", 53935);
            var server = TestClasses.CreateClient("192.168.1.3", 53935);

            var clientInitGP = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x00, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x01, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x02, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xBB, 0x37, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN3 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x03, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xBB, 0x37, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };

            var serverInitGP = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x00, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x01, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x02, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xD2, 0xAE, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN3 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x03, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xD2, 0xAE, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientRequests = new List<byte[]> { clientInitGP, clientInitNN1, clientInitNN2, clientInitNN3 };
            var serverRequests = new List<byte[]> { serverInitGP, serverInitNN1, serverInitNN2, serverInitNN3 };

            foreach (var request in clientRequests)
            {
                ((ITestClient)client).TestReceived(request);
            }
            foreach (var request in serverRequests)
            {
                ((ITestClient)server).TestReceived(request);
            }
            // because the process is running in background we need to wait it finish, so we can debug
            Thread.Sleep(10000);
        }

        [Fact]
        public void Anno1701()
        {
            var client1GP = TestClasses.CreateClient("31.18.120.193", 21701);
            var client1NN1 = TestClasses.CreateClient("31.18.120.193", 51463);
            var client1NN2 = TestClasses.CreateClient("31.18.120.193", 51463);
            var client1NN3 = TestClasses.CreateClient("31.18.120.193", 51463);

            var client2GP = TestClasses.CreateClient("79.209.224.29", 21701);
            var client2NN1 = TestClasses.CreateClient("79.209.224.29", 51463);
            var client2NN2 = TestClasses.CreateClient("79.209.224.29", 51463);
            var client2NN3 = TestClasses.CreateClient("79.209.224.29", 51463);


            var client3GP = TestClasses.CreateClient("79.209.224.29", 1024);
            var client3NN1 = TestClasses.CreateClient("79.209.224.29", 55111);
            var client3NN2 = TestClasses.CreateClient("79.209.224.29", 55111);
            var client3NN3 = TestClasses.CreateClient("79.209.224.29", 55111);

            var clients = new Dictionary<string, IClient>()
            {
                {"client1GP",client1GP},
                {"client1NN1",client1NN1},
                {"client1NN2",client1NN2},
                {"client1NN3",client1NN3},

                {"client2GP",client1GP},
                {"client2NN1",client1NN1},
                {"client2NN2",client1NN2},
                {"client2NN3",client1NN3},

            };
            var requests = new List<KeyValuePair<string, byte[]>>()
            {
                new KeyValuePair<string, byte[]>("client1GP",new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x00,0x00,0x01,0xC0,0xA8,0x00,0xD5,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client1NN1",new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x01,0x00,0x01,0xC0,0xA8,0x00,0xD5,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client1NN2",new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x02,0x00,0x01,0xC0,0xA8,0x00,0xD5,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client1NN3",new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x03,0x00,0x01,0xC0,0xA8,0x00,0xD5,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),

                new KeyValuePair<string, byte[]>("client2GP",new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x00,0x01,0x01,0xC0,0xA8,0x00,0x32,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client2NN1",new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x01,0x01,0x01,0xC0,0xA8,0x00,0x32,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client2NN2",new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x02,0x01,0x01,0xC0,0xA8,0x00,0x32,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client2NN3",new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x03,0x01,0x01,0xC0,0xA8,0x00,0x32,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
            };

            foreach (var req in requests)
            {
                ((ITestClient)clients[req.Key]).TestReceived(req.Value);
            }
            // because the process is running in background we need to wait it finish, so we can debug
            Thread.Sleep(10000);
        }
    }
}