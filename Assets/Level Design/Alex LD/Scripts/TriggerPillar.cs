using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPillar : MonoBehaviour {

    private Animator animator;
    public GameObject Target;
    private int TimeBeforeFalse;
    

    private void Awake()
    {
        animator = Target.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("Up", true);
            
        }

    }


        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                animator.SetBool("Up", false);

            }

        }

}
