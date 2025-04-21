using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Evidence : MonoBehaviour
{
    [Header("Evidence")] //Variables related to evidence
    public int evidenceCount; //int for how much evidence is collected
    public int evidenceTotal; //int for total evidence in level
    bool evidenceFinished = false; //if evidence is finished being collected call win condition
    [SerializeField] GameObject timerText;


    // Start is called before the first frame update
    void Start()
    {
        evidenceCount = 0; //initialize evidence collected
        evidenceTotal = 3;
        evidenceFinished = false; //initialize win condition for level
    }

    // Update is called once per frame
    void Update()
    {
        if (evidenceCount < evidenceTotal) { evidenceFinished = false; } //if evidence collected is less than total, win not met
        if (evidenceCount == evidenceTotal) { evidenceFinished = true; } //if evidence collected is equal to total, win met

        if (evidenceFinished) //if win condition met
        {
            if (SavedData.apartmentBestTime > (60 - timerText.gameObject.GetComponent<timer>().timerTime))
            {
                SavedData.apartmentBestTime = 60 - timerText.gameObject.GetComponent<timer>().timerTime;
            }
            SavedData.apartmentCompletion = true;

            Debug.Log("rooftop best time is " + SavedData.sewerBestTime);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(5);
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