using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages all the animation of the player character

public class PlayerAnimation : MonoBehaviour
{
    Animator m_Animator;

    void Start()
    {
        m_Animator = GetComponent<Animator>(); //GetComponent function is part of MonoBehaviour
    }

    void FixedUpdate()
    {
        //the first parameter is the name of the Animator Parameter that you want to set the value of, and the second is the value you want to set it to.
        m_Animator.SetBool("IsSkipping", ThirdPersonMovement.isSkipping); 
        m_Animator.SetBool("IsJumping", ThirdPersonMovement.isJumping); 
        m_Animator.SetBool("IsPushing", ThirdPersonMovement.isPushing);
    }
}