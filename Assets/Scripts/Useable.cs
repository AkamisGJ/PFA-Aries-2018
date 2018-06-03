using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Useable : MonoBehaviour {

	public bool activate = false;
	public bool PlaqueDePression = false;
	public Animator[] connections;
	public enum fonctionnement{
		ON_OFF,
		Cooldown,
		AlwaysTrue,
	}
	public fonctionnement Dropdown;
	public float delay_cooldown = 5f;
	private float cooldown = 0f;
	private MeshRenderer m_mesh;
	private Color m_color = Color.red;


	void Start()
	{
		if(GetComponent<MeshRenderer>()){
			m_mesh = GetComponent<MeshRenderer>();
		}
		activate = false;
	}


	public void Toogle(){

		if(Dropdown.ToString() == "ON_OFF"){
			activate = !activate;
			foreach (var connection in connections)
			{
				connection.GetComponent<Useable_output>().Activate();
				if(connection.GetComponent<Teleporteur>()){
					connection.GetComponent<Teleporteur>().Alimenter = !connection.GetComponent<Teleporteur>().Alimenter;
				}
			}
		}

		if(Dropdown.ToString() == "Cooldown"){
			cooldown = 0f;
			activate = !activate;
			foreach (var connection in connections)
			{
				connection.GetComponent<Useable_output>().Activate();
				if(connection.GetComponent<Teleporteur>()){
					connection.GetComponent<Teleporteur>().Alimenter = !connection.GetComponent<Teleporteur>().Alimenter;
				}
			}
		}

		if(Dropdown.ToString() == "AlwaysTrue"){
			if(activate == false){
				foreach (var connection in connections)
				{
					connection.GetComponent<Useable_output>().Activate();
					if(connection.GetComponent<Teleporteur>()){
						connection.GetComponent<Teleporteur>().Alimenter = true;
					}
				}
			}
			activate = true;
		}
	}


	void Update()
	{
		if(activate == true)
		m_color = Color.green;
		else
		m_color = Color.red;

		if(m_mesh){
			m_mesh.material.color = m_color;
		}


		if(Dropdown.ToString() == "Cooldown" && activate == true){
			cooldown += Time.deltaTime;
		}

		if(cooldown > delay_cooldown){
			activate = false;
			cooldown = 0f;
			foreach (var connection in connections)
			{
				connection.GetComponent<Useable_output>().Activate();
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(PlaqueDePression == true){
			Toogle();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(PlaqueDePression == true){
			Toogle();
		}
	}
}
