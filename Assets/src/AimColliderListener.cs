﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimColliderListener : MonoBehaviour {

	Player player;
	public List<Character> enemies = new List<Character> ();

	void Update() {
		player = player == null ? GameManager.GetPlayer() : player;
		if (player.IsMeleeAttacking ()) {
			foreach (Putinho enemy in enemies){
				if (enemy != null) {
					enemy.SendHit (player.GetCurrentHitInfo ());
				} 
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		player = player == null ? GameManager.GetPlayer() : player;
		if (col.gameObject.CompareTag ("Enemy")) {
			enemies.Add (col.gameObject.GetComponent<Putinho> ());
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		player = player == null ? GameManager.GetPlayer() : player;
		if (col.gameObject.CompareTag ("Enemy")) {
			enemies.Remove (col.gameObject.GetComponent<Putinho> ());
		}

	}


}
