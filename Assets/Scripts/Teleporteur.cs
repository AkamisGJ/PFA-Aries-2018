﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporteur : MonoBehaviour {

	public bool Alimenter = true;
	public Material surface_vert;
	public Material surface_bleu;

	private MeshRenderer m_mesh;
	private Animator m_animator;
	public GameObject m_VitreHolographic;

	void Start()
	{
		m_mesh = GetComponent<MeshRenderer>();
		m_mesh.material = surface_vert;
		m_animator = GetComponent<Animator>();
		if(m_animator)
		m_animator.SetBool("Alimenter", Alimenter);
	}

	public void ChangeMaterial()
	{
		m_mesh.material = surface_bleu;
<<<<<<< HEAD
		//print("Change Material");
=======
>>>>>>> 5ba77217119c19e80d6e63011008b1c5e55d8abf
	}

	void Update()
	{
		LayerMask m_teleport = LayerMask.NameToLayer("CanTeleport");
		if(Alimenter == true){
			m_VitreHolographic.layer = m_teleport.value;
		}else{
			m_VitreHolographic.layer = 0;
		}
	}

}
