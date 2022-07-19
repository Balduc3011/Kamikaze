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

    // Fire

    public GameObject bulletPrefab;
    public Transform LBarrel;
    public Transform RBarrel;
    float fireTimer = 0f;
    public float fireCoolDown = 0.33f;

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
        fireTimer += Time.deltaTime;
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
            if (Input.GetButton("Fire1"))
            {
                Fire();
            }
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

    public void Fire()
    {
        if (fireTimer < fireCoolDown)
        {
            return;
        }
        else
        {
            fireTimer = 0f;
            GameObject Lbullet = Instantiate(bulletPrefab, LBarrel.position, transform.rotation);
            GameObject Rbullet = Instantiate(bulletPrefab, RBarrel.position, transform.rotation);
        }
    }
}
