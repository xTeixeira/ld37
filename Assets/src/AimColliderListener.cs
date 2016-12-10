using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimColliderListener : MonoBehaviour {

	void OnTriggerStay2D (Collider2D col) {
		Destroy (col.gameObject);
	}
}
