using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // movement variables
    public float movingSpeed;
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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Enemy vision
        // transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * visionDistance, Color.yellow);
        // if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitInfo, visionDistance))
        // {
        //     if (hitInfo.collider.tag == "Player")
        if (Vector3.Distance(transform.position, player.position) < shootDistance)
        {
            // Enemy shooting
            if (Time.time > nextShotTime)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                nextShotTime = Time.time + timeBetweenShots;
            }
        }
        // }

        // Enemy Movement
        if (transform.position != patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, movingSpeed * Time.deltaTime);
            // transform.LookAt(patrolPoints[currentPointIndex].transform);
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
