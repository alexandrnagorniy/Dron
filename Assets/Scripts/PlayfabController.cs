using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayfabController : MonoBehaviour
{

    public static PlayfabController Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    void Login() 
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams 
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginResult, OnError);
    }

    private void OnLoginResult(LoginResult obj)
    {

        string name = null;
        if (obj.InfoResultPayload.PlayerProfile != null)
            name = obj.InfoResultPayload.PlayerProfile.DisplayName;

        if (name == null)
        {
            MenuUIController.Instance.StateObject(MenuUIController.Instance.nameDisplay, true);
        }
        else
        {
            MenuUIController.Instance.StateObject(MenuUIController.Instance.defaultDisplay, true);
            MenuUIController.Instance.SetNameText(name);
        }
        MenuUIController.Instance.StateObject(MenuUIController.Instance.loadingDisplay, false);
    }

    public void UpdatePlayerName(string value) 
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = value
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult obj)
    {
        MenuUIController.Instance.StateObject(MenuUIController.Instance.defaultDisplay, true);
        MenuUIController.Instance.StateObject(MenuUIController.Instance.nameDisplay, false);
        MenuUIController.Instance.SetNameText(obj.DisplayName);
    }

    public void UpdateLeaderboard(int value) 
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
             {
                new StatisticUpdate
                {
                    StatisticName= "Свинособак",
                    Value = value
                }
             }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdatedStatistic, OnError);
    }

    private void OnUpdatedStatistic(UpdatePlayerStatisticsResult obj)
    {
        Debug.Log("Updated");
    }

    private void OnError(PlayFabError obj)
    {
        Debug.LogWarning(obj.GenerateErrorReport());
    }

}