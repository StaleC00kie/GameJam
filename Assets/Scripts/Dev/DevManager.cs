using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpicTransport;
public class DevManager : MonoBehaviour
{
    public static DevManager Instance;

    [HideInInspector]
    public EOSSDKComponent eOSSDKComponent;

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

            eOSSDKComponent = FindObjectOfType<EOSSDKComponent>();
        }
    }

    public void Init(string userName)
    {
        eOSSDKComponent.devAuthToolCredentialName = userName;

        EOSSDKComponent.Initialize();
    }
}
