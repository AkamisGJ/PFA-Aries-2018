﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTrigger : MonoBehaviour
{

    private Animator animator;
    public GameObject Target;
    public GameObject DeathZone;

    private void Awake()
    {
        animator = Target.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("Fall", true);
            DeathZone.SetActive(true);

        }

    }
}
    