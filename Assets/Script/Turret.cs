using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform kamikaze;
    public Transform gunBase;
    public Transform barrel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if(Vector3.Distance(transform.position, kamikaze.position) <= 100f)
        {
            Vector3 rotateBase = new Vector3(kamikaze.position.x, gunBase.position.y, kamikaze.position.z);
            gunBase.LookAt(rotateBase);
            if (transform.position.y <= kamikaze.position.y)
            {
                barrel.LookAt(kamikaze);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 100f);
    }
}
