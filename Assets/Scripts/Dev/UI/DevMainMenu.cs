using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class DevMainMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField userNameField;

    private void Start()
    {
        DOTween.Init(false, true);
    }

    public void OnLoginClicked()
    {
        if(userNameField.text == string.Empty)
        {
            Debug.Log("No username was entered");
            return;
        }

        if(userNameField != null)
        {
            DevManager.Instance.Init(userNameField.text);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogErrorFormat("userNameField is not set in the inspector.");
        }
    }
}
