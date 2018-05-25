using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeTrigger : MonoBehaviour {

	public Eboulement script;
	void OnTriggerEnter(Collider other) {
		script.active = true;
	}
}
