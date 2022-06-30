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

    }

    #endregion

    #region Public Methods

    public void CreateButtons()
    {
        foreach (Friend friend in SteamFriends.GetFriends())
        {
            if(friend.IsOnline && Lobby.IsFriendPlaying(friend))
            {
                // Create a button
                Button button = Instantiate(friendButtonPrefab, transform).GetComponent<Button>();

                button.onClick.AddListener(() => OnClicked(friend.GameInfo.Value.Lobby.Value));

                // Set button's text to friend's display name
                button.GetComponent<FriendButton>().usernameText.text = friend.Name;

                // Resize the content holder to allow scrolling
                contentTransform.sizeDelta += new Vector2(0, gridLayoutGroup.cellSize.y);
            }
        }
    }

    public async void OnClicked(Steamworks.Data.Lobby? lobbyData)
    {
        RoomEnter result = await lobbyData.Value.Join();

        switch (result)
        {
            case RoomEnter.Success:
                Debug.Log("Joined friend's lobby!");
                break;
            case RoomEnter.DoesntExist:
                Debug.Log("Lobby does not exist!");
                break;
            case RoomEnter.NotAllowed:
                break;
            case RoomEnter.Full:
                Debug.Log("Lobby is full!");
                break;
            case RoomEnter.Error:
                break;
            case RoomEnter.Banned:
                break;
            case RoomEnter.Limited:
                break;
            case RoomEnter.ClanDisabled:
                break;
            case RoomEnter.CommunityBan:
                break;
            case RoomEnter.MemberBlockedYou:
                break;
            case RoomEnter.YouBlockedMember:
                break;
            case RoomEnter.RatelimitExceeded:
                break;
            default:
                break;
        }
    }

    #endregion

    #region Private Methods


    #endregion
}
