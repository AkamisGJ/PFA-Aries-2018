using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class AnimationGun : MonoBehaviour {

	public Animator m_animator;
    private Player playerRewired;

    void Start()
    {
        playerRewired = ReInput.players.GetPlayer(0);
    }
	
	void Update () {
        if (playerRewired.GetButtonDown("Fire1") || (bool)(Input.GetAxis("Fire1Joy") > 0.3f))
            m_animator.SetTrigger("PrimaryShoot");

        if (playerRewired.GetButtonDown("Fire2") || (bool)(Input.GetAxis("Fire2Joy") > 0.3f))
            m_animator.SetTrigger("SecondaryShoot");
    }
}
