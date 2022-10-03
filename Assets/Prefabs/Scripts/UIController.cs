using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{                                                                                                           
    public static UIController Instance;

    public Joystick moveJoystick;

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

    IEnumerator Starter() 
    {
        yield return new WaitForSeconds(0.25f);
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
        loadingDisplay.SetActive(true);
        Application.LoadLevel(0);
    }

    public void RefreshCounter(int value) 
    {
        counter.text = $"Ворогів вбито : {value}";
    }

    public void RefreshEnergy(int value) 
    {
        
    }

    public void RefreshShoot() 
    {
        
    }

    void Update()
    {
        Camera.main.fieldOfView = 60 - 30 * zoom.value;
    }
}
