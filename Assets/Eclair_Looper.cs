using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Eclair_Looper : MonoBehaviour {

	public LightningBoltScript m_eclair;
	public MeshFilter m_mesh;
	public GameObject m_StartPosition;

	int totalVertice;
	void Start () {
		m_eclair.StartObject = m_StartPosition;
		totalVertice = m_mesh.mesh.vertexCount;
		print("Total Vertice = " + totalVertice);
	}
	
	// Update is called once per frame
	void Update () {
		int randompoint = Random.Range(0, totalVertice);
		m_eclair.EndPosition = m_mesh.mesh.vertices[randompoint];
		print(m_eclair.EndPosition.ToString());
	}
}
