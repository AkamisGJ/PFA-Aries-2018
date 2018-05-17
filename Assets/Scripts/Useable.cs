using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Useable : MonoBehaviour {

	public bool activate = false;
	public Useable_output connection;
	public enum fonctionnement{
		ON_OFF,
		Cooldown
	}
	public fonctionnement Dropdown;
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
		activate = !activate;

		if(activate == true)
		m_color = Color.green;
		else
		m_color = Color.red;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		m_mesh.material.color = m_color;
	}
}
