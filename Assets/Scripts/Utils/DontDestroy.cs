using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class DontDestroy : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
