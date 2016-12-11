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
	}

	void HandleAI () {
		playerPosition = GameManager.GetPlayerPosition();
		nextWaypoint = Vector2.Distance (transform.position, nextWaypoint) <= 1 ? 
			new Vector2 (Random.Range (-40, 40), Random.Range (-40, 40)) : nextWaypoint;

		if (Vector3.Distance (transform.position, playerPosition) <= minPlayerDistance) {
			Vector2 direction = transform.InverseTransformPoint (playerPosition).normalized;
			this.SetMoveDirection (direction);
			this.HandleOrientation (direction);
		} else {
			Vector2 direction = transform.InverseTransformPoint (nextWaypoint).normalized;
			this.SetMoveDirection (direction);
			this.HandleOrientation (direction);
		}
		if (canAttack && Vector3.Distance (transform.position, playerPosition) < currentWeapon.range) {
			GameManager.SendPlayerHit (new HitInfo (1));
			StartCoroutine (AttackCooldown ());
		}
			
	}

	void HandleLife(){
		if (this.life <= 0)
			Destroy (gameObject);
	}


}
