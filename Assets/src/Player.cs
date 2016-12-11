using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	public GameObject aim;

	// Use this for initialization
	void Start () {
		this.InitCharacter ();
	}



	// Update is called once per frame
	void Update () {
		this.SetMoveDirection (new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")));
		this.HandleMovement ();
		this.HandleAim ();
		this.HandleOrientation (aim.transform.up);
		this.HandleAttack ();
	}

	void HandleAim () {
		Vector3 mouse_pos = Input.mousePosition;
		Vector3 object_pos = Camera.main.WorldToScreenPoint(aim.transform.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;
		float angle = (Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg)-90;
		aim.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	void HandleAttack() {
		if (animator.GetBool("attack")){
			animator.SetBool("attack",false);
		}

		if (Input.GetButtonDown ("Fire1") && meleeWeapon.ready) {
			animator.SetBool ("attack", true);
			this.isMeleeAttacking = meleeWeapon.Attack (Vector3.zero);


		}
		if (Input.GetButtonDown ("Fire2") && rangedWeapon.ready) {
			animator.SetBool ("attack", true);
			rangedWeapon.Attack (aim.transform.up);
		}
	}

	public bool IsMeleeAttacking() {
		if(isMeleeAttacking) {
			isMeleeAttacking = false;
			return true;
		}
		return false;
	}


}
