using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploseEverthinks : MonoBehaviour {

	public FinalExplosion script;
	void Explosion(){
		script.StartCoroutine(script.ExploseEverythinks());
	}
}
