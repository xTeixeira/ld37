using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Character : MonoBehaviour {

	public float life;
	public float speed;
	Vector2 moveDirection;
	Rigidbody2D rigidBody;

	public Weapon meleeWeapon, rangedWeapon;
	public GameObject hitParticle;
	public Animator animator;

	protected bool isMeleeAttacking;
	protected bool canMove = true;

	public SpriteRenderer spriteRenderer;
	public AudioClip hitAudioClip, deathAudioClip;

	//invunelaribity
	public float invulnerableTime;
	public bool canBeDamaged = true;
	public Color invulnerabilityColor;
	public bool hasInvulnerability;
	public float hitFeedbackTime;
	protected bool isHit = false;

	// Use this for initialization
	protected void InitCharacter () {
		rigidBody = GetComponent<Rigidbody2D> ();
		meleeWeapon = Instantiate (meleeWeapon, transform);
		rangedWeapon = Instantiate (rangedWeapon, transform);
	}

	protected void HandleMovement () {
		if(canMove)
			rigidBody.velocity = (moveDirection * speed);
	}

	protected void SetMoveDirection (Vector2 direction){
		this.moveDirection = direction;
	}

	protected void Move(Vector2 direction){
		rigidBody.AddForce (direction);
	}

	public void SendHit(HitInfo hit){
		if (canBeDamaged) {
			life -= hit.damage;

			//vulnerability
			if (hasInvulnerability) {
				StartCoroutine (Invulnerability ());
			}
			StartCoroutine (HitFeedback ());
            
			//FX
			if(hitParticle != null)
				Instantiate (hitParticle, transform.position, Quaternion.identity);
			GameManager.PlayAudioOneShot (life <= 0 ? deathAudioClip : hitAudioClip);

		}
	}

	protected void HandleOrientation (Vector2 oriDirection){

		string currentAxis = "down";
		float directionAngle = Vector2.Angle (Vector2.up, oriDirection);

		if (directionAngle <= 45)
			currentAxis = "up";
		else if (directionAngle <= 145)
			currentAxis = oriDirection.x > 0 ? "right" : "left";
		else
			currentAxis = "down";

		foreach (AnimatorControllerParameter parameter in animator.parameters) {
			if (!parameter.name.Equals (currentAxis) && !parameter.name.Equals ("attack"))
				animator.SetBool (parameter.name, false);
		}

		animator.SetBool (currentAxis, true);
	}
		
	public HitInfo GetCurrentHitInfo() {
		return meleeWeapon.GetHitInfo();
	}

	IEnumerator Invulnerability(){
		canBeDamaged = false;
		yield return new WaitForSeconds(invulnerableTime);
		canBeDamaged = true;
	}

	IEnumerator HitFeedback(){
		isHit = true;
		spriteRenderer.GetComponent<SpriteRenderer>().color = invulnerabilityColor;
		yield return new WaitForSeconds(hitFeedbackTime);
		spriteRenderer.GetComponent<SpriteRenderer>().color = new Color (255, 255, 255);
		isHit = false;
	}
}
