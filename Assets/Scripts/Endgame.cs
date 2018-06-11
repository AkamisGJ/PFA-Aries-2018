using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour {

public GameObject GOToDeactivate;
public bool Activate;
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player"){
			GOToDeactivate.SetActive(Activate);
		}
		//Destroy(this); //Delette this Script
	}
}
