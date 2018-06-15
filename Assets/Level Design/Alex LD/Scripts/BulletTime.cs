using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

public class BulletTime : MonoBehaviour
{

    public float SlowMoFactor = 0.05f;
    public float SlowMoLenght = 2f;


    public Color color_Time;
    private BoxCollider cubePosition;

    private Camera MainCamera;
    PostProcessingBehaviour PostProd;

    void Start()
    {
        cubePosition = GetComponent<BoxCollider>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        PostProd = MainCamera.GetComponent<PostProcessingBehaviour>();

        //Reset TimeScale
        Time.timeScale = 1;
        //Aberation Chromatique
        ChromaticAberrationModel.Settings setting = PostProd.profile.chromaticAberration.settings;
        setting.intensity = 0f;
        PostProd.profile.chromaticAberration.settings = setting;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            //Aberation Chromatique
            ChromaticAberrationModel.Settings setting = PostProd.profile.chromaticAberration.settings;
            setting.intensity = 1f;
            PostProd.profile.chromaticAberration.settings = setting;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 1;
            //Aberation Chromatique
            ChromaticAberrationModel.Settings setting = PostProd.profile.chromaticAberration.settings;
            setting.intensity = 0f;
            PostProd.profile.chromaticAberration.settings = setting;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color_Time;
        cubePosition = GetComponent<BoxCollider>();
        Gizmos.DrawCube(cubePosition.center + transform.position, transform.localScale);
    }
}
