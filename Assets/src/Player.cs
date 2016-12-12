using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	public GameObject aim;

	MeleeAnimator meleeAnimator;
	bool hasJoystickInput;
	public float dashSpeed;
	bool canDash = true;
	public float dashCooldown;
	public float dashDuration;

	void Start () {
		this.InitCharacter ();
		meleeAnimator = GetComponentInChildren<MeleeAnimator> ();
	}

	void Update () {
		hasJoystickInput = (Input.GetAxis ("HorizontalAim") != 0 || Input.GetAxis ("VerticalAim") != 0);
		this.SetMoveDirection (new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")));
		this.HandleMovement ();
		this.HandleAim ();
		this.HandleOrientation (aim.transform.up);
		this.HandleInput ();
	}

	void HandleAim () {
		if(hasJoystickInput) {
			Vector3 axis = new Vector3(Input.GetAxis("HorizontalAim"), Input.GetAxis("VerticalAim"), 0) * 10;
			float angle = (Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg)-90;
			aim.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}
		else {
			Vector3 mouse_pos = Input.mousePosition;
			Vector3 object_pos = Camera.main.WorldToScreenPoint(aim.transform.position);
			mouse_pos.x = mouse_pos.x - object_pos.x;
			mouse_pos.y = mouse_pos.y - object_pos.y;
			float angle = (Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg)-90;
			aim.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}

	}

	void HandleInput() {
		isMeleeAttacking = false;

		if (meleeAnimator.isAimLocked && meleeWeapon.ready) {
			meleeAnimator.Unlock();
		}

		if (animator.GetBool("attack")){
			animator.SetBool("attack",false);
		}

		meleeAnimator.CheckAttackAnimation ();

		if (Input.GetButtonDown ("Fire2") && meleeWeapon.ready) {
			animator.SetBool ("attack", true);
			meleeAnimator.StartAttackAnimation (aim.transform.up);
			this.isMeleeAttacking = meleeWeapon.Attack (transform.position, Vector3.zero);
		}

		if ((Input.GetButton ("Fire1")) && rangedWeapon.ready) {
			animator.SetBool ("attack", true);
			rangedWeapon.Attack (transform.position, aim.transform.up);
		}
			
		if((Input.GetButton("Dash") || Input.GetAxis("Dash") > 0 ) && canDash){
			StartCoroutine (Dash ());
			StartCoroutine (EnterDashCooldown ());
		}
	}

	public bool IsMeleeAttacking() {
		if(isMeleeAttacking) {
			isMeleeAttacking = false;
			return true;
		}
		return false;
	}

	IEnumerator Dash() {
		float oldSpeed = speed;
		this.speed = speed * dashSpeed;
		yield return new WaitForSeconds(dashDuration);
		this.speed = oldSpeed;

	}

	IEnumerator EnterDashCooldown (){
		this.canDash = false;
		yield return new WaitForSeconds (dashCooldown);
		this.canDash = true;

	}

}
