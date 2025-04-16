using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float showTime;
    public float timerTime;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        InitValues();
    }

    // Update is called once per frame
    void Update()
    {
        RunTimer();
    }

    void InitValues()
    {
        timerTime = 60;
    }


    void RunTimer()
    {
        //count downs time and shows in seconds
        timerTime -= Time.deltaTime;
        showTime = (Mathf.Round(timerTime) * 100) / 100;
        timerText.text = showTime.ToString();
        
        //you lose screen
        if(timerTime <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(6);
        }
    }
}
