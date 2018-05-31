using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pente : MonoBehaviour {

	public float penteValue = 0.15f;
	// Use this for initialization
	void Start () {
		if(gameObject.tag != "Rampe"){
			gameObject.tag = "Rampe";
		}
	}
	
	
}
