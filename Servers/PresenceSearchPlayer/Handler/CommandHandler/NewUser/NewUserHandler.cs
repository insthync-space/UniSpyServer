﻿using GameSpyLib.Common;
using GameSpyLib.Common.Entity.Interface;
using GameSpyLib.Database.DatabaseModel.MySql;
using PresenceSearchPlayer.Enumerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PresenceSearchPlayer.Handler.CommandHandler.NewUser
{
    public class NewUserHandler : PSPCommandHandlerBase
    {
        private Users _users;
        private Profiles _profiles;
        private Subprofiles _subProfiles;
        protected new NewUserRequestModel _request;
        private Dictionary<string, string> _recv;
        public NewUserHandler(ISession client, Dictionary<string, string> recv) : base(client, recv)
        {
            _recv = recv;
            _request = new NewUserRequestModel(recv);
        }

        protected enum _newUserStatus
        {
            CheckAccount,
            AccountNotExist,
            AccountExist,
            CheckProfile,
            ProfileNotExist,
            ProfileExist,
            CheckSubProfile,
            SubProfileNotExist,
            SubProfileExist
        }

        protected override void CheckRequest()
        {
            _errorCode = _request.Parse();
        }

        protected override void DataOperation()
        {
            using (var db = new retrospyContext())
            {
                try
                {
                    switch (_newUserStatus.CheckAccount)
                    {
                        case _newUserStatus.CheckAccount:
                            int count = db.Users.Where(u => u.Email == _request.Email).Select(u => u).Count();
                            if (count == 0)
                            {
                                goto case _newUserStatus.AccountNotExist;
                            }
                            else
                            {
                                goto case _newUserStatus.AccountExist;
                            }

                        case _newUserStatus.AccountNotExist:
                            _users = new Users { Email = _request.Email, Password = _request.PassEnc };
                            db.Users.Add(_users);
                            db.SaveChanges();
                            goto case _newUserStatus.CheckProfile;

                        case _newUserStatus.AccountExist:
                            //we have to check password correctness
                            _users = db.Users.Where(u => u.Email == _request.Email && u.Password == _request.PassEnc).FirstOrDefault();
                            if (_users == null)
                            {
                                _errorCode = GPErrorCode.NewUserBadPasswords;
                                break;
                            }
                            else
                            {
                                goto case _newUserStatus.CheckProfile;
                            }

                        case _newUserStatus.CheckProfile:
                            _profiles = db.Profiles.Where(p => p.Userid == _users.Userid && p.Nick == _request.Nick).FirstOrDefault();
                            if (_profiles == null)
                            {
                                goto case _newUserStatus.ProfileNotExist;
                            }
                            else
                            {
                                goto case _newUserStatus.ProfileExist;
                            }

                        case _newUserStatus.ProfileNotExist:
                            _profiles = new Profiles { Userid = _users.Userid, Nick = _request.Nick };
                            db.Profiles.Add(_profiles);
                            db.SaveChanges();
                            goto case _newUserStatus.CheckSubProfile;

                        case _newUserStatus.ProfileExist:
                        //we do nothing here

                        case _newUserStatus.CheckSubProfile:
                            _subProfiles = db.Subprofiles
                                .Where(s => s.Profileid == _profiles.Profileid
                                && s.Uniquenick == _request.Uniquenick
                                && s.Namespaceid == _request.NamespaceID).FirstOrDefault();
                            if (_subProfiles == null)
                            {
                                goto case _newUserStatus.SubProfileNotExist;
                            }
                            else
                            {
                                goto case _newUserStatus.SubProfileExist;
                            }

                        case _newUserStatus.SubProfileNotExist:
                            //we create subprofile and return
                            _subProfiles = new Subprofiles
                            {
                                Profileid = _profiles.Profileid,
                                Uniquenick = _request.Uniquenick,
                                Namespaceid = _request.NamespaceID
                            };

                            db.Subprofiles.Add(_subProfiles);
                            db.SaveChanges();
                            break;

                        case _newUserStatus.SubProfileExist:
                            _errorCode = GPErrorCode.NewUserUniquenickInUse;
                            break;
                    }
                }
                catch (Exception)
                {
                    _errorCode = GPErrorCode.DatabaseError;
                }

                //update other information
                if (_errorCode != GPErrorCode.DatabaseError)
                {
                    UpdateOtherInfo();
                }
            }
        }

        protected override void ConstructResponse()
        {
            if (_errorCode != GPErrorCode.NoError)
            {
                _sendingBuffer = $@"\nur\{_errorCode}";
                return;
            }

            if (ServerManagerBase.ServerName
                == RetroSpyServerName.PresenceSearchPlayer)
            {
                //PSP NewUser
                _sendingBuffer = $@"\nur\0\pid\{_subProfiles.Profileid}";
            }
            else if (ServerManagerBase.ServerName
                == RetroSpyServerName.PresenceConnectionManager)
            {
                //PCM NewUser
                _sendingBuffer = $@"\nur\0\userid\{_users.Userid}\profileid\{_subProfiles.Profileid}";
            }
            base.ConstructResponse();
        }

        private void UpdateOtherInfo()
        {
            using (var db = new retrospyContext())
            {
                uint partnerid;

                if (_recv.ContainsKey("partnerid"))
                {
                    if (uint.TryParse(_recv["partnerid"], out partnerid))
                    {
                        _subProfiles.Partnerid = partnerid;
                    }
                    else
                    {
                        _errorCode = GPErrorCode.Parse;
                    }
                }

                uint productid;

                if (_recv.ContainsKey("productid"))
                {
                    if (uint.TryParse(_recv["productid"], out productid))
                    {
                        _subProfiles.Productid = productid;
                    }
                    else
                    {
                        _errorCode = GPErrorCode.Parse;
                    }
                }

                if (_recv.ContainsKey("gamename"))
                {
                    _subProfiles.Gamename = _recv["gamename"];
                }

                uint port;

                if (_recv.ContainsKey("port"))
                {
                    if (uint.TryParse(_recv["port"], out port))
                    {
                        _subProfiles.Port = port;
                    }
                    else
                    {
                        _errorCode = GPErrorCode.Parse;
                    }
                }

                if (_recv.ContainsKey("cdkeyenc"))
                {
                    _subProfiles.Cdkeyenc = _recv["cdkeyenc"];
                }
                db.Subprofiles.Update(_subProfiles);
                db.SaveChanges();
            }
        }
    }
}
