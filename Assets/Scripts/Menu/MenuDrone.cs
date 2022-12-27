using System.Collections;
using UnityEngine;

public class MenuDrone : MonoBehaviour
{
    //public Outline[] manipulators;
    //public Outline[] top;
    //public Outline battery;
    //public Outline cam;
    //public Outline myOutine;

    //public Outline[] currentOutlines;
    public UpdateUIInfo currentInfo;

    private void Awake()
    {
        //EnableOutline(GetOut(myOutine), new UpdateUIInfo());
    }

    Outline[] GetOut(Outline outline) 
    {
        Outline[] tmp = new Outline[1];
        tmp[0] = outline;
        return tmp;
    }

    public void ShowZoom()
    {
        //EnableOutline(GetOut(cam), MenuUIController.Instance.zoom);
        MenuUIController.Instance.byeButton.onClick.AddListener(() => MenuController.Instance.AddZoomLevel());
    }

    public void ShowBattery()
    {
        //EnableOutline(GetOut(battery), MenuUIController.Instance.battery);
        MenuUIController.Instance.byeButton.onClick.AddListener(() => MenuController.Instance.AddBatteryLevel());
    }

    public void ShowTop()
    {
        //EnableOutline(top, MenuUIController.Instance.moving);
        MenuUIController.Instance.byeButton.onClick.AddListener(() => MenuController.Instance.AddMovingLevel());
    }

    public void ShowManipulators() 
    {
        //EnableOutline(manipulators, MenuUIController.Instance.shoot);
        MenuUIController.Instance.byeButton.onClick.AddListener(() => MenuController.Instance.AddShootingLevel());
    }

    //public void EnableOutline(Outline[] outlines, UpdateUIInfo info) 
    //{
    //    MenuUIController.Instance.byeButton.onClick.RemoveAllListeners();
    //    if (currentInfo.button != null)
    //        currentInfo.HideButton();

    //    if(currentOutlines.Length > 0 || currentOutlines != null)
    //        foreach (var item in currentOutlines)
    //        {
    //            item.enabled = false;
    //        }

    //    currentOutlines = outlines;
    //    currentInfo = info;

    //    if(currentOutlines.Length > 0)
    //        foreach (var item in currentOutlines)
    //        {
    //            item.enabled = true;
    //        }

    //    if(currentInfo.button != null)
    //        currentInfo.ShowButton();
    //}

    public void ShowBaseUpgrade() 
    {
        
        //EnableOutline(manipulators, MenuUIController.Instance.shoot);
        //MenuUIController.Instance.ShowUpgradeDisplay();
    }

    public void HideBaseUpgrade() 
    {
        
        
        //MenuUIController.Instance.HideUpgradeDisplay();
        //EnableOutline(GetOut(myOutine), new UpdateUIInfo());
    }

    public void OnMouseDown()
    { 
        
            //MenuUIController.Instance.ShowUpgradeDisplay();
            ShowBattery();
        
    }
}