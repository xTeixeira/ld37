using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	public float speed = 0.2f;
	Rigidbody2D body;
	Vector3 input;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		input = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

		Move ();

	}

	void Move () {
		body.velocity = input*speed;

	}
}
