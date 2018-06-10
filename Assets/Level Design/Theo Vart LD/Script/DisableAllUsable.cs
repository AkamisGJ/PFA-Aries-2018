using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAllUsable : MonoBehaviour {

	public Transform Parent;
	public GameObject BlueLight;
	public GameObject Bumper;

	private Useable[] m_Useable;
	private Useable_output[] m_UseableOutput;
	private Teleporteur[] m_teleporteur;
	private Light[] Lights;


	void Start()
	{
		Bumper.SetActive(false);
		BlueLight.SetActive(false);
	}

	
	void OnTriggerEnter(Collider other)
	{
		m_Useable = Parent.GetComponentsInChildren<Useable>();
		m_UseableOutput = Parent.GetComponentsInChildren<Useable_output>();
		m_teleporteur = Parent.GetComponentsInChildren<Teleporteur>();
		Lights = Parent.GetComponentsInChildren<Light>();


		foreach (Teleporteur teleporteur in m_teleporteur)
		{
			teleporteur.Alimenter = false;
		}

		foreach (AutoRotation item in Parent.GetComponentsInChildren<AutoRotation>())
		{
			item.enabled = false;
		}


		Bumper.SetActive(true);
		Parent.GetComponent<Animator>().SetTrigger("Active"); //Cache les Téléporteur
	}
}
