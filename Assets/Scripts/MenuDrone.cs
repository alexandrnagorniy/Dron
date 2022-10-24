using System.Collections;
using UnityEngine;

[System.Serializable]
public class TargetContainer
{
    public Transform targetTransform;
    public Vector3 cameraPosition;

    public Vector3 GetTargetPosition() 
    {
        return targetTransform.position;
    }

    public Vector3 GetCameraPosition() 
    {
        return cameraPosition;
    }
}


public class MenuDrone : MonoBehaviour
{
    public GameObject lockGO;

    public TargetContainer[] containers;

    public TargetContainer targetContainer;

    public Outline[] manipulators;
    public Outline[] top;
    public Outline battery;
    public Outline cam;

    private void Awake()
    {
        targetContainer = containers[0];
        lockGO = new GameObject();
        lockGO.transform.position = targetContainer.GetTargetPosition();
    }

    IEnumerator OutlineWife()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Outline>().OutlineWidth *= -1;
        StartCoroutine(OutlineWife());
    }

    private void Update()
    {
        lockGO.transform.position = Vector3.MoveTowards(lockGO.transform.position, targetContainer.GetTargetPosition(), 0.0225f);
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, targetContainer.GetCameraPosition(), 0.05f);
        Camera.main.transform.LookAt(lockGO.transform.position);
    }

    public void ShowBaseOutline()
    {
        GetComponent<Outline>().enabled = true;
        foreach (var item in manipulators)
        {
            item.enabled = false;
        }
        foreach (var item in top)
        {
            item.enabled = false;
        }
        cam.enabled = false;
        battery.enabled = false;
    }

    public void ShowZoom()
    {
        targetContainer = containers[3];
        ChangeOutline(false, false, false, true, false);
    }

    public void ShowBattery()
    {
        targetContainer = containers[2];
        ChangeOutline(false, false, false, false, true);
    }

    public void ShowTop()
    {
        targetContainer = containers[4];
        ChangeOutline(false, false, true, false, false);
    }

    public void ShowManipulators() 
    {
        targetContainer = containers[5];
        ChangeOutline(false, true, false, false, false);
    }

    public void ChangeOutline(bool _myOut, bool _manipulators, bool _top, bool _camera, bool _battery) 
    {
        GetComponent<Outline>().enabled = _myOut;
        foreach (var item in manipulators)
        {
            item.enabled = _manipulators;
        }
        foreach (var item in top)
        {
            item.enabled = _top;
        }
        cam.enabled = _camera;
        battery.enabled = _battery;
    }

    public void ShowBaseUpgrade() 
    {
        targetContainer = containers[1];
        ChangeOutline(false, true, true, true, true);
        MenuUIController.Instance.ShowUpgradeDisplay();
    }

    public void HideBaseUpgrade() 
    {
        targetContainer = containers[0];
        ChangeOutline(true, false, false, false, false);
        MenuUIController.Instance.HideUpgradeDisplay();
    }

    public void OnMouseDown()
    { 
        if (targetContainer == containers[0])
        {
            ShowBaseUpgrade();
        }
    }
}