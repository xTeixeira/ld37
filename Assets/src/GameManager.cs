using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static Player player;
	public static ArrayList tombstones;


	public GameObject[] enemies;
	public float enemiesPerMinute;
	bool canSpawnEnemy = true;
	public Texture2D cursorTexture;

	void Start () {
		player = player == null ? GameObject.Find ("Player").GetComponent<Player>() : player;
		CursorMode mode = CursorMode.Auto;
		Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width/2, cursorTexture.height/2), mode);
	}

	void Update (){
		if (canSpawnEnemy) {
			((Tombstone) tombstones [Random.Range (0, tombstones.Count)]).
							SpawnEnemy (enemies[Random.Range (0, enemies.Length)]);
			StartCoroutine (EnemySpawnCooldown ());
		}
	}

	public static Vector3 GetPlayerPosition(){
		player = player == null ? GameObject.Find ("Player").GetComponent<Player>() : player;
		return player.gameObject.transform.position;
	}

	public static void SendPlayerHit(HitInfo hitInfo){
		player.SendHit (hitInfo);
	}

	public static void AddTombstone(Tombstone tombstone){
		tombstones = tombstones == null ? new ArrayList() : tombstones;
		tombstones.Add(tombstone);
	}

	IEnumerator EnemySpawnCooldown (){
		canSpawnEnemy = false;
		yield return new WaitForSeconds (60 / enemiesPerMinute);
		canSpawnEnemy = true;
		enemiesPerMinute++;
	}

	public static Player GetPlayer(){
		return player;
	}
}
