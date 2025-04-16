using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverLaser : MonoBehaviour
{
    [Header("Laser")] //Variables
    public AudioSource LeverSource;
    public bool isPowered;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject whatAmI;
    [SerializeField] GameObject interactText;

    // Start is called before the first frame update
    void Start()
    {
        laser.SetActive(true); //lasers are on at start 
        isPowered = true; //laser is on at start
    }

    // Update is called once per frame
    void Update()
    {
        laser.SetActive(isPowered); //update lasers based on power to lever
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPowered)
        {
            if (whatAmI.CompareTag("Lever"))
            {
                interactText.SetActive(true);
            }
        }
        if (other.gameObject.CompareTag("ShockFist")) // powered by shockfist ability
        {
            if (whatAmI.CompareTag("ElectricalBox")) // is this a electrical box
            {
                isPowered = !isPowered; //turn power on/off
                //LeverSource.Play(); //play vfx if punched
            }
        }

        if (other.gameObject.CompareTag("PickUp")) // pressing pick up
        {
            if (whatAmI.CompareTag("Lever")) // is this a lever
            {
                isPowered = !isPowered; //turn power on/off
                LeverSource.Play(); //play vfx if flipped
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
