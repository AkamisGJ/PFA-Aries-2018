using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GetWeapon : MonoBehaviour {

    
    public float timeShaking = 3f;
    public float timeBeforeDestroy = 3f;
    public ParticleSystem m_Explosion;
    public GameObject m_Fissure;
    public Animator Dome;
    public MeshRenderer Hexa;
    public Material ToitRouge;
    public AudioSource alarme;

    private GameObject m_WeaponPlayer;
    private MeshRenderer m_WeaponMesh;
    private GameObject m_Player;
    void Start () {
        m_WeaponMesh = GetComponent<MeshRenderer>();
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_WeaponPlayer = GameObject.FindGameObjectWithTag("Looper");
        if(m_WeaponPlayer != null && m_WeaponPlayer.activeInHierarchy == true){
            m_WeaponPlayer.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Desactive l'arme d'exposition
            m_WeaponMesh.enabled = false; 
            GetComponent<SphereCollider>().enabled = false;
            transform.Find("Looper Deco").gameObject.SetActive(false);

            //Active l'arme du joueur
            m_WeaponPlayer.gameObject.SetActive(true); 

            alarme.Play(); //L'alame s'active

            //Change la couleur du toit
            Hexa.material = ToitRouge;

            StartCoroutine(Destruction());

        }

    }

    IEnumerator Destruction(){

        yield return new WaitForSeconds(timeBeforeDestroy);
        
        //Shaking de la camera
        CameraShakeInstance m_CameraPresset = new CameraShakeInstance(6f, 15f);
        m_Player.GetComponentInChildren<CameraShaker>().ShakeOnce(m_CameraPresset.Magnitude , m_CameraPresset.Roughness , timeShaking, timeShaking);


        //Explosion
        m_Explosion.Play(true); //Joue l'animation du Particule Systeme
        m_Explosion.GetComponent<AudioSource>().Play(); //Joue le son de l'explosion

        //La fissure tombe
        m_Fissure.AddComponent(typeof(Rigidbody));

        //Les platformes tombes aussi
        Dome.SetBool("PlatformFall", true);
        GameObject[] platformes = GameObject.FindGameObjectsWithTag("Platforme Dome");

        foreach (var platforme in platformes)
        {
            platforme.AddComponent(typeof(Rigidbody));
        }

        //L'hexagone disparait
        Dome.SetBool("RemoveAlpha", true);

    }
}
