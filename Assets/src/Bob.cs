using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : Character {

	Vector3 playerPosition;

	Vector2 direction;

	bool isTimeToMove = true;

	// Use this for initialization
	void Start () {
		this.InitCharacter ();
	}
	
	// Update is called once per frame
	void Update () {
		//HandleShooting ();

		playerPosition = GameManager.GetPlayerPosition();

		direction = transform.InverseTransformPoint (playerPosition).normalized;
		this.HandleOrientation (direction);
	}

	void HandleShooting(){
		
	
	}


}
