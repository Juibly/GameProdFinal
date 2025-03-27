using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject player;
    public Vector3 checkpointCords;

    public void Hit()
    {
        Debug.Log("Target Hit " + name);
        player.transform.position = checkpointCords;
    }
}
