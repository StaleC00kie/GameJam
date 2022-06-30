using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;
using Steamworks;

public class GenerateFriendButtons : MonoBehaviour
{
    #region SerializeFields
    [SerializeField]
    private GameObject friendButtonPrefab;

    #endregion

    #region Private Fields

    private GridLayoutGroup gridLayoutGroup;

    private RectTransform contentTransform;

    #endregion

    #region Mono Behaviour Methods
    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        contentTransform = GetComponent<RectTransform>();
    }

    public void Start()
    {
        CreateButtons();
    }

    #endregion

    #region Public Methods

    public void CreateButtons()
    {
        foreach (Friend friend in SteamFriends.GetFriends())
        {
            if(friend.IsOnline)
            {
                // Create a button
                Button button = Instantiate(friendButtonPrefab, transform).GetComponent<Button>();

                //button.onClick.AddListener(() => OnClicked(lobby.foundLobbies[0]));

                // Set button's text to friend's display name
                button.GetComponent<FriendButton>().usernameText.text = friend.Name;

                // Resize the content holder to allow scrolling
                contentTransform.sizeDelta += new Vector2(0, gridLayoutGroup.cellSize.y);
            }
        }
    }

    public void OnClicked()
    {
        //lobby.JoinLobby(lobbyDetails);
    }

    #endregion

    #region Private Methods


    #endregion
}
