using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Script : MonoBehaviour {

	[Header("Gizmos")]
	public Color color_checkpoint;
	public Color color_respawnpoint;
	public float radius = 0.5f;
	private BoxCollider cubePosition;
	
	[Header("Checkpoint")]
	public int CheckPoint_value;

	void Start () {
		cubePosition = GetComponent<BoxCollider>();
	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		Gizmos.color = color_checkpoint;
		cubePosition = GetComponent<BoxCollider>();
		Gizmos.DrawCube(cubePosition.center + transform.position, cubePosition.size);
		Gizmos.color = color_respawnpoint;
		Gizmos.DrawSphere(transform.position, radius);
	}
}
