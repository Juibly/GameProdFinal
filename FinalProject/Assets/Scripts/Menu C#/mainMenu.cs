using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerPrefab;
    private GameManager gameManager;
    void Start()
    {
        
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

 public void playRooftop(){
        SceneManager.LoadScene(4);
    }

}

