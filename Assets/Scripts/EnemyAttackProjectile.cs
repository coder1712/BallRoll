using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackProjectile : MonoBehaviour
{
    public float speed;
    public float lifetime = 4f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(GetComponentInParent<Transform>().forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            // Debug.Log("Projectile hit the player!");

            // // Get the intersection point of the projectile and the player's raycast
            // RaycastHit hit;
            // if (Physics.Raycast(transform.position, transform.forward, out hit))
            // {
            //     // Get the intersection point
            //     Vector3 intersectionPoint = hit.point;
            //     Debug.Log("Intersection point: " + intersectionPoint);
            // }

            // // Destroy the projectile
            Destroy(gameObject);
        }
    }

}
