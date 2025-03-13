using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockFist : MonoBehaviour
{
    //Variables
    public KeyCode shockKey = KeyCode.Mouse0; //left click punch

    //Variables for cooldown/timer on using the shockfist
    public bool shockFistCooldown = false;
    public float cooldownRemaining = 10f;
    public bool cheatOn; //if there is cheat that turns off cooldown

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(shockKey))
        {
            //play vfx like animation state and audio source

        }
        //timer for cooldown
        if ((shockFistCooldown) && (cooldownRemaining > 0)) { cooldownRemaining -= Time.deltaTime; } //counts down
        if ((shockFistCooldown) && (cooldownRemaining <= 0)) { shockFistCooldown = false; } //ends at 0
        if (!shockFistCooldown) { cooldownRemaining = 10f; } //reset timer
        if (cheatOn) { shockFistCooldown = false; }
    }
}
