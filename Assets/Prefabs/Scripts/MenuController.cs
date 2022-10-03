using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//
public class MenuController : MonoBehaviour
{
    public int money;
    public int maxKills;
    public int currentKills;

    public DroneLevel dl;

    void Start()
    {
        LoadData();
        if (currentKills > maxKills)
        {
            maxKills = currentKills;
            PlayfabController.Instance.UpdateLeaderboard(maxKills);
        }
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

        money = savedMoney + currentKills * 10;
        MenuUIController.Instance.SetMoneyText(money);
        PlayerPrefs.DeleteKey("Kills");

        MenuUIController.Instance.battery.UpdateUIParts(dl.battery.GetCurrentLevel(), dl.battery.GetCurrentPrise().ToString());
        MenuUIController.Instance.moving.UpdateUIParts(dl.moving.GetCurrentLevel(), dl.moving.GetCurrentPrise().ToString());
        MenuUIController.Instance.shoot.UpdateUIParts(dl.shoot.GetCurrentLevel(), dl.shoot.GetCurrentPrise().ToString());
        MenuUIController.Instance.zoom.UpdateUIParts(dl.zoom.GetCurrentLevel(), dl.zoom.GetCurrentPrise().ToString());
    }



    public void AddingLevel(UpdateUI ui, BaseObject baseObject) 
    {
        money -= baseObject.GetCurrentPrise();
        baseObject.AddLevel();
        ui.UpdateUIParts(baseObject.GetCurrentLevel(), baseObject.GetCurrentPriseString());
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
        if(dl.moving.GetCanBuying(money))
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