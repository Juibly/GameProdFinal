using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //variables related to movement speed
    public float walkSpeed = 20f;
    public float runSpeed = 28f;
    public float jumpPower = 18f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 1f;
    public float crouchHeight = .5f;
    public float crouchSpeed = 15f;
    public float gravity = 12f;

    //variables related to abilities
    public float slowfall;

    //variables related to keybinds
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode runKey = KeyCode.LeftShift;

    //variables misc
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    public CharacterController characterController;
    public Camera playerCamera;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);


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
            walkSpeed = 20f;
            runSpeed = 28f;
        }

        characterController.Move(moveDirection * Time.deltaTime); //move the character controller

        if (canMove) //if you can move
        {
            //changes rotation X value
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed; 
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            //changes the rotation of the player (and consequently the camera because it is a child) to match mouse movement
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);


            //this changes the camera rotation, so this is what youll prob change for your rework of the camera Christian
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); 
            

        }


    }
}
