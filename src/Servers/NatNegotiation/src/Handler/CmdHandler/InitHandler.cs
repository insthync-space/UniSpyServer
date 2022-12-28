using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniSpyServer.Servers.NatNegotiation.Abstraction.BaseClass;
using UniSpyServer.Servers.NatNegotiation.Application;
using UniSpyServer.Servers.NatNegotiation.Entity.Enumerate;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Redis;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Request;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Response;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Result;
using UniSpyServer.UniSpyLib.Abstraction.Interface;

namespace UniSpyServer.Servers.NatNegotiation.Handler.CmdHandler
{

    public sealed class InitHandler : CmdHandlerBase
    {
        private new InitRequest _request => (InitRequest)base._request;
        private new InitResult _result { get => (InitResult)base._result; set => base._result = value; }
        /// <summary>
        /// Local NatInitInfo storage, after all init packets are received we send all into redis database
        /// </summary>
        public static Dictionary<string, NatInitInfo> LocalInitInfoPool;
        private NatAddressInfo _addressInfo;
        /// <summary>
        /// The current init info of the client.
        /// </summary>
        private NatInitInfo _initInfo;
        /// <summary>
        /// The key using to search on local storage InitInfoPool to get the client init info.
        /// </summary>
        private string _searchKey;
        static InitHandler()
        {
            LocalInitInfoPool = new Dictionary<string, NatInitInfo>();
        }
        public InitHandler(IClient client, IRequest request) : base(client, request)
        {
            _result = new InitResult();
        }
        protected override void RequestCheck()
        {
            base.RequestCheck();
            _searchKey = NatInitInfo.CreateKey((uint)_request.Cookie, (NatClientIndex)_request.ClientIndex, (uint)_request.Version);
            // note: async socket may cause problem when adding _initInfo to _initInfoPool, requires a lock
            _initInfo = new NatInitInfo()
            {
                ServerID = _client.Connection.Server.ServerID,
                Cookie = (uint)_request.Cookie,
                UseGamePort = _request.UseGamePort,
                ClientIndex = (NatClientIndex)_request.ClientIndex,
                Version = _request.Version,
            };
            lock (LocalInitInfoPool)
            {
                // we only have add or get in the lock to reduce performance cost on create object
                if (!LocalInitInfoPool.Keys.Contains(_searchKey))
                {
                    LocalInitInfoPool.TryAdd(_searchKey, _initInfo);
                }
                else
                {
                    _initInfo = LocalInitInfoPool[_searchKey];
                }
            }
        }
        protected override void DataOperation()
        {
            _addressInfo = new NatAddressInfo()
            {
                Version = _request.Version,
                PortType = _request.PortType,
                PublicIPEndPoint = _client.Connection.RemoteIPEndPoint,
                PrivateIPEndPoint = _request.PrivateIPEndPoint
            };
            _client.LogInfo($"Received init request with private ip: [{_addressInfo.PrivateIPEndPoint}], cookie: {_initInfo.Cookie}, client index: {_initInfo.ClientIndex}.");
            // note: async socket may cause problem when adding _mappingInfo to _initInfo.NatMappingInfos, requires a lock
            lock (_initInfo)
            {
                if (!_initInfo.AddressInfos.ContainsKey((NatPortType)_request.PortType))
                {
                    _initInfo.AddressInfos.Add((NatPortType)_request.PortType, _addressInfo);
                }
            }
            _result.RemoteIPEndPoint = _client.Connection.RemoteIPEndPoint;
        }
        protected override void ResponseConstruct()
        {
            _response = new InitResponse(_request, _result);
        }
        protected override void Response()
        {
            base.Response();

            if (_client.Info.ClientIndex is null)
            {
                _client.Info.ClientIndex = _request.ClientIndex;
            }
            if (_client.Info.Cookie is null)
            {
                _client.Info.Cookie = _request.Cookie;
            }
            // note: async socket may cause multiple call of natnegotiation, requires a lock
            lock (_initInfo)
            {
                if (_initInfo.AddressInfos.ContainsKey(NatPortType.NN1)
                && _initInfo.AddressInfos.ContainsKey(NatPortType.NN2)
                && _initInfo.AddressInfos.ContainsKey(NatPortType.NN3)
                && _initInfo.isNegotiating == false)
                {
                    _initInfo.isNegotiating = true;
                    // we have use sync code to make sure the data is saved on redis
                    StorageOperation.Persistance.UpdateInitInfo(_initInfo);
                    Task.Run(() => PrepareForConnecting());
                }
            }
        }


        /// <summary>
        /// Prepare to send connect response
        /// </summary>
        private void PrepareForConnecting()
        {
            _client.LogInfo($"Watting for negotiator's initInfo with cookie:{_initInfo.Cookie}.");
            int waitCount = 1;
            // we only wait 8 seconds
            while (waitCount <= 4)
            {
                var initCount = StorageOperation.Persistance.CountInitInfo((uint)_request.Cookie, (byte)_request.Version);
                if (initCount == 2)
                {
                    _client.LogInfo("2 neigotiators found, start negotiating.");
                    StartConnecting();
                    break;
                }
                else
                {
                    _client.LogInfo($"Have no negotiator found with cookie: {_initInfo.Cookie}, retry count: {waitCount}.");
                }
                waitCount++;
                Thread.Sleep(2000);
            }
            // if server can not find the client2 within 8 retry, then we log the error. 
            if (waitCount > 4)
            {
                _client.LogWarn($"cookie: {_initInfo.Cookie} have no negotiator found , we clean init information, please connect again.");
                lock (LocalInitInfoPool)
                {
                    if (LocalInitInfoPool.ContainsKey(_searchKey))
                    {
                        LocalInitInfoPool.Remove(_searchKey);
                    }
                }
            }
        }

        /// <summary>
        ///  Start connect handler to tell each other's public ip and port
        /// </summary>
        private void StartConnecting()
        {
            var request = new ConnectRequest
            {
                Version = _initInfo.Version,
                Cookie = _initInfo.Cookie,
                ClientIndex = _initInfo.ClientIndex,
                IsUsingRelay = _initInfo.IsUsingRelayServer
            };
            new ConnectHandler(_client, request).Handle();
        }


    }
}
