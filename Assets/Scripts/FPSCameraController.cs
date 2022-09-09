using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCameraController : MonoBehaviour
{
    [SerializeField]
    private VariableJoystick _moveJoystick;

    [SerializeField]
    private VariableJoystick _rotateJoystick;

    [SerializeField]
    private int moveSensevity = 5;

    public Transform MovableObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovableObject.Translate(Vector3.right * _moveJoystick.Horizontal / moveSensevity);
        MovableObject.localRotation = Quaternion.Euler(MovableObject.localEulerAngles.x - _rotateJoystick.Vertical * moveSensevity, MovableObject.localEulerAngles.y + _rotateJoystick.Horizontal * moveSensevity, 0);
    }
}
