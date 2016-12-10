using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour {

	public float secondsToKill;

	void Start () {
		StartCoroutine (DeathTimer ());
	}

	IEnumerator DeathTimer() {
		yield return new WaitForSeconds (secondsToKill);
		Destroy (gameObject);
	}

}
