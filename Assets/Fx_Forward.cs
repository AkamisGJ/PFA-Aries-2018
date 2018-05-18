using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx_Forward : MonoBehaviour {

	// Use this for initialization
	public float speed = 20f;
	public float time_before_die = 10f;
	public GameObject hitfx;
	void Start () {
		Destroy(gameObject, time_before_die);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed;
	}

	void OnCollisionEnter(Collision other)
	{
		
		Instantiate(hitfx, other.contacts[0].point + (other.contacts[0].normal), Quaternion.LookRotation(GameObject.FindWithTag("Player").transform.position - transform.position));
		print(other.contacts[0].point + (other.contacts[0].normal * 0.1f));
		Destroy(gameObject);
		print("Collision !");
	}

}
