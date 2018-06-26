using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Mirror : MonoBehaviour {

	public MyMirrorReflection Mirror;

	void OnTriggerStay(Collider other)
	{
		Mirror.enabled = true;
	}


	void OnTriggerExit(Collider other)
	{
		Mirror.enabled = false;
	}

}
