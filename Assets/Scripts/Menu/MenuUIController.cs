using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UpdateUIInfo
{
    public string top;
    public Text topText;
    public string description;
    public Text descriptionText;
    public Image button;
    public Text level;
    public Text prise;

    public void UpdateUIParts(string _level, string _prise)
    {
        level.text = _level;
        prise.text = _prise;
    }

    public void ShowButton()
    {
        button.color = Color.green;
    }

    public void HideButton() 
    {
        button.color = Color.white;
    }
}

public class MenuUIController : MonoBehaviour
{
    public static MenuUIController Instance;

    [Header("Displays")]
    public GameObject loadingDisplay;//-
    public GameObject nameDisplay;//-
    public GameObject defaultDisplay;//+
    public GameObject upgradeDisplay;//+-
    public GameObject settingsDisplay;//+-

    [Header("UpgradeMenu")]
    public Text topText;
    public Text middleText;
    public Button byeButton;//+

    [Header("")]
    public InputField nameField;//-
    public Text nameText;//+
    public Text moneyText;//+

    [Header("")]
    public UpdateUIInfo moving;
    public UpdateUIInfo battery;
    public UpdateUIInfo shoot;
    public UpdateUIInfo zoom;

    private void Awake()
    {
        Instance = this;
        StateObject(loadingDisplay, true);
    }

    public void StateObject(GameObject _go, bool value) 
    {
        _go.SetActive(value);
    }

    public void StateDoubleObject(GameObject first, bool firstValue, GameObject second, bool secondValue) 
    {
        first.SetActive(firstValue);
        second.SetActive(secondValue);
    }

    public void StateSettings(bool state) 
    {
        settingsDisplay.SetActive(state);
    }

    public void ShowUpgradeDisplay() 
    {
        StateDoubleObject(upgradeDisplay, true, defaultDisplay, false);
    }

    public void HideUpgradeDisplay() 
    {
        StateDoubleObject(defaultDisplay, true, upgradeDisplay, false);
    }

    public void SetNameText(string value) 
    {
        nameText.text = value;
    }

    public void SetUsername() 
    {
        PlayfabController.Instance.UpdatePlayerName(nameField.text);
    }

    public void SetMoneyText(int value) 
    {
        moneyText.text = $"$ {value}";
    }

    public void StartGame() 
    {
        MenuController.Instance.SaveDroneSettings();
        StateObject(loadingDisplay, true);
        Application.LoadLevel(1);
    }
}