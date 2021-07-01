﻿using QueryReport.Abstraction.BaseClass;
using QueryReport.Entity.Enumerate;
using System.Collections.Generic;
using UniSpyLib.Abstraction.BaseClass;

namespace QueryReport.Entity.Structure.Response
{
    internal sealed class AvaliableResponse : QRResponseBase
    {
        public static readonly byte[] ResponsePrefix = { 0xfe, 0xfd, 0x09, 0x00, 0x00, 0x00 };

        public AvaliableResponse(UniSpyRequest request, UniSpyResult result) : base(request, result)
        {
        }

        public override void Build()
        {
            List<byte> buffer = new List<byte>();

            buffer.AddRange(ResponsePrefix);
            buffer.Add((byte)ServerAvailability.Available);
            // NOTE: Change this if you want to make the server not avaliable.
            SendingBuffer = buffer.ToArray();
        }
    }
}
