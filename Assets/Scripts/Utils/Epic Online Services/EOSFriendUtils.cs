using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Epic;
using Epic.OnlineServices;
using Epic.OnlineServices.Friends;
using Epic.OnlineServices.Presence;
using EpicTransport;
using System;
using System.Reflection;

namespace Utils.EOSFriends
{
    public class EOSFriendUtils : MonoBehaviour
    {

        private static Func<int, object> function;

        #region Public Static Fields;

        public static List<EpicAccountId> friends = new List<EpicAccountId>();
        public static List<EpicAccountId> onlineFriends = new List<EpicAccountId>();

        public static bool Initialized = false;

        #endregion

        #region Private Static Fields

        private static FriendsInterface friendsInterface;
        private static PresenceInterface presenceInterface;

        #endregion


        #region Public Static Events & Delegates

        public delegate void GetOnlineFriendsCountCallback(int friendsCount);

        public static event GetOnlineFriendsCountCallback OnGetOnlineFriendsCountCallback;

        public delegate EpicAccountId[] GetOnlineFriends(EpicAccountId[] friends);

        public event GetOnlineFriends OnGetOnlineFriends;

        #endregion

        #region Public Static Methods


        /// <summary>
        /// Initializes the required variables (FriendsInterface, PresenceInterface, etc.). This function must be called once before calling any other function
        /// </summary>
        public static void Initialize()
        {
            CacheInterfaces();

            Initialized = true;
        }

        public static void GetOnlineFriendsCount(Func<int, object> func)
        {
            if(Initialized == false)
            {
                Initialize();
            }

            function = func;

            QueryFriendsOptions queryFriendsOptions = new QueryFriendsOptions
            {
                LocalUserId = EOSSDKComponent.LocalUserAccountId,
            };

            friendsInterface.QueryFriends(queryFriendsOptions, null, OnQueriedFriends);
        }



        #endregion

        #region Private Static Methods

        private static void CacheInterfaces()
        {
            friendsInterface = EOSSDKComponent.GetFriendsInterface();
            presenceInterface = EOSSDKComponent.GetPresenceInterface();
        }

        private static void CheckStatus(EpicAccountId friendAccountId)
        {
            QueryPresenceOptions queryPresenceOptions = new QueryPresenceOptions
            {
                LocalUserId = EOSSDKComponent.LocalUserAccountId,
                TargetUserId = friendAccountId,
            };

            presenceInterface.QueryPresence(queryPresenceOptions, null, OnQueryPresence);
        }

        #endregion

        #region Public Methods



        #endregion

        #region Epic Online Services Callbacks

        private static void OnQueriedFriends(QueryFriendsCallbackInfo data)
        {
            GetFriendsCountOptions getFriendsCountOptions = new GetFriendsCountOptions
            {
                LocalUserId = EOSSDKComponent.LocalUserAccountId,
            };

            int friendCount = friendsInterface.GetFriendsCount(getFriendsCountOptions);

            for (int i = 0; i < friendCount; i++)
            {

                GetFriendAtIndexOptions getFriendAtIndexOptions = new GetFriendAtIndexOptions
                {
                    Index = i,
                    LocalUserId = EOSSDKComponent.LocalUserAccountId,
                };

                EpicAccountId currentFriendId = friendsInterface.GetFriendAtIndex(getFriendAtIndexOptions);

                CheckStatus(currentFriendId);

            }

            function.Invoke(onlineFriends.Count);
        }

        private static void OnQueryPresence(QueryPresenceCallbackInfo data)
        {
            CopyPresenceOptions copyPresenceOptions = new CopyPresenceOptions
            {
                LocalUserId = EOSSDKComponent.LocalUserAccountId,
                TargetUserId = data.TargetUserId,
            };

            Info info;

            presenceInterface.CopyPresence(copyPresenceOptions, out info);

            if(info.Status == Status.Online)
            {
                onlineFriends.Add(data.TargetUserId);
            }
        }

        #endregion
    }
}

