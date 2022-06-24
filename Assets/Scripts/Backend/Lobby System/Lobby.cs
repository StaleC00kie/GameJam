using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Epic.OnlineServices.Lobby;

public class Lobby : EOSLobby
{
    private List<Attribute> lobbyData = new List<Attribute>();
    private List<LobbyDetails> foundLobbies = new List<LobbyDetails>();

    private NetworkManager networkManager;

    private void Awake()
    {
        networkManager = GetComponent<NetworkManager>();
    }

    private void OnEnable()
    {
        //subscribe to events
        CreateLobbySucceeded += OnCreateLobbySuccess;
        JoinLobbySucceeded += OnJoinLobbySuccess;
        FindLobbiesSucceeded += OnFindLobbiesSuccess;
        LeaveLobbySucceeded += OnLeaveLobbySuccess;
    }

    private void OnDisable()
    {
        //unsubscribe from events
        CreateLobbySucceeded -= OnCreateLobbySuccess;
        JoinLobbySucceeded -= OnJoinLobbySuccess;
        FindLobbiesSucceeded -= OnFindLobbiesSuccess;
        LeaveLobbySucceeded -= OnLeaveLobbySuccess;
    }

    private void OnCreateLobbySuccess(List<Attribute> attributes)
    {
        lobbyData = attributes;
        networkManager.StartHost();
    }

    private void OnJoinLobbySuccess(List<Attribute> attributes)
    {
        lobbyData = attributes;
        networkManager.networkAddress = attributes.Find((x) => x.Data.Key == hostAddressKey).Data.Value.AsUtf8;
        networkManager.StartClient();
    }

    private void OnFindLobbiesSuccess(List<LobbyDetails> lobbiesFound)
    {
        foundLobbies = lobbiesFound;
    }

    private void OnLeaveLobbySuccess()
    {
        networkManager.StopHost();
        networkManager.StopClient();
    }
}