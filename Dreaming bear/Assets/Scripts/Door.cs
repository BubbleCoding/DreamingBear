using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// This script is used to handle the animations of the doors in the game

public class Door : MonoBehaviour
{
    Animator anim;
    public bool locked;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) // Open the door
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("OpenDoor");
        }

    }
    void OnTriggerExit(Collider other)  // Close the door
    {
        if (other.CompareTag("Player") && !locked)
        {
            anim.enabled = true;
        }

    }
    void pauseAnimationEvent()  // Make sure the door stays open or closed
    {
        anim.enabled = false;
    }
    void OnTriggerStay(Collider other)  // Keep the door where you need a key for open. This way its easier to push the large box through
    {
        if (locked)
        {
            if (other.CompareTag("Player"))
            {
                anim.SetTrigger("OpenDoor");
            }
        }
    }
}
