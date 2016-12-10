using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static Player player;

	// Use this for initialization
	void Start () {
		player = player == null ? GameObject.Find ("Player").GetComponent<Player>() : player;
	}

	public static Vector3 GetPlayerPosition(){
		player = player == null ? GameObject.Find ("Player").GetComponent<Player>() : player;
		return player.gameObject.transform.position;
	}

	public static void SendPlayerHit(HitInfo hitInfo){
		player.SendHit (hitInfo);
	}
}
