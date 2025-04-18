using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject Shield;

    [SerializeField] private GameObject player;

    //[SerializeField] private KeyCode shieldKey = KeyCode.Mouse1;

    public AudioSource ShieldUpSource;
    public AudioSource ShieldDownSource;

    // Start is called before the first frame update
    private void Start()
    {
        Shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Shield")) { ShieldUpSource.Play(); } //on frame you open shield play sound
        if (Input.GetButton("Shield")) //while holding button shield is up
        {
            player.GetComponent<playerMovement>().shieldUp = true;
            Shield.SetActive(true);
        }
        else if(Input.GetButtonUp("Shield")) //on frame you release shield button
        {
            player.GetComponent<playerMovement>().shieldUp = false;
            Shield.SetActive(false);
            ShieldUpSource.Stop(); // stop sound from putting shield up if the shield is being put down
            ShieldDownSource.Play();
        }
    }
}
