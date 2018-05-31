using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GameControlleur : MonoBehaviour {

	public Canvas PauseMenu;
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
	}

	public void Pause(){	

		PauseMenuState = !PauseMenuState;
		PauseMenu.enabled = PauseMenuState;

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
}
