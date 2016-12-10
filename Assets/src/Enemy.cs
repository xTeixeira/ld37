using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	Vector2 nextWaypoint;

	// Use this for initialization
	void Start () {
		this.InitCharacter ();
	}
	
	// Update is called once per frame
	void Update () {
		this.HandleAI ();
		this.HandleMovement ();
	}

	void HandleAI () {
		this.SetDirection (transform.InverseTransformPoint (nextWaypoint).normalized);
		nextWaypoint = Vector2.Distance (transform.position, nextWaypoint) <= 1 ? 
			new Vector2 (Random.Range (-40, 40), Random.Range (-40, 40)) : nextWaypoint;
	}
}
