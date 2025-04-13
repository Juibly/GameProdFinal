using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hubManager : MonoBehaviour
{
        public GameObject hubMenu;

// when kevin steps on purple button , opens menu with the corressponding button active
//think we need gameobject rooftop button, sewer button, apartment button defining which is which to their collidier ect 


    // hub menu is off by default
    void Start()
    {
        hubMenu.SetActive(false);
    }

    // button selection code
   public void playRooftop(){
SceneManager.LoadScene(4);
   }

   public void playSewer(){
SceneManager.LoadScene(7);
   }

   public void playApartment(){
SceneManager.LoadScene(8);
   }

   public void resumeHub(){


   }
}
