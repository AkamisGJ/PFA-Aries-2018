using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour {

	public AudioClip timerSound;
	public AudioSource audiosource;
	private Useable useable;
	private bool active;

	void Start()
	{
		useable = GetComponent<Useable>();
	}
	void Update () {
		active = useable.activate;
		
		if(active == true && useable.cooldown == 0f){
			audiosource.clip = timerSound;
			audiosource.Play();
		}
	}
	

	void OnTriggerEnter(Collider other)
	{
		
	}
}
