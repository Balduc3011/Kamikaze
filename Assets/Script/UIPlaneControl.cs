using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlaneControl : MonoBehaviour
{
    public PlaneControl plane;
    public bool isFire = false;
    public void Fire()
    {
        isFire = true;
    }
    public void StopFire()
    {
        isFire = false;
    }

    private void Update()
    {
        if(isFire)
        {
            plane.Fire();
        }
    }
}
