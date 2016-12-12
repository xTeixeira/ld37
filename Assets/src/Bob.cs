using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : Character {

	//GameObject aim  = new GameObject ();

	Vector3 playerPosition;

	Vector2 direction;

	bool isTimeToMove = true;

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

		} else {
			
			isTimeToMove = false;
		}
	} 

	void HandleAim(){
		/*Vector3 mouse_pos = Input.mousePosition;
		Vector3 object_pos = Camera.main.WorldToScreenPoint(aim.transform.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;
		float angle = (Mathf.Atan2(playerPosition.y, playerPosition.x) * Mathf.Rad2Deg)-90;
		aim.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));*/
	}

	void HandleLife(){
		if (this.life <= 0)
			Destroy (gameObject);
	}
}
