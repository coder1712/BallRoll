using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyDoor : MonoBehaviour
{
    public int keyCount;
    PlayerController player;
    public GameObject lDoor, rDoor;
    void Start()
    {
        player = GetComponent<PlayerController>();
        lDoor = transform.GetChild(0).gameObject;
        rDoor = transform.GetChild(1).gameObject;
    }
    void Update()
    {
        if (player.key == keyCount)
        {
            lDoor.transform.Rotate(Vector3.down * 90);
        }

    }
}
