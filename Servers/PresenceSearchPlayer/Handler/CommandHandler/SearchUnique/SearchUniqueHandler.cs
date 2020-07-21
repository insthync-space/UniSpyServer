﻿using GameSpyLib.Common.Entity.Interface;
using GameSpyLib.Database.DatabaseModel.MySql;
using PresenceSearchPlayer.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PresenceSearchPlayer.Handler.CommandHandler.SearchUnique
{
    internal class SearchUniqueDBResult
    {
        public uint Profileid;
        public string Nick;
        public string Uniquenick;
        public string Email;
        public string Firstname;
        public string Lastname;
        public uint NamespaceID;
    }

    /// <summary>
    /// Search with uniquenick and namespace
    /// </summary>
    public class SearchUniqueHandler : PSPCommandHandlerBase
    {
        protected new SearchUniqueRequestModel _request;
        private List<SearchUniqueDBResult> _result;
        public SearchUniqueHandler(ISession client, Dictionary<string, string> recv) : base(client, recv)
        {
            _request = new SearchUniqueRequestModel(recv);
        }

        protected override void CheckRequest()
        {
            _errorCode = _request.Parse();
        }

        protected override void DataOperation()
        {
            using (var db = new retrospyContext())
            {
                foreach (var id in _request.Namespaces)
                {
                    var result = from p in db.Profiles
                                 join n in db.Subprofiles on p.Profileid equals n.Profileid
                                 join u in db.Users on p.Userid equals u.Userid
                                 where n.Uniquenick == _request.Uniquenick
                                 && n.Namespaceid == id
                                 select new SearchUniqueDBResult
                                 {
                                     Profileid = n.Profileid,
                                     Nick = p.Nick,
                                     Uniquenick = n.Uniquenick,
                                     Email = u.Email,
                                     Firstname = p.Firstname,
                                     Lastname = p.Lastname,
                                     NamespaceID = n.Namespaceid
                                 };
                    _result = result.ToList();
                }
            }
        }

        protected override void ConstructResponse()
        {
            _sendingBuffer = @"\bsr";
            foreach (var info in _result)
            {
                _sendingBuffer += info.Profileid;
                _sendingBuffer += @"\nick\" + info.Nick;
                _sendingBuffer += @"\uniquenick\" + info.Uniquenick;
                _sendingBuffer += @"\namespaceid\" + info.NamespaceID;
                _sendingBuffer += @"\firstname\" + info.Firstname;
                _sendingBuffer += @"\lastname\" + info.Lastname;
                _sendingBuffer += @"\email\" + info.Email;
            }
            _sendingBuffer += @"\bsrdone\\more\0";

            base.ConstructResponse();
        }
    }
}
