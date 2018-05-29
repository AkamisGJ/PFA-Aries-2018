using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.Characters.FirstPerson;


public class Weapon_sway : MonoBehaviour {
	
	//Sway
	[Header("Gun Sway")]
	public float amout = 1;
	public float maxAmount;
	public float smoothAmount;
	private Vector3 initalPosition;

	//GunBob  
	[Header("Gun  Bob")]
	private float timer = 0.0f;

	public float bobbingSpeedWalking = 0.15f;
	public float bobbingAmountWalking = 0.01f;

	public float bobbingSpeedRunning = 0.2f;
	public float bobbingAmountRunning = 0.2f;
	private float bobbingSpeed;
	private float bobbingAmount;
	public float midpoint = 2.0f;

	public FirstPersonController FPS;
	private bool isRunning;
	private bool isJumping;

	// Use this for initialization
	void Start () {
		initalPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
		isRunning = !FPS.m_IsWalking;
		isJumping = ! (bool)(FPS.GetComponent<CharacterController>().isGrounded);
		SwayGun();
	}

	void LateUpdate()
	{
		GunBob();
	}

	void SwayGun(){
		float movementX = -Input.GetAxis("Mouse X") * amout;
		movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
		
		float movementY = -Input.GetAxis("Mouse Y") * amout;
		movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

		Vector3 finalPosition = new Vector3(movementX, movementY, 0f);
		transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initalPosition, Time.deltaTime * smoothAmount);
	}

	void GunBob(){

		if(isRunning){
			bobbingAmount = bobbingAmountRunning;
			bobbingSpeed = bobbingSpeedRunning;
		}
		else
		{
			bobbingAmount = bobbingAmountWalking;
			bobbingSpeed = bobbingSpeedWalking;
		}
		if(isJumping == false){

			float waveslice = 0.0f;
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");

			Vector3 cSharpConversion = transform.localPosition; 

			if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) {
				timer = 0.0f;
			}
			else {
				waveslice = Mathf.Sin(timer);
				timer = timer + bobbingSpeed;
				if (timer > Mathf.PI * 2) {
				timer = timer - (Mathf.PI * 2);
				}
			}
			if (waveslice != 0) {
				float translateChange = waveslice * bobbingAmount;
				float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
				totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f);
				translateChange = totalAxes * translateChange;
				cSharpConversion.y = midpoint + translateChange;
			}
			else {
				cSharpConversion.y = midpoint;
			}

			transform.localPosition = cSharpConversion;
		}
	}
}
