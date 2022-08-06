using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpicTransport;
using Steamworks;
using Mirror;

public class LobbyManager : MonoBehaviour
{
    #region Public Static Fields

    public static LobbyManager Instance;

    #endregion

    #region Public Fields

    #endregion

    #region SerializeFields

    #endregion

    #region Private Fields

    private NetworkRoomManager networkRoomManager;

    private GenerateFriendButtons generateFriendButtons;

    private Lobby lobby;

    #endregion

    #region Mono Behaviours Methods
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Start()
    {
        try
        {
            SteamClient.Init(480, true);

        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }

        InitUser();


    }

    public void OnApplicationQuit()
    {
        SteamClient.Shutdown();
    }

    #endregion

    #region Private Methods
    private void InitUser()
    {
        networkRoomManager = FindObjectOfType<NetworkRoomManager>();

        GenerateButtons();
        SpawnInRoomPlayer();
    }

    private void SpawnInRoomPlayer()
    {

    }
    #endregion


    #region Public Methods

    public void GenerateButtons()
    {
        if(generateFriendButtons == null)
        {
            generateFriendButtons = FindObjectOfType<GenerateFriendButtons>();
        }

        if(lobby == null)
        {
            lobby = networkRoomManager.GetComponent<Lobby>();
        }

        networkRoomManager.StartHost();

        lobby.CreateLobby(LobbyType.Friends);

        generateFriendButtons.CreateButtons();

    }

    public void OnReadyClicked()
    {

    }

    #endregion
}
