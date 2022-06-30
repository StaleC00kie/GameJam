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

    [SerializeField]
    private GameObject roomManagerPrefab;

    #endregion

    #region Private Fields

    private NetworkRoomManager networkRoomManager;

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

        try
        {
            SteamClient.Init(480, true);

        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void OnApplicationQuit()
    {
        SteamClient.Shutdown();
    }

    #endregion

    #region Public Methods

    public void InitUser()
    {
        networkRoomManager = Instantiate(roomManagerPrefab).GetComponent<NetworkRoomManager>();
    }


    public void GenerateButtons()
    {
        var generateFriendButtons = FindObjectOfType<GenerateFriendButtons>();

        generateFriendButtons.CreateButtons();
    }
    #endregion
}
