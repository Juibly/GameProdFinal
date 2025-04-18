using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZone : MonoBehaviour
{
public AudioSource killSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player in killzone");
            killSound.Play();
            other.gameObject.GetComponent<Target>().Hit();

        }
    }
}
