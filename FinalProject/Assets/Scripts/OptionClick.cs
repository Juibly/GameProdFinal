using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionClick : MonoBehaviour
{

    public GameObject ClickOp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpClick()
    {
        playerMovement.cheatOn = true;
        Debug.Log("cheatOn");
    }
}
