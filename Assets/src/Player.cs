using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	public GameObject aim;

	Animator meleeAnimator;
	bool hasJoystickInput;
	public float dashSpeed;
	bool canDash = true;
	public float dashCooldown;
	public float dashDuration;

	// Use this for initialization
	void Start () {
		this.InitCharacter ();
		meleeAnimator = GameObject.Find ("MeleeEffect").GetComponent<Animator>() as Animator;
	}

	// Update is called once per frame
	void Update () {
		hasJoystickInput = (Input.GetAxis ("HorizontalAim") != 0 || Input.GetAxis ("VerticalAim") != 0);
		this.SetMoveDirection (new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")));
		this.HandleMovement ();
		this.HandleAim ();
		this.HandleOrientation (aim.transform.up);
		this.HandleAttack ();
		this.HandleDashInput ();
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

	void HandleAttack() {
		isMeleeAttacking = false;

		if (animator.GetBool("attack")){
			animator.SetBool("attack",false);
		}
		if (meleeAnimator.GetBool ("attack")) {
			meleeAnimator.SetBool ("attack", false);
		}

		if (Input.GetButtonDown ("Fire1") && meleeWeapon.ready) {
			animator.SetBool ("attack", true);
			meleeAnimator.GetComponent<Animator> ().SetBool ("attack", true);
			this.isMeleeAttacking = meleeWeapon.Attack (Vector3.zero);


		}
		if ((Input.GetButton ("Fire2") || hasJoystickInput) && rangedWeapon.ready) {
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

	void HandleDashInput() {

		if (Input.GetKeyDown (KeyCode.LeftShift) && canDash) {
			StartCoroutine(Dash());
			StartCoroutine (EnterDashCooldown ());
		}

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
