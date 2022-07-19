using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string target = "Player";
    public float bulletSpeed = 100f;
    float timer = 0f;
    public GameObject explosion;
    public float existTime = 2f;

    private void Start()
    {
        existTime = 200 / bulletSpeed;
    }
    void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        timer += Time.deltaTime;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.tag == target)
            {
                KaBoom();
            }
        }
        if (timer >= existTime)
        {
            KaBoom();
        }
    }
    void KaBoom()
    {
        if(explosion != null)
        {
            GameObject boom = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(boom, 1f);
        }
        Destroy(gameObject);
    }
}
