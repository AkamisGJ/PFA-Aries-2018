using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Eboulement : MonoBehaviour
{
    public int m_mass;
    public float timeShaking = 3f;
    private GameObject m_Player;
    private bool Tédjapassé = false;
    public float m_magnitude = 1f;
    public float m_roughness = 12f;

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider col)
    {
        int nb_child = transform.childCount;
        if(Tédjapassé == false)
        {
            for (int i = 0; i < nb_child; i++)
            {
                Tédjapassé = true;
                transform.GetChild(i).gameObject.AddComponent(typeof(Rigidbody));

                transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                transform.GetChild(i).GetComponent<Rigidbody>().mass = m_mass;

                CameraShakeInstance m_CameraPresset = new CameraShakeInstance(m_magnitude, m_roughness);
                m_Player.GetComponentInChildren<CameraShaker>().ShakeOnce(m_CameraPresset.Magnitude, m_CameraPresset.Roughness, timeShaking, timeShaking);
            }
        }

    }
}
