using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionClick : MonoBehaviour
{
    shockFist shockFist;

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
        shockFist.cheatOn = true;
        Debug.Log("cheatOn");
    }
}
