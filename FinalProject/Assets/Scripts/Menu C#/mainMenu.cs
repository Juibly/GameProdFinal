using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    //creates don't destroy on load
    [SerializeField] private GameObject gameManagerPrefab;
    private GameManager gameManager;


    public GameObject menuScreen;
    public GameObject optionScreen;
    void Start()
    {
        optionScreen.SetActive(false);
        menuScreen.SetActive(true);
    }


//OPTION SELECTION
public void back(){ //if say no to freeplay options screen goes away, and menu is back to normal
        optionScreen.SetActive(false);
        menuScreen.SetActive(true);

}

//MENU SELECTIONS
    public void loadMainMenu(){
        SceneManager.LoadScene(1);
    }

    //OPENS HUB
    public void playGame(){
        SceneManager.LoadScene(0);
    }

    public void howToPlay(){
        SceneManager.LoadScene(2);
    }

    public void settings(){ //turns on options pop up selection and turns off main menu screen
        optionScreen.SetActive(true);
        menuScreen.SetActive(false);
    }
    public void quit(){
        Application.Quit();
    }

 public void playRooftop(){
        SceneManager.LoadScene(4);
    }

}

