using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public float speed;
<<<<<<< HEAD
	public Vector2 direction;
=======
	Vector2 direction;
>>>>>>> character-class
	Rigidbody2D rigidBody;

	// Use this for initialization
	protected void InitCharacter () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	protected void HandleMovement () {
		rigidBody.velocity = (direction * speed);
	}

<<<<<<< HEAD
	protected void SetDirection (Vector2 direction){
		this.direction = direction;
	
	}

=======
	protected void SetDirection (Vector2 dir){
		this.direction = dir;
	}
>>>>>>> character-class
}
