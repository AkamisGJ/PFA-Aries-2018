using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporteur : MonoBehaviour {

	public Material surface_vert;
	public Material surface_bleu;

	private MeshRenderer m_mesh;

	void Start()
	{
		m_mesh = GetComponent<MeshRenderer>();
		m_mesh.material = surface_vert;
	}

	public void ChangeMaterial()
	{
		m_mesh.material = surface_bleu;
	}
}
