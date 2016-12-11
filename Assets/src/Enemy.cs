using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public float minPlayerDistance;

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
		nextWaypoint = Vector2.Distance (transform.position, nextWaypoint) <= 1 ? 
			new Vector2 (Random.Range (-40, 40), Random.Range (-40, 40)) : nextWaypoint;

		Vector2 direction;

		if (Vector3.Distance (transform.position, playerPosition) <= minPlayerDistance) {
			direction = transform.InverseTransformPoint (playerPosition).normalized;
			this.SetMoveDirection (direction);
			this.HandleOrientation (direction);
		} else {
			direction = transform.InverseTransformPoint (nextWaypoint).normalized;
			this.SetMoveDirection (direction);
			this.HandleOrientation (direction);
		}
		if (Vector3.Distance (transform.position, playerPosition) < meleeWeapon.meleeRange) {
			isMeleeAttacking = meleeWeapon.Attack (transform.position, direction);
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
