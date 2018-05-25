using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eboulement_alex : MonoBehaviour
{
    public int m_mass;
    public bool active = false;

    void Update()
    {
        if(active == true){
        int nb_child = transform.GetChildCount();

        for(int i = 0; i < nb_child; i++)
        {
            transform.GetChild(i).gameObject.AddComponent(typeof(Rigidbody));

            transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
            transform.GetChild(i).GetComponent<Rigidbody>().mass = m_mass;


        }
            active = false;
        }
    }
}
