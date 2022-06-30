using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpicTransport;
using Epic.OnlineServices.Lobby;
using Mirror;
public class LobbyManager : MonoBehaviour
{
    #region Public Static Fields

    public static LobbyManager Instance;

    #endregion

    #region Public Fields

    public Lobby lobby;

    #endregion

    #region SerializeFields

    [SerializeField]
    private bool devMode;

    [SerializeField]
    private GameObject EOSPrefab;

    [SerializeField]
    private GameObject devEOSPrefab;

    [SerializeField]
    private GameObject roomManagerPrefab;

    [SerializeField]
    private string devName;

    #endregion

    #region Private Fields

    private NetworkRoomManager networkRoomManager;

    private EOSSDKComponent EOS;

    #endregion

    #region Mono Behaviours Methods
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        if (Debug.isDebugBuild && devMode == true)
        {
            if(devName != string.Empty)
            {
                InitDev(devName);
            }
            else
            {
                Debug.LogWarning("Dev name is not set in lobbyManager");
            }
        }
        else
        {
            InitUser();
        }
    }

    #endregion

    #region Public Methods
    public void InitDev(string userName)
    {
        EOS = Instantiate(devEOSPrefab).GetComponent<EOSSDKComponent>();

        networkRoomManager = Instantiate(roomManagerPrefab).GetComponent<NetworkRoomManager>();

        EOS.devAuthToolCredentialName = userName;

        EOSSDKComponent.Initialize();

        InvokeRepeating("WaitUntilEOSInit", 0, 0.1f);
    }

    public void InitUser()
    {
        EOS = Instantiate(EOSPrefab).GetComponent<EOSSDKComponent>();

        networkRoomManager = Instantiate(roomManagerPrefab).GetComponent<NetworkRoomManager>();

        EOSSDKComponent.Initialize();

        InvokeRepeating("WaitUntilEOSInit", 0, 0.1f);

    }

    public void WaitUntilEOSInit()
    {
        if(EOSSDKComponent.Initialized)
        {
            CancelInvoke("WaitUntilEOSInit");
            lobby = networkRoomManager.GetComponent<Lobby>();

            lobby.Create();
        }
        else
        {
            Debug.Log("Not ready");
        }
    }

    public void GenerateButtons()
    {
        var generateFriendButtons = FindObjectOfType<GenerateFriendButtons>();

        generateFriendButtons.CreateButtons();
    }

    public void SearchForLobbies(int maxResults)
    {
        lobby.FindLobbies((uint)maxResults);
    }

    #endregion

    #region Epic Online Services Callbacks



    #endregion
}
