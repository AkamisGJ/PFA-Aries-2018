using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eboulement : MonoBehaviour
{
    public int m_mass;

    void OnTriggerEnter(Collider col)
    {
        int nb_child = transform.GetChildCount();

        for(int i = 0; i < nb_child; i++)
        {
            transform.GetChild(i).gameObject.AddComponent(typeof(Rigidbody));

            transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
            transform.GetChild(i).GetComponent<Rigidbody>().mass = m_mass;


        }
    }
}
