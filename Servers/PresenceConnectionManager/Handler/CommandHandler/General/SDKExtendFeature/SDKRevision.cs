﻿using System;
using System.Collections;
using PresenceConnectionManager.Entity.Structure;

namespace PresenceConnectionManager.Handler.General.SDKExtendFeature
{
    public static class SDKRevision
    {
        /// <summary>
        /// Tell server send back extra information according to the number of  sdkrevision
        /// </summary>
        public static void ExtendedFunction(GPCMSession session)
        {
            if (session.UserInfo.SDKRevision == 0)
            {
                session.ToLog(GameSpyLib.Logging.LogLevel.Error, "[SDKRev] No sdkrevision!");
                return;
            }

            if ((session.UserInfo.SDKRevision ^ (uint)SDKRevisionType.GPINewAuthNotification) != 0)
            {
                //Send add friend request
            }

            if ((session.UserInfo.SDKRevision ^ (uint)SDKRevisionType.GPINewRevokeNotification) != 0)
            {
                //send revoke request
            }

            if ((session.UserInfo.SDKRevision ^ (uint)SDKRevisionType.GPINewStatusNotification) != 0)
            {
                //send new status info
            }
     
            if ((session.UserInfo.SDKRevision ^ (uint)SDKRevisionType.GPINewListRetrevalOnLogin) != 0)
            {
                //send buddy list and block list
            }

            if ((session.UserInfo.SDKRevision ^ (uint)SDKRevisionType.GPIRemoteAuthIDSNotification)!=0)
            {
                //Remote auth
            }

            if ((session.UserInfo.SDKRevision ^ (uint)SDKRevisionType.GPINewCDKeyRegistration) != 0)
            {
                //register cdkey with product id
            }
        }
    }
}
