using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script triggers if the player interacts with the shrink potion

public class Shrink : Interactable
{
    [FMODUnity.EventRef]
    public string inputsoundShrink;
    public virtual void Trigger()
    {
        if(ThirdPersonMovement.small == false) // If the player interacts  with the shrink potion shrink
        {
            ThirdPersonMovement.shrink = true;
            FMODUnity.RuntimeManager.PlayOneShot(inputsoundShrink);
        }
        
    }


}