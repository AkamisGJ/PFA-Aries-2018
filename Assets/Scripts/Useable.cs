using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Useable : MonoBehaviour {

	public bool activate = false;
	public GameObject[] connections;
	public enum fonctionnement{
		ON_OFF,
		Cooldown
	}
	public fonctionnement Dropdown;
	public float delay_cooldown = 5f;
	private float cooldown = 0f;
	private MeshRenderer m_mesh;
	private Color m_color = Color.red;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		m_mesh = GetComponent<MeshRenderer>();
	}


	public void Toogle(){

		if(Dropdown.ToString() == "ON_OFF"){
			activate = !activate;
			foreach (var connection in connections)
			{
				connection.GetComponent<Useable_output>().Activate();
			}
		}

		if(Dropdown.ToString() == "Cooldown"){
			cooldown = 0f;
			activate = !activate;
			foreach (var connection in connections)
			{
				connection.GetComponent<Useable_output>().Activate();
			}
		}
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(activate == true)
		m_color = Color.green;
		else
		m_color = Color.red;

		m_mesh.material.color = m_color;

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
}
