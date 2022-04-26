using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

// This script manages the player character
// Movement, size, physics, animations triggers, audio triggers

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    // Physics variables
    public float pushPower = 0.5f;
    public float speed = 6;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;

    public GameObject respawnPoint; // Respawn point location
    
    // Variable to see if the player is on the ground
    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    // Player possible states
    public static bool isPushing;
    public static bool isSkipping;
    public static bool isSkippingSound;
    public static bool isJumping;
    public static bool isInAir;

    // sfx references
    [FMODUnity.EventRef]
    public string inputsoundWalk;
    [FMODUnity.EventRef]
    public string inputsoundJump;
    [FMODUnity.EventRef]
    public string inputsoundShrink;
    [FMODUnity.EventRef]
    public string inputsoundGrow;
    [FMODUnity.EventRef]
    public string inputsoundDrop;


    //changeSize variables
    public static bool shrink = false;
    public static bool grow = false;
    public static bool small = false;
    public static bool large = true;

    // Change size speed
    int count = 0;
    int growTime = 100;
    float timer = 0;
    float waittime = 0.001f;
    public static Vector3 Playersize = new Vector3(1, (float)1.154, 1);

    public static bool gotKey; // Does the player have the key

    // fixed interval for the skipping sound
    float timer2 = 0;
    float waittime2 = 1;

    void Start()
    {
        // Remove the cursor from the screen and lock on the game screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        grounded();
        ChangeSize();
        Jump();
        Gravity();
        Movement();

    }

    void OnControllerColliderHit(ControllerColliderHit hit) // Manage pushing objects and pushing animation
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // Check if object is pushable 
        if (body == null || body.isKinematic || hit.gameObject.tag != "Pushable")
        {
            isPushing = false;
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            isPushing = false;
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);  // Push direction

        // push object
        body.velocity = pushDir * pushPower;
        isPushing = true;
        isSkipping = false;

    }

    void ChangeSize()   // Change the size of the player character when drinking the grow or shrink potion
    {
        if (shrink == true || grow == true)
        {
            if (shrink == true)
            {
                if (count <= growTime)
                {
                    timer += Time.deltaTime;
                    if (timer >= waittime)  // Shrink in a timer interval
                    {
                        count++;
                        Playersize = Vector3.Scale(Playersize, new Vector3(0.995f, 0.995f, 0.995f));    //Reduce size of player character
                        timer -= waittime;
                    }
                }
                if (count > growTime)  // Check if needs to stop shrinking
                {
                    count = 0;
                    shrink = false;
                    small = true;
                }
            }


            if (grow == true)
            {
                if (count <= growTime)
                {
                    timer += Time.deltaTime;
                    if (timer >= waittime)  // Grow in a timer interval
                    {
                        count++;
                        Playersize = Vector3.Scale(Playersize, new Vector3(1.005f, 1.005f, 1.005f));    //Increase size of player character
                        timer -= waittime;
                    }
                }
                if (count > growTime)   // Check if needs to stop growing
                {
                    count = 0;
                    grow = false;
                    small = false;
                }
            }
            transform.localScale = Playersize;
        }
       
    }

    public void Jump()  // Manage the jumping movement and sound effect
    {

        if (isGrounded && velocity.y < 0) // Makes the gravity of the consistent
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)  // Check if legal place to jump
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            isJumping = true;
            isInAir = true;
        }
        else if (isGrounded && isInAir) // Play drop sfx if fall on the ground
        {
            isJumping = false;
            isInAir = false;
            FMODUnity.RuntimeManager.PlayOneShot(inputsoundDrop);
        }

        if (isJumping && isGrounded)    // Play jump sfx if jumping
        {
            FMODUnity.RuntimeManager.PlayOneShot(inputsoundJump);
        }
    }

    public void Gravity() // Manage in game gravity
    {  
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Movement() // Manage the character x and z axis movement
    {    
        
        float horizontal = Input.GetAxisRaw("Horizontal")/2;
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f && !isPushing)  // If there is movement and the player isn't pushing an object
        {
            if (isGrounded) // if the player is standing on the ground the player character should be skipping
            {
                isSkipping = true;
            } else {isSkipping = false; }


            // Movement math
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            //FMODUnity.RuntimeManager.CreateInstance(inputsoundWalk);  // footstep sfx 

            // Play the stepping sound every waittime2 time
            timer2 += Time.deltaTime;
            if (timer2 > waittime2) { 
                FMODUnity.RuntimeManager.PlayOneShot(inputsoundWalk);
                timer2 -= waittime2;
            }
            

        }
        else {
            isSkipping = false;
        }
    }

    public void respawn()  // Changes the position of the player character if the player fall of the map
    {
        controller.enabled = false;
        controller.transform.position = respawnPoint.transform.position;
        controller.enabled = true;
    }

    public void setRespawnPoint() // Set respawn point in case the player falls of the map
    {
        respawnPoint.transform.position = controller.transform.position;
    }

    public void grounded() // Check if the player is standing on the ground
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Check if the player is standing on the ground

        if (isGrounded)
        {
            setRespawnPoint();
        }
    }
}