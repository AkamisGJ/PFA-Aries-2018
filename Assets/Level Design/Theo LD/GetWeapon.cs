using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeapon : MonoBehaviour {

    public GameObject m_WeaponGet;
    public GameObject m_WeaponPlayer;

    void Start () {
        m_WeaponPlayer.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("magie !");
            m_WeaponGet.gameObject.SetActive(false);
            m_WeaponPlayer.gameObject.SetActive(true);
        }

    }
}
