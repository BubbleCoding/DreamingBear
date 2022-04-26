using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This script manages every type of interaction
// Other scripts inherit from this script to gain interactivity
public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public bool isInRange = false;
    public KeyCode interactKey;
    public UnityEvent interactAction;


    private void Update()
    {
        if (isInRange)  // If in range run interaction
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }

    public virtual void OnTriggerEnter(Collider collision) // Check if in range to interact
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;

        }
    }
    public virtual void OnTriggerExit(Collider collision) // Check if in range to interact
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;

        }
    }

    public virtual void Small(Collision col)
    {
        col.gameObject.transform.localScale += new Vector3(1, 0, 1);
    }


    public virtual void Trigger()   // If no override was given flip the render of the object
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        bool flip = !renderer.enabled;

        renderer.enabled = flip;
    }


}
