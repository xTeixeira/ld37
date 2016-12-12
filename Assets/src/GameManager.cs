using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static Player player;
	public static ArrayList tombstones;
	public static GameObject entitiesHolder;

	public GameObject playerPrefab;
	public GameObject[] enemies;
	public float enemiesPerMinute;
	bool canSpawnEnemy = true;
	public Texture2D cursorTexture;

	public GameObject gameOverUIHolder;
	public Text scoreText;

	static Vector3 lastPlayerPosition;
	static bool playerIsAlive = true;
	public static int playerScore;

	void Start () {
		GetEntitiesHolder ();
		player = Instantiate(playerPrefab, transform).GetComponent<Player>();
		CursorMode mode = CursorMode.Auto;
		Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width/2, cursorTexture.height/2), mode);
	}

	void Update (){
		HandleEnemySpawn ();
		HandleGameOver ();
	}

	void HandleEnemySpawn () {
		entitiesHolder = entitiesHolder == null ? GetEntitiesHolder () : entitiesHolder;
		if (canSpawnEnemy) {
			((Tombstone) tombstones [Random.Range (0, tombstones.Count)]).
			SpawnEnemy (enemies[Random.Range (0, enemies.Length)], entitiesHolder.transform);
			StartCoroutine (EnemySpawnCooldown ());
		}
	}

	void HandleGameOver () {
		if (playerIsAlive) {
			if (player.life <= 0) {
				playerIsAlive = false;
				lastPlayerPosition = player.transform.position;
				Camera.main.transform.SetParent (null);
				scoreText.text = playerScore.ToString();
				gameOverUIHolder.SetActive (true);
				Destroy (player.gameObject);
			}
		}
	}

	public void UIRetry () {
		Destroy (Camera.main.gameObject);
		player = Instantiate (playerPrefab, transform).GetComponent<Player>() as Player;
		gameOverUIHolder.SetActive(false);
		playerIsAlive = true;
		KillAllEnemies ();
		GetEntitiesHolder ();
		playerScore = 0;
	}

	public static Vector3 GetPlayerPosition(){
		if (player == null)
			return lastPlayerPosition;
		else
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

	static public GameObject GetEntitiesHolder () {
		entitiesHolder = entitiesHolder == null ? new GameObject ("EntitiesHolder") : entitiesHolder;
		return entitiesHolder;
	}

	void KillAllEnemies () {
		Destroy(entitiesHolder.gameObject);
	}

	public static Player GetPlayer () {
		return player;
	}

	public static void AddScore (int score) {
		playerScore += score;
	}
}
