using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : Character {

	Vector3 playerPosition;

	Vector2 direction;

	bool isAbleToMove = true;
	bool isAbleToAttack = false;
	bool hasLockedAttackState = false;

	public float timeMovingMin = 1.0f;
	public float timeMovingMax = 5.0f;

	public float timeStoppedMin = 1.0f;
	public float timeStoppedMax = 6.0f;

	void Start () {
		this.InitCharacter ();
	}
	
	void Update () {
		HandleAI();
		HandleMovement ();
		HandleLife ();
	}

	void HandleAI(){

		if(animator.GetBool("attack")){
			animator.SetBool ("attack", false);
		}

		if (isAbleToAttack) {
			if (!hasLockedAttackState) {
				float secondsStopped = Random.Range (timeStoppedMin, timeStoppedMax);
				StartCoroutine (lockStateAtacking (secondsStopped));
				this.SetMoveDirection (Vector2.zero);
				hasLockedAttackState = true;
			}

			playerPosition = GameManager.GetPlayerPosition ();
			direction = transform.InverseTransformPoint (playerPosition).normalized;
			this.HandleOrientation (direction);

			if (rangedWeapon.ready) {
				animator.SetBool ("attack", true);
				rangedWeapon.Attack (transform.position, direction);
			}
		}

		if(isAbleToMove){

			direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
			this.SetMoveDirection (direction);

			float secondsMoving = Random.Range (timeMovingMin, timeMovingMax); 
			StartCoroutine( lockStateMoving (secondsMoving));
		}
	}

	void HandleLife(){
		if (this.life <= 0) {
			GameManager.AddScore (15);
			Kill ();
		}
	}

	IEnumerator lockStateAtacking(float secondsOnState){
		isAbleToMove = false;
		yield return new WaitForSeconds (secondsOnState);
		hasLockedAttackState = false;
		isAbleToAttack = false;
		isAbleToMove = true;
	}

	IEnumerator lockStateMoving(float secondsOnState){
		isAbleToAttack = false;
		isAbleToMove = false;
		yield return new WaitForSeconds (secondsOnState);
		isAbleToAttack = true;
	}
		
	void OnCollisionEnter2D(Collision2D col){
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
