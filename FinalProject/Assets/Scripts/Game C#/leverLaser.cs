using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverLaser : MonoBehaviour
{
    [Header("Laser")] //Variables
    public AudioSource LeverSource;
    public bool isPowered;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject lever;

    // Start is called before the first frame update
    void Start()
    {
        laser.SetActive(true); //lasers are on at start 
        isPowered = false; //lever is off at start
    }

    // Update is called once per frame
    void Update()
    {
        laser.SetActive(isPowered); //update lasers based on power to lever
        if(isPowered)
        {
            //turn lever in on position
        }
        if(!isPowered)
        {
            //turn lever in off position
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) // pressing pick up
        {
            isPowered = !isPowered; //turn power on/off
            LeverSource.Play(); //play vfx if flipped
        }
    }
}
