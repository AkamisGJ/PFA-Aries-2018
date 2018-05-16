using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	[Header("Gizmos")]
	public Color color_zone;
	private BoxCollider cubePosition;

	void Start () {
		cubePosition = GetComponent<BoxCollider>();
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = color_zone;
		cubePosition = GetComponent<BoxCollider>();
		Gizmos.DrawCube(cubePosition.center + transform.position, transform.localScale);
	}
}
