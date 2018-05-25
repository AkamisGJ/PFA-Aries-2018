using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeTrigger : MonoBehaviour {

	public Eboulement_alex script;
	void OnTriggerEnter(Collider other) {
		script.active = true;
	}
}