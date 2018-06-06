using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Endgame : MonoBehaviour {

public GameObject GOToDeactivate;
public bool Activate;
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<FirstPersonController>()){
			GOToDeactivate.SetActive(Activate);
		}
		Destroy(this); //Delette this Script
	}
}
