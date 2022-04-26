using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script manages the instruction popup within the game

public class PopUp : MonoBehaviour
{
    // All the needed components
    [SerializeField] private Image customImage;
    [SerializeField] private Text customText;
    [SerializeField] private GameObject canvas;

    public bool fadeIn;
    public float fadeInTime = 2;
    public bool timedShow;
    public float showTime;
    public bool fadeOut;
    public float fadeOutTime = 2;
    public bool oneRun;
    private bool stop;


    private void OnTriggerEnter(Collider other) // Activate the canvas
    {
        

        if (other.CompareTag("Player") && !stop)    // Fade in the canvas
        {
            if (fadeIn)
            {
                customImage.canvasRenderer.SetAlpha(0.01f);
                customText.canvasRenderer.SetAlpha(0.01f);
                canvas.SetActive(true);
                FadeIn();
            }
            else    // instant canvas no fade
            {
                canvas.SetActive(true);
            }

        }
    }
    private void OnTriggerExit(Collider other)// Deactivate the canvas
    {
        if (other.CompareTag("Player") && !stop)
        {
            if (fadeOut)    // Fadeout in the canvas
            { 
                FadeOut();
            }
            if (oneRun) {
                stop = true;
            }
        }
    }

    private void FadeIn() { //manage  fadeIn
        customImage.CrossFadeAlpha(1, fadeInTime, false);
        customText.CrossFadeAlpha(1, fadeInTime, false);
    }

    private void FadeOut() {  //manage  fadeOut
        customImage.CrossFadeAlpha(0, fadeOutTime, false);
        customText.CrossFadeAlpha(0, fadeOutTime, false);
        
    }
}
