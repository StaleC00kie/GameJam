using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Epic.OnlineServices.Lobby;

public class Lobby : EOSLobby
{
    #region Public Fields

    [HideInInspector]
    public List<LobbyDetails> foundLobbies = new List<LobbyDetails>();

    #endregion

    #region Private Fields
    private List<Attribute> lobbyData = new List<Attribute>();


    private string lobbyName = "LobbyName";

    private NetworkRoomManager networkRoomManager;

    private bool lobbyExists;

    #endregion

    #region Mono Behaviour Methods
    private void Awake()
    {
        networkRoomManager = FindObjectOfType<NetworkRoomManager>();
    }

    public override void Start()
    {
        base.Start();


    }

    public void Create()
    {
        CreateLobby((uint)networkRoomManager.maxConnections,
            LobbyPermissionLevel.Joinviapresence,
            true,
            new AttributeData[] { new AttributeData { Key = AttributeKeys[0], Value = lobbyName },
            });
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

    #endregion

    #region Epic Online Services Callbacks

    private void OnCreateLobbySuccess(List<Attribute> attributes)
    {
        lobbyData = attributes;
        Debug.Log("Created Lobby Successfully");
        networkRoomManager.StartHost();
    }

    private void OnJoinLobbySuccess(List<Attribute> attributes)
    {
        lobbyData = attributes;
        networkRoomManager.networkAddress = attributes.Find((x) => x.Data.Key == hostAddressKey).Data.Value.AsUtf8;
        networkRoomManager.StartClient();
    }

    private void OnFindLobbiesSuccess(List<LobbyDetails> lobbiesFound)
    {
        foundLobbies = lobbiesFound;
    }

    private void OnLeaveLobbySuccess()
    {
        networkRoomManager.StopHost();
        networkRoomManager.StopClient();
    }

    #endregion
}