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

	private void Start() 
	{
		TriggerZone = gameObject.GetComponent<BoxCollider>();
	
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Player") 
		{
			print("Sound Is Playing");
			m_audiosource.PlayOneShot(explosion);
			OnceOnly += 1;
			print (OnceOnly);
			TriggerZone.enabled = false;
		}
	}

}
