using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rooftopWinTrigger : MonoBehaviour
{
    [SerializeField] GameObject timerText;

    private void OnTriggerEnter(Collider other)
    {
        if(SavedData.rooftopBestTime > (60 - timerText.gameObject.GetComponent<timer>().timerTime))
        {
            SavedData.rooftopBestTime = 60 - timerText.gameObject.GetComponent<timer>().timerTime;
        }
        SavedData.rooftopCompletion = true;

        Debug.Log("rooftop best time is " + SavedData.rooftopBestTime);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(5);
    }


    
}
