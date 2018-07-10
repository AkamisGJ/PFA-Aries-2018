using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour {

	public TextMeshProUGUI intro;
	public TextMeshProUGUI Level_1;
	public TextMeshProUGUI Level_2;
	public TextMeshProUGUI Level_3;
	public TextMeshProUGUI Level_4;
	public TextMeshProUGUI Level_5;
	void Start () {
		intro.text = BestScoreOnThisLevel(1);
		Level_1.text = BestScoreOnThisLevel(2);
		Level_2.text = BestScoreOnThisLevel(3);
		Level_3.text = BestScoreOnThisLevel(4);
		Level_4.text = BestScoreOnThisLevel(5);
		Level_5.text = BestScoreOnThisLevel(6);
	}

	public string BestScoreOnThisLevel(int scene){
		string indexSaveTime = "BestTime_" + scene;
		float BestTimeFloat = PlayerPrefs.GetFloat(indexSaveTime, 0f);
		return FormatTime(BestTimeFloat);
	}

	public string FormatTime (float Time){
		string FormatTime = string.Format("{0:0}:{1:00}.{2:000}",
		Mathf.Floor(Time / 60),//minutes
		Mathf.Floor(Time) % 60,//seconds
		Mathf.Floor((Time*1000) % 1000));//miliseconds

		return FormatTime;
	}
	
	
}
