using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimColliderListener : MonoBehaviour {

	Player player;
	List<Putinho> enemies = new List<Putinho> ();

	void Update() {
		player = player == null ? GameManager.GetPlayer() : player;
		if (player.IsMeleeAttacking ()) {
			foreach (Putinho enemy in enemies){
				enemy.SendHit (player.GetCurrentHitInfo ());
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
