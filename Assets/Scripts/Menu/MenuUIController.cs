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

    [Header("")]
    public UpdateUIInfo moving;
    public UpdateUIInfo battery;
    public UpdateUIInfo shoot;
    public UpdateUIInfo zoom;

    [Header("Money")]
    public Text moneyText;
    public Animator currentMoneyAnimator;
    public Text[] setMoneyText;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SetMoney(Random.Range(100, 999999));
        if (Input.GetKeyDown(KeyCode.M))
            SetMoney(Random.Range(-999999, -100));
    }

    public void SetMoney(int money)
    {
        Text moneyText = null;
        Animator anim = null;
        foreach (var item in setMoneyText)
        {
            if (!item.gameObject.activeSelf)
            { 
                moneyText = item;
                anim = moneyText.GetComponent<Animator>();
                break;
            }
        }

        if (money > 0)
        {
            moneyText.text = "+";
            moneyText.color = Color.green;
        }
        else 
        {
            moneyText.color = Color.red;
        }

        moneyText.text += money;
        StateObject(moneyText.gameObject, true);
        StartCoroutine(DisableChangedMoneyText(moneyText, money));
    }

    IEnumerator DisableChangedMoneyText(Text value,int moneyValue)
    {
        yield return new WaitForSeconds(0.5f);
        currentMoneyAnimator.enabled = true;
        moneyText.color = value.color;
        value.color = Color.white;
        value.text = "";
        StateObject(value.gameObject, false);
        yield return new WaitForSeconds(0.25f);
        MenuController.Instance.SetMoney(moneyValue);
        yield return new WaitForSeconds(0.25f);
        moneyText.color = Color.white;
        currentMoneyAnimator.enabled = false;
    }

    private void Awake()
    {
        Instance = this;
        StartCoroutine(Starting());
    }

    IEnumerator Starting() 
    {
        StateObject(loadingDisplay, true);
        yield return new WaitForSeconds(0.5f);
        StateObject(loadingDisplay, false);
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
        moneyText.text = $"{value}";
    }

    public void StartGame(int value) 
    {
        MenuController.Instance.SaveDroneSettings();
        StateObject(loadingDisplay, true);
        Application.LoadLevel(value);
    }
}