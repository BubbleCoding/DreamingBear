using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the on button click sound behaviour
public class onclick : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string inputSoundClick;
 
    public void Sound() // if clicked play sound
    {
        FMODUnity.RuntimeManager.PlayOneShot(inputSoundClick);
    }

   
}
