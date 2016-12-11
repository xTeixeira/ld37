using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	Vector2 nextWaypoint;
	Vector3 playerPosition;

	void Start () {
		this.InitCharacter ();
	}
	
	void Update () {
		this.HandleAI ();
		this.HandleMovement ();
		this.HandleLife ();
		this.HandleAttack ();
	}

	void HandleAI () {
		playerPosition = GameManager.GetPlayerPosition();

		Vector2 direction = transform.InverseTransformPoint (playerPosition).normalized;
		this.SetMoveDirection (direction);
		this.HandleOrientation (direction);
		
		if (Vector3.Distance (transform.position, playerPosition) < meleeWeapon.meleeRange) {
			isMeleeAttacking = meleeWeapon.Attack (direction);
		}
			
	}

	void HandleAttack (){
		if (isMeleeAttacking)
			GameManager.SendPlayerHit (meleeWeapon.GetHitInfo ());
		isMeleeAttacking = false;
	}

	void HandleLife(){
		if (this.life <= 0)
			Destroy (gameObject);
	}


}
