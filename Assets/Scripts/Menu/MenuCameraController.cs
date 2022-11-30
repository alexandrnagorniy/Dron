using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CameraPositions 
{
    public string key;
    public Vector3 lockingPosition;
    public Vector3[] movePosition;
    public bool useSteps;

    public GameObject[] display;

    public Vector3 GetTargetPosition()
    {
        return lockingPosition;
    }

    public Vector3 GetCameraPosition(int value)
    {
        return movePosition[value];
    }

    public void Hide() 
    {
        foreach (var item in display)
        {
            item.SetActive(false);
        }
        
    }

    public void Show() 
    {
        foreach (var item in display)
        {
            item.SetActive(true);
        }
    }
}

public class MenuCameraController : MonoBehaviour
{
    private GameObject lookingObject;

    [SerializeField]
    private CameraPositions[] camPos;

    
    private CameraPositions currentPos;
    int step = 0;
    Vector3 moveTo;

    public void GetPosition(string value) 
    {
        if(value != currentPos.key)
            foreach (var item in camPos)
            {
                currentPos.Hide();
                if (item.key == value)
                    currentPos = item;
            }
    }

    void Move() 
    {
        lookingObject.transform.position = Vector3.MoveTowards(lookingObject.transform.position, currentPos.GetTargetPosition(), 0.25f);
        if (Vector3.Distance(transform.position, moveTo) < 0.01f)
        {
            if(currentPos.display != null)
                currentPos.Show();
        }
        
        moveTo = currentPos.GetCameraPosition(step);
        transform.position = Vector3.MoveTowards(transform.position, moveTo, 0.1f);
        transform.LookAt(lookingObject.transform.position);
    }

    void Start()
    {
        currentPos = camPos[0];
        lookingObject = new GameObject();
        lookingObject.transform.position = currentPos.GetTargetPosition();
    }

    void Update()
    {
        Move();
    }
}
