using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	Rigidbody2D rigidBody2D;
	[SerializeField]
	float velocity;
	[SerializeField]
	Vector3 direction;
	HitInfo hitInfo;

	void Start (){
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void Update (){
		rigidBody2D.velocity = direction * velocity;
		transform.Rotate (new Vector3(0, 0, 200 * Time.deltaTime));
	}

	public void InitProjectile (Vector3 direction, float velocity, HitInfo hitInfo) {
		this.velocity = velocity;
		this.direction = direction;
		this.hitInfo = hitInfo;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (!col.gameObject.CompareTag (hitInfo.ownerTag) && col.gameObject.layer != 8) {
			
			try{col.gameObject.GetComponent<Character> ().SendHit (hitInfo);}
			catch{
			}
			
			Destroy (gameObject);
		}
	}

}
