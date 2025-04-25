using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

public AudioSource jumpSound;

    [Header("Cheats")] //Variables for Cheats
    public static bool cheatOn = false;
    public bool cheatOn1 = false;

    //variables related to movement speed
    [Header("Movement Speed")]
    public float walkSpeed = 38f;
    public float runSpeed = 58f;
    public float jumpPower = 22f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float gravity = 12f;

    //variables related to abilities
    [Header("Abilities")]
    public float slowfall;
    public bool shieldUp = false;

    //variables related to camera
    [Header("Camera")]
    public CharacterController characterController;
    public Camera playerCamera;

    //variables related to character orientation
    [Header("Orientation")]
    public Transform playerOrientation;
    public Vector3 moveDirection = Vector3.zero;
    [SerializeField] GameObject player;
    private float rotationX;
    private float rotationY;
    public Vector3 movementInput;
    private float movementAmount;

    //misc 
    public bool canMove = true;
    private Animator animator; // kyle animator

    //variables related to keybinds
    [Header("Animation")]
    public bool DForward; //Direction Forward
    public bool DBackward; //Direction Backward
    public bool DLeft; //Direction Left
    public bool DRight; //Direction Right
    public bool hasPunched; // is shockfist pressed
    public bool hasGrappled; // is grapple pressed
    public bool jumping; // is jumping PRESSED
    public bool isRunning; // is player running
    public bool isWalking; // is player walking

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //acquire animations

    }

    // Update is called once per frame
    void Update()
    {
        if (cheatOn == true)
        {
            cheatOn1 = true;
        }

        if (cheatOn1 == true)
        {
            runSpeed = 600;
            walkSpeed = 400;
        }

        if (GetComponent<Grappling>().freeze)//for freezing while grappling
        {
            moveDirection = Vector3.zero;
            characterController.Move(Vector3.zero);
            return;
        }

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        isRunning = Input.GetButton("Run"); //running is true if you hold the button for running
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0; //detect current speed for vertical movement
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0; //detect current speed for horizontal movement

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButtonDown("Jump") && canMove && characterController.isGrounded) { jumping = true; jumpSound.Play();} //turn on jump animation the frame the button is PRESSED
        
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) //if jump is HELD and you can move and are on the ground
        {
            moveDirection.y = jumpPower;
        }
        else //if not jumping, reset the value
        {
            moveDirection.y = movementDirectionY;
            jumping = false; // turn off animation
        }

        

            if (!characterController.isGrounded) //if character is in air
        {
            //where youd apply the slowfall when you write this Mason
            moveDirection.y -= gravity * Time.deltaTime; //apply gravity slash friction
        }

        // movement animation bools
        if(Input.GetAxis("Vertical") > 0) //if vertical movement is positive
        {
            DForward = true; //facing forward true
            DBackward = false; //facing backward false
        }
        else if(Input.GetAxis("Vertical") < 0) //if vertical movement is negative
        {
            DForward = false; //facing forward false
            DBackward = true; //facing backward true
        }
        else { DForward = false; DBackward = false; } //if no vertical movement, neither is true

        if (Input.GetAxis("Horizontal") > 0) //if horizontal movement is positive
        {
            DRight = true; //facing right true
            DLeft = false; //facing left false
        }
        else if (Input.GetAxis("Horizontal") < 0) //if horizontal movement is negative
        {
            DRight = false; //facing right false
            DLeft = true; //facing left true
        }
        else { DRight = false; DLeft = false; } //if no horizontal movement, neither is true
        if(isRunning) { isWalking = false; } // if running, walking is false
        if (!isRunning && (Input.GetAxis("Vertical") == 0) && (Input.GetAxis("Horizontal") == 0)) { isWalking = false; } // if not running but no movement input then walking is false
        else { isWalking = true; } // else walking is true

        // ability animation bools
        if (Input.GetButtonDown("Shockfist")) { hasPunched = true; } //if press shockfist button, bool is true
        else { hasPunched = false; } //else it is false

        if (Input.GetButtonDown("Grapple")) { hasGrappled = true; } //if press grapple button, bool is true
        else { hasGrappled = false; } //else it is false

        //Animator Bools set to update to whatever the bool is
        animator.SetBool("SWforward", DForward);
        animator.SetBool("SWback", DBackward);
        animator.SetBool("SWleft", DLeft);
        animator.SetBool("SWright", DRight);
        animator.SetBool("haspunched", hasPunched);
        animator.SetBool("hasgrabbled", hasGrappled);
        animator.SetBool("hasjumped", jumping);
        animator.SetBool("hasshield", shieldUp);
        animator.SetBool("isrunning", isRunning);
        animator.SetBool("iswalking", isWalking);


        if (canMove) //if you can move
        {
            characterController.Move(moveDirection * Time.deltaTime); //move the character controller

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && shieldUp == false)//changes player rotation based off movedirection magnitude
            {
                player.transform.rotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
            }
            if (shieldUp == true)
            {
                player.transform.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);
            }
            
            // orientation object mason set up
            playerOrientation.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);
        }
    }
}