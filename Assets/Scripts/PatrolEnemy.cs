using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    // movement variables
    public float movingSpeed;
    public Transform[] patrolPoints;
    public float waitTime;
    int currentPointIndex;
    bool once;

    // vision variables
    public float rotationSpeed;
    public float visionDistance;

    //shoot variables
    public GameObject projectile;
    public float timeBetweenShots;
    private float nextShotTime;
    public Vector3 playerHit;


    private void Update()
    {
        // Enemy vision
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, visionDistance))
        {
            if (hitInfo.collider.tag == "Player")
            {
                // Enemy shooting
                if (Time.time > nextShotTime)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity, gameObject.transform);
                    nextShotTime = Time.time + timeBetweenShots;
                }
            }
        }

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
