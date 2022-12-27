using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Joystick moveJoystick;
    public Joystick rotateJoystick;

    [Header("All Canvas")]
    public GameObject loadingDisplay;
    public GameObject startingDisplay;
    public GameObject pauseDisplay;
    public GameObject endDisplay;

    [Header("Starting display")]
    public Text timerText;

    [Header("Game display")]
    public Image warningImage;
    public Transform minimap;
    public Text counter;
    public Image shootButton;
    public Slider energyBar;
    public Scrollbar zoom;

    [Header("End display")]
    public GameObject ADSButton;
    public Text moneyCounterText;
    public Text detailsCounterText;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        StartCoroutine(LoadCoroutine());
    }

    public void SetupWarningImage(Color color) 
    {
        warningImage.color = color;
    }

    public void ShowEndDisplay(int value) 
    {
        moneyCounterText.text = $"{LocalizationController.Instance.GetLanguageText("rusnya")}:{value}={value * 10}$";
        detailsCounterText.text = " ";
        endDisplay.SetActive(true);
    }

    #region loading
    IEnumerator LoadCoroutine() 
    {
        yield return new WaitForSeconds(1.2f);
        HideLoadingDisplay();
    }

    public void ShowLoadingDisplay() 
    {
        loadingDisplay.SetActive(true);
    }
    
    public void HideLoadingDisplay()
    {
        startingDisplay.SetActive(true);
        StartCoroutine(Starting(3));
        loadingDisplay.SetActive(false);

    }

    //
    public void UpdateLoadingBar() 
    {
    
    }
    #endregion

    IEnumerator Starting(int value) 
    {
        timerText.text = value.ToString();
        value--;
        yield return new WaitForSeconds(1);
        if (value == 0)
        {
            startingDisplay.SetActive(false);
            StartCoroutine(StartedLogic());
        }
        else 
        {
            StartCoroutine(Starting(value));
        }
    }

    IEnumerator StartedLogic() 
    {
        startingDisplay.SetActive(false);
        yield return new WaitForSeconds(0.15f);
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
        Time.timeScale = 1;
        GameController.Instance.SaveKills();
        loadingDisplay.SetActive(true);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ToMenu() 
    {
        Time.timeScale = 1;
        loadingDisplay.SetActive(true);
        Application.LoadLevel(0);
    }

    public void RefreshCounter(int value) 
    {
        counter.text = $"{LocalizationController.Instance.GetLanguageText("rusnya")} : {value}";
    }

    void Update()
    {
        minimap.rotation = Quaternion.Euler(0, 0, minimap.localEulerAngles.z + rotateJoystick.Horizontal);
        //minimap.GetChild(0).localEulerAngles = new Vector3(0, 0, minimap.GetChild(0).localEulerAngles.z + rotateJoystick.Horizontal / 4);
        Camera.main.fieldOfView = 60 - GameController.Instance.GetZoomLevel() * zoom.value;

        if (Input.GetKeyDown(KeyCode.Space))
            PopupController.Instance.Show("test", "test message", 2);
    }

    public void Pause(bool value) 
    {
        Time.timeScale = System.Convert.ToInt32(!value);
        pauseDisplay.SetActive(value);
    }
}
