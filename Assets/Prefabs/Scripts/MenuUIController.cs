using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UpdateUI 
{
    public Text level;
    public Text prise;

    public void UpdateUIParts (string _level, string _prise)
    {
        level.text = _level;
        prise.text = _prise;
    }
}

public class MenuUIController : MonoBehaviour
{
    public static MenuUIController Instance;

    public GameObject loadingDisplay;
    public GameObject nameDisplay;
    public GameObject defaultDisplay;
    public GameObject upgradeDisplay;
    public GameObject moneyDisplay;
    [Header("")]
    public InputField nameField;
    public Text nameText;
    public Text moneyText;
    [Header("")]
    public UpdateUI moving;
    public UpdateUI battery;
    public UpdateUI shoot;
    public UpdateUI zoom;

    private void Awake()
    {
        Instance = this;
        StateObject(loadingDisplay, true);
    }

    public void StateObject(GameObject _go, bool value) 
    {
        _go.SetActive(value);
    }

    public void ShowUpgradeDisplay() 
    {
        StateObject(defaultDisplay, false);
        upgradeDisplay.SetActive(true);
    }

    public void HideUpgradeDisplay() 
    {
        StateObject(defaultDisplay, true);
        upgradeDisplay.SetActive(false);
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