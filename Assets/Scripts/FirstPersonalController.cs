using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonalController : MonoBehaviour
{
    private Transform camera;
    [SerializeField]
    private Joystick movingJoystick;
    [SerializeField]
    private Joystick rotationJoystick;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.right * movingJoystick.Horizontal /*Input.GetAxis("Horizontal")*/ + Vector3.forward * movingJoystick.Vertical /*Input.GetAxis("Vertical")*/) * (Time.deltaTime * 5));
        transform.rotation = Quaternion.Euler(transform.localEulerAngles + Vector3.up * rotationJoystick.Horizontal * 2 /*Input.GetAxis("Mouse X")*/);
        camera.transform.rotation = Quaternion.Euler(camera.localEulerAngles.x - rotationJoystick.Vertical * 2, transform.localEulerAngles.y, 0);
    }
}