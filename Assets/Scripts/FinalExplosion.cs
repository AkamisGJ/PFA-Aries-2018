using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalExplosion : MonoBehaviour {

	public TextMeshProUGUI m_Timer;
	public float timeExplosion;
	
	public GameObject[] Explosion;

	public GameObject LastExplosion;

	private int nextExplosion;
	private int nombreExplosion = 0;
	void Start () {
		nextExplosion = Mathf.RoundToInt(timeExplosion) - 5;

		foreach (var explosion in Explosion)
		{
			explosion.SetActive(false);
		}
		LastExplosion.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		if(m_Timer.gameObject.active == true){
			timeExplosion -= Time.deltaTime;

			string niceTime = string.Format("{0:0}:{1:00}.{2:000}",
			Mathf.Floor(timeExplosion / 60),//minutes
			Mathf.Floor(timeExplosion) % 60,//seconds
			Mathf.Floor((timeExplosion*1000) % 1000));//miliseconds

			//Affiche le temps
			m_Timer.text = "Explosion in : " + niceTime;

			if(Mathf.RoundToInt(timeExplosion) < nextExplosion){
				Explosion[nombreExplosion].SetActive(true);
				nombreExplosion++;
				nextExplosion = Mathf.RoundToInt(timeExplosion) - 5;
			}

			if(timeExplosion <= 0f){
				gameObject.SetActive(false);
				LastExplosion.SetActive(true);
			}
		}
	}
}
