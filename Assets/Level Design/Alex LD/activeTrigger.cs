using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeTrigger : MonoBehaviour {

	public Eboulement_alex[] ScriptArray;

	public Animator m_Anim;

	private void Start() 
	{
		m_Anim.GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other) {
	if (other.tag == "Player") 
		{

			foreach (var eboul in ScriptArray)
			{
				eboul.active = true;		
			}

			m_Anim.enabled = false;
		}
	}
}