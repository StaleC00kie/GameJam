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

public class FriendsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject friendButtonPrefab;

    private GridLayoutGroup gridLayoutGroup;

    private RectTransform contentTransform;

    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        contentTransform = GetComponent<RectTransform>();
    }

    public void Start()
    {

        FriendsInterface friendsInterface = EOSSDKComponent.GetFriendsInterface();


        QueryFriendsOptions queryFriendsOptions = new QueryFriendsOptions
        {
            LocalUserId = EOSSDKComponent.LocalUserAccountId,
        };

        friendsInterface.QueryFriends(queryFriendsOptions, null, OnFoundFriendsCallback);
    }

    private void OnFoundFriendsCallback(QueryFriendsCallbackInfo data)
    {
        FriendsInterface friendsInterface = EOSSDKComponent.GetFriendsInterface();


        GetFriendsCountOptions options = new GetFriendsCountOptions()
        {
            LocalUserId = EOSSDKComponent.LocalUserAccountId,
        };

        int friendsCount = friendsInterface.GetFriendsCount(options);

        for (int i = 0; i < friendsCount; i++)
        {

            GetFriendAtIndexOptions getFriendAtIndexOptions = new GetFriendAtIndexOptions()
            {
                Index = i,
                LocalUserId = EOSSDKComponent.LocalUserAccountId,
            };

            Button button = Instantiate(friendButtonPrefab, transform).GetComponent<Button>();

            button.onClick.AddListener(() => OnClicked(friendsInterface.GetFriendAtIndex(getFriendAtIndexOptions)));

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

            button.GetComponentInChildren<TextMeshProUGUI>().text = userInfoData.DisplayName;

            contentTransform.sizeDelta += new Vector2(0, gridLayoutGroup.cellSize.y);
        }

        
    }

    public void OnClicked(EpicAccountId id)
    {

    }

    public void QueryUserInfoCallback(QueryUserInfoCallbackInfo data)
    {

    }
}
