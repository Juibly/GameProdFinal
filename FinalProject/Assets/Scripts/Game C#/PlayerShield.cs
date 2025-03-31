using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject Shield;

    [SerializeField] private GameObject player;

    [SerializeField] private KeyCode shieldKey = KeyCode.Mouse1;

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
        if (Input.GetKeyDown(shieldKey))
        {
            player.GetComponent<playerMovement>().shieldUp = true;
            Shield.SetActive(true);
            ShieldUpSource.Play();
        }
        else if(Input.GetKeyUp(shieldKey))
        {
            player.GetComponent<playerMovement>().shieldUp = false;
            Shield.SetActive(false);
            ShieldDownSource.Play();
        }
    }
}
