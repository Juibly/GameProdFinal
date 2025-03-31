using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player in killzone");
        other.gameObject.GetComponent<Target>().Hit();
        
    }
}
