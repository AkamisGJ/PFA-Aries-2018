using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ControlAnimation : MonoBehaviour {

	public Animator m_Animator;
	public FirstPersonController FPS;
	void Update () {
		//Movement
		float inputX = Input.GetAxis("Horizontal");
		float inputy = Input.GetAxis("Vertical");

		m_Animator.SetFloat("BlendX", inputX);
		m_Animator.SetFloat("BlendY", inputy);

		//Jump
		if(Input.GetButtonDown("Jump") && FPS.m_Jumping == false){
			m_Animator.SetTrigger("Jumping 0");
		}

		//Running
		if(Input.GetButton("Running") && ( (bool) (inputX != 0f) || (bool)(inputy != 0f)) ) {
			m_Animator.SetBool("Running", true);
		}else{
			m_Animator.SetBool("Running", false);
		}
	}
}
