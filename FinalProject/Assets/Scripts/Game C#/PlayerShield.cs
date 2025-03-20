using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject Shield;

    [SerializeField] private KeyCode shieldKey = KeyCode.Mouse1;

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
            Shield.SetActive(true);
        }
        else if(Input.GetKeyUp(shieldKey))
        {
            Shield.SetActive(false);
        }
    }
}
