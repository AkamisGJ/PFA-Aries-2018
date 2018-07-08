using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class FinalExplosion : MonoBehaviour {

	public TextMeshProUGUI m_Timer;
	public float timeExplosion;
	
	public GameObject[] Explosion;

	public GameObject LastExplosion;

	private int nextExplosion;
	private int nombreExplosion = 0;
	private FirstPersonController FPS;
	void Start () {
		nextExplosion = Mathf.RoundToInt(timeExplosion) - 5;

		foreach (var explosion in Explosion)
		{
			explosion.SetActive(false);
		}
		LastExplosion.SetActive(false);

		FPS = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {

		if(m_Timer.gameObject.activeSelf == true){
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
					foreach (var explosion in Explosion)
				{
					explosion.SetActive(true);
				}
				m_Timer.enabled = false;
				LastExplosion.SetActive(true);
				FPS.CameraShake();
				StartCoroutine(ReloadScene());

				
			}
		}
	}

	public IEnumerator ExploseEverythinks(){
		foreach (var explosion in Explosion)
		{
			explosion.SetActive(true);
			yield return new WaitForSecondsRealtime(0.2f);
		}
	}

	IEnumerator ReloadScene(){
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
