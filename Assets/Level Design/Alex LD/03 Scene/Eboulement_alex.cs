using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eboulement_alex : MonoBehaviour
{
    public int m_mass = 1000;
    public float m_drag = 0.001f;
     public float m_angulardrag = 0.001f;
    public bool active = false;

    public bool Addforce = false;

    void Update()
    {
        if(active == true){
        int nb_child = transform.GetChildCount();
        print("Nombre object : " + nb_child);

        for(int i = 0; i < nb_child; i++)
        {
            transform.GetChild(i).gameObject.AddComponent(typeof(Rigidbody));
            float random_angular = Random.Range(0.001f, 0.2f);

            transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
            transform.GetChild(i).GetComponent<Rigidbody>().mass = m_mass;
            transform.GetChild(i).GetComponent<Rigidbody>().drag = random_angular;
            transform.GetChild(i).GetComponent<Rigidbody>().angularDrag = m_angulardrag;

            print(transform.GetChild(i).name);
            if(Addforce){
                Vector3 direction = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
                transform.GetChild(i).GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
            }
        }
            Destroy(this);
        }
    }
}
