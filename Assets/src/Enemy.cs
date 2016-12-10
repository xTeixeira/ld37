using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public float minPlayerDistance;
	public float attackRange;
	public float attackSecondsDelay;

	bool canAttack = true;
	Vector2 nextWaypoint;
	Vector3 playerPosition;

	void Start () {
		this.InitCharacter ();
	}
	
	void Update () {
		this.HandleAI ();
		this.HandleMovement ();
	}

	void HandleAI () {
		playerPosition = GameManager.GetPlayerPosition();
		nextWaypoint = Vector2.Distance (transform.position, nextWaypoint) <= 1 ? 
			new Vector2 (Random.Range (-40, 40), Random.Range (-40, 40)) : nextWaypoint;

		if(Vector3.Distance(transform.position, playerPosition) <= minPlayerDistance)
			this.SetDirection (transform.InverseTransformPoint (playerPosition).normalized);
		else
			this.SetDirection (transform.InverseTransformPoint (nextWaypoint).normalized);

		if (canAttack && Vector3.Distance (transform.position, playerPosition) < attackRange) {
			GameManager.SendPlayerHit (new HitInfo (1));
			StartCoroutine (AttackCooldown ());
		}
			
	}

	IEnumerator AttackCooldown(){
		canAttack = false;
		yield return new WaitForSeconds(attackSecondsDelay);
		canAttack = true;
	}
}
