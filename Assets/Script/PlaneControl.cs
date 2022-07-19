using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneControl : MonoBehaviour
{
    public GameObject pointer;
    public Joystick joystick;
    public Slider speedControl;
    float maxSpeed = 0f;
    float minSpeed = 0f;
    float deltaSpeed = 0f;
    public Image speedState;
    public float highLimit = 60f;
    public float flySpeed = 5f;
    public float yawSpeed = 50f;

    float curVertical = 0f;
    float smoothV = 0.4F;
    float velocityV = 0f;
    float curHorizontal = 0f;
    float smoothH = 0.4F;
    float velocityH = 0f;
    float yaw;

    private void Start()
    {
        maxSpeed = speedControl.maxValue;
        minSpeed = speedControl.minValue;
        deltaSpeed = maxSpeed - minSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        ControlPlane();
    }
    void ControlPlane()
    {
        flySpeed = speedControl.value;
        float speedRate = (flySpeed - minSpeed) / deltaSpeed;
        if (speedRate < 0.3f)
        {
            speedState.color = Color.green;
        }
        else if (speedRate < 0.6f)
        {
            speedState.color = Color.yellow;
        }
        else
        {
            speedState.color = Color.red;
        }
        GetComponent<Rotation>().zRotate = speedControl.value;
        pointer.transform.position = transform.position;
        transform.position += transform.forward * flySpeed * Time.deltaTime;
        float horizontal;
        float vertical;
        if (joystick == null)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
        }
        if (transform.position.y >= highLimit)
        {
            vertical = 0.5f;
        }
        curVertical = Mathf.SmoothDamp(curVertical, vertical, ref velocityV, smoothV);
        curHorizontal = Mathf.SmoothDamp(curHorizontal, horizontal, ref velocityH, smoothH);

        yaw += curHorizontal * yawSpeed * Time.deltaTime;
        float pitch = Mathf.Lerp(0, 30, Mathf.Abs(curVertical)) * Mathf.Sign(curVertical);
        float roll = Mathf.Lerp(0, 40, Mathf.Abs(curHorizontal)) * -Mathf.Sign(curHorizontal);
        //
        transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
    }

    void Bomb()
    {

    }
}
