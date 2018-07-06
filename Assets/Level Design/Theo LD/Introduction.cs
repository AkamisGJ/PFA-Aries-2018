using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Introduction : MonoBehaviour {

	public float TimeBeforeEscape = 10;
	public Animator PorteOrsha;
	public Eboulement_alex Plaques;
	void Start () {
		StartCoroutine(StartIntroduction());
	}

	IEnumerator StartIntroduction(){
		yield return new WaitForSeconds(TimeBeforeEscape);
		GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().CameraShake();
		GetComponent<AudioSource>().Play();
		Plaques.active = true;

		yield return new WaitForSeconds(5f);
		GameObject[] allLight = GameObject.FindGameObjectsWithTag("AlarmeLight");
		foreach (GameObject light in allLight)
		{
			if(light.GetComponent<Animator>()){
				light.GetComponent<Animator>().SetBool("ActiveAlarme", true);
			}
		}

		
		yield return new WaitForSeconds(3f);
		PorteOrsha.SetBool("OpenDoor", true);
	}
}
