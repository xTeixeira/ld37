using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	Rigidbody2D rigidBody2D;
	float velocity;
	Vector3 direction;
	HitInfo hitInfo;

	void Start (){
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void Update (){
		rigidBody2D.velocity = direction * velocity;
	}

	public void InitProjectile (Vector3 direction, float velocity, HitInfo hitInfo) {
		this.velocity = velocity;
		this.direction = direction;
		this.hitInfo = hitInfo;
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Enemy")) {
			col.gameObject.GetComponent<Enemy> ().SendHit (hitInfo);
		}
		Destroy (gameObject);
	}

}
