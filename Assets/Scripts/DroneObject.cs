using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
[System.Serializable]
public class BaseObject
{
    public int currentLevel;
    public int maximumLevel;
    public const int prisePerLevel = 1500;
    public int currentPrise;

    public void SetDefault(int maxLVL)
    {
        currentLevel = 1;
        maximumLevel = maxLVL;
        UpdateCurrentPrise();
    }

    public void SetCustom(int curLVL, int maxLVL)
    {
        currentLevel = curLVL;
        maximumLevel = maxLVL;
        UpdateCurrentPrise();
    }

    public void UpdateCurrentPrise()
    {
        currentPrise = currentLevel * prisePerLevel;
    }

    public string GetCurrentPriseString()
    {
        if (GetCurrentPrise() < 4)
            return "MAX";
        else
            return GetCurrentPrise().ToString();
    }

    public int GetCurrentPrise()
    {
        if (currentLevel < maximumLevel)
            return currentPrise;
        else
            return 0;
    }

    public int AddLevel()
    {
        int prise = currentPrise;
        currentLevel++;
        UpdateCurrentPrise();
        return prise;
    }

    public bool GetCanBuying(int value)
    {
        if (currentLevel < maximumLevel)
            return value >= currentPrise;
        else
            return false;
    }

    public string GetCurrentLevel()
    {
        return currentLevel.ToString();
    }
}

[System.Serializable]
public class DroneLevel
{
    public BaseObject moving = new BaseObject();
    public BaseObject battery = new BaseObject();
    public BaseObject shoot = new BaseObject();
    public BaseObject zoom = new BaseObject();
}

[System.Serializable]
public class DroneAttribute 
{
    [SerializeField]
    private int level;
    [SerializeField]
    private float countPerLevel;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private int maximumLevel;
    [SerializeField]
    private int costPerLevel;
}

[CreateAssetMenu(fileName = "DroneObject", menuName = "Drone object", order = 51)]
public class DroneObject : ScriptableObject
{
    [SerializeField]
    private GameObject model;

    [SerializeField]
    private DroneAttribute shootAttribute;

    [SerializeField]
    private DroneAttribute controlAttribute;

    [SerializeField]
    private DroneAttribute batteryAttribute;

    [SerializeField]
    private DroneAttribute zoomAttribute;











    //[SerializeField]
    //private int movingSpeedLevel;
    //[SerializeField]
    //private float movingSpeedPerLevel;



    //public BaseObject moving = new BaseObject();
    //public BaseObject battery = new BaseObject();
    //public BaseObject shoot = new BaseObject();
    //public BaseObject zoom = new BaseObject();
}
