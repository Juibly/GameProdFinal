using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceText : MonoBehaviour
{
    [SerializeField] GameObject interactText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.SetActive(true);
        }
    }
}
