using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Position
{
    public float MinValue;
    public float MaxValue;
}

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private Joystick moveJoystick;
    private Joystick rotateJoystick;

    public Transform mainCam;
    public Transform minimapCam;

    public Position yPosition;

    public Position xPosition;
    public Position zPosition;

    [Range(0f, 0.25f)]
    public float alpha;
    [Range(0.251f, 0.5f)]
    public float green;
    [Range(0.501f, 0.75f)]
    public float yellow;
    [Range(0.751f, 1f)]
    public float red;

    float GetRate() 
    {
        float rate = 0;
        rate = (yPosition.MinValue - yPosition.MaxValue) / -100;
        float distance = Vector3.Distance(mainCam.localPosition,new Vector3(0, 0, yPosition.MinValue));
        float rateDistance = distance * rate;
        return ((yPosition.MinValue - yPosition.MaxValue) - rateDistance) * 100;
    }

    float GetColor() 
    {
        float currentColor = 0;

        return currentColor;
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        moveJoystick = UIController.Instance.moveJoystick;
        rotateJoystick = UIController.Instance.rotateJoystick;
    }

    float GetMoving(float positionValue, Position positionData, float joystickValue)
    {
        if (positionValue >= positionData.MaxValue && joystickValue > 0)
            return 0;
        else if (positionValue <= positionData.MinValue && joystickValue < 0)
            return 0;
        else
            return joystickValue;
    }

    void Update()
    {
        float horizontal = GetMoving(transform.position.x, xPosition, moveJoystick.Horizontal);
        float vertical = GetMoving(transform.position.z, zPosition, moveJoystick.Vertical);
        Debug.Log(GetRate());
        transform.Translate(vertical * (Vector3.up + Vector3.up * GameController.Instance.GetMoveLevel()) +
           horizontal * (Vector3.right + Vector3.right * GameController.Instance.GetMoveLevel()));

        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y + rotateJoystick.Horizontal, 
            transform.localEulerAngles.z);

        minimapCam.localRotation = Quaternion.Euler(minimapCam.localEulerAngles.x, minimapCam.localEulerAngles.y, 
            minimapCam.localEulerAngles.z + rotateJoystick.Horizontal);

        mainCam.position += mainCam.forward * GetMoving(mainCam.localPosition.z, yPosition, rotateJoystick.Vertical);
    }
}