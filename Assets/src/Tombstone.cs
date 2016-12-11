using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : MonoBehaviour {


	public GameObject buraco;

	void Start(){ 
	}

	public void SpawnEnemy(GameObject enemy){
		buraco.GetComponent<Animator> ().Play("Spawning");

		Instantiate (enemy, transform.position, transform.rotation);
	}

	void Update(){
	}
}
