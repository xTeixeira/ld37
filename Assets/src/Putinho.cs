﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Putinho : Character {

	Vector2 nextWaypoint;
	Vector3 playerPosition;

	Vector2 direction;

	void Start () {
		this.InitCharacter ();
	}
	
	void Update () {
		this.HandleAI ();
		this.HandleMovement ();
		this.HandleLife ();
	}

	void HandleAI () {
		playerPosition = GameManager.GetPlayerPosition();

		direction = transform.InverseTransformPoint (playerPosition).normalized;
		this.SetMoveDirection (direction);
		this.HandleOrientation (direction);

		float playerDistance = Vector3.Distance(transform.position, playerPosition);

		Color color = spriteRenderer.color;
		if (!isHit) {
			color.a = playerDistance * 0.15f;
		}
		spriteRenderer.color = color;
			
	}

	void HandleLife(){
		if (this.life <= 0) {
			GameManager.AddScore (10);
			Kill ();
		}
	}

	IEnumerator DeathFade (){
		yield return new WaitForSeconds (0.1f);
		Destroy (gameObject);
	}



	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag ("Player")) {
			if (!isMeleeAttacking) {
				GameManager.SendPlayerHit (meleeWeapon.GetHitInfo ());
				Kill ();
			}
		}
	}

	void Kill() {
		Destroy (gameObject);
		if(hitParticle != null)
			Instantiate (hitParticle, transform.position, Quaternion.identity);
	}
}
