using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeTrigger : MonoBehaviour {

	public Eboulement_alex script;
	public Eboulement_alex script2;

	public Eboulement_alex script3;
	void OnTriggerEnter(Collider other) {
		script.active = true;
		script2.active = true;
		script3.active = true;
	}
}