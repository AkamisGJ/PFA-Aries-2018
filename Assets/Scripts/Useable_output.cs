using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Useable_output: MonoBehaviour {

	public bool activate = false;
	private Animator m_animator;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		m_animator = GetComponent<Animator>();
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
}
