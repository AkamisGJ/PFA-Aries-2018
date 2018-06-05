using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Endgame : MonoBehaviour {

public GameObject GOToDeactivate;
public bool Activate;
public bool ResetJumpPower = false;

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<FirstPersonController>()){
			if(ResetJumpPower == true){
				other.GetComponent<FirstPersonController>().FlyingPower = 0;
			}
			GOToDeactivate.SetActive(Activate);
		}
		Destroy(this); //Delette this Script
	}
}
