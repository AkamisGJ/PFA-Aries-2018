using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArrow : MonoBehaviour
{ 
    
    public GameObject ArrowToRemove;
    public GameObject ArrowToRemove2;
    public GameObject ArrowToRemove3;

    private void OnTriggerStay (Collider other)
    {
        if (other.tag=="Player")
        {
            ArrowToRemove.SetActive(true);
            print("test");

            if(ArrowToRemove2 != null){
                ArrowToRemove2.SetActive(true);
                ArrowToRemove3.SetActive(true);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ArrowToRemove.SetActive(false);

            if(ArrowToRemove2 != null){
                ArrowToRemove2.SetActive(false);
                ArrowToRemove3.SetActive(false);
            }

        }
    }
}
