﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RetroSpyServer.Servers.Chat.Structures
{
    public class GSPeerChatCTX
    {
        public byte GSPeerChat1;
        public byte GSPeerChat2;
        public byte[] GSPeerChatCrypt = new byte[256];
    }
}
