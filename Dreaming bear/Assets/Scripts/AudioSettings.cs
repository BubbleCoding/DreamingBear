using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script manages the volume of the game

public class AudioSettings : MonoBehaviour
{

    FMOD.Studio.Bus Master;
    float MasterVolume = 1f;
    public GameObject VolumeSlider;

    void Awake()    // On awake check the current volume and set the volume and the slider to this value
    {
        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
        Master.getVolume(out MasterVolume);
        VolumeSlider.GetComponent<Slider>().value = MasterVolume;
    }


    void Update()   // set new volume
    {
        Master.setVolume(MasterVolume);
    }

    public void MasterVolumeLevel(float newMasterVolume) // receive new volume
    {
        MasterVolume = newMasterVolume;
    }
}
