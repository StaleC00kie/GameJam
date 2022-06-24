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

    [SerializeField]
    private GameObject devEOSPrefab;

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

        devEOS = Instantiate(devEOSPrefab).GetComponent<EOSSDKComponent>();

        devNetworkManager = Instantiate(devNetworkManagerPrefab).GetComponent<NetworkManager>();


        devEOS.devAuthToolCredentialName = userName;

        EOSSDKComponent.Initialize();
    }
}
