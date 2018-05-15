using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerTP : MonoBehaviour
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
            Target.layer = LayerMask.NameToLayer("CanTeleport");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("Turn", false);
            Target.layer = LayerMask.NameToLayer("Default");
        }
    }
}
