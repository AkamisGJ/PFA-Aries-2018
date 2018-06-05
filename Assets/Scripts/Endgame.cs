using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Endgame : MonoBehaviour {


	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<FirstPersonController>()){
			other.GetComponent<FirstPersonController>().FlyingPower = 0;
		}
	}
}
