using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // movement variables
    [SerializeField]
    private float movingSpeed;
    public Transform[] patrolPoints;
    public float waitTime;
    int currentPointIndex;
    bool once;

    //shoot variables
    public GameObject projectile;
    public float timeBetweenShots;
    private float nextShotTime = 1f;
    public Vector3 playerHit;
    public float shootDistance;

    public Transform player;
    // [SerializeField]
    // private int health = 3;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < shootDistance)
        {
            // Enemy shooting
            if (Time.time > nextShotTime)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                nextShotTime = Time.time + timeBetweenShots;
            }
        }

        // Enemy Movement
        if (transform.position != patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, movingSpeed * Time.deltaTime);
        }
        else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }

    }
    //Enemy Health
    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("playerFire"))
    //     {
    //         health -= 1;
    //     }
    //     if (health <= 0)
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;
    }
}
