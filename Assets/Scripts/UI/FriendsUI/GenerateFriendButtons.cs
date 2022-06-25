using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Epic;
using Epic.OnlineServices.Platform;
using Epic.OnlineServices;
using Epic.OnlineServices.Friends;
using Epic.OnlineServices.UserInfo;
using EpicTransport;
using System;
using UnityEngine.UI;
using Epic.OnlineServices.Lobby;
using Mirror;

public class GenerateFriendButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject friendButtonPrefab;

    private Lobby lobby;
    private NetworkRoomManager networkRoomManager;

    private GridLayoutGroup gridLayoutGroup;

    private RectTransform contentTransform;

    private float searchLobbiesDelay = 10;

    private float searchLobbiesTimer;

    private FriendsInterface friendsInterface;

    private int friendsCount = 0;

    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        contentTransform = GetComponent<RectTransform>();

        lobby = FindObjectOfType<Lobby>();

        friendsInterface = EOSSDKComponent.GetFriendsInterface();
    }
    public void Update()
    {
        searchLobbiesTimer += Time.deltaTime;

        if (searchLobbiesTimer >= searchLobbiesDelay)
        {
            searchLobbiesTimer = 0;
            
            SearchForFriends();

            SearchForLobbies();
        }
    }

    public void SearchForLobbies()
    {
        lobby.FindLobbies((uint)friendsCount);
    }

    public void SearchForFriends()
    {
        QueryFriendsOptions queryFriendsOptions = new QueryFriendsOptions
        {
            LocalUserId = EOSSDKComponent.LocalUserAccountId,
        };

        friendsInterface.QueryFriends(queryFriendsOptions, null, OnFoundFriendsCallback);
    }

    private void OnFoundFriendsCallback(QueryFriendsCallbackInfo data)
    {
        GetFriendsCountOptions options = new GetFriendsCountOptions()
        {
            LocalUserId = EOSSDKComponent.LocalUserAccountId,
        };

        friendsCount = friendsInterface.GetFriendsCount(options);

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

            CopyUserInfoOptions copyUserInfoOptions = new CopyUserInfoOptions
            {
                LocalUserId = EOSSDKComponent.LocalUserAccountId,
                TargetUserId = friendsInterface.GetFriendAtIndex(getFriendAtIndexOptions),
            };

            UserInfoData userInfoData;

            userInfoInterface.CopyUserInfo(copyUserInfoOptions, out userInfoData);

            // Create Button
            Button button = Instantiate(friendButtonPrefab, transform).GetComponent<Button>();

            button.onClick.AddListener(() => OnClicked(lobby.foundLobbies[i]));

            // Set button text to username
            button.GetComponentInChildren<TextMeshProUGUI>().text = userInfoData.DisplayName;

            // Resize the content holder to allow scrolling
            contentTransform.sizeDelta += new Vector2(0, gridLayoutGroup.cellSize.y);
        }

        
    }

    public void OnClicked(LobbyDetails lobbyDetails)
    {
        lobby.JoinLobby(lobbyDetails);
    }

    public void QueryUserInfoCallback(QueryUserInfoCallbackInfo data)
    {

    }


}
