using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGun : MonoBehaviour {

	public Animator m_animator;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") || (bool)(Input.GetAxis("Fire1Joy") > 0.3f))
            m_animator.SetTrigger("PrimaryShoot");

        if (Input.GetButtonDown("Fire2") || (bool)(Input.GetAxis("Fire2Joy") > 0.3f))
            m_animator.SetTrigger("SecondaryShoot");
    }
}
