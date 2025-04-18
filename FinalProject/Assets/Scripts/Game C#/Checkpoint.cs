using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
 public AudioSource CheckpointSource;
public GameObject checkpointText;

public void Start(){
 checkpointText.SetActive(false);
}

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            other.GetComponent<Target>().checkpointCords = other.transform.position;
            CheckpointSource.Play();
            checkpointText.SetActive(true);
        }
    }
}
