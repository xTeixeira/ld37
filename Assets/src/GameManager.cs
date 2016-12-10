using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameObject player;

	// Use this for initialization
	void Start () {
		player = player == null ? GameObject.Find ("Player") : player;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static Vector3 GetPlayerPosition(){
		return player.transform.position;
	}
}
