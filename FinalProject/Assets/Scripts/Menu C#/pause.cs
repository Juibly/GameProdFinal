using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    //holds ingame pause canvas
    public GameObject pMenu;
    public bool isPause;

      private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        pMenu.SetActive(false);
        isPause = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        pMenu.SetActive(true);
        isPause = true;

    }

    public void resumeGame()
    {
        pMenu.SetActive(false);
        isPause = false;
    }
    public void goMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}


