using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Propulleurs : MonoBehaviour {

	public float GivePower = 2f;
	private FirstPersonController m_characterController;

	void OnTriggerEnter(Collider other)
	{
		m_characterController = other.transform.GetComponent<FirstPersonController>();
		m_characterController.FlyingPower = GivePower;
	}
}
