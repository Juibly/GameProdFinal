using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hubManager : MonoBehaviour
{
        public GameObject hubMenu;
        public bool isSelect;


    // Start is called before the first frame update
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
