using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControlleur : MonoBehaviour {

	public Canvas PauseMenu;
	public GameObject MainPauseMenu; 
	public GameObject Speedrun;

	public TextMeshProUGUI BestTime;
	public TextMeshProUGUI ActualTime;
	private bool PauseMenuState = false;
	private GameObject Player;
	public FinalExplosion script;

	[Header("Options")]
	public GameObject OptionMenu;
	public Slider volume;
	public Toggle muteVolume;
	public Slider sensibility;
	public FirstPersonController FPSControlleur;
	public Toggle TrailToogle;
	public TrailRenderer Trail;
	public Toggle TimerToogle;
	public TextMeshProUGUI TimerText;
	

	void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");

		if(GameObject.FindGameObjectWithTag("PauseMenu") == null){
			Debug.LogError("PauseMenu is not in the scene !!");
			//Debug.Break();	
		}
		PauseMenu.enabled = PauseMenuState;


		//Setup Options Menu
		if(PlayerPrefs.GetFloat("Sensibility") == 0f){
			sensibility.value = 2f;
		}
		sensibility.value = PlayerPrefs.GetFloat("Sensibility", 2f);
		SetSensibility();

		volume.value = PlayerPrefs.GetFloat("Volume", 1);
		SetVolume();
		
		TrailToogle.isOn = PlayerPrefs2.GetBool("Trail");
		SetTrail();
		
		TimerToogle.isOn = PlayerPrefs2.GetBool("Timer");
		SetTimer();

		
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

	public void SetTimer(){
		TimerText.enabled = TimerToogle.isOn;
		PlayerPrefs2.SetBool("Timer", TimerToogle.isOn);
	}

	public void SetTrail(){
		Trail.enabled = TrailToogle.isOn;
		PlayerPrefs2.SetBool("Trail", TrailToogle.isOn);
	}

	public void SetSensibility(){
		FPSControlleur.m_MouseLook.XSensitivity = sensibility.value;
		FPSControlleur.m_MouseLook.YSensitivity = sensibility.value;
		PlayerPrefs.SetFloat("Sensibility", sensibility.value);
	}

	public void SetVolume(){
		AudioListener.volume = volume.value;
		PlayerPrefs.SetFloat("Volume", volume.value);
	}
	public void MuteVolume(){
		if(muteVolume.isOn){
			AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
		}else{
			AudioListener.volume = 0;
		}
	}

	public void Pause(){	

		PauseMenuState = !PauseMenuState;
		PauseMenu.enabled = PauseMenuState;

		//Desactive le compteur de speedrun dans le menu
		Speedrun.GetComponent<TextMeshProUGUI>().text = "";

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
	
	public void BackToMenu(){
		SceneManager.LoadScene(0);
	}

	public void Option(bool State){
		OptionMenu.SetActive(State);
		MainPauseMenu.SetActive(!State);
	}

	public string BestScoreOnThisLevel(){
		string indexSaveTime = "BestTime_" + SceneManager.GetActiveScene().buildIndex;
		//print("Game Controlleur = " + indexSaveTime);
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
