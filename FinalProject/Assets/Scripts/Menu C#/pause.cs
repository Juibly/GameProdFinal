using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    //holds ingame pause canvas
    public GameObject pMenu;
    public bool isPause;
    public GameObject howImage;
    public GameObject opImage;



    // pause menu is off when game starts
    void Start()
    {
        pMenu.SetActive(false);
        howImage.SetActive(false);
        opImage.SetActive(false);
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        //PRESS TO PAUSE
        if (Input.GetButtonDown("Cancel")){
            pauseGame();
        }

    //checks bool, allowing menu access if pause
         if (isPause == true) {
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void pauseGame()
    {
        //pauses game activity and turns menu on
        Time.timeScale = 0f;
        pMenu.SetActive(true);
        isPause = true;

    }

    public void resumeGame()
    {
//resumes game activity, turning menu off
        pMenu.SetActive(false);
        isPause = false;
        Time.timeScale = 1f;
        Cursor.visible = false;

    }
    public void goMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void howToPlay(){
        howImage.SetActive(true);
    }

    public void settings(){
        opImage.SetActive(true);
    }
    public void quit(){
        Application.Quit();
    }


}


