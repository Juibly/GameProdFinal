using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SewerWinTrigger : MonoBehaviour
{
    [SerializeField] GameObject timerText;

    private void OnTriggerEnter(Collider other)
    {
        if(SavedData.sewerBestTime > (60 - timerText.gameObject.GetComponent<timer>().timerTime))
        {
            SavedData.sewerBestTime = 60 - timerText.gameObject.GetComponent<timer>().timerTime;
        }
        SavedData.sewerCompletion = true;

        Debug.Log("rooftop best time is " + SavedData.sewerBestTime);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(5);
    }


    
}
