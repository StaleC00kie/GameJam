using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraController : MonoBehaviour
{

    private Camera localCamera;

    [SerializeField]
    private Transform cameraAnchordPoint;

    private void Awake()
    {
        localCamera = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(localCamera.enabled)
        {
            localCamera.transform.parent = null;
        }
    }

    void LateUpdate()
    {
        localCamera.transform.position = cameraAnchordPoint.position;
    }
}
