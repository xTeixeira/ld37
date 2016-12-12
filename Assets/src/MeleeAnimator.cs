using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimator : MonoBehaviour {

	public Animator animator;
	public ParticleSystem meleeParticle;
	Quaternion aimRotation;
	public bool isAimLocked;

	public Transform aim;

	void Update () {
		transform.rotation = aimRotation;
	}
	
	public void StartAttackAnimation (Vector3 direction) {
		animator.SetBool ("attack", true);
		meleeParticle.Play ();
		aimRotation = gameObject.transform.rotation;
		isAimLocked = true;
	}

	public void CheckAttackAnimation () {
		if (animator.GetBool ("attack")) {
			animator.SetBool ("attack", false);
		}
	}

	public void Unlock () {
		isAimLocked = false;
		aimRotation = new Quaternion (0, 0, 0, 0);
	}
}
