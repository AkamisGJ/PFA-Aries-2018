using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_door : MonoBehaviour {


	public Animator m_animationComponnent;
	[Range(0f, 1f)] public float volume_door;
	public AudioClip OpenDoor;
	public AudioClip CloseDoor;
	private AudioSource AudioSource;

	
	void Start()
	{
		AudioSource = GetComponent<AudioSource>();
	}

	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player"){
			m_animationComponnent.SetBool("InFrontDoor", true);
			AudioSource.Stop();
			AudioSource.volume = volume_door;
			AudioSource.PlayOneShot(OpenDoor);
			
		}
		
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player"){
			m_animationComponnent.SetBool("InFrontDoor", false);
			AudioSource.Stop();
			AudioSource.volume = volume_door;
			AudioSource.PlayOneShot(CloseDoor);
		}
	}
}
