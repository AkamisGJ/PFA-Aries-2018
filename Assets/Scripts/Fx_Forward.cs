using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx_Forward : MonoBehaviour
{

	// Use this for initialization
	public float speed = 0.5f;
	public float time_before_die = 10f;
	public GameObject hitfx;

	public GameObject FX;
	void Start ()
    {
		Destroy( gameObject, time_before_die);
	}
	
	void FixedUpdate ()
    {
		transform.position += transform.forward * speed;
	}

	void OnCollisionEnter(Collision other)
	{
		//print("Tir secondaire touche : " + other.transform.name);
		
		int layer = LayerMask.NameToLayer("Mirror");
		if( other.gameObject.layer == layer){
			Vector3 reflexion = Vector3.Reflect(transform.forward, other.contacts[0].normal);
			Instantiate(gameObject, other.contacts[0].point + (other.contacts[0].normal), Quaternion.LookRotation(reflexion));
			Destroy(gameObject);
		}
		else{
			layer = LayerMask.NameToLayer("Useable");
			if( other.gameObject.layer == layer){
				Useable script = other.gameObject.GetComponent<Useable>();
				script.Toogle();
			}
			
			Instantiate( hitfx, other.contacts[0].point + (other.contacts[0].normal), Quaternion.LookRotation(GameObject.FindWithTag("Player").transform.position - transform.position));
			Destroy(gameObject, 0.1f);
		}
	}
}
