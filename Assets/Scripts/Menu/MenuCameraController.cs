using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraPositions 
{
    public string key;
    public Vector3 lockingPosition;
    public Vector3[] movePosition;

    public Vector3 GetTargetPosition()
    {
        return lockingPosition;
    }

    public Vector3 GetCameraPosition()
    {
        return movePosition[0];
    }
}

public class MenuCameraController : MonoBehaviour
{
    private GameObject lookingObject;

    [SerializeField]
    private CameraPositions[] camPos;

    private CameraPositions currentPos;

    public void GetPosition(string value) 
    {
        if(value != currentPos.key)
            foreach (var item in camPos)
            {
                if (item.key == value)
                    currentPos = item;
            }
    }

    void Move() 
    {
        lookingObject.transform.position = Vector3.MoveTowards(lookingObject.transform.position, currentPos.GetTargetPosition(), 0.0225f);
        transform.position = Vector3.MoveTowards(transform.position, currentPos.GetCameraPosition(), 0.05f);
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
