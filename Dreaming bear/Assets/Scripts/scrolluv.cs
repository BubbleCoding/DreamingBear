using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the background in the main menu

public class scrolluv : MonoBehaviour
{
    public float speed = 10f; // scroll speed

    void Update()   // Move the texture of the background
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.y += Time.deltaTime / speed;
        mat.mainTextureOffset = offset;
        
    }
}
