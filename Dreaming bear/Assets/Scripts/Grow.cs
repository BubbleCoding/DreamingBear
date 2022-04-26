using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script triggers if the player interacts with the grow potion

public class Grow : Interactable
{
    [FMODUnity.EventRef]
    public string inputsoundGrow;

    public virtual void Trigger() // If the player intreacs with the grow potion grow
    {
        if (ThirdPersonMovement.small == true)
        {
            ThirdPersonMovement.grow = true;
            FMODUnity.RuntimeManager.PlayOneShot(inputsoundGrow);
        }
    }


}