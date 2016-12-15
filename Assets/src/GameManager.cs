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
	public float enemiesPerMinuteStart;
	public float enemiesAddPerWave;
	public float enemyWaveTime;
	float enemiesSpawnRate;
	bool canSpawnEnemy = true;
	int wave = 1;

	public LevelManager levelManager;
	public GameObject gameOverUIHolder;
	public GameObject menuUIHolder;
	public GameObject mainMenuUIHolder;
	public GameObject tutorialUIHolder;
	public static AudioSource audioSource;

	public Texture2D cursorTexture;

	public GameObject playerPrefab;
	static Vector3 lastPlayerPosition;
	static bool playerIsAlive = true;
	bool isInGame = false;
	public Text scoreText;
	public Text waveText;

	public static int playerScore;

	public Image lifeUI;

	void Start () {
		mainMenuUIHolder.SetActive (true);

		//cursor
		CursorMode mode = CursorMode.Auto;
		Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width/2, cursorTexture.height/2), mode);
	}

	void Update (){
		if (isInGame) {
			HandleEnemySpawn ();
			HandleGameOver ();
			lifeUI.fillAmount = player.life * 0.1f;
			waveText.text = "Wave: " + wave.ToString();
		}

		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
	}

	void HandleEnemySpawn () {
		entitiesHolder = entitiesHolder == null ? GetEntitiesHolder () : entitiesHolder;
		if (canSpawnEnemy && tombstones.Count > 0) {
			((Tombstone) tombstones [Random.Range (0, tombstones.Count)]).
			SpawnEnemy (enemies[Random.Range (0, enemies.Length)], entitiesHolder.transform);
			StartCoroutine (EnemySpawnCooldown ());
		}
	}

	void StartGame () {
		player = Instantiate(playerPrefab, transform).GetComponent<Player>();
		audioSource = GetComponent<AudioSource> ();

		levelManager = levelManager == null ? 
			GameObject.Find ("LevelManager").GetComponent<LevelManager>() : levelManager;
		levelManager.CreateLevel ();
		GetEntitiesHolder ();
		enemiesSpawnRate = enemiesPerMinuteStart;
		isInGame = true;
		waveText.enabled = true;

		StartCoroutine (WaitForEnemyWave ());
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
				StopCoroutine (WaitForEnemyWave ());
				StopCoroutine (EnemySpawnCooldown ());
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
		wave = 1;
		enemiesSpawnRate = enemiesPerMinuteStart;
	}

	public void UIStartMenu () {
		mainMenuUIHolder.SetActive (false);
		tutorialUIHolder.SetActive (true);
	}

	public void UITutorialContinue () {
		menuUIHolder.SetActive (false);
		StartGame ();
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

	IEnumerator WaitForEnemyWave() {
		yield return new WaitForSeconds (enemyWaveTime);
		enemiesSpawnRate += enemiesAddPerWave;
		wave++;
		StartCoroutine (WaitForEnemyWave ());
	}
}
