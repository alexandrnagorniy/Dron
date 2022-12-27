using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject rocket;
    public DroneLevel dl;
    public static GameController Instance;
    public Transform parent;
    private int points;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void SaveKills() 
    {
        PlayerPrefs.SetInt("Kills", points);
    }

    public float GetShootCountdown() 
    {
        float cd = 5f - 0.2f * dl.shoot.currentLevel;
        return cd;
    }

    private void Start()
    {
        dl = JsonUtility.FromJson<DroneLevel>(PlayerPrefs.GetString("Dl"));
        UIController.Instance.RefreshCounter(points);
    }

    public void Shoot()
    {
        if (canShoot)
        {
            Instantiate(rocket, CameraController.Instance.transform.position + Vector3.up * 10, Quaternion.Euler(rocket.transform.eulerAngles));
            StartCoroutine(UIController.Instance.UpdateShootButton(0));
            canShoot = false;
        }
    }

    public IEnumerator Energy(float value) 
    {
        yield return new WaitForSeconds(0.1f);
        value += 0.1f;
        UIController.Instance.UpdateEnergy(GetBatteryState(value));
        if (GetBatteryState(value) < 1)
            StartCoroutine(Energy(value));
        else
            UIController.Instance.ShowEndDisplay(points);
    }

    public float GetBatteryState(float value)
    {
        float bat = 0;
        bat = value / (30 + 5 * dl.battery.currentLevel);
        return bat;
    }

    public float GetMoveLevel() 
    {
        float move = 0.1f * dl.moving.currentLevel;
        return move;
    }

    public float GetZoomLevel() 
    {
        float zoom = 30 * dl.zoom.currentLevel;
        return zoom;
    }

    public void CanShoot() 
    {
        canShoot = true;
    }
    public void AddPiggydog(int value) 
    {
        points += value;
        UIController.Instance.RefreshCounter(points);
        PlayerPrefs.SetInt("Score", points);
    }
}
