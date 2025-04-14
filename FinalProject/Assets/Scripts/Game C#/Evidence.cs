using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class evidence : MonoBehaviour
{
    [Header("Evidence")] //Variables related to evidence
    public int evidenceCount; //int for how much evidence is collected
    public int evidenceTotal; //int for total evidence in level
    bool evidenceFinished = false; //if evidence is finished being collected call win condition


    // Start is called before the first frame update
    void Start()
    {
        evidenceCount = 0; //initialize evidence collected
        evidenceFinished = false; //initialize win condition for level
    }

    // Update is called once per frame
    void Update()
    {
        if (evidenceCount < evidenceTotal) { evidenceFinished = false; } //if evidence collected is less than total, win not met
        if (evidenceCount == evidenceTotal) { evidenceFinished = true; } //if evidence collected is equal to total, win met

        if (evidenceFinished) //if win condition met
        {
            //win condition true, i believe the win script was changed to be a different script for rooftop than it is apartment
            //whatever the variable is that brings up the win screen/ save data etc goes here!
            // for now here is a placeholder !
            Debug.Log("All evidence collected!");
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Evidence")) // if evidence hitbox collides with pickup hitbox
        {
            other.gameObject.SetActive(false); //make evidence that was collected with pickup no longer active
            Debug.Log("Evidence Collected");
            evidenceCount++; //add one to evidence collected counter
        }
    }
}