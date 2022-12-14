using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
public class MenuController : MonoBehaviour
{
    public static MenuController Instance;

    public int money;
    public int maxKills;
    public int currentKills;

    public DroneLevel dl;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadData();
        if (currentKills > maxKills)
        {
            maxKills = currentKills;
            PlayfabController.Instance.UpdateLeaderboard(maxKills);
        }
    }

    public void SaveDroneSettings() 
    {
        string json = JsonUtility.ToJson(dl);
        PlayerPrefs.SetString("Dl", json);
    }

    void LoadData() 
    {
        if (PlayerPrefs.HasKey("Dl"))
            dl = JsonUtility.FromJson<DroneLevel>(PlayerPrefs.GetString("Dl"));
        else
        {
            dl = new DroneLevel();
            dl.battery.SetDefault(15);
            dl.moving.SetDefault(10);
            dl.shoot.SetDefault(20);
            dl.zoom.SetDefault(5);
            string json = JsonUtility.ToJson(dl);
            PlayerPrefs.SetString("Dl", json);
        }

        dl.battery.UpdateCurrentPrise();
        dl.moving.UpdateCurrentPrise();
        dl.shoot.UpdateCurrentPrise();
        dl.zoom.UpdateCurrentPrise();

        int savedMoney = PlayerPrefs.GetInt("Money");
        int currentKills = PlayerPrefs.GetInt("Kills");
        int maxKills = PlayerPrefs.GetInt("Max");

        if (currentKills > maxKills) 
        {
            PlayfabController.Instance.UpdateLeaderboard(currentKills);
            maxKills = currentKills;
            PlayerPrefs.SetInt("Max", maxKills);
        }

        money = savedMoney + (currentKills * 10);
        PlayerPrefs.SetInt("Money", money);
        MenuUIController.Instance.SetMoneyText(money);
        PlayerPrefs.DeleteKey("Kills");
    }

    public void AddingLevel(UpdateUIInfo ui, BaseObject baseObject) 
    {
        money -= baseObject.GetCurrentPrise();
        baseObject.AddLevel();
        ui.UpdateUIParts(baseObject.GetCurrentLevel(), baseObject.GetCurrentPriseString());
        MenuUIController.Instance.SetMoneyText(money);
        PlayerPrefs.SetInt("Money", money);
        SaveDroneSettings();
    }

    public void AddBatteryLevel() 
    {
        if (dl.battery.GetCanBuying(money))
        {
            AddingLevel(MenuUIController.Instance.battery, dl.battery);
        }
    }

    public void AddMovingLevel() 
    {
        if (dl.moving.GetCanBuying(money))
            AddingLevel(MenuUIController.Instance.moving, dl.moving);
        
    }

    public void AddShootingLevel() 
    {
        if (dl.shoot.GetCanBuying(money))
            AddingLevel(MenuUIController.Instance.shoot, dl.shoot);
    }

    public void AddZoomLevel() 
    {
        if (dl.zoom.GetCanBuying(money))
            AddingLevel(MenuUIController.Instance.zoom, dl.zoom);
    }
}