using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControlleur : MonoBehaviour {

	public Canvas PauseMenu;
	public GameObject Speedrun;

	public TextMeshProUGUI BestTime;
	public TextMeshProUGUI ActualTime;
	private bool PauseMenuState = false;
	

	void Start()
	{
		if(GameObject.FindGameObjectWithTag("PauseMenu") == null){
			Debug.LogError("PauseMenu is not in the scene !!");
			//Debug.Break();	
		}
		PauseMenu.enabled = PauseMenuState;
	}
	void Update () {

		if(Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.Escape)){
			Pause();
		}

		if(PauseMenuState == true){
			Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
		}
		else{
			Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
		}

		BestTime.text = "Best Time = " + BestScoreOnThisLevel();
		ActualTime.text = "Actual Time = " + ActualTimeOnThisLevel();	
	}

	public void Pause(){	

		PauseMenuState = !PauseMenuState;
		PauseMenu.enabled = PauseMenuState;

		//Desactive le compteur de speedrun dans le menu
		Speedrun.GetComponent<TextMeshProUGUI>().enabled = !PauseMenuState;

		if(PauseMenuState == true){
			Time.timeScale = 0f;
			GetComponent<FirstPersonController>().enabled = false;
			if(GetComponentInChildren<Raygun>()){
				GetComponentInChildren<Raygun>().enabled = false;
			}
		}
		else{
			Time.timeScale = 1f;
			GetComponent<FirstPersonController>().enabled = true;
			if(GetComponentInChildren<Raygun>()){
				GetComponentInChildren<Raygun>().enabled = true;
			}
		}
	}

	public void Restart(){
		Pause();
		int indexCurrentScene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(indexCurrentScene);
	}

	public void Quit(){
		Application.Quit();
	}

	public string BestScoreOnThisLevel(){
		string indexSaveTime = "BestTime_" + SceneManager.GetActiveScene().name;
		float BestTimeFloat = PlayerPrefs.GetFloat(indexSaveTime, 0f);
		return FormatTime(BestTimeFloat);
	}

	public string ActualTimeOnThisLevel(){
		float actualtime = Speedrun.GetComponent<SpeedRun>().timeLevel;
		return FormatTime(actualtime);
	}

	public string FormatTime (float Time){
		string FormatTime = string.Format("{0:0}:{1:00}.{2:000}",
		Mathf.Floor(Time / 60),//minutes
		Mathf.Floor(Time) % 60,//seconds
		Mathf.Floor((Time*1000) % 1000));//miliseconds

		return FormatTime;
	}
}
