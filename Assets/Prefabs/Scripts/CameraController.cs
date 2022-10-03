using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    private Joystick moveJoystick;

    public Transform target;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        moveJoystick = UIController.Instance.moveJoystick;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * moveJoystick.Vertical + Vector3.right * moveJoystick.Horizontal);
    }
}
