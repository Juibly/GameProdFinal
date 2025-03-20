using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTrigger : MonoBehaviour
{
    [Header("Door")] //Variables
    public bool isPowered;
    [SerializeField] GameObject door;


    [Header("Door Movement")] //Variables for opening and closing door
    public float speed; //speed door opens/closes
    private float step; //incriments door moves
    public Vector3 openPos; //set position for when open
    public Vector3 closePos; //set position for when closed

    // Start is called before the first frame update
    void Start()
    {
        door.transform.position = closePos; //at start door is closed
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
        if (other.gameObject.CompareTag("ShockFist"))
        {
            isPowered = !isPowered;
        }
    }
}