using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script changes scenes after you got to teddy

public class Victory : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Victory");
    }
}
