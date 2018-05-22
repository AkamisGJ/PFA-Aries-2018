using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointZone : MonoBehaviour {

    public Transform pointToTP;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = pointToTP.position;
            other.transform.rotation = pointToTP.rotation;
        }
    }
}