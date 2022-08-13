using Steamworks;
using System;
using System.Threading.Tasks;
using Steamworks.Data;
using UnityEngine;

namespace Utils.Steamworks
{
    public static class SteamworksFriendUtils
    {
        private static async Task<Image?> GetAvatar()
        {
            try
            {
                // Get Avatar using await
                return await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);
            }
            catch (Exception e)
            {
                // If something goes wrong, log it
                Debug.Log(e);
                return null;
            }
        }

        public static Texture2D ConvertToTexture2D(this Image image)
        {
            // Create a new Texture2D
            var avatar = new Texture2D((int)image.Width, (int)image.Height, TextureFormat.ARGB32, false);

            // Set filter type, or else its really blurry
            avatar.filterMode = FilterMode.Trilinear;

            // Flip image
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var p = image.GetPixel(x, y);
                    avatar.SetPixel(x, (int)image.Height - y, new UnityEngine.Color(p.r / 255.0f, p.g / 255.0f, p.b / 255.0f, p.a / 255.0f));
                }
            }

            avatar.Apply();
            return avatar;
        }
    }

}
