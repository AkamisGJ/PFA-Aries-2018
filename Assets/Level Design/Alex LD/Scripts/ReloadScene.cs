using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {

    [Header("Gizmos")]
    public Color color_zone;
    private BoxCollider cubePosition;
    public SceneLoader m_Sceneloader;

    void Start()
    {
        cubePosition = GetComponent<BoxCollider>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color_zone;
        cubePosition = GetComponent<BoxCollider>();
        Gizmos.DrawCube(cubePosition.center + transform.position, transform.localScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            m_Sceneloader.FadeToLevel(index);
        }
    }

}
