﻿using GameStatus.Abstraction.BaseClass;
using GameStatus.Entity.Structure.Request;
using GameStatus.Entity.Structure.Result;
using System;
using UniSpyLib.Abstraction.BaseClass;

namespace GameStatus.Entity.Structure.Response
{
    internal sealed class UdpGameResponse : GSResponseBase
    {
        private new UpdGameRequest _request => (UpdGameRequest)base._request;
        private new UdpGameResult _result => (UdpGameResult)base._result;
        public UdpGameResponse(UniSpyRequestBase request, UniSpyResultBase result) : base(request, result)
        {
        }

        protected override void BuildNormalResponse()
        {
            throw new NotImplementedException();
        }
    }
}
