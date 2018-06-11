using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{ 
    private int Index;
    public Animator animator;

    [TextArea(5, 20)]
    public string Information;

    [Header("Gizmos")]
    public Color color_End;
    private BoxCollider cubePosition;



    void Start()
    {
        cubePosition = GetComponent<BoxCollider>();
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
        {
            //Save the Time of the run
            string indexSaveTime = "BestTime_" + SceneManager.GetActiveScene().buildIndex;
            float finishTime = other.GetComponentInChildren<SpeedRun>().timeLevel;
            if(PlayerPrefs.GetFloat(indexSaveTime, float.MaxValue) > finishTime){
                PlayerPrefs.SetFloat(indexSaveTime, finishTime);
            }

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
        print(Index);
        SceneManager.LoadSceneAsync(Index, LoadSceneMode.Single);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color_End;
        cubePosition = GetComponent<BoxCollider>();
        Gizmos.DrawCube(cubePosition.center + transform.position, transform.localScale);
    }

}
