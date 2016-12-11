using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimColliderListener : MonoBehaviour {

	Player player;

	void OnTriggerStay2D (Collider2D col) {
		player = player == null ? GameManager.GetPlayer() : player;

		if (col.gameObject.CompareTag ("Enemy")) {
			if (player.IsAttacking ()) {
				col.gameObject.GetComponent<Enemy> ().SendHit (player.GetCurrentHitInfo ());

			}
		}
	}
}
