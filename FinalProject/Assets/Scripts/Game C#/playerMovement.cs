using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //variables related to movement speed
    [Header("Movement Speed")]
    public float walkSpeed = 38f;
    public float runSpeed = 58f;
    public float jumpPower = 22f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 35f;
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

    //variables related to keybinds
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode runKey = KeyCode.LeftShift;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<Grappling>().freeze)//for freezing while grappling
        {
            moveDirection = Vector3.zero;
            characterController.Move(Vector3.zero);
            return;
        }

        if (Input.GetKeyUp("escape")) //kyle quit code :3 
        {
            Debug.Log($"Quitting App on Escape Key struck.");
            Application.Quit();
        }

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        bool isRunning = Input.GetKey(runKey); //running is true if you hold the button for running
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0; //detect current speed for vertical movement
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0; //detect current speed for horizontal movement

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) //if jump is pressed and you can move and are on the ground
        {
            moveDirection.y = jumpPower;
        }
        else //if not jumping, reset the value
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded) //if character is in air
        {
            //where youd apply the slowfall when you write this Mason
            moveDirection.y -= gravity * Time.deltaTime; //apply gravity slash friction
        }

        if (Input.GetKey(crouchKey) && canMove) //if holding crouch and can move
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else //if not crouched, reset the values
        {
            characterController.height = defaultHeight;
            walkSpeed = 38f;
            runSpeed = 58f;
        }



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