using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMirror : MonoBehaviour
{ 
    
    public GameObject MirrorToRemove;
    public GameObject MirrorToRemove2;
    public GameObject MirrorToRemove3;

    private void OnTriggerEnter (Collider other)
    {
        if (other.tag=="Player")
        {
            MirrorToRemove.SetActive(false);
            MirrorToRemove2.SetActive(false);
            MirrorToRemove3.SetActive(false);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Babidpzkdlazk");
            MirrorToRemove.SetActive(true);
            MirrorToRemove2.SetActive(true);
            MirrorToRemove3.SetActive(true);

        }
    }
}
