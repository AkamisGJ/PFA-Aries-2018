using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour
{

    public float SlowMoFactor = 0.05f;
    public float SlowMoLenght = 2f;


    public Color color_Time;
    private BoxCollider cubePosition;

    void Start()
    {
        cubePosition = GetComponent<BoxCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 1;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color_Time;
        cubePosition = GetComponent<BoxCollider>();
        Gizmos.DrawCube(cubePosition.center + transform.position, transform.localScale);
    }
}
