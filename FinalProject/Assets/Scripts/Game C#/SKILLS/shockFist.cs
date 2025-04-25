using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockFist : MonoBehaviour
{

    [Header("Shockfist")] //Variables
    public bool shockfistActive = false;
    [SerializeField] GameObject shockfistCollider;
    public AudioSource ShockFistSource;


    [Header("Keybinds")] //Variables for Keybinds
    public KeyCode shockKey = KeyCode.Mouse0; //left click punch


    [Header("Cooldown Timer")] //Variables for cooldown/timer on using the shockfist
    public bool shockFistCooldown = true;
    public float timerTime;
    public float cooldownRemaining;


    [Header("Cheats")] //Variables for Cheats
    public static bool cheatOn; //if there is cheat that turns off cooldown
    public bool cheatOn1;


    //[Header("VFX")] //Variables for VFX
    //public AudioSource shockfistSource;
    //Animator playerAnimator;


    // Start is called before the first frame update
    void Start()
    {
        shockfistCollider.SetActive(false); // this turns off the shock fist collider on start
        cheatOn1 = false;
        cooldownRemaining = 8;

        //playerAnimator = gameObject.GetComponent<Animator>(); //get animator for player
    }

    // Update is called once per frame
    void Update()
    {
        shockfistCollider.SetActive(shockfistActive); // this changes if the collision for shockfist is active or not based on bool

        if (cheatOn == true)
        {
            cheatOn1 = true;
        }

        if (Input.GetButtonDown("Shockfist"))
        {
            if (shockFistCooldown) //if cooldown ongoing
            {
                //sound or other cue to show fist didnt happen
            }
            if (!shockFistCooldown)
            {
                shockfistActive = true; //activates collision for shock fist
                shockFistCooldown = true; // starts cooldown before you can press again
                ShockFistSource.Play();
                if (cheatOn1 == false)
                {
                    cooldownRemaining = 8;
                }
               else
                {
                    cooldownRemaining = 0;
                }
                //play vfx like animation state and audio source
                //shockfistSource.Play(); //sound when hitting
                //playerAnimator.Play(shockfist); //animation when hitting
            }

        }

        //timer for cooldown
        if ((shockFistCooldown) && (cooldownRemaining > 0)) //counts down
        {
            cooldownRemaining -= Time.deltaTime;
        }
        if ((shockFistCooldown) && (cooldownRemaining <= 0)) //ends at 0
        {
            shockFistCooldown = false; //cooldown ends
            shockfistActive = false;
        }
        if (!shockFistCooldown) { cooldownRemaining = timerTime; } //reset timer
        if (cheatOn) { shockFistCooldown = false; } //if cheat is on - cooldown is always off
    }
}
