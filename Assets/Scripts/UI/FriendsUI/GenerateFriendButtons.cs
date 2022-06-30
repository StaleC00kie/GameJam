using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Epic;
using Epic.OnlineServices.Friends;
using Epic.OnlineServices.UserInfo;
using EpicTransport;
using UnityEngine.UI;
using Epic.OnlineServices.Lobby;
using Mirror;
using Utils.EOSFriends;

public class GenerateFriendButtons : MonoBehaviour
{
    #region SerializeFields
    [SerializeField]
    private GameObject friendButtonPrefab;

    #endregion

    #region Private Fields

    private NetworkRoomManager networkRoomManager;

    private GridLayoutGroup gridLayoutGroup;

    private RectTransform contentTransform;

    private FriendsInterface friendsInterface;

    private int friendsCount = 0;

    private UserInfoData userInfoData;

    #endregion

    #region Events & Delegates


    #endregion

    #region Mono Behaviour Methods
    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        contentTransform = GetComponent<RectTransform>();

        friendsInterface = EOSSDKComponent.GetFriendsInterface();
    }

    private void OnDisable()
    {
        
    }
    #endregion

    #region Public Methods

    public void CreateButtons()
    {
        BindEvents();

        //QueryFriendsOptions queryFriendsOptions = new QueryFriendsOptions
        //{
        //    LocalUserId = EOSSDKComponent.LocalUserAccountId,
        //};

        //// Read user's friends list and cache it for future use in the OnFoundFriendsCallback callback.
        //friendsInterface.QueryFriends(queryFriendsOptions, null, OnFoundFriendsCallback);


        E//OSFriendUtils.GetOnlineFriendsCount((int i) => friendCounter.text = i.ToString());

        // TODO: Wrap this up in EOSFriendUtils

        for (int i = 0; i < friendsCount; i++)
        {
            GetFriendAtIndexOptions getFriendAtIndexOptions = new GetFriendAtIndexOptions()
            {
                Index = i,
                LocalUserId = EOSSDKComponent.LocalUserAccountId,
            };

            UserInfoInterface userInfoInterface = EOSSDKComponent.GetUserInfoInterface();


            QueryUserInfoOptions queryUserInfoOptions = new QueryUserInfoOptions()
            {
                LocalUserId = EOSSDKComponent.LocalUserAccountId,
                TargetUserId = friendsInterface.GetFriendAtIndex(getFriendAtIndexOptions),
            };

            userInfoInterface.QueryUserInfo(queryUserInfoOptions, null, QueryUserInfoCallback);
        }
    }

    public void OnClicked(LobbyDetails lobbyDetails)
    {
        //lobby.JoinLobby(lobbyDetails);
    }

    public void QueryUserInfoCallback(QueryUserInfoCallbackInfo data)
    {
        UserInfoInterface userInfoInterface = EOSSDKComponent.GetUserInfoInterface();

        CopyUserInfoOptions copyUserInfoOptions = new CopyUserInfoOptions
        {
            LocalUserId = EOSSDKComponent.LocalUserAccountId,
            TargetUserId = data.TargetUserId,
        };


        userInfoInterface.CopyUserInfo(copyUserInfoOptions, out userInfoData);


        LobbyManager.Instance.SearchForLobbies(100);
    }

    #endregion

    #region Private Methods
    public void BindEvents()
    {
        LobbyManager.Instance.lobby.FindLobbiesSucceeded += FindLobbiesSuccess;

        EOSFriendUtils.OnGetOnlineFriendsCountCallback += OnFoundFriendsCallback;
    }

    private void FindLobbiesSuccess(List<LobbyDetails> foundLobbies)
    {
        // Create a button
        Button button = Instantiate(friendButtonPrefab, transform).GetComponent<Button>();

        //button.onClick.AddListener(() => OnClicked(lobby.foundLobbies[0]));

        // Set button's text to friend's display name
        button.GetComponent<FriendButton>().usernameText.text = userInfoData.DisplayName;

        // Resize the content holder to allow scrolling
        contentTransform.sizeDelta += new Vector2(0, gridLayoutGroup.cellSize.y);
    }

    #endregion

    #region Epic Online Services Callbacks
    private void OnFoundFriendsCallback(int friendCount)
    {
        friendsCount = friendCount;
    }

    #endregion
}
