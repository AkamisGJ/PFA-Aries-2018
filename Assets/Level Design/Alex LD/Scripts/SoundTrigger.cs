using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTrigger : MonoBehaviour 
{

	private BoxCollider TriggerZone;
	private int OnceOnly =0;
	public AudioSource m_audiosource;
	public AudioClip explosion;
	public bool Looping;

	private void Start() 
	{
		TriggerZone = gameObject.GetComponent<BoxCollider>();
	
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Player") 
		{
			if(Looping == true){
				m_audiosource.loop = true;
				m_audiosource.clip = explosion;
				m_audiosource.Play();
			}
			else{
				m_audiosource.PlayOneShot(explosion);
			}
			OnceOnly += 1;
			print (OnceOnly);
			TriggerZone.enabled = false;
		}
	}

}
