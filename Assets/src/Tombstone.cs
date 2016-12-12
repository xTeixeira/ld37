using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : MonoBehaviour {

	public GameObject hole;
	public GameObject spawnEffect;
	public GameObject spawnPoint;

	void Start (){
		
	}

	public void SpawnEnemy(GameObject enemy, Transform parent){
		if (enemy != null && hole != null & spawnEffect != null) {
			hole.GetComponent<Animator> ().Play ("Spawning");
			spawnEffect.GetComponent<Animator> ().Play ("Spawning");
			StartCoroutine (SpawnEnemyDelayed (enemy, parent));
		}
	}

	IEnumerator SpawnEnemyDelayed(GameObject enemy, Transform parent){
		yield return new WaitForSeconds (0.5f);
		Instantiate (enemy, spawnPoint.transform.position, transform.rotation).transform.SetParent(parent);
	}
}
