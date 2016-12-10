using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public float life;
	public float speed;
	Vector2 moveDirection;
	Rigidbody2D rigidBody;

	// Use this for initialization
	protected void InitCharacter () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	protected void HandleMovement () {
		rigidBody.velocity = (moveDirection * speed);
	}

	protected void SetMoveDirection (Vector2 direction){
		this.moveDirection = direction;
	}

	public void SendHit(HitInfo hit){
		life -= hit.damage;
	}

	protected void HandleOrientation (
}
