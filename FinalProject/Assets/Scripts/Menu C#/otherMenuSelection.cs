using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class otherMenuSelection : MonoBehaviour
{

//objects for turning on and off showing keyboard/controller controls
    public Toggle checkController; //checklist-truefalse
    public GameObject controlParent; // what shows controller controls
    public GameObject keyboardParent; // what shows keyboard controls

    // Start is called before the first frame update
    void Start()
    {
        checkController.isOn = false; //toggle is off from start
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

//on howToPlay Scene
public void showController(){
    
    if(checkController.isOn == false){
        keyboardParent.SetActive(true);//on start is false, keyboard true
        controlParent.SetActive(false);
    } 
    else{
        keyboardParent.SetActive(false);//when click toggle keyboard false
        controlParent.SetActive(true);
    }

}

    //MENU SELECTIONS
    public void loadMainMenu(){
        SceneManager.LoadScene(0);
    }

    //OPENS HUB
    public void playGame(){
        SceneManager.LoadScene(1);
    }

    public void howToPlay(){
        SceneManager.LoadScene(2);
    }

    public void settings(){
        SceneManager.LoadScene(3);
    }
    public void quit(){
        Application.Quit();
    }


}

