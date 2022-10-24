using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Joystick moveJoystick;

    public Text endCounterText;
    public GameObject endDisplay;
    public GameObject loadingDisplay;
    public Text counter;
    public Image shootButton;
    public Slider energyBar;
    public Scrollbar zoom;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        StartCoroutine(Starter());
    }

    public void ShowEndDisplay(int value) 
    {
        endCounterText.text = $"{LocalizationController.Instance.GetLanguageText("rusnya")}:{value}={value * 10}$";
        endDisplay.SetActive(true);
    }

    IEnumerator Starter() 
    {
        yield return new WaitForSeconds(0.15f);
        loadingDisplay.SetActive(false);
        StartCoroutine(GameController.Instance.Energy(0));
    }

    public void UpdateEnergy(float value) 
    {
        energyBar.value = value;
    }

    public IEnumerator UpdateShootButton(float cd) 
    {
        yield return new WaitForSeconds(0.1f);
        cd += 0.1f;
        float fill = cd / GameController.Instance.GetShootCountdown();
        shootButton.fillAmount = fill;
        if (fill < 1)
            StartCoroutine(UpdateShootButton(cd));
        else
            GameController.Instance.CanShoot();
    }

    public void Restart() 
    {
        GameController.Instance.SaveKills();
        loadingDisplay.SetActive(true);
        Application.LoadLevel(0);
    }

    public void RefreshCounter(int value) 
    {
        counter.text = $"{LocalizationController.Instance.GetLanguageText("rusnya")} : {value}";
    }

    void Update()
    {
        Camera.main.fieldOfView = 60 - GameController.Instance.GetZoomLevel() * zoom.value;
    }
}
