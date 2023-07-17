using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackProjectile : MonoBehaviour
{
    public float speed;
    public float lifetime = 4f;
    public Transform player;
    public Vector3 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
