using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class AnalyticStandartEventSender : MonoBehaviour {


	[Header("Send Event when the starting")]
	public bool gameover = false;
	public bool levelstart = false;

	void Start()
	{
		if(gameover){
			GameOver();
		}

		if(levelstart){
			LevelStart();
		}
		
	}

	public void GameStart(){
		AnalyticsEvent.GameStart();
	}

	public void GameOver(){
		AnalyticsEvent.GameOver();
	}

	public void LevelQuit(){
		int index = GetSceneIndex();
		AnalyticsEvent.LevelQuit(index);
	}

	public void LevelFail(){
		int index = GetSceneIndex();
		AnalyticsEvent.LevelFail(index);
	}

	public void LevelComplete(){
		int index = GetSceneIndex();
		AnalyticsEvent.LevelComplete(index);
	}

	public void LevelStart(){
		int index = GetSceneIndex();
		AnalyticsEvent.LevelStart(index);
	}

	public void ScreenVisit(string screenName){
		AnalyticsEvent.ScreenVisit(screenName);
	}



	int GetSceneIndex(){
		Scene current = SceneManager.GetActiveScene();
		int index = current.buildIndex;
		return index;
	}
	
}
