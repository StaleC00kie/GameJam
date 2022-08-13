using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks.Data;
using UnityEngine;

namespace Utils.Steamworks
{
    public static class SteamworksLobbyUtils
    {
        private static List<Image?> avatars = new List<Image?>();

        #region Static Async Methods
        
        private static async Task<Image?[]> GetAvatar(Lobby lobby)
        {
            try
            {
                // Get Avatar using await
                //return await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);
                foreach (var member in lobby.Members)
                {
                    Image? avatar = await member.GetLargeAvatarAsync();

                    avatars.Add(avatar);
                }

                return avatars.ToArray();
            }
            catch (Exception e)
            {
                // If something goes wrong, log it
                Debug.Log(e);
                return null;
            }
        }

        #endregion

        #region Static Methods



        #endregion
    }

}
