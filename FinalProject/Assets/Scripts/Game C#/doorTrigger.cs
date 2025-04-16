using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTrigger : MonoBehaviour
{
    [Header("Door")] //Variables
    public bool isPowered;
    [SerializeField] GameObject door;
    [SerializeField] GameObject interactText;
    public AudioSource LeverSource;


    [Header("Door Movement")] //Variables for opening and closing door
    public float speed; //speed door opens/closes
    private float step; //incriments door moves
    public Vector3 openPos; //set position for when open
    public Vector3 closePos; //set position for when closed

    [Header("Lever")]
    public bool onOff; //bool for switching the levers animation state
    [SerializeField] GameObject whatAmI; //see if the item that is the source of the script is a lever or a powerbox 

    // Start is called before the first frame update
    void Start()
    {
        door.transform.position = closePos; //at start door is closed
        onOff = false; // lever turned off at start
    }


    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;

        if (isPowered)
        {
            //door position open
            door.transform.position = Vector3.MoveTowards(door.transform.position, openPos, step);
        }
        if (!isPowered)
        {
            //door position closed
            door.transform.position = Vector3.MoveTowards(door.transform.position, closePos, step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPowered) {
            if (whatAmI.CompareTag("Lever"))
            {
                interactText.SetActive(true);
            }
        }
        if (other.gameObject.CompareTag("ShockFist")) // powered by shockfist ability
        {
            if (whatAmI.CompareTag("ElectricalBox")) // is this a electrical box
            {
                isPowered = !isPowered;
            }
        }

        if (other.gameObject.CompareTag("PickUp")) // pressing pick up
        {
            if(whatAmI.CompareTag("Lever")) // is this a lever
            {
                isPowered = !isPowered;
                onOff = !onOff;
                LeverSource.Play();
                interactText.SetActive(false);
                //flip lever
                if (isPowered) { whatAmI.transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self); }
                if (!isPowered) { whatAmI.transform.Rotate(0.0f, -180.0f, 0.0f, Space.Self); }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        interactText.SetActive(false);
    }
}