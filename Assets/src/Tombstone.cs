using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : MonoBehaviour {

	public void SpawnEnemy(GameObject enemy){
		Instantiate (enemy, transform.position, transform.rotation);
	}
}
