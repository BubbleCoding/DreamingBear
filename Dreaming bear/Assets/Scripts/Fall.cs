using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Since we have to change the drag of the large box to make it be pushed slower we need to fix the gravity since the drag changes the gravity aswell.
// In the location that the big box falls down the drag becomes 0 and asoon as it hits the ground it goes back to 40

public class Fall : MonoBehaviour
{
    public bool fall;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody body = other.attachedRigidbody;

        if (fall)
        {
            body.drag = 0;
        }
        else
        {
            body.drag = 40;
        }
    }
}
