using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : Character {

	Vector3 playerPosition;

	Vector2 direction;

	bool isTimeToMove = true;

	public float timeMovingMin = 1.0f;
	public float timeMovingMax = 5.0f;

	public float timeStoppedMin = 1.0f;
	public float timeStoppedMax = 6.0f;

	// Use this for initialization
	void Start () {
		this.InitCharacter ();
	}
	
	// Update is called once per frame
	void Update () {
		
		HandleAI();
		HandleMovement ();
		HandleLife ();
	}

	void HandleAI(){
		if (!isTimeToMove) {
			playerPosition = GameManager.GetPlayerPosition ();
			direction = transform.InverseTransformPoint (playerPosition).normalized;
			this.HandleOrientation (direction);

			if (rangedWeapon.ready) {
				animator.SetBool ("attack", true);
				rangedWeapon.Attack (transform.position, direction);
			}
		}else{
			direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
			this.SetMoveDirection (direction);

			float secondsStopped = Random.Range (timeStoppedMin, timeStoppedMax);
			float secondsMoving = Random.Range (timeMovingMin, timeMovingMax); 

			StartCoroutine (whaitForNextWaypoint(secondsMoving, secondsStopped));
		}
	}

	void HandleLife(){
		if (this.life <= 0)
			Destroy (gameObject);
	}
		
	IEnumerator whaitForNextWaypoint (float secondsMoving, float secondsStopped)
	{
		isTimeToMove = false;
		yield return new WaitForSeconds (secondsMoving);
		this.SetMoveDirection (Vector2.zero);
		StartCoroutine( waitStopped (secondsStopped));
		isTimeToMove = true;
	}

	IEnumerator waitStopped(float seconds){
		yield return new WaitForSeconds (seconds);
	}
}
