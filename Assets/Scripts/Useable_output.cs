using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Useable_output: MonoBehaviour {

	public bool activate = false;
	private Animator m_animator;
	private AudioSource audioSource;


	void Start()
	{
		activate = false;
		m_animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	public void Activate(){
		activate = !activate;

		if(activate == true){
			m_animator.SetBool("Active", true);	
		}
		else{
			m_animator.SetBool("Active", false);
		}

	}

	public void PlaySound(){
		audioSource.Play();
	}
}
