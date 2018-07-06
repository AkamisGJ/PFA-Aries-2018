using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : GameControlleur {

	// Use this for initialization
	void Start () {
		//Setup Options Menu
		sensibility.value = PlayerPrefs.GetFloat("Sensibility");
		SetSensibility();
		
		TrailToogle.isOn = PlayerPrefs2.GetBool("Trail");
		SetTrail();
		
		TimerToogle.isOn = PlayerPrefs2.GetBool("Timer");
		SetTimer();
	}
	

	public void SetTimer(){
	PlayerPrefs2.SetBool("Timer", TimerToogle.isOn);
	}

	public void SetTrail(){
		PlayerPrefs2.SetBool("Trail", TrailToogle.isOn);
	}

	public void SetSensibility(){
		PlayerPrefs.SetFloat("Sensibility", sensibility.value);
	}
}
