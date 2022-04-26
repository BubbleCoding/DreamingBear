using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script mangeses the key and the locked door

public class Key : Interactable
{

    [SerializeField] private GameObject DoorObject;   // The door
    [SerializeField] private GameObject KeyObject;    // The key
    [SerializeField] private GameObject keyPopUp;     // The text pop up at the key
    [SerializeField] private GameObject ReturnStairs; // The stairs that appear after that the key got picked up
    [SerializeField] private GameObject TextWhenGotKey; // The popup at the door when we have the key
    [SerializeField] private GameObject TextWhenNotKey; // The popup at the door when we don't have the key

    //Is this script attached to the door or to the key?
    public bool door;
    public bool key;


    [FMODUnity.EventRef]
    public string inputsoundKey;
    public override void Trigger()
    {
        if (ThirdPersonMovement.gotKey == false && key) // Found the key
        {
            ThirdPersonMovement.gotKey = true;

            KeyObject.SetActive(false);
            keyPopUp.SetActive(false);
            ReturnStairs.SetActive(true);
            TextWhenNotKey.SetActive(false);
            TextWhenGotKey.SetActive(true);

            FMODUnity.RuntimeManager.PlayOneShot(inputsoundKey);
        }
        else if (ThirdPersonMovement.gotKey == true && door) // Has the key and opens the door
        {
            DoorObject.GetComponent<Door>().enabled = true;
            DoorObject.GetComponent<Key>().enabled = false;
            keyPopUp.SetActive(false);
            KeyObject.SetActive(false);
        }
            
        else if (ThirdPersonMovement.gotKey == false && door)   // Doesnt have the key
        {
            Debug.Log("You need a key");
        }
    }


}