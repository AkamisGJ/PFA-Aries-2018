using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide2 : MonoBehaviour
{
    private Animator animator;
    public GameObject Target;

    private void Awake()
    {
        animator = Target.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            animator.SetBool("Turn", true);
            print("Lol");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("Turn", true);
            
        }
    }
}
