using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimation : MonoBehaviour {

	public Animator m_Animator;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			m_Animator.SetTrigger("Jumping 0");
		}
	}
}
