using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedRun : MonoBehaviour {

	TextMeshProUGUI m_Timer;
	public float timeLevel;
	public float timeGame;
	void Start () {
		m_Timer = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		timeGame = Time.time;
		timeLevel = Time.timeSinceLevelLoad;

		string niceTime = string.Format("{0:0}:{1:00}.{2:000}",
		Mathf.Floor(timeLevel / 60),//minutes
		Mathf.Floor(timeLevel) % 60,//seconds
		Mathf.Floor((timeLevel*1000) % 1000));//miliseconds

		//Affiche le temps
		m_Timer.text = "Time : " + niceTime;
	}
}
