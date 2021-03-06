﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public Sprite[] tiles;
	public GameObject[] fences;
	public GameObject[] tombstones;
	public GameObject stoneTile;
	public Vector2 minRoomSize;
	public Vector2 maxRoomSize;
	Vector2 roomSize;
	int tombstonesNumber;
	int stonesNumber;
	int[,] assetsArray;

	GameObject tilesHolder;
	GameObject assetsHolder;

	private int tileUnitSize = 3;

	public void CreateLevel () {

		roomSize = new Vector2 (Random.Range ((int) minRoomSize.x, (int) maxRoomSize.x), 
								Random.Range ((int) minRoomSize.y, (int) maxRoomSize.y));
		tombstonesNumber = Random.Range (3, (int) (roomSize.x + roomSize.y) / 2 );
		stonesNumber = Random.Range (1, (int) (roomSize.x + roomSize.y) / 2 );

		assetsArray = new int[(int)roomSize.x, (int)roomSize.y];

		CreateHolders ();
		CreateTiles ();
		CreateFences ();
		CreateTombstones ();
		CreateStones ();

		assetsHolder.transform.Translate (Vector3.left * roomSize.x * tileUnitSize);
		assetsHolder.transform.Translate (Vector3.down * roomSize.y * tileUnitSize);
	}

	public void DestroyLevel () {
		Destroy (tilesHolder);
		Destroy (assetsHolder);
	}

	void CreateHolders () {
		tilesHolder = new GameObject ("TilesHolder");
		tilesHolder.transform.SetParent (transform.parent);
		assetsHolder = new GameObject ("AssetsHolder");
		assetsHolder.transform.SetParent (transform.parent);
	}
	
	void CreateTiles (){
		int tilesSizeX = (int) (roomSize.x * tileUnitSize) * 3;
		int tilesSizeY = (int)(roomSize.y * tileUnitSize) * 3;

		for (int i = 0; i < tilesSizeX; i++) {
			for (int j = 0; j < tilesSizeY; j++) {
				GameObject tile = new GameObject (("tile" + i) + j);
				tile.transform.position = (new Vector3 (i * tileUnitSize, 
					j* tileUnitSize, 
					0));

				SpriteRenderer spriteRenderer = tile.AddComponent <SpriteRenderer>();
				spriteRenderer.sprite = tiles [Random.Range (0, 3)];
				spriteRenderer.sortingOrder = -2;
				tile.transform.localScale = new Vector3 (5, 5, 1);
				tile.transform.SetParent (tilesHolder.transform);
			}
		}

		tilesHolder.transform.Translate (Vector3.left * tilesSizeX/2 * tileUnitSize);
		tilesHolder.transform.Translate (Vector3.down * tilesSizeY/2 * tileUnitSize);
	}

	void CreateFences (){

		for (int i = 0; i < (int) roomSize.x; i++) {
			for (int j = 0; j < (int)roomSize.y; j++) {
				if (i == 0 || i == roomSize.x - 1 || j == 0 || j == roomSize.y - 1) {
					GameObject fence = Instantiate (fences[i == 0 || i == roomSize.x - 1 ? 1 : 0],
					new Vector3 (i * 8, j * 8, 0),
					Quaternion.identity);

					fence.transform.SetParent (assetsHolder.transform);
				}
			}
		}
	}

	void CreateTombstones (){

		for (int i = 0; i < tombstonesNumber; i++) {
			int x = Mathf.RoundToInt(Random.Range (1, roomSize.x - 2));
			int y = Mathf.RoundToInt(Random.Range (1, roomSize.y - 2));

			if (assetsArray [x, y] == 0) {
				GameObject tombstone = Instantiate (tombstones [Random.Range (0, 2)],
					new Vector3 (x * 8, y * 8, 0),
					                      Quaternion.identity);

				tombstone.transform.SetParent (assetsHolder.transform);
				assetsArray [x, y] = 1;
				GameManager.AddTombstone (tombstone.GetComponent<Tombstone>() as Tombstone);
			}
		}
	}

	void CreateStones (){

		for (int i = 0; i < stonesNumber; i++) {
			int x = Mathf.RoundToInt(Random.Range (1, roomSize.x - 2));
			int y = Mathf.RoundToInt(Random.Range (1, roomSize.y - 2));

			if (assetsArray [x, y] == 0) {
				GameObject stone = Instantiate (stoneTile,
					new Vector3 (x * 8, y * 8, 0),
					Quaternion.identity);

				stone.transform.SetParent (assetsHolder.transform);
				assetsArray [x, y] = 1;
			}
		}
	}
}
