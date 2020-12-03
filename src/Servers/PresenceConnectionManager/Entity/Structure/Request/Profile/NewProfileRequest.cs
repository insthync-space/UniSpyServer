﻿using PresenceConnectionManager.Abstraction.BaseClass;
using PresenceSearchPlayer.Entity.Enumerate;
using System.Collections.Generic;

namespace PresenceConnectionManager.Entity.Structure.Request.Profile
{
    public class NewProfileRequest : PCMRequestBase
    {
        //create a new profile with new nick 
        // @"  \newprofile\sesskey\<>\nick\<>\id\1\final\"
        //replace a existed nick with new nick
        //@"  \newprofile\sesskey\<>\nick\<>\replace\1\oldnick\<>\id\1\final\"

        public NewProfileRequest(string rawRequest) : base(rawRequest)
        {
        }

        public string OldNick { get; protected set; }
        public string NewNick { get; protected set; }
        public bool IsReplaceNickName { get; protected set; }
        public override object Parse()
        {
            var flag = (GPErrorCode)base.Parse();
            if (flag != GPErrorCode.NoError)
            {
                return flag;
            }

            if (_recv.ContainsKey("replace"))
            {
                if (!_recv.ContainsKey("oldnick") && !_recv.ContainsKey("nick"))
                {
                    return GPErrorCode.Parse;
                }

                OldNick = _recv["oldnick"];
                NewNick = _recv["nick"];
                IsReplaceNickName = true;
            }
            else
            {
                if (!_recv.ContainsKey("nick"))
                {
                    return GPErrorCode.Parse;
                }
                NewNick = _recv["nick"];
                IsReplaceNickName = false;
            }

            return GPErrorCode.NoError;
        }
    }
}
