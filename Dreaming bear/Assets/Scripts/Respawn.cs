using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  This script makes sure that the player doesnt fall for ever if the player falls of the map

public class Respawn : MonoBehaviour
{
    public ThirdPersonMovement thirdPersonMovement;
    public GameObject Player;

    public void OnTriggerEnter(Collider collision) { // if player hits the respawn box place him back were he came from
        if(collision.gameObject == Player) { 
            thirdPersonMovement.respawn(); 
        }
        
    }
}
