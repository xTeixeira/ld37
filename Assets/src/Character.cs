using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public float speed;
	public Vector3 direction;
	Rigidbody2D rigidBody;

	// Use this for initialization
	protected void InitCharacter () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	protected void HandleMovement () {
		rigidBody.velocity = (direction * speed);
	}
}
