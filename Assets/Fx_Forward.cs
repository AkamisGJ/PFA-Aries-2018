using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx_Forward : MonoBehaviour {

	// Use this for initialization
	public float speed = 20f;
	public float time_before_die = 10f;
	void Start () {
		Destroy(gameObject, time_before_die);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed;
	}

	void OnCollisionEnter(Collision other)
	{
		Destroy(gameObject);
		print("Collision !");
	}

}
