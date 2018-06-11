using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStart : MonoBehaviour {

    float coolDown = 1;
    float coolDownTimer = 0;


    void Start () {
		
	}
	

	void Update () {
        if (coolDownTimer > -1)
            coolDownTimer -= Time.deltaTime;
        Debug.Log("CoolDownTimer = "+coolDownTimer);
    }
}
