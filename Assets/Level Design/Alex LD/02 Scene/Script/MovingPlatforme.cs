using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforme : MonoBehaviour {

	public Animator anim;

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "Player"){
			anim.SetTrigger("Up");
		}
	}

	
	void OnTriggerExit(Collider other)
	{
		if(other.transform.tag == "Player"){
			anim.SetTrigger("Down");
		}
	}
	

}
