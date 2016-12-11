﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Character : MonoBehaviour {

	public float life;
	public float speed;
	Vector2 moveDirection;
	Rigidbody2D rigidBody;

	public Weapon meleeWeapon;
	public GameObject hitParticle;
	public Animator animator; 
	float oriAngle;
	protected bool isMeleeAttacking;

	// Use this for initialization
	protected void InitCharacter () {
		rigidBody = GetComponent<Rigidbody2D> ();
		meleeWeapon = Instantiate (meleeWeapon, transform);
	}

	protected void HandleMovement () {
		rigidBody.velocity = (moveDirection * speed);
	}

	protected void SetMoveDirection (Vector2 direction){
		this.moveDirection = direction;
	}

	public void SendHit(HitInfo hit){
		life -= hit.damage;
		if(hitParticle != null)
			Instantiate (hitParticle, transform.position, Quaternion.identity);
	}

	protected void HandleOrientation (Vector2 oriDirection){

		string currentAxis = "down";
		float directionAngle = Vector2.Angle (Vector2.up, oriDirection);

		if (directionAngle <= 45)
			currentAxis = "up";
		else if (directionAngle <= 145)
			currentAxis = oriDirection.x > 0 ? "right" : "left";
		else
			currentAxis = "down";

		foreach (AnimatorControllerParameter parameter in animator.parameters) {
			if (!parameter.name.Equals (currentAxis))
				animator.SetBool (parameter.name, false);
		}

		animator.SetBool (currentAxis, true);
	}
		
	public HitInfo GetCurrentHitInfo() {
		return meleeWeapon.GetHitInfo();
	}

}
