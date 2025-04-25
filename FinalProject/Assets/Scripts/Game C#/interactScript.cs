using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactScript : MonoBehaviour
{
    [Header("Pick Up Collider")] //Variables
    public bool pickUp = false;
    [SerializeField] GameObject PickupCollider;


    //[Header("Keybinds")] //Variables for Keybinds
    //public KeyCode pickupKey = KeyCode.E; // E to interact
    //this was changed to instead use the input manager with the button "PickUp"

    // Start is called before the first frame update
    void Start()
    {
        PickupCollider.SetActive(false); // this turns off the pickup collider on start
    }

    // Update is called once per frame
    void Update()
    {
        PickupCollider.SetActive(pickUp); // this changes if the collision for pickup is active or not based on bool

        if (Input.GetButton("PickUp"))
        {
            pickUp = true; //activates collision for picking stuff up
        }
        else { pickUp = false; } // if not picking something up, remove the collision for that
    }
}
