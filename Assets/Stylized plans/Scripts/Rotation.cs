using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Transform fan;
    public float zRotate = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fan.Rotate(0, 0, -1f * zRotate, Space.Self);
    }
}
