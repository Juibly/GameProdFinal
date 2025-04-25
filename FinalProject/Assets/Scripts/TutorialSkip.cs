using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkip : MonoBehaviour
{

    [SerializeField] GameObject player;


    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player in killzone");
            other.gameObject.transform.position = new Vector3(12.1999998f, 114.199997f, 827.099976f);

        }
    }
}
