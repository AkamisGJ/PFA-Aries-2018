using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class SceneLoader : MonoBehaviour
{ 
    private int Index;
    public Animator animator;
    private AnalyticStandartEventSender analytics;

    [TextArea(5, 20)]
    public string Information;

    [Header("Gizmos")]
    public Color color_End;
    private BoxCollider cubePosition;



    void Start()
    {
        cubePosition = GetComponent<BoxCollider>();
        analytics = GameObject.FindGameObjectWithTag("Player").GetComponent<AnalyticStandartEventSender>();
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
        {
            //Save the Time of the run
            string indexSaveTime = "BestTime_" + SceneManager.GetActiveScene().buildIndex;
            float finishTime = Time.timeSinceLevelLoad;
            if(PlayerPrefs.GetFloat(indexSaveTime, float.MaxValue) > finishTime){
                PlayerPrefs.SetFloat(indexSaveTime, finishTime);
            }

            //Data Analyses
            analytics.LevelComplete();

            //Load Next Scene
            Scene current = SceneManager.GetActiveScene();
            Index = current.buildIndex;
            Index++;
            FadeToLevel(Index);
        }
    }

    public void FadeToLevel(int Index) 
    {
        animator.SetTrigger("FadeOut");
        SceneManager.LoadScene(Index);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color_End;
        cubePosition = GetComponent<BoxCollider>();
        Gizmos.DrawCube(cubePosition.center + transform.position, transform.localScale);
    }

}
