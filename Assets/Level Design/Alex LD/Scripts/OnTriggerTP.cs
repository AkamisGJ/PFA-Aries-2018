using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerTP : MonoBehaviour
{
    private Animator animator;
    private MeshCollider NewCollider;
    public GameObject Target;

    private void Awake()
    {
        animator = Target.GetComponent<Animator>();
        NewCollider = Target.GetComponent<MeshCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            animator.SetBool("Turn", true);
            Target.layer = LayerMask.NameToLayer("CanTeleport");
            NewCollider.enabled = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("Turn", false);
            Target.layer = LayerMask.NameToLayer("Default");
            NewCollider.enabled = false;
        }
    }
}
