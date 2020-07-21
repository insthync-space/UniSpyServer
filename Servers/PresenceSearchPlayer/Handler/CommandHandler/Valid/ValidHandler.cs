﻿using GameSpyLib.Common.Entity.Interface;
using GameSpyLib.Database.DatabaseModel.MySql;
using GameSpyLib.MiscMethod;
using PresenceSearchPlayer.Enumerator;
using System.Collections.Generic;
using System.Linq;

namespace PresenceSearchPlayer.Handler.CommandHandler.Valid
{
    public class ValidHandler : PSPCommandHandlerBase
    {
        protected new ValidRequestModel _request;
        private bool _isAccountValid;
        public ValidHandler(ISession client, Dictionary<string, string> recv) : base(client, recv)
        {
            _request = new ValidRequestModel(recv);
        }

        protected override void CheckRequest()
        {
            _errorCode = _request.Parse();
        }

        protected override void DataOperation()
        {
            using (var db = new retrospyContext())
            {
                var result = from u in db.Users
                             join p in db.Profiles on u.Userid equals p.Userid
                             join n in db.Subprofiles on p.Profileid equals n.Profileid
                             //According to FSW partnerid is not nessesary
                             where u.Email == _request.Email
                             && n.Gamename == _request.GameName
                             && n.Namespaceid == _request.NamespaceID
                             select p.Profileid;

                if (result.Count() == 0)
                {
                    _isAccountValid = false;
                }
                else if (result.Count() == 1)
                {
                    _sendingBuffer = @"\vr\1\final\";
                }

            }

        }
        protected override void ConstructResponse()
        {
            if (_isAccountValid)
            {
                _sendingBuffer = @"\vr\1";
            }
            else
            {
                _sendingBuffer = @"\vr\0";
            }

            base.ConstructResponse();
        }
    }
}
