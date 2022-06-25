using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FriendsList : MonoBehaviour
{
    [SerializeField]
    private float extendDuration = 1;
    private bool isExtended;

    private RectTransform panelTransform;

    private Vector3 oldPos;
    private void Awake()
    {
        panelTransform = GetComponent<RectTransform>();

        oldPos = panelTransform.anchoredPosition;
    }
    public void OnShowFriendsButtonClicked()
    {
        if(isExtended)
        {
            DOTween.To(() => panelTransform.anchoredPosition, x => panelTransform.anchoredPosition = x, new Vector2(oldPos.x, panelTransform.anchoredPosition.y), extendDuration);

           // panelTransform.DOMoveX(oldPos.x, 1);
        }
        else
        {
            DOTween.To(() => panelTransform.anchoredPosition, x => panelTransform.anchoredPosition = x, new Vector2(0, panelTransform.anchoredPosition.y), extendDuration);

        }

        isExtended = !isExtended;
    }
}
