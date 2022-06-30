using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public enum LobbyType
{
    Public,
    Friends,
    Private,
    Invisible,
}


public class Lobby : MonoBehaviour
{
    #region Public Fields


    #endregion

    #region Private Fields


    private string lobbyName = "LobbyName";

    private NetworkRoomManager networkRoomManager;

    #endregion


    #region Mono Behaviour Methods
    private void Awake()
    {
        networkRoomManager = FindObjectOfType<NetworkRoomManager>();
    }

    public void Start()
    {

    }

    public async void CreateLobby(LobbyType lobbyType)
    {
        var lobby = await SteamMatchmaking.CreateLobbyAsync(networkRoomManager.maxConnections);

        switch (lobbyType)
        {
            case LobbyType.Public:
                lobby.Value.SetPublic();
                break;
            case LobbyType.Friends:
                lobby.Value.SetFriendsOnly();
                break;
            case LobbyType.Private:
                lobby.Value.SetPrivate();
                break;
            case LobbyType.Invisible:
                lobby.Value.SetInvisible();
                break;
            default:
                break;
        }
    }

    public static bool IsFriendPlaying(Friend friend)
    {
        bool isValid = (friend.GameInfo.HasValue ? true : false) && friend.GameInfo.Value.Lobby.HasValue ? true : false;
        if (isValid)
        {
            return true; // Is in a lobby and is playing this game.
        }

        return false; // Is either not in a lobby or not playing this game.
    }


    #endregion

}