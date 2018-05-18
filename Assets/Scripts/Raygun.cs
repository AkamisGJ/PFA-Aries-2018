﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
public class Raygun : MonoBehaviour {
	
	public Transform player;

	[Header("RayCast")]
	public Transform firepoint;
	public Transform cursor;
	public float maxDistance;
	//public LayerMask m_layerMask;
	public float cooldown_delay;
	private float cooldown = 0;
	private float cooldown_2 = 0;
	private FirstPersonController m_FPS_script;
	public Camera Guncamera;
	public Camera MainCamera;
	public LayerMask RaygunLayer;

	public GameObject m_TrailGenerator;
	public GameObject m_FXShoot;

	// Use this for initialization
	void Start () {
		m_FPS_script = player.GetComponent<FirstPersonController>();
		//m_TrailGenerator.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		cooldown += Time.deltaTime;
		cooldown_2 += Time.deltaTime;

		if(Input.GetButtonDown("Fire1") && cooldown > cooldown_delay){
			RaycastHit hit_info;
			if(Physics.Raycast(cursor.position, cursor.forward, out hit_info ,maxDistance, RaygunLayer.value)){

				Debug.DrawRay(cursor.position, cursor.forward * hit_info.distance, Color.cyan, 2.0f);
				m_TrailGenerator.SetActive(false);

				if(m_TrailGenerator.activeSelf == true){
					foreach (Transform Trail in m_TrailGenerator.transform)
					{
						print(Trail.name);
						Trail.localPosition = new Vector3(0.5f,0.5f,10);
					}
				}

				//Si le joueur tir sur une surface téléportable
				int layer = LayerMask.NameToLayer("CanTeleport");
				if( hit_info.collider.gameObject.layer == layer){
					StartCoroutine(Teleportation(player.position ,hit_info.point));
					cooldown = 0; //Reset le cooldown
				}

				//Si le joueur tir sur un miroir
				layer = LayerMask.NameToLayer("Mirror");
				if( hit_info.collider.gameObject.layer == layer){
					Vector3 reflexion = Vector3.Reflect(cursor.forward, hit_info.normal);
					float angleY = Vector3.SignedAngle(cursor.forward, reflexion, Vector3.up);
					print("AngleY = " + angleY);

					RaycastHit hit_info_reflexion;
					if(Physics.Raycast(hit_info.point, reflexion, out hit_info_reflexion ,maxDistance, RaygunLayer.value)){
						
						//Si le joueur tir sur une surface téléportable
						layer = LayerMask.NameToLayer("CanTeleport");
						if(hit_info_reflexion.collider.gameObject.layer == layer){
							StartCoroutine(Teleportation(player.position ,hit_info.point, hit_info_reflexion.point, angleY));
							cooldown = 0; //Reset le cooldown
						}
					}
				}
				
			}else{
				Debug.DrawRay(cursor.position, cursor.forward * 1000, Color.black, 2.0f);
			}
		}

		//Tir Secondaire
		if(Input.GetButtonDown("Fire2") && cooldown_2 > cooldown_delay){
			Quaternion rotation = Quaternion.LookRotation(cursor.transform.forward, player.transform.up);
			Instantiate(m_FXShoot, firepoint.position, rotation);
			
			RaycastHit hit_info;
			if(Physics.Raycast(cursor.position, cursor.forward, out hit_info ,maxDistance, RaygunLayer.value)){
				
				//Si le joueur tir sur un élément activable
				int layer = LayerMask.NameToLayer("Useable");
				if( hit_info.collider.gameObject.layer == layer){
					var script = hit_info.collider.GetComponent<Useable>();
						script.Toogle();
						
						cooldown_2 = 0; //Reset le cooldown
				}
			}
		}
	}

	IEnumerator Teleportation(Vector3 StartPoint, Vector3 EndPoint){
		
		Vector3 distance = EndPoint - StartPoint;
		//print("Distance = " + distance.sqrMagnitude);
		m_FPS_script.m_Active = false;

		float time = 0f;
		int point;
		if(distance.sqrMagnitude < 800f){
			point = 6; //La distance est courte 16
		}else{
			point = 16; //La distance est longue 30
		}

		for(int i = point; i > 0; i--){
			//Mouvement
			player.position += (distance/i);
			distance = EndPoint - player.position;

			//Field Of View
			if(i > point/2.5){
				//1er partie du TP
				Guncamera.fieldOfView ++;
				MainCamera.fieldOfView++;
			}

			time += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		Guncamera.fieldOfView = 60;
		MainCamera.fieldOfView = 60;

		if(m_TrailGenerator.activeSelf == true){
			foreach (Transform Trail in m_TrailGenerator.transform)
				{
					Trail.localPosition = new Vector3(0,0,0);
					print("Reste to 0");
				}
		}
		//m_TrailGenerator.SetActive(false);

		m_FPS_script.m_Active = true;
		//print("Temps = " + time);
	}

	//Teleportation avec un mirroir
	IEnumerator Teleportation(Vector3 StartPoint, Vector3 EndPoint, Vector3 EndPoint_2, float angleY){
		//Direct Way
			//player.position = EndPoint;

		//With distance
		
			//Première TP
			Vector3 distance = EndPoint - StartPoint;
			//print("Distance = " + distance.sqrMagnitude);
			m_FPS_script.m_Active = false;

			float time = 0f;
			int point;
			if(distance.sqrMagnitude < 800f){
				point = 10; //La distance est courte 16
			}else{
				point = 20; //La distance est longue 30
			}

			for(int i = point; i > 0; i--){
				//Mouvement
				player.position += (distance/i);
				distance = EndPoint - player.position;
	
				//Field Of View
				if(i > point/2.5){
					//1er partie du TP
					Guncamera.fieldOfView ++;
					MainCamera.fieldOfView++;
				}

				time += Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}

			//Deuxieme téléportation
			
			//Rotation du perso
			Quaternion newRotationY = Quaternion.Euler (0f, angleY, 0f);
			
			player.GetComponent<FirstPersonController>().m_MouseLook.smooth = true;
			player.GetComponent<FirstPersonController>().m_MouseLook.m_CharacterTargetRot *= newRotationY;
			

			distance = EndPoint_2 - transform.position;
			time = 0f;
			if(distance.sqrMagnitude < 800f){
				point = 10; //La distance est courte 16
			}else{
				point = 22; //La distance est longue 30
			}

			for(int i = point; i > 0; i--){
				//Mouvement
				player.position += (distance/i);
				distance = EndPoint_2 - player.position;

				time += Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}
			Guncamera.fieldOfView = 60;
			MainCamera.fieldOfView = 60;


			if(m_TrailGenerator.activeSelf == true){
				foreach (Transform Trail in m_TrailGenerator.transform)
					{
						Trail.localPosition = new Vector3(0,0,0);
						print("Reste to 0");
					}
			}
			//m_TrailGenerator.SetActive(false);
			player.GetComponent<FirstPersonController>().m_MouseLook.smooth = false;
			m_FPS_script.m_Active = true;

		//Framerate independant
	}
}
