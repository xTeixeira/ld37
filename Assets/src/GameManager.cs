using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {

	public static Player player;
	public static ArrayList tombstones;
	public static GameObject entitiesHolder;

	public GameObject[] enemies;
	public float enemiesPerMinute;
	public float enemiesPerMinuteIncreaseRate;
	float enemiesSpawnRate;
	bool canSpawnEnemy = true;

	public LevelManager levelManager;
	public GameObject gameOverUIHolder;
	public Text scoreText;
	public static AudioSource audioSource;

	public Texture2D cursorTexture;

	public GameObject playerPrefab;
	static Vector3 lastPlayerPosition;
	static bool playerIsAlive = true;
	public Text scoreText;

	public static int playerScore;

	void Start () {
		player = Instantiate(playerPrefab, transform).GetComponent<Player>();
		audioSource = GetComponent<AudioSource> ();

		levelManager = levelManager == null ? 
						GameObject.Find ("LevelManager").GetComponent<LevelManager>() : levelManager;
		levelManager.CreateLevel ();
		GetEntitiesHolder ();
		enemiesSpawnRate = enemiesPerMinute;

		//cursor
		CursorMode mode = CursorMode.Auto;
		Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width/2, cursorTexture.height/2), mode);

	}

	void Update (){
		HandleEnemySpawn ();
		HandleGameOver ();
	}

	void HandleEnemySpawn () {
		entitiesHolder = entitiesHolder == null ? GetEntitiesHolder () : entitiesHolder;
		if (canSpawnEnemy && tombstones.Count > 0) {
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
		levelManager.DestroyLevel ();
		levelManager.CreateLevel ();
		GetEntitiesHolder ();
		playerScore = 0;
		enemiesSpawnRate = enemiesPerMinute;
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

	public static void PlayAudioOneShot (AudioClip clip) {
		audioSource.PlayOneShot (clip);
	}

	IEnumerator EnemySpawnCooldown (){
		canSpawnEnemy = false;
		yield return new WaitForSeconds (60 / enemiesSpawnRate);
		canSpawnEnemy = true;
		enemiesSpawnRate += enemiesPerMinuteIncreaseRate;
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
