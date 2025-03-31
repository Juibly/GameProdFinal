using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject player;
    public Vector3 checkpointCords;

    public void Hit()
    {
        player.transform.position = checkpointCords;
    }
}
