using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using VolumetricLines;
public class Raygun : MonoBehaviour {
	
	public Transform player;

	[Header("RayCast")]
	public Transform firepoint;
	public Transform firepoint_FX;
	public Transform cursor;
	public float maxDistance;
	public float cooldown_delay = 0.4f;

	public float cooldown_delay_2 = 1f;
	private float cooldown = 0;
	private float cooldown_2 = 0;
	public float m_timeScaleFactor = 0.5f;
	private FirstPersonController m_FPS_script;
	public Camera Guncamera;
	public Camera MainCamera;
	public LayerMask RaygunLayer;
	public GameObject m_FXShoot;
	public GameObject m_Sparck_shoot;

	private PostProcessingBehaviour PostProd;
	private Animator m_animator;
	private bool OnTeleporation = false;

	public LineRenderer m_lineRendererPrefab;

	[Header("Audio")]
	[Range(0f, 1f)] public float volumeFiring1 = 1f;
	public AudioClip[] Fire1;
	[Range(0f, 1f)] public float volumeFiring2 = 1f;
	public AudioClip Fire2;
	private AudioSource m_audioSource;

	  [Header("Debug")]

    public bool DesactiveColliderDuringTeleportation;
	public SphereCollider HeadBlocker;
	public float HeadDistance = 2f;

	// Use this for initialization
	void Start () {
		m_FPS_script = player.GetComponent<FirstPersonController>();
		PostProd = MainCamera.GetComponent<PostProcessingBehaviour>();
		m_animator = GetComponentInChildren<Animator>();
		m_audioSource = GetComponent<AudioSource>();

		//Ajoute le laser si il n'es pas déja dans la scene
		GameObject m_laser = GameObject.FindGameObjectWithTag("LaserTP");
		if(m_laser == null){
			LineRenderer laser = Instantiate(m_lineRendererPrefab, Vector3.zero, Quaternion.identity);
			m_lineRendererPrefab = laser;
		}
		else{
			m_lineRendererPrefab = m_laser.GetComponent<LineRenderer>();
		}
		HideLineRenderer();

	}

	void HideLineRenderer(){
		//Line renderer
		Vector3 hide = new Vector3(-5000f, -5000f, -5000f);
		Vector3[] posLineRenderer = new []{
			hide,
			hide,
			hide
		};
		m_lineRendererPrefab.SetPositions(posLineRenderer);
	}
	
	// Update is called once per frame
	void Update () {

		cooldown += Time.deltaTime;
		cooldown_2 += Time.deltaTime;

		//Tir Principalle
		if((Input.GetButtonDown("Fire1") || (bool)(Input.GetAxis("Fire1Joy") > 0.3f) )&& cooldown > cooldown_delay && OnTeleporation == false){
			
			//Animation and Sound
			m_animator.SetTrigger("Shoot");
			m_audioSource.Stop();
			m_audioSource.volume = volumeFiring1;
			int index = Random.Range(0, Fire1.Length);
			m_audioSource.PlayOneShot(Fire1[index]);

			cooldown = 0; //Reset le cooldown

			RaycastHit hit_info;
			if(Physics.Raycast(cursor.position, cursor.forward, out hit_info ,maxDistance, RaygunLayer.value)){
				

				Debug.DrawRay(cursor.position, cursor.forward * hit_info.distance, Color.cyan, 2.0f);

				//Line renderer
				Vector3[] posLineRenderer = new []{
					firepoint.position,
					hit_info.point,
					hit_info.point
				};
				m_lineRendererPrefab.SetPositions(posLineRenderer);


				//Si le joueur tir sur une surface téléportable
				int layer = LayerMask.NameToLayer("CanTeleport");
				if( hit_info.collider.gameObject.layer == layer){
					StartCoroutine(Teleportation(player.position ,hit_info.point));
					if(hit_info.collider.GetComponentInParent<Teleporteur>()){
						hit_info.collider.GetComponentInParent<Teleporteur>().ChangeMaterial(); //Change le materiaux
					}
					print("Téléporte a : " + hit_info.transform.gameObject.name);
				}
				else{
					print("Tir sur : " + hit_info.transform.gameObject.name);
				}

				//Si le joueur tir sur un miroir
				layer = LayerMask.NameToLayer("Mirror");
				if( hit_info.collider.gameObject.layer == layer){

					Vector3 reflexion = Vector3.Reflect(cursor.forward, hit_info.normal);
					float angleY = Vector3.SignedAngle(cursor.forward, reflexion, Vector3.up);
					//print("AngleY = " + angleY);

					//Line renderer
					posLineRenderer = new []{
						firepoint.position,
						hit_info.point,
						reflexion * 100f,
					};
					m_lineRendererPrefab.SetPositions(posLineRenderer);

					RaycastHit hit_info_reflexion;
					if(Physics.Raycast(hit_info.point, reflexion, out hit_info_reflexion ,maxDistance, RaygunLayer.value)){
						
						//Si le joueur tir sur une surface téléportable
						layer = LayerMask.NameToLayer("CanTeleport");
						if(hit_info_reflexion.collider.gameObject.layer == layer){
							if(hit_info.collider.GetComponentInParent<Teleporteur>()){
								hit_info.collider.GetComponentInParent<Teleporteur>().ChangeMaterial(); //Change le materiaux
							}
							cooldown = 0; //Reset le cooldown
							StartCoroutine(Teleportation(player.position ,hit_info.point, hit_info_reflexion.point, angleY));
						}
					}
				}
				
			}else{
				Debug.DrawRay(cursor.position, cursor.forward * 1000, Color.black, 2.0f);
			}
		}

		//Tir Secondaire
		if( (Input.GetButtonDown("Fire2") || Input.GetAxis("Fire2Joy") > 0.3f ) && (cooldown_2 > cooldown_delay_2 && OnTeleporation == false) ){
			
			//Animation
			m_animator.SetTrigger("Shoot");
			m_audioSource.Stop();
			m_audioSource.volume = volumeFiring2;
			m_audioSource.PlayOneShot(Fire2);

			//Effect de particule
			Instantiate(m_Sparck_shoot, firepoint_FX.position, player.localRotation * MainCamera.transform.localRotation);
			cooldown_2 = 0; //Reset le cooldown
			
			RaycastHit hit_info;
			bool Raycast = Physics.Raycast(cursor.position, cursor.forward, out hit_info ,maxDistance, RaygunLayer.value);
			Quaternion rotation = Quaternion.LookRotation(cursor.transform.forward, player.transform.up);
			float additionalRoot = Vector3.SignedAngle(firepoint.position, cursor.position, cursor.forward);
			Instantiate(m_FXShoot, firepoint.position, rotation * Quaternion.Euler(0f, - additionalRoot, 0f));
		}
	}

	private float m_point_TP_court = 10f;
	private float m_point_TP_long = 21f;
	IEnumerator Teleportation(Vector3 StartPoint, Vector3 EndPoint){
		OnTeleporation = true;

		if(DesactiveColliderDuringTeleportation == true){
			player.GetComponent<CharacterController>().enabled = false;
		}
		player.GetComponent<FirstPersonController>().m_UseFovKick = false;
		GetComponent<Weapon_sway>().enabled = false;

		ChromaticAberrationModel.Settings setting = PostProd.profile.chromaticAberration.settings;
		PostProd.profile.chromaticAberration.settings = setting;

		setting.intensity = 0f;
		
		Vector3 distance = EndPoint - StartPoint;
		//print("Distance = " + distance.sqrMagnitude);
		m_FPS_script.m_Active = false;

		float time = 0f;
		float point;
		if(distance.sqrMagnitude < 800f){
			point = m_point_TP_court; //La distance est courte 16
		}else{
			point = m_point_TP_long; //La distance est longue 30
		}

		//Aberation Chromatique
		float value = 1f;
		setting.intensity = value;
		PostProd.profile.chromaticAberration.settings = setting;


		for(float i = point; i > 0; i--){
			//Mouvement
			Vector3 ClosestPoint = HeadBlocker.ClosestPoint(EndPoint);
			Vector3 distanceHeadCollision = EndPoint - ClosestPoint;
			if(distanceHeadCollision.magnitude < HeadDistance){
				i = 0;
				break;
			}

			player.position += (distance/i);
			distance = EndPoint - player.position;
			

			//Field Of View
			if(i > point/2.5){
				//1er partie du TP
				Guncamera.fieldOfView ++;
				MainCamera.fieldOfView++;
			}

			if(i <= 2){
				player.GetComponent<CharacterController>().enabled = true;
			}

			time += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		player.GetComponent<CharacterController>().enabled = true;
		player.GetComponent<FirstPersonController>().m_UseFovKick = true;
		GetComponent<Weapon_sway>().enabled = true;

		Guncamera.fieldOfView = 60;
		MainCamera.fieldOfView = 60;

		setting.intensity = 0f;
		PostProd.profile.chromaticAberration.settings = setting;
		m_FPS_script.m_Active = true;
		HideLineRenderer();
		//print("Temps = " + time);

		OnTeleporation = false;
	}

	//Teleportation avec un mirroir

	private float m_point_TP_court_2 = 20f;
	private float m_point_TP_long_2 = 41f;
	IEnumerator Teleportation(Vector3 StartPoint, Vector3 EndPoint, Vector3 EndPoint_2, float angleY){

		OnTeleporation = true;
		
		if(DesactiveColliderDuringTeleportation == true){
			player.GetComponent<CharacterController>().enabled = false;
		}
		player.GetComponent<FirstPersonController>().m_UseFovKick = false;
		GetComponent<Weapon_sway>().enabled = false;

		ChromaticAberrationModel.Settings setting = PostProd.profile.chromaticAberration.settings;
		PostProd.profile.chromaticAberration.settings = setting;

		setting.intensity = 0f;
			//Première TP
			Vector3 distance = EndPoint - StartPoint;
			//print("Distance = " + distance.sqrMagnitude);
			m_FPS_script.m_Active = false;


			float point;
			if(distance.sqrMagnitude < 800f){
				point = m_point_TP_court_2; //La distance est courte 20
			}else{
				point = m_point_TP_long_2; //La distance est longue 41
			}

			//Aberation Chromatique
			float value = 1f;
			setting.intensity = value;
			PostProd.profile.chromaticAberration.settings = setting;

			for(float i = point; i > 0; i-- ){

				//Mouvement
				player.position += (distance/i);
				distance = EndPoint - player.position;
	
				//Field Of View
				if(i > point/1.5){
					//1er partie du TP
					Guncamera.fieldOfView ++;
					MainCamera.fieldOfView++;
				}		
				//Slow Motion
				if(i < point/6f){
					SlowMotion(true);
				}	

				yield return new WaitForFixedUpdate();
			}

			//Deuxieme téléportation
			
			//Rotation
			StartCoroutine(Rotation(angleY));
			SlowMotion(true);
			
			distance = EndPoint_2 - transform.position;
			if(distance.sqrMagnitude < 800f){
				point = m_point_TP_court_2; //La distance est courte 20
			}else{
				point = m_point_TP_long_2; //La distance est longue 41
			}

			for(float i = point; i > 0; i--){	

				//Mouvement
				player.position += (distance/i);
				distance = EndPoint_2 - player.position;

				//Slow Motion
				if(i < point/0.9f){
					SlowMotion(false);
				}

				if(i <= 2){
					player.GetComponent<CharacterController>().enabled = true;
				}

				yield return new WaitForFixedUpdate();
			}
			Guncamera.fieldOfView = 60;
			MainCamera.fieldOfView = 60;

			player.GetComponent<CharacterController>().enabled = true;
			player.GetComponent<FirstPersonController>().m_UseFovKick = true;
			GetComponent<Weapon_sway>().enabled = true;

			HideLineRenderer();
			setting.intensity = 0f;
			PostProd.profile.chromaticAberration.settings = setting;
			player.GetComponent<FirstPersonController>().m_MouseLook.smooth = false;
			m_FPS_script.m_Active = true;

			OnTeleporation = false;
	}

	IEnumerator Rotation(float angleY){
		//Rotation
		SlowMotion(false);
		Quaternion newRotationY = Quaternion.Euler (0f, angleY, 0f);
		Quaternion initalRot = player.GetComponent<FirstPersonController>().m_MouseLook.m_CharacterTargetRot;
		player.GetComponent<FirstPersonController>().m_MouseLook.smooth = true;
		player.GetComponent<FirstPersonController>().m_MouseLook.m_CharacterTargetRot *= newRotationY;

		yield return null;
	}
	void SlowMotion(bool state){
		print("Slow Motion = " + state.ToString());
		if(state == true){
			player.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
			while(Time.timeScale > m_timeScaleFactor){
				Time.timeScale -= m_timeScaleFactor;
			}
			//Time.fixedDeltaTime =  0.02f * Time.timeScale; //0.02 = 1/50 FixedUpdate is call 50 time per second
		}
		else{
			player.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
			Time.timeScale = 1f;
			//Time.fixedDeltaTime =  0.02f * Time.timeScale; //0.02 = 1/50 FixedUpdate is call 50 time per second
			
		}
	}
}
