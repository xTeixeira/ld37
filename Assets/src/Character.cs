using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public float speed;
	public Vector3 direction;
	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {		
		Move ();
	}

	void Move () {
		rigidBody.velocity = (direction * speed);
	}
}
