using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class otherMenuSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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


}

