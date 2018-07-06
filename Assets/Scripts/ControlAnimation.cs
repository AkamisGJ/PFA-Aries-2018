using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Rewired;

public class ControlAnimation : MonoBehaviour {

	public Animator m_Animator;
	public FirstPersonController FPS;
	private Player player;

	void Start()
	{
		player = ReInput.players.GetPlayer(0);
	}

	void Update () {
		//Movement
		float inputX = player.GetAxis("Horizontal");
		float inputy = player.GetAxis("Vertical");

		m_Animator.SetFloat("BlendX", inputX);
		m_Animator.SetFloat("BlendY", inputy);

		//Jump
		if(player.GetButtonDown("Jump") && FPS.m_Jumping == false){
			m_Animator.SetTrigger("Jumping 0");
		}

		//Running
		if(player.GetButton("Running") && ( (bool) (inputX != 0f) || (bool)(inputy != 0f)) ) {
			m_Animator.SetBool("Running", true);
		}else{
			m_Animator.SetBool("Running", false);
		}
	}
}
