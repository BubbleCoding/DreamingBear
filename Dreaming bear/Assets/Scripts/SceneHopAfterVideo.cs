using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

// This script is used to change scenes after a video is ran

public class SceneHopAfterVideo : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public int sceneNumber;
    private bool fix;
    FMOD.Studio.Bus MasterBus;

    void Start()
    {
        MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/"); // Get sound
    }

    void Update()   
    {
        if (videoPlayer.isPlaying) {    // Check if video is still playing
            fix = true;
        }
        if (videoPlayer.isPlaying == false && fix == true)  // If video stopped playing go to the next scene
        {
            SceneManager.LoadScene(sceneNumber);
            MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

    }
}
