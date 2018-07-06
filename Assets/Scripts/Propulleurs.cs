using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Propulleurs : MonoBehaviour {

	public float JumpPower = 2f;
	private FirstPersonController FPSControlleur;
	private bool Propullseur = false;

	

	void Start()
	{
		FPSControlleur =  GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player"){
			Propullseur = true;	
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player"){
			GetComponent<AudioSource>().Play();
		}
	}

	void FixedUpdate()
	{
		if(Propullseur){
			FPSControlleur.JumpWithPropullseur(JumpPower);
			Propullseur = false;
		}
	}
}
