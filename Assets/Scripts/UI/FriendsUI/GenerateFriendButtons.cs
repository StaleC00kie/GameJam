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

    private GridLayoutGroup _gridLayoutGroup;

    private RectTransform _contentTransform;

    private List<Button> _buttons = new List<Button>();

    private List<Friend> _friends;

    #endregion

    #region Mono Behaviour Methods
    private void Awake()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();

        _contentTransform = GetComponent<RectTransform>();
    }

    public void Start()
    {

    }


    public void Update()
    {
        if(SteamClient.IsValid)
        {
            UpdateFriendList();
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Instantiates _buttons for each friend and sets the button's text to the _friends username.
    /// </summary>
    public void CreateButtons()
    {
        foreach (Friend friend in SteamFriends.GetFriends())
        {
            if(friend.IsOnline && Lobby.IsFriendPlaying(friend))
            {
                // Create a button.
                Button button = Instantiate(friendButtonPrefab, transform).GetComponent<Button>();

                // When button is clicked, call OnClicked(), and pass in the lobby the respective friend is in.
                button.onClick.AddListener(() => OnClicked(friend.GameInfo.Value.Lobby.Value));

                // Set button's text to friend's display name.
                button.GetComponent<FriendButton>().usernameText.text = friend.Name;
                
                // Cache button for future use (clearing the buttons).
                _buttons.Add(button);

                // Resize the content holder to allow scrolling.
                _contentTransform.sizeDelta += new Vector2(0, _gridLayoutGroup.cellSize.y);
            }
        }
    }

    /// <summary>
    /// Creates a new friend button.
    /// </summary>
    /// <param name="friend">The friend to create a button for.</param>
    public void CreateFriendButton(Friend friend)
    {
        if (friend.IsOnline && Lobby.IsFriendPlaying(friend))
        {
            // Create a button.
            Button button = Instantiate(friendButtonPrefab, transform).GetComponent<Button>();

            // When button is clicked, call OnClicked(), and pass in the lobby the respective friend is in.
            button.onClick.AddListener(() => OnClicked(friend.GameInfo.Value.Lobby.Value));

            // Set button's text to friend's display name.
            button.GetComponent<FriendButton>().usernameText.text = friend.Name;

            // Cache button for future use (clearing the buttons).
            _buttons.Add(button);

            // Resize the content holder to allow scrolling.
            _contentTransform.sizeDelta += new Vector2(0, _gridLayoutGroup.cellSize.y);
        }
    }

    /// <summary>
    /// Removes all _buttons from the user's friends list.
    /// </summary>
    public void ClearButtons()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].onClick.RemoveAllListeners();
            
            Destroy(_buttons[i]);
        }

        _buttons.Clear();
    }
    
    /// <summary>
    /// Adds friends to the friends list if they are not already in the list.
    /// </summary>
    public void UpdateFriendList()
    {
        if(_friends == null)
        {
            return;
        }

        foreach (Friend friend in SteamFriends.GetFriends())
        {
            if (_friends.Contains(friend) == false)
            {
                CreateFriendButton(friend);
            }
        }
    }




    /// <summary>
    /// Attempt to join the friend's lobby.
    /// </summary>
    /// <param name="lobbyData">The lobby to join.</param>
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
