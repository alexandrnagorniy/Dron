using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private Joystick moveJoystick;
    private Joystick rotateJoystick;

    public Transform mainCam;
    public Transform miniaamapCam;
    public Transform target;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        moveJoystick = UIController.Instance.moveJoystick;
        rotateJoystick = UIController.Instance.rotateJoystick;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveJoystick.Vertical * (Vector3.up + Vector3.up * GameController.Instance.GetMoveLevel()) +
           moveJoystick.Horizontal * (Vector3.right + Vector3.right * GameController.Instance.GetMoveLevel()));

        mainCam.position += mainCam.forward * rotateJoystick.Vertical;
        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y + rotateJoystick.Horizontal, transform.localEulerAngles.z);
        miniaamapCam.localRotation = Quaternion.Euler(miniaamapCam.localEulerAngles.x, miniaamapCam.localEulerAngles.y, miniaamapCam.localEulerAngles.z + rotateJoystick.Horizontal);
    }
}
