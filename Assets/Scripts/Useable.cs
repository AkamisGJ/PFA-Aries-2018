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
	private Animator BoutonAnimator;

	[Header("Audio")]

	[Range(0f, 1f)] public float volume_buttonActivate = 1f;
	public AudioClip BoutonActivate;
	[Range(0f, 1f)] public float volume_buttonDeactivate = 1f;
	public AudioClip BoutonDesactivate;

	private AudioSource m_AudioSource;

	void Start()
	{
		if(GetComponentInParent<Animator>()){
			BoutonAnimator = GetComponentInParent<Animator>();
		}

		if(GetComponent<Animator>()){
			BoutonAnimator = GetComponent<Animator>();
		}

		if(GetComponent<MeshRenderer>()){
			m_mesh = GetComponent<MeshRenderer>();
		}

		if(GetComponentInParent<AudioSource>()){
			m_AudioSource = GetComponentInParent<AudioSource>();
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
			if(BoutonAnimator){
				BoutonAnimator.SetBool("Active", activate);
			}

			//Sound Design
			if(m_AudioSource == true && activate == true){
				m_AudioSource.Stop();
				m_AudioSource.volume = volume_buttonActivate;
				m_AudioSource.PlayOneShot(BoutonActivate);
			}
			if(m_AudioSource == true && activate == false){
				m_AudioSource.Stop();
				m_AudioSource.volume = volume_buttonDeactivate;
				m_AudioSource.PlayOneShot(BoutonDesactivate);
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
			if(BoutonAnimator){
				BoutonAnimator.SetBool("Active", activate);
			}

			//Sound Design
			if(m_AudioSource == true && activate == true){
				m_AudioSource.Stop();
				m_AudioSource.volume = volume_buttonActivate;
				m_AudioSource.PlayOneShot(BoutonActivate);
			}
			if(m_AudioSource == true && activate == false){
				m_AudioSource.Stop();
				m_AudioSource.volume = volume_buttonDeactivate;
				m_AudioSource.PlayOneShot(BoutonDesactivate);
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

			if(BoutonAnimator){
				BoutonAnimator.SetBool("Active", activate);
			}

			//Sound Design
			if(m_AudioSource == true && activate == true){
				m_AudioSource.Stop();
				m_AudioSource.volume = volume_buttonActivate;
				m_AudioSource.PlayOneShot(BoutonActivate);
			}
			if(m_AudioSource == true && activate == false){
				m_AudioSource.Stop();
				m_AudioSource.volume = volume_buttonDeactivate;
				m_AudioSource.PlayOneShot(BoutonDesactivate);
			}
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
			m_mesh.material.SetColor("_EmissionColor", (m_color * Mathf.LinearToGammaSpace(1f)));
		}


		if(Dropdown.ToString() == "Cooldown" && activate == true){
			cooldown += Time.deltaTime;
		}

		if(cooldown > delay_cooldown){
			Toogle();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(PlaqueDePression == true && other.tag == "Player"){
			Toogle();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(PlaqueDePression == true && other.tag == "Player" && Dropdown.ToString() != "Cooldown"){
			Toogle();
		}
	}
}
