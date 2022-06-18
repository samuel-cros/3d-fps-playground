using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    // GENERAL
    private Rigidbody bulletRigidbody;
    [SerializeField] float speed = 10f;

    //oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
    // AWAKE
    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    //oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
    // Start
    private void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed;
    }

    //oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
    // Collisions
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

}
