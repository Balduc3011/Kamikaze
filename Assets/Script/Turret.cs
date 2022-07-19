using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform kamikaze;
    public Transform gunBase;
    public Transform barrel;
    public float aimRange = 100f;
    public float highEdgeRange = 85f;
    public float highLook = 0f;
    public float lowEdgeRange = -5f;
    public float lowLook = 0f;

    public GameObject bulletPrefab;
    public Transform shootPoint;
    float fireTimer = 0f;
    public float fireCoolDown = 0.67f;
    void Start()
    {
        CalculateRange();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        fireTimer += Time.deltaTime;
        if(Vector3.Distance(transform.position, kamikaze.position) <= 100f)
        {
            Vector3 rotateBase = new Vector3(kamikaze.position.x, gunBase.position.y, kamikaze.position.z);
            gunBase.LookAt(rotateBase);
            if (kamikaze.position.y >= transform.position.y + lowLook && kamikaze.position.y <= transform.position.y + highLook)
            {
                barrel.LookAt(kamikaze);
                Fire();
            }
        }
    }

    void Fire()
    {
        if(fireTimer < fireCoolDown)
        {
            return;
        }
        else
        {
            fireTimer = 0f;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
        
    }

    void CalculateRange()
    {

        float highPiAngle = Mathf.PI * (highEdgeRange) / 180f;
        highLook = aimRange * Mathf.Sin(highPiAngle);

        float lowPiAngle = Mathf.PI * (lowEdgeRange) / 180f;
        lowLook = aimRange * Mathf.Sin(lowPiAngle);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 100f);
    }
}
