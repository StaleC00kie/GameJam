using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpicTransport;
using Mirror;

public class DevManager : MonoBehaviour
{
    public static DevManager Instance;

    [SerializeField]
    private GameObject devNetworkManagerPrefab;

    private NetworkManager devNetworkManager;

    private EOSSDKComponent devEOS;

    void Awake()
    {
        if (Debug.isDebugBuild)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void Init(string userName)
    {
        devNetworkManager = Instantiate(devNetworkManagerPrefab).GetComponent<NetworkManager>();


        devEOS = devNetworkManager.GetComponent<EOSSDKComponent>();

        devEOS.devAuthToolCredentialName = userName;

        EOSSDKComponent.Initialize();
    }
}
