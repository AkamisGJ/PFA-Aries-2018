using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Script : MonoBehaviour {

	[TextArea(5,20)]
	public string Information;
	
	[Header("Gizmos")]
	public bool m_ActiveGizmos = true;
	public Color color_checkpoint;
	public Color color_respawnpoint;
	public float radius = 0.5f;
	private BoxCollider cubePosition;
	
	[Header("Checkpoint")]
	public int CheckPoint_value;
	[HideInInspector] public Quaternion rotation;

	void Start () {
		cubePosition = GetComponent<BoxCollider>();
		rotation = transform.rotation;
	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		if(m_ActiveGizmos == true){
			Gizmos.color = color_checkpoint;
			cubePosition = GetComponent<BoxCollider>();
			Gizmos.DrawCube(cubePosition.center + transform.position, cubePosition.size);
			Gizmos.color = color_respawnpoint;
			Gizmos.DrawSphere(transform.position, radius);
		}
	}
}
