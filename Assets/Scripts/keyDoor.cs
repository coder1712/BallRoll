using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyDoor : MonoBehaviour
{
    public int keyCount;
    public GameObject player;
    public GameObject lDoor, rDoor;
    public AudioSource audio;
    int close = 0;
    bool play = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lDoor = transform.GetChild(0).gameObject;
        rDoor = transform.GetChild(1).gameObject;
    }
    void Update()
    {
        if (player.GetComponent<PlayerController>().key == keyCount && close <= 50)
        {
            if (Vector3.Distance(rDoor.transform.position, player.transform.position) < 15f)
            {
                if(!play){
                    audio.Play();
                    play=true;
                }

                lDoor.transform.Rotate(Vector3.down * 90 * Time.deltaTime );
                rDoor.transform.Rotate(Vector3.down * -90 * Time.deltaTime );
                close += 1;
            }
        }

    }
}
